﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfApp1.PointShapeFiles
{
    class MyCustomPolyline : PointShape
    {


        public MyCustomPolyline(Canvas canvas, int x, int y, int width)
            : base(canvas, x, y, width)
        {
            pointCollection.RemoveAt(num - 1);
            num--;


        }

        public MyCustomPolyline(Canvas canvas, int x, int y)
            : base(canvas, x, y, 0)
        {
            pointCollection = new PointCollection();
            pointCollection.Add(new System.Windows.Point(x, y));
            num++;
        }



        override public System.Windows.UIElement draw()
        {

            Polyline tr = new Polyline();

            tr.Points = pointCollection;
            brush = new SolidColorBrush(new Color
            {
                A = 0,
                R = 0x27,
                G = 0x27,
                B = 0x27
            });
            init(tr, pen, brush);

            canvas.Children.Add(tr);

            return tr;

        }
    }
}
