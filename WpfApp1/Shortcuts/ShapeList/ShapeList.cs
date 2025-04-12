using System.Windows.Controls;

namespace WpfApp1.Shortcuts.ShapeList
{

    public class ShapeList
    {

        private List<Shape> shapeList = new List<Shape>();
        private Canvas canvas;

        private Undo.Undo undoStack;
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

            undoStack = new Undo.Undo();
            redoStack = new Redo.Redo();
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
                sl.add(shapeList[i].copy());
            }
            return sl;
        }

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
        }

        public void removeLast()
        {
            shapeList.RemoveAt(size() - 1);
        }

        public Shape last()
        {
            return shapeList.Last();
        }

        public void undo()
        {
            if (!undoStack.isEmpty())
            {
                redoStack.add(this.copy());
                shapeList = undoStack.pop().getList();
            }
            reDraw();
            
        }

        public void redo()
        {
            if (!redoStack.isEmpty())
            {
                undoStack.add(this.copy());
                shapeList = redoStack.pop().getList();
            }
            reDraw();
        }

        public void addUndo()
        {
            undoStack.add(copy());
            redoStack.clear();
        }
    }
}
