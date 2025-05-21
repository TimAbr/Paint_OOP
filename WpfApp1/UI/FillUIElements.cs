using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using WpfApp1.Shapes;
using static WpfApp1.Draw;

namespace WpfApp1.UI
{
    class FillUIElements
    {
        private static Color mainColor = new Color
        {
            A = 255,
            R = 0x27,
            G = 0x27,
            B = 0x27
        };

        private static Brush mainColorBrush = new SolidColorBrush(mainColor);

        public static void setDropdownPopup(Popup DropdownPopup,double[] allStrokeWidths,ToggleButton[] widthButtons,RoutedEventHandler click,int curWidth)
        {

            Border b = (Border)DropdownPopup.Child;
            StackPanel s = (StackPanel)b.Child;

            for (int i = 0; i < allStrokeWidths.Length; i++)
            {
                ToggleButton tButton = new ToggleButton();

                tButton.Height = 25;
                tButton.Background = mainColorBrush;

                StackPanel child = new StackPanel();
                child.VerticalAlignment = VerticalAlignment.Center;
                child.HorizontalAlignment = HorizontalAlignment.Stretch;
                child.Height = tButton.Height;
                child.Width = 120;
                child.FlowDirection = FlowDirection.RightToLeft;

                child.Orientation = Orientation.Horizontal;

                TextBlock text = new TextBlock();
                text.Text = allStrokeWidths[i].ToString();
                text.FontSize = 10;
                text.Foreground = new SolidColorBrush(Colors.LightGray);
                text.VerticalAlignment = VerticalAlignment.Center;
                text.HorizontalAlignment = HorizontalAlignment.Right;
                text.Margin = new Thickness(5, 0, 0, 0);

                child.Background = mainColorBrush;


                Line l = new Line();
                l.Y1 = 1;
                l.Y2 = 1;
                l.X1 = 5;
                l.X2 = 100;
                l.Stroke = new SolidColorBrush(Colors.LightGray);
                l.VerticalAlignment = VerticalAlignment.Center;
                l.StrokeThickness = allStrokeWidths[i];

                child.Children.Add(l);
                child.Children.Add(text);



                tButton.Content = child;

                s.Children.Add(tButton);
                widthButtons[i] = tButton;
                tButton.Click +=  click;

                if (curWidth == i)
                {
                    tButton.IsChecked = true;
                }
            }
        }


        public static void setColorList(WrapPanel colorList, Color[] allColors,MouseButtonEventHandler click)
        {

            int colorPickerSize = 15;
            int colorPickerFieldWidth = 140;
            colorList.Width = colorPickerFieldWidth;

            int colorPickerFieldHeight = 25 * 2 + (100 - 7 * 2 - 25 * 2) / 2;
            colorList.Height = 100;

            double startMargin = ((double)(100 - colorPickerFieldHeight - 7 * 2)) / 2;
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
                el.MouseDown += click;
                colorList.Children.Add(el);
            }
        }

        public static void setShapeButtons(WrapPanel shapeButtonList, Dictionary<int, Type> shapeTypeList, ToggleButton[] shapeButtons, RoutedEventHandler click)
        {

            shapeButtonList.Children.Clear();
            
            int shapeButtonSize = 20;
            int shapeButtonrFieldWidth = 141;

            int minHorMargin = 2;

            int numCol = shapeButtonrFieldWidth / (shapeButtonSize + minHorMargin);
            double horMargin = ((double)(shapeButtonrFieldWidth - numCol * shapeButtonSize)) / numCol;
            double vertMargin = 2;

            var keys = shapeTypeList.Keys.ToArray();


            for (int i = 0; i < keys.Length; i++)
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

              
                Shape temp = ShapeFactory.Instance().get(keys[i], new object[] { 1, 1, 16 });
                
                ShapeSettings s = new ShapeSettings();
                s.mouseUp = null;
                s.borderColor = Colors.LightGray;
                s.fillColor = mainColor;
                s.lineWidth = 1;
                s.isLast = true;

                temp.Settings = s;

                temp.draw(cnv);

                el.Content = cnv;
                shapeButtons[i] = el;
                el.Click += click;

                shapeButtonList.Children.Add(el);
            }
        }
    }
}
