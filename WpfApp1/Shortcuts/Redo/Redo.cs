using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Shortcuts.Redo
{
    public class Redo
    {

        private Stack<ShapeList.ShapeList> redoStack;

        public Redo()
        {
            redoStack = new Stack<ShapeList.ShapeList>();
        }

        public int size()
        {
            return redoStack.Count();
        }


        public void add(ShapeList.ShapeList s)
        {
            redoStack.Push(s);
        }

        public bool isEmpty()
        {
            return redoStack.Count == 0;
        }

        public ShapeList.ShapeList pop()
        {
            return redoStack.Pop();
        }

        public ShapeList.ShapeList top()
        {
            return redoStack.Peek();
        }

        public ShapeList.ShapeList redo()
        {
            return top();
        }
        public void clear()
        {
            redoStack.Clear();
        }
    }
}
