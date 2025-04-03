using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfApp1.Shortcuts.ShapeList
{

    public class ShapeList
    {

        private List<Shape> shapeList = new List<Shape>();
        private Canvas canvas;

        public Shape? this[int index]
        {
            get {
                if (shapeList.Count <= index)
                {
                    return null;
                } else
                {
                    return shapeList[index];
                }
            }
            set
            {
                if (shapeList.Count > index)
                {
                    shapeList[index] = value;
                }
            }
        }

        public ShapeList(int n, Canvas canvas)
        {
            shapeList = new List<Shape>(n);
            this.canvas = canvas;
        }


        public void reDraw()
        {
            canvas.Children.Clear();
            for (int i = 0; i<this.size(); i++)
            {
                shapeList[i].draw();
            }
        }

        private ShapeList copy()
        {
            ShapeList sl = new ShapeList(shapeList.Count, canvas);
            for (int i = 0; i<shapeList.Count; i++)
            {
                sl[i] = shapeList[i].copy();
            }
            return sl;
        }

        public int size()
        {
            return shapeList.Count();
        }


        public void add(Shape s)
        {
            shapeList.Add(s);
        }

        public void removeLast()
        {
            shapeList.RemoveAt(size() - 1);
        }

        public Shape last()
        {
            return shapeList.Last();
        }
    }
}
