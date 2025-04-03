using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp1.PointShapeFiles
{

    
    public abstract class PointShape: Shape
    {
        
        protected void setPoints(int x, int y, int width, int height, int num)
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
            }
            ;

        }

        public bool AddPoint(int x, int y)
        {
            bool flag = num > 2 && x >= pointCollection[num - 2].X-2 && x <= pointCollection[num - 2].X+2 && y >= pointCollection[num - 2].Y-2 && y <= pointCollection[num - 2].Y+2;
            pointCollection.Add(new System.Windows.Point(x, y));
            num++;
            return flag;
        }

        public void RemoveLastPoint()
        {
            ((PointShape)Draw.curShape).pointCollection.RemoveAt(num - 1);
            num--;
        }


        public PointCollection pointCollection = new PointCollection();

        protected int num;
        protected int count = 0;

        public PointShape(Canvas canvas, int x, int y, int width)
            : base(canvas, x, y, width)
        {
            this.width = width;
            height = width;
            this.x = x;
            this.y = y;
            if (width > 0)
            {
                num = 5;
                setPoints(x, y, width, width, num);

                Random rnd = new Random();
                for (int i = 0; i < num; i++)
                {
                    int x1 = pointCollection[i].X - 2 <= x ? x + rnd.Next(1, width / num) : pointCollection[i].X + 2 >= x + width - 1 ? x + width - rnd.Next(1, width / num) : (int)pointCollection[i].X + rnd.Next(-width / num, width / num + 1);
                    int y1 = pointCollection[i].Y - 2 <= y ? y + rnd.Next(1, height / num) : pointCollection[i].Y + 2 >= y + height - 1 ? y + height - rnd.Next(1, height / num) : (int)pointCollection[i].Y + rnd.Next(-height / num, height / num + 1);
                    pointCollection[i] = new System.Windows.Point(x1, y1);
                }
            }

            brush = new SolidColorBrush(Colors.White);
            this.canvas = canvas;
            Pen = null;

            isPointShape = true;

        }

      
    }
}
