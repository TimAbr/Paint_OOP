using System;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfApp1.FrameShapeFiles
{
    public class MyRect : FrameShape
    {

        public static int id { get => 0; }
        public MyRect(int x1, int y1, int x2, int y2)
            : base(x1, y1, x2, y2)
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
        public MyRect(int x, int y, int width)
            : base(x, y, width)
        {

        }

        override public UIElement draw(Canvas canvas)
        {
            Rectangle tr = new Rectangle();
            tr.Width = width;
            tr.Height = height;

            init(tr);

            canvas.Children.Add(tr);
            Canvas.SetLeft(tr, x);
            Canvas.SetTop(tr, y);

            return tr;
        }

        [JsonConstructor]
        public MyRect(Color borderColor, Color fillColor, double borderLineWidth, int x, int y, int width, int height)
           : base(borderColor, fillColor, borderLineWidth, x, y, width, height)
        {

        }

    }
}
