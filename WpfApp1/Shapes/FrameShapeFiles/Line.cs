using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using WpfApp1.PointShapeFiles;

namespace WpfApp1.FrameShapeFiles
{
    public class MyLine : FrameShape
    {
        public static int id = 1;
        
        public MyLine(Canvas canvas, int x1, int y1, int x2, int y2)
            : base(canvas, x1, y1, x2, y2)
        {
            
        }
        public MyLine(Canvas canvas, int x, int y, int width)
            : base(canvas, x, y, width)
        {

        }

        override public UIElement draw()
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

        public override Shape copy()
        {
            MyLine clone = new MyLine(canvas, x, y, width, height);


            clone.pen = pen.Clone();
            clone.brush = brush.Clone();

            return clone;
        }
    }
}
