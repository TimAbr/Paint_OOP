using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using static WpfApp1.Draw;
using System.Reflection;

namespace WpfApp1;
using PointShapeFiles;
using UI;
using WpfApp1.Shapes;
using WpfApp1.Shortcuts;

public partial class MainWindow : Window
{
  
    private static ToggleButton[] shapeButtons;
    private static ToggleButton[] widthButtons;

    private Shortcuts.Shortcuts shortcuts;

    private static int curShape = -1;
    private int curWidth = 0;

    private double[] allStrokeWidths = { 1, 1.5, 2, 3, 5};
    
    private Color[] allColors = { Colors.Red, Colors.LightGray, Colors.Green, Colors.LightGray, Colors.LightGreen, Colors.DarkGray, Colors.LightBlue, Colors.LavenderBlush, Colors.LightCoral, Colors.White, Colors.Black };
   

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

    public static int compareTypes(Type a, Type b)
    {
        int i1 = (int)a.GetProperty("id", BindingFlags.Static | BindingFlags.Public).GetValue(null);
        int i2 = (int)b.GetProperty("id", BindingFlags.Static | BindingFlags.Public).GetValue(null);

        return i1.CompareTo(i2);
    }

    static WrapPanel ShapeButtonList;

    public static void updateShapeButtons()
    {
        shapeButtons = new ToggleButton[ShapeFactory.Instance().getTypeMap().Keys.Count];

        FillUIElements.setShapeButtons(ShapeButtonList, ShapeFactory.Instance().getTypeMap(), shapeButtons, new RoutedEventHandler(ShapeButtonClick));
    }


    public MainWindow()
    {
        InitializeComponent();

        FillUIElements.setColorList(colorList,allColors, new MouseButtonEventHandler(colorMouseDown));

        widthButtons = new ToggleButton[allStrokeWidths.Length];
        FillUIElements.setDropdownPopup(DropdownPopup,allStrokeWidths, widthButtons, new RoutedEventHandler(widthButtonClick), curWidth);

        ShapeButtonList = shapeButtonList;

        updateShapeButtons();

        chosenEllipse = borderEllipse;
        chosenEllipse.Stroke = Brushes.LightSteelBlue;
        chosenEllipse.StrokeThickness = 2;
        fillEllipse.StrokeThickness = 0;

        shortcuts = new Shortcuts.Shortcuts();
    }

    private void CanvasMouseDown(object sender, MouseButtonEventArgs e)
    {

        if (curShape >= 0)
        {
            shapeList.addUndo();
            if (ShapeFactory.Instance().getTypeMap()[curShape].IsSubclassOf(typeof(PointShape)))
            {
                
            }
            else
            {
                Draw.onMouseDown(e);
            }
        }
    }

    private void addPluginCommand(object sender, RoutedEventArgs e)
    {
        shortcuts.addPlugin();
    }

    private void CanvasMouseUp(object sender, MouseButtonEventArgs e)
    {
        if (curShape >= 0)
        {
            ShapeSettings s = new ShapeSettings();
            int x = (int)e.GetPosition(mainCanvas).X;
            int y = (int)e.GetPosition(mainCanvas).Y;
            s.mouseUp = new MouseButtonEventHandler(CanvasMouseUp);
            s.borderColor = ((borderEllipse.Fill) as SolidColorBrush).Color;
            s.fillColor = ((fillEllipse.Fill) as SolidColorBrush).Color;
            s.lineWidth = allStrokeWidths[curWidth];

            if (ShapeFactory.Instance().getTypeMap()[curShape].IsSubclassOf(typeof(PointShape)))
            {
                s.isLast = false;
                
                Draw.onPolyMouseUp(x, y, curShape, s);
            }
            else
            {
                s.isLast = false;

                
                Draw.onMouseUp(x, y, curShape, s);
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
            s.mouseUp = new MouseButtonEventHandler(CanvasMouseUp);
            s.borderColor = ((borderEllipse.Fill) as SolidColorBrush).Color;
            s.fillColor = ((fillEllipse.Fill) as SolidColorBrush).Color;
            s.lineWidth = allStrokeWidths[curWidth];

            if (ShapeFactory.Instance().getTypeMap()[curShape].IsSubclassOf(typeof(PointShape))) 
            {
                s.isLast = false;
                         
                Draw.onPolyMouseMove(x, y, curShape, s);

            }
            else
            {
                s.isLast = false;

                Draw.onMouseMove(x, y, curShape, s);
            }
        }
    }

    

    private static void ShapeButtonClick(object sender, RoutedEventArgs e)
    {
        var keys = ShapeFactory.Instance().getTypeMap().Keys.ToArray();
        for (int i = 0; i<keys.Length; i++)
        {
            if (shapeButtons[i]==(ToggleButton)sender)
            {
                curShape = keys[i];
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
        Draw.shapeList = new Shortcuts.ShapeList.ShapeList(0, Draw.mainCanvas);

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
        shortcuts.Open();
    }
    private void SaveCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        shortcuts.Save();
    }
    private void UndoCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        shortcuts.Undo();
    }
    private void RedoCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        shortcuts.Redo();
    }
    private void NewFileCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        shortcuts.NewFile();
    }

    private void SaveAsCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        shortcuts.SaveAs();
    }



}
