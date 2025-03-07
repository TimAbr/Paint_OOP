using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfApp1
{
    public class MyPolygon : Shape
    {

        protected PointCollection pointCollection = new PointCollection();
        protected int num;
        public int Num { get => num; set{ num = value; setPoints(x, y, width, height, num); } }
        private void setPoints(int x, int y, int width, int height, int num)
        {
            // Центр ограничивающего прямоугольника
            double centerX = x + width / 2.0;
            double centerY = y + height / 2.0;

            // Генерируем вершины в нормализованном виде (радиус = 1)
            List<System.Windows.Point> normalizedPoints = new List<System.Windows.Point>();
            double angleStep = 2 * Math.PI / num;
            double initialAngle = -Math.PI / 2; // Первая вершина сверху

            for (int i = 0; i < num; i++)
            {
                double angle = initialAngle + i * angleStep;
                double xNormalized = Math.Cos(angle);
                double yNormalized = Math.Sin(angle);
                normalizedPoints.Add(new System.Windows.Point(xNormalized, yNormalized));
            }

            // Находим границы Bounding Box
            double minX = normalizedPoints.Min(p => p.X);
            double maxX = normalizedPoints.Max(p => p.X);
            double minY = normalizedPoints.Min(p => p.Y);
            double maxY = normalizedPoints.Max(p => p.Y);

            // Вычисляем масштабные коэффициенты
            double scaleX = width / (maxX - minX);
            double scaleY = height / (maxY - minY);

            // Масштабируем и смещаем точки
            foreach (var point in normalizedPoints)
            {
                double xScaled = (point.X - minX) * scaleX + x;
                double yScaled = (point.Y - minY) * scaleY + y;
                pointCollection.Add(new System.Windows.Point(xScaled, yScaled));
            };
            
        }


        public MyPolygon(Canvas canvas, int x1, int y1, int x2, int y2) 
            : base(canvas, x1, y1, x2, y2)
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

        public MyPolygon(Canvas canvas, int x, int y, int width)
            : base(canvas, x, y, width)
        {
            num = 5;
            setPoints(x, y, width, height, 5);
        }

        override public System.Windows.UIElement draw()
        {

            Polygon tr = new Polygon();
  
            tr.Points = pointCollection;
            tr.Fill = brush;
            tr.Stroke = pen.Brush;
            tr.StrokeDashArray = pen.DashStyle.Dashes;
            tr.StrokeThickness = pen.Thickness;
            tr.StrokeDashCap = pen.DashCap;
               
            canvas.Children.Add(tr);

            return tr;
            
        }
    
    }

}
