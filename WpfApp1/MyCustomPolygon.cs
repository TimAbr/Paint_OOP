using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfApp1
{
    class MyCustomPolygon: PointShape
    {
        

        public MyCustomPolygon(Canvas canvas, int x, int y, int width)
            : base(canvas, x, y, width)
        {
        

            
        }

        public MyCustomPolygon(Canvas canvas, int x, int y)
            : base(canvas, x, y, 0)
        {
            pointCollection = new PointCollection();
            pointCollection.Add(new System.Windows.Point(x , y));
            num++;
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
