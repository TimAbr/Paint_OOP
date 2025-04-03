using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfApp1.PointShapeFiles
{
    class MyCustomPolygon: PointShape
    {

        public static int id = 4;

        public MyCustomPolygon(Canvas canvas, int x, int y, int width)
            : base(canvas, x, y, width)
        {
            isPointShape = true;
           
        }

        public MyCustomPolygon(Canvas canvas, int x, int y)
            : base(canvas, x, y, 0)
        {
            pointCollection = new PointCollection();
            pointCollection.Add(new System.Windows.Point(x , y));
            num++;
            isPointShape = true;

        }

        

        override public System.Windows.UIElement draw()
        {

            Polygon tr = new Polygon();

            tr.Points = pointCollection;
            init(tr);

            canvas.Children.Add(tr);

            return tr;

        }

        public override Shape copy()
        {
            MyCustomPolygon clone = new MyCustomPolygon(canvas, x, y);

            for (int i = 1; i < pointCollection.Count; i++)
            {
                clone.AddPoint((int)pointCollection[i].X, (int)pointCollection[i].Y);
            }

            clone.pen = pen.Clone();
            clone.brush = brush.Clone();

            return clone;
        }
    }
}
