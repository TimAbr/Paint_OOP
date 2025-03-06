using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfApp1
{
    public class MyLine : Shape
    {

        public MyLine(Canvas canvas, int x1, int y1, int x2, int y2)
            : base(canvas, x1, y1, x2, y2)
        {

        }
        public MyLine(Canvas canvas, int x, int y, int width)
            : base(canvas, x, y, width)
        {

        }

        override public System.Windows.UIElement draw()
        {

            Line tr = new Line();
            tr.X1 = x;
            tr.Y1 = y;
            tr.X2 = width;
            tr.Y2 = height;
            tr.Stroke = pen.Brush;

            canvas.Children.Add(tr);

            return tr;
            
        }
    }
}
