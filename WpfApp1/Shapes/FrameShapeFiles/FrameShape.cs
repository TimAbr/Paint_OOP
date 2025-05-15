using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp1.FrameShapeFiles
{
    public abstract class FrameShape: Shape 
    {
        public FrameShape(int x, int y, int width):base(x, y, width)
        {
            isPointShape = false;
        }

        public FrameShape(int x, int y, int width, int height):base(x,y,width,height)
        {
            isPointShape = false;
        }

        public FrameShape(Color borderColor, Color fillColor, double borderLineWidth, int x, int y, int width, int height)
            : base(x, y, width, height)
        {
            this.fillColor = fillColor;
            fillBrush = new SolidColorBrush(fillColor);
            this.borderColor = borderColor;
            borderBrush = new SolidColorBrush(borderColor);
            this.borderLineWidth = borderLineWidth;
        }

    }
}
