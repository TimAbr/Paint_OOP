using System;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfApp1.FrameShapeFiles
{
    public class MyPolygon : FrameShape
    {

        public static int id { get => 3; }


        protected PointCollection pointCollection = new PointCollection();
        protected int num;
        public int Num { get => num; set{ num = value; setPoints(x, y, width, height, num); } }
        private void setPoints(int x, int y, int width, int height, int num)
        {

            double centerX = x + width / 2.0;
            double centerY = y + height / 2.0;


            List<System.Windows.Point> normalizedPoints = new List<System.Windows.Point>();
            double angleStep = 2 * Math.PI / num;
            double initialAngle = -Math.PI / 2; 

            for (int i = 0; i < num; i++)
            {
                double angle = initialAngle + i * angleStep;
                double xNormalized = Math.Cos(angle);
                double yNormalized = Math.Sin(angle);
                normalizedPoints.Add(new System.Windows.Point(xNormalized, yNormalized));
            }


            double minX = normalizedPoints.Min(p => p.X);
            double maxX = normalizedPoints.Max(p => p.X);
            double minY = normalizedPoints.Min(p => p.Y);
            double maxY = normalizedPoints.Max(p => p.Y);


            double scaleX = width / (maxX - minX);
            double scaleY = height / (maxY - minY);

            foreach (var point in normalizedPoints)
            {
                double xScaled = (point.X - minX) * scaleX + x;
                double yScaled = (point.Y - minY) * scaleY + y;
                pointCollection.Add(new System.Windows.Point(xScaled, yScaled));
            };
            
        }


        public MyPolygon(int x1, int y1, int x2, int y2) 
            : base(x1, y1, x2, y2)
        {
            num = 5;
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

            setPoints(x, y, width, height, num);

        }

        public MyPolygon(int x, int y, int width)
            : base(x, y, width)
        {
            num = 5;
            setPoints(x, y, width, height, 5);
        }

        override public UIElement draw(Canvas canvas)
        {

            Polygon tr = new Polygon();
            tr.Points = pointCollection;
            init(tr);

            canvas.Children.Add(tr);

            return tr;
            
        }

        [JsonConstructor]
        public MyPolygon(Color borderColor, Color fillColor, double borderLineWidth, int x, int y, int width, int height)
           : base(borderColor, fillColor, borderLineWidth, x, y, width, height)
        {

        }

    }

}
