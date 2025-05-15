using System;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using WpfApp1.PointShapeFiles;

namespace WpfApp1.FrameShapeFiles
{
    public class MyLine : FrameShape
    {
        public static int id { get => 1; }

        public MyLine(int x1, int y1, int x2, int y2)
            : base(x1, y1, x2, y2)
        {
            
        }
        public MyLine(int x, int y, int width)
            : base(x, y, width)
        {

        }

        override public UIElement draw(Canvas canvas)
        {

            Line tr = new Line();
            tr.X1 = x;
            tr.Y1 = y;
            tr.X2 = width;
            tr.Y2 = height;
            init(tr);

            canvas.Children.Add(tr);

            return tr;
            
        }

        [JsonConstructor]
        public MyLine(Color borderColor, Color fillColor, double borderLineWidth, int x, int y, int width, int height)
           : base(borderColor, fillColor, borderLineWidth, x, y, width, height)
        {

        }
    }
}
