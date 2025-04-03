using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static WpfApp1.Draw;

namespace WpfApp1
{
    public abstract class Shape
    {
        protected int x;
        protected int y;
        protected int width;
        protected int height;

        public ShapeSettings settings;

        public bool isPointShape;

        public Shape(Canvas canvas, int x, int y, int width, int height)
        {
            this.width = width;
            this.height = height;
            this.x = x;
            this.y = y;

            brush = new SolidColorBrush(Colors.White);
            this.canvas = canvas;
            Pen = null;
        }


        public Shape(Canvas canvas, int x, int y, int width)
        {
            this.width = width;
            this.height = width;
            this.x = x;
            this.y = y;

            brush = new SolidColorBrush(Colors.White);
            this.canvas = canvas;
            Pen = null;

        }

        public int X { 
            get {
                return x;
            }

            set { 
                if (value < 0)
                {
                    x = 0;
                } else {
                    x = value; 
                }
            } 
        }

        public int Y
        {
            get
            {
                return y;
            }

            set
            {
                if (value < 0)
                {
                    y = 0;
                }
                else
                {
                    y = value;
                }
            }
        }

        public int Width
        {
            get
            {
                return width;
            }

            set
            {
                if (value < 0)
                {
                    width = 0;
                }
                else
                {
                    width = value;
                }
            }
        }
        public int Height
        {
            get
            {
                return height;
            }

            set
            {
                if (value < 0)
                {
                    height = 0;
                }
                else
                {
                    height = value;
                }
            }
        }
        protected Brush brush;
        protected Canvas canvas;
        protected Pen pen;

        public Brush Brush
        {
            get
            {
                return brush;
            }
            set
            {
                if (value == null)
                {
                    brush = new SolidColorBrush(Colors.White);
                }
                else
                {
                    brush = value;
                }
            }
        }

        public Canvas Canvas
        {
            get { return canvas; }
            set
            {
                if (value != null)
                {
                    canvas = value;
                }
            }
        }

        public Pen Pen
        {
            get
            {
                return pen;
            }
            set
            {
                if (value == null)
                {
                    pen = new Pen(Brushes.Black, 1.0);
                }
                else
                {
                    pen = value;
                }
            }
        }

        protected void init(System.Windows.Shapes.Shape s)
        {
            brush = settings.fillColor;
            Pen.Brush = settings.borderColor;
            pen.Thickness = settings.lineWidth;

            s.Fill = brush;
            s.Stroke = pen.Brush;
            s.StrokeDashArray = pen.DashStyle.Dashes;
            s.StrokeThickness = pen.Thickness;
            s.StrokeDashCap = pen.DashCap;

            if (!settings.isLast)
            {
                s.MouseUp += settings.mouseUp;
            }
        }

        abstract public System.Windows.UIElement draw();

        abstract public Shape copy();

    }
}
