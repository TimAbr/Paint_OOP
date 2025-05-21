using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using Windows.ApplicationModel.Chat;

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
                PointCollection.Add(new System.Windows.Point(xScaled, yScaled));
            }
            ;

        }

        public bool AddPoint(int x, int y)
        {
            bool flag = num > 2 && x >= PointCollection[num - 2].X-2 && x <= PointCollection[num - 2].X+2 && y >= PointCollection[num - 2].Y-2 && y <= PointCollection[num - 2].Y+2;
            PointCollection.Add(new System.Windows.Point(x, y));
            num++;
            return flag;
        }

        public void RemoveLastPoint()
        {
            ((PointShape)Draw.curShape).PointCollection.RemoveAt(num - 1);
            num--;
        }


        public PointCollection PointCollection
        {
            set {
                pointCollection = value;
            }
            get
            {
                return pointCollection;
            }
        }

        public PointCollection pointCollection;



        protected int num;

        public PointShape(int x, int y, int width)
            : base(x, y, width)
        {
            this.width = width;
            height = width;
            this.x = x;
            this.y = y;
            pointCollection = new PointCollection();

            if (width > 0)
            {
                num = 5;
                setPoints(x, y, width, width, num);

                Random rnd = new Random();
                for (int i = 0; i < num; i++)
                {
                    int x1 = PointCollection[i].X - 2 <= x ? x + rnd.Next(1, width / num) : PointCollection[i].X + 2 >= x + width - 1 ? x + width - rnd.Next(1, width / num) : (int)PointCollection[i].X + rnd.Next(-width / num, width / num + 1);
                    int y1 = PointCollection[i].Y - 2 <= y ? y + rnd.Next(1, height / num) : PointCollection[i].Y + 2 >= y + height - 1 ? y + height - rnd.Next(1, height / num) : (int)PointCollection[i].Y + rnd.Next(-height / num, height / num + 1);
                    PointCollection[i] = new System.Windows.Point(x1, y1);
                }
            }

            isPointShape = true;

        }

        public PointShape(int x, int y, int width, int height, Color fillColor, Color borderColor, PointCollection pointCollection)
            :base(x,y,width) {
            this.pointCollection = pointCollection;
            this.fillColor = fillColor;
            fillBrush = new SolidColorBrush(fillColor);
            this.borderColor = borderColor;
            borderBrush = new SolidColorBrush(borderColor);
            this.pointCollection = pointCollection;
        }



      
    }
}
