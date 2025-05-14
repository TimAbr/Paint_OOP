using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Shortcuts.Redo
{
    public class Redo
    {

        private Stack<Shape> redoStack;

        public Redo()
        {
            redoStack = new Stack<Shape>();
        }

        public int size()
        {
            return redoStack.Count();
        }


        public void add(Shape s)
        {
            redoStack.Push(s);
        }

        public bool isEmpty()
        {
            return redoStack.Count == 0;
        }

        public Shape pop()
        {
            return redoStack.Pop();
        }

        public Shape top()
        {
            return redoStack.Peek();
        }

        public Shape redo()
        {
            return top();
        }
        public void clear()
        {
            redoStack.Clear();
        }
    }
}
