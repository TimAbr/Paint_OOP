using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfApp1.FrameShapeFiles
{
    public abstract class FrameShape: Shape 
    {
        public FrameShape(Canvas canvas, int x, int y, int width):base(canvas, x, y, width)
        {
            isPointShape = false;
        }

        public FrameShape(Canvas canvas, int x, int y, int width, int height):base(canvas,x,y,width,height)
        {
            
        }
    }
}
