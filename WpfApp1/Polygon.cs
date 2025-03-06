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
        public int Num { get => num; set{ num = value; setPoints(x, y, width, num); } }
        private void setPoints(int x, int y, int width, int num)
        {
            double oX = x + ((double)width) / 2, oY = y + ((double)height) / 2;
            double y1 = y;
            double x1;
            if (num % 2 == 1)
            {
                x1 = x + ((double)width) / 2;
            }
            else
            {
                x1 = x + (width - height * Math.Tan(Math.Acos(-1) / num)) / 2;
            }
            pointCollection.Add(new System.Windows.Point(x1, y1));

            double radNextX = x1 - oX, radNextY = y1 - oY;
            double xCur, yCur;
            double polarX, polarY;
            double tempX, tempY;

            for (int i = 0; i < num - 1; ++i)
            {
                polarX = 1 * Math.Cos(Math.Acos(-1.0) * 2 / num);
                polarY = 1 * Math.Sin(Math.Acos(-1.0) * 2 / num);
                tempX = radNextX * polarX - radNextY * polarY;
                tempY = radNextX * polarY + radNextY * polarX;
                radNextX = tempX;
                radNextY = tempY;
                xCur = oX + radNextX;
                yCur = oY + radNextY;
                pointCollection.Add(new System.Windows.Point(xCur, yCur));
            }
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
            height = width;

            setPoints(x, y, width, num);
        }

        public MyPolygon(Canvas canvas, int x, int y, int width)
            : base(canvas, x, y, width)
        {
            num = 5;
            setPoints(x, y, width, 5);
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
