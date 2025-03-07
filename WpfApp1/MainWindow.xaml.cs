using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static WpfApp1.MyPolygon;
using static WpfApp1.Draw;
using static Microsoft.Maui.ApplicationModel.Permissions;
using System.Collections.ObjectModel;
using Xceed.Wpf.AvalonDock.Controls;
using System.Reflection;
using System.Security.Cryptography;

namespace WpfApp1;

public partial class MainWindow : Window
{
  
    private ToggleButton[] shapeButtons;
    
    private int curShape = -1;
    
    private Color[] allColors = { Colors.Red, Colors.LightGray, Colors.Green, Colors.LightGray, Colors.LightGreen, Colors.DarkGray, Colors.LightBlue, Colors.LavenderBlush, Colors.LightCoral, Colors.White, Colors.Black };
    
    private Type[] shapeTypeList;

    private Brush mainColorBrush = new SolidColorBrush(new Color
    {
        A = 255,
        R = 0x27,
        G = 0x27,
        B = 0x27
    });

    Ellipse chosenEllipse;


    public MainWindow()
    {
        InitializeComponent();

        setColorList();

        Type ourtype = typeof(Shape); // Базовый тип
        shapeTypeList = Assembly.GetAssembly(ourtype).GetTypes().Where(type => type.IsSubclassOf(ourtype)).ToArray<Type>();
        shapeButtons = new ToggleButton[shapeTypeList.Length];

        setShapeButtons();
        chosenEllipse = borderEllipse;
        chosenEllipse.Stroke = Brushes.LightSteelBlue;
        chosenEllipse.StrokeThickness = 2;
        fillEllipse.StrokeThickness = 0;

    }

    private void CanvasMouseDown(object sender, MouseButtonEventArgs e)
    {
        if (curShape >= 0)
        {
            Draw.onMouseDown(e);
        }
    }
    private void CanvasMouseUp(object sender, MouseButtonEventArgs e)
    {
        if (curShape >= 0)
        {
            ShapeSettings s = new ShapeSettings();
            int x = (int)e.GetPosition(mainCanvas).X;
            int y = (int)e.GetPosition(mainCanvas).Y;
            s.borderColor = borderEllipse.Fill;
            s.fillColor = fillEllipse.Fill;
            s.lineWidth = 1;
            ConstructorInfo constructor = shapeTypeList[curShape].GetConstructors().Where(_ => _.GetParameters().Length == 5).First();
            System.Windows.UIElement? tempShape = Draw.onMouseUp(x, y, constructor, s);
            if (tempShape != null)
            {
                tempShape.MouseUp -= new MouseButtonEventHandler(CanvasMouseUp);
            }
        }
    }
    private void CanvasMouseMove(object sender, MouseEventArgs e)
    {
        if (curShape >= 0)
        {
            ShapeSettings s = new ShapeSettings();
            int x = (int)e.GetPosition(mainCanvas).X;
            int y = (int)e.GetPosition(mainCanvas).Y;
            s.borderColor = borderEllipse.Fill;
            s.fillColor = fillEllipse.Fill;
            s.lineWidth = 1;
            ConstructorInfo constructor = shapeTypeList[curShape].GetConstructors().Where(_ => _.GetParameters().Length == 5).First();
            System.Windows.UIElement? tempShape = Draw.onMouseMove(x, y, constructor, s);
            if (tempShape != null)
            {
                tempShape.MouseUp += new MouseButtonEventHandler(CanvasMouseUp);
            }
        }
    }


    private void setColorList()
    {
        
        int colorPickerSize = 15;
        int colorPickerFieldWidth = 140;
        colorList.Width = colorPickerFieldWidth;
        
        int colorPickerFieldHeight = 25 * 2 + (80 - 7 * 2 - 25 * 2) / 2;
        colorList.Height = 80;

        double startMargin = ((double)(80 - colorPickerFieldHeight - 7 * 2)) / 2;
        int minHorMargin = 10;


        int numCol = colorPickerFieldWidth / (colorPickerSize + minHorMargin);
        double horMargin = ((double)(colorPickerFieldWidth - numCol * colorPickerSize)) / numCol;
        int numRow = allColors.Length / numCol + (allColors.Length % numCol == 0 ? 0 : 1);
        double vertMargin = ((double)(colorPickerFieldHeight - numRow * colorPickerSize)) / (numRow == 1 ? 1 : numRow - 1);

        for (int i = 0; i < allColors.Length; i++)
        {
            Ellipse el = new Ellipse();
            el.Height = colorPickerSize;
            el.Width = colorPickerSize;
            el.Fill = new SolidColorBrush(allColors[i]);
            el.Stroke = new SolidColorBrush(allColors[i]);
            el.StrokeThickness = 1;
            if (numRow == 1)
            {
                el.Margin = new Thickness(0, vertMargin / 2, horMargin, vertMargin / 2);
            }
            else
            {
                if (i < numCol)
                {
                    el.Margin = new Thickness(0, startMargin, horMargin, vertMargin);
                }
                else
                {
                    el.Margin = new Thickness(0, 0, horMargin, vertMargin);
                }
            }
            el.MouseDown += new MouseButtonEventHandler(colorMouseDown);
            colorList.Children.Add(el);
        }
    }

    private void setShapeButtons()
    {

        int shapeButtonSize = 20;
        int shapeButtonrFieldWidth = 141;

        int minHorMargin = 2;

        int numCol = shapeButtonrFieldWidth / (shapeButtonSize + minHorMargin);
        double horMargin = ((double)(shapeButtonrFieldWidth - numCol * shapeButtonSize)) / numCol;
        double vertMargin = 2;

        for (int i = 0; i < shapeTypeList.Length; i++)
        {
            ToggleButton el = new ToggleButton();
            el.Height = shapeButtonSize;
            el.Width = shapeButtonSize;
            el.BorderBrush = mainColorBrush;
            el.Background = mainColorBrush;

            
            Canvas cnv = new Canvas();
            cnv.Width = 18;
            cnv.Height = 18;

            cnv.Background = mainColorBrush;

            el.Padding = new Thickness(0);
            el.Margin = new Thickness(horMargin, vertMargin, 0, 0);

            ConstructorInfo constructor = shapeTypeList[i].GetConstructors().Where(_ => _.GetParameters().Length == 4).First();

            Shape temp = (Shape) constructor.Invoke(new object[] { cnv, 1, 1, 16});
            temp.Brush = mainColorBrush;
            temp.Pen = new Pen();
            temp.Pen.Brush = Brushes.LightGray;
            temp.draw();

            el.Content = cnv;
            shapeButtons[i] = el;
            el.Click += new RoutedEventHandler(ShapeButtonClick);

            shapeButtonList.Children.Add(el);
        }
    }

    private void ShapeButtonClick(object sender, RoutedEventArgs e)
    {
        for (int i = 0; i<shapeTypeList.Length; i++)
        {
            if (shapeButtons[i]==(ToggleButton)sender)
            {
                curShape = i;
                shapeButtons[i].IsChecked=true;
            } else
            {
                shapeButtons[i].IsChecked = false;
            }
        }
    }

    private void LoadedCanvas(object sender, RoutedEventArgs e)
    {
        Draw.mainCanvas = (Canvas)sender;

    }

    private void colorMouseDown(object sender, MouseButtonEventArgs e)
    {
        chosenEllipse.Fill = ((Ellipse)sender).Fill;
    }

    private void chooseBorder(object sender, MouseButtonEventArgs e)
    {
        chosenEllipse = borderEllipse;
        chosenEllipse.Stroke = Brushes.LightSteelBlue;
        chosenEllipse.StrokeThickness = 2;
        fillEllipse.StrokeThickness = 0;
    }
    private void chooseFill(object sender, MouseButtonEventArgs e)
    {
        chosenEllipse = fillEllipse;
        chosenEllipse.Stroke = Brushes.LightSteelBlue;
        chosenEllipse.StrokeThickness = 2;
        borderEllipse.StrokeThickness = 0;
    }


}
