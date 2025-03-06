using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfApp1
{
    public class MyEllipse : Shape
    {

        public MyEllipse(Canvas canvas, int x1, int y1, int x2, int y2)
            : base(canvas, x1, y1, x2, y2)
        {
            if (x1 > x2)
            {
                int temp = x1;
                x1 = x2;
                x2 = temp;
            }

            if (y1 >= y2)
            {
                int temp = y1;
                y1 = y2;
                y2 = temp;
            }

            x = x1;
            y = y1;
            width = Math.Abs(x2 - x1);
            height = Math.Abs(y2 - y1);
        }

        public MyEllipse(Canvas canvas, int x, int y, int width)
            : base(canvas, x, y, width)
        {

        }

        override public System.Windows.UIElement draw()
        {
            
            Ellipse tr = new Ellipse();
            tr.Width = width;
            tr.Height = height;
                               
            tr.Fill = brush;
            tr.Stroke = pen.Brush;
            tr.StrokeDashArray = pen.DashStyle.Dashes;
            tr.StrokeThickness = pen.Thickness;
            tr.StrokeDashCap = pen.DashCap;

            canvas.Children.Add(tr);
            Canvas.SetLeft(tr, x);
            Canvas.SetTop(tr, y);

            return tr;
            
        }
    }
}
