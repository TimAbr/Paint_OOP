using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfApp1.PointShapeFiles
{
    class MyCustomPolyline : PointShape
    {
        public static int id { get => 5; }


        public MyCustomPolyline(int x, int y, int width)
            : base(x, y, width)
        {
            PointCollection.RemoveAt(num - 1);
            num--;
        }

        public MyCustomPolyline(int x, int y)
            : base(x, y, 0)
        {
            PointCollection = new PointCollection();
            PointCollection.Add(new System.Windows.Point(x, y));
            num++;
        }



        override public System.Windows.UIElement draw(Canvas canvas)
        {

            Polyline tr = new Polyline();

            tr.Points = PointCollection;
            
            init(tr);


            canvas.Children.Add(tr);

            return tr;

        }

        [JsonConstructor]
        public MyCustomPolyline(int x, int y, int width, int height, Color fillColor, Color borderColor, PointCollection pointCollection)
            : base(x, y, width, height, fillColor, borderColor, pointCollection)
        {

        }
    }
}
