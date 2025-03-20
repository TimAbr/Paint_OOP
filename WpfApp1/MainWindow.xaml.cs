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
using static WpfApp1.FrameShapeFiles.MyPolygon;
using static WpfApp1.Draw;
using static Microsoft.Maui.ApplicationModel.Permissions;
using System.Collections.ObjectModel;
using Xceed.Wpf.AvalonDock.Controls;
using System.Reflection;
using System.Security.Cryptography;
using System.Globalization;

namespace WpfApp1;
using PointShapeFiles;
using UI;

public partial class MainWindow : Window
{
  
    private ToggleButton[] shapeButtons;
    private ToggleButton[] widthButtons;

    private int curShape = -1;
    private int curWidth = 0;

    private double[] allStrokeWidths = { 1, 1.5, 2, 3, 5};
    
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

    bool widthPopUpVisible = false;
    
    public void widthListButtonMouthDown(object sender, RoutedEventArgs e)
    {
        widthPopUpVisible = !widthPopUpVisible;
        DropdownPopup.IsOpen = widthPopUpVisible;
    }


    public MainWindow()
    {
        InitializeComponent();

        FillUIElements.setColorList(colorList,allColors, new MouseButtonEventHandler(colorMouseDown));

        widthButtons = new ToggleButton[allStrokeWidths.Length];
        FillUIElements.setDropdownPopup(DropdownPopup,allStrokeWidths, widthButtons, new RoutedEventHandler(widthButtonClick), curWidth);

        Type ourtype = typeof(Shape); // Базовый тип
        shapeTypeList = Assembly.GetAssembly(ourtype).GetTypes().Where(type => type.IsSubclassOf(ourtype) && !type.IsAbstract).ToArray<Type>();
        shapeButtons = new ToggleButton[shapeTypeList.Length];

        FillUIElements.setShapeButtons(shapeButtonList,shapeTypeList,shapeButtons, new RoutedEventHandler(ShapeButtonClick));
        chosenEllipse = borderEllipse;
        chosenEllipse.Stroke = Brushes.LightSteelBlue;
        chosenEllipse.StrokeThickness = 2;
        fillEllipse.StrokeThickness = 0;
    }

    private void CanvasMouseDown(object sender, MouseButtonEventArgs e)
    {

        if (curShape >= 0)
        {
            if (shapeTypeList[curShape].IsSubclassOf(typeof(PointShape)))
            {
                
            }
            else
            {
                Draw.onMouseDown(e);
            }
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
            s.lineWidth = allStrokeWidths[curWidth];

            if (shapeTypeList[curShape].IsSubclassOf(typeof(PointShape)))
            {
                ConstructorInfo constructor = shapeTypeList[curShape].GetConstructors().Where(_ => _.GetParameters().Length == 3).First();
                System.Windows.UIElement? tempShape = Draw.onPolyMouseUp(x, y, constructor, s);

                if (tempShape != null)
                {
                    tempShape.MouseUp -= new MouseButtonEventHandler(CanvasMouseUp);
                }
            }
            else
            {
                ConstructorInfo constructor = shapeTypeList[curShape].GetConstructors().Where(_ => _.GetParameters().Length == 5).First();
                System.Windows.UIElement? tempShape = Draw.onMouseUp(x, y, constructor, s);
                if (tempShape != null)
                {
                    tempShape.MouseUp -= new MouseButtonEventHandler(CanvasMouseUp);
                }
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
            s.lineWidth = allStrokeWidths[curWidth];

            if (shapeTypeList[curShape].IsSubclassOf(typeof(PointShape))) 
            {

                ConstructorInfo constructor = shapeTypeList[curShape].GetConstructors().Where(_ => _.GetParameters().Length == 3).First();
                System.Windows.UIElement? tempShape = Draw.onPolyMouseMove(x, y, constructor, s);
                if (tempShape != null)
                {
                    tempShape.MouseUp += new MouseButtonEventHandler(CanvasMouseUp);
                }
            }
            else
            {
                ConstructorInfo constructor = shapeTypeList[curShape].GetConstructors().Where(_ => _.GetParameters().Length == 5).First();
                System.Windows.UIElement? tempShape = Draw.onMouseMove(x, y, constructor, s);
                if (tempShape != null)
                {
                    tempShape.MouseUp += new MouseButtonEventHandler(CanvasMouseUp);
                }
            }
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

    private void widthButtonClick(object sender, RoutedEventArgs e)
    {
        for (int i = 0; i < allStrokeWidths.Length; i++)
        {
            if (widthButtons[i] == (ToggleButton)sender)
            {
                curWidth = i;
                widthButtons[i].IsChecked = true;
            }
            else
            {
                widthButtons[i].IsChecked = false;
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

    private void OpenCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        Shortcuts.Shortcuts.Open();
    }
    private void SaveCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        Shortcuts.Shortcuts.Save();
    }
    private void UndoCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        Shortcuts.Shortcuts.Undo();
    }
    private void RedoCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        Shortcuts.Shortcuts.Redo();
    }
    private void NewFileCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        Shortcuts.Shortcuts.NewFile();
    }


}
