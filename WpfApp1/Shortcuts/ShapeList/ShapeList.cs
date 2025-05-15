using System.Windows.Controls;

namespace WpfApp1.Shortcuts.ShapeList
{

    public class ShapeList
    {

        private List<Shape> shapeList;
        private Canvas canvas;

        private Redo.Redo redoStack;

        public Shape? this[int index]
        {
            get {
                if (shapeList.Count <= index)
                {
                    return null;
                } 
                else
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
            redoStack = new Redo.Redo();
        }


        //public void reDraw()
        //{
        //    canvas.Children.Clear();
        //    for (int i = 0; i < this.size(); i++)
        //    {
        //        shapeList[i].draw();
        //    }
        //}

        //private ShapeList copy()
        //{
        //    ShapeList sl = new ShapeList(shapeList.Count, canvas);
        //    for (int i = 0; i<shapeList.Count; i++)
        //    {
        //        sl.add(shapeList[i].copy());
        //    }
        //    return sl;
        //}

        public int size()
        {
            return shapeList.Count();
        }

        public List<Shape> getList()
        {
            return shapeList;
        }


        public void add(Shape s)
        {
            shapeList.Add(s);
            s.draw(canvas);
            addUndo();
        }

        public Shape removeLast()
        {
            var s = shapeList[size() - 1];
            canvas.Children.RemoveAt(size() - 1);
            shapeList.RemoveAt(size() - 1); 
            return s;
        }

        public Shape last()
        {
            return shapeList.Last();
        }

        public void undo()
        {

            if (size()>0)
            {
                Shape s = removeLast();
                redoStack.add(s);
            }
            //reDraw();
            
        }

        public void update()
        {
            if (size() > 0)
            {
                Shape s = removeLast();
                add(s);
            }
        }

        public void redo()
        {
            if (!redoStack.isEmpty())
            {
                var s = redoStack.pop();
                shapeList.Add(s);
                s.draw(canvas);
            }
            //reDraw();
        }

        public void addUndo()
        {
            redoStack.clear();
        }
    }
}
