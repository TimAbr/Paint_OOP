using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
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

        public Color borderColor { get; set; }
        public Color fillColor { get; set; }

        protected Brush borderBrush;
        protected Brush fillBrush;

        public double borderLineWidth {get;set;}


        public ShapeSettings Settings
        {
            set
            {
                fillColor = value.fillColor;
                fillBrush = new SolidColorBrush(fillColor);

                borderColor = value.borderColor;
                borderBrush = new SolidColorBrush(borderColor);

                borderLineWidth = value.lineWidth;
            }
        }


        public bool isPointShape;

        public Shape(int x, int y, int width, int height)
        {
            this.width = width;
            this.height = height;
            this.x = x;
            this.y = y;

            borderColor = Colors.Black;
            fillColor = Colors.White;
            borderBrush = new SolidColorBrush(borderColor);
            fillBrush = new SolidColorBrush(fillColor);

            borderLineWidth = 1;
        }


        public Shape(int x, int y, int width)
        {
            this.width = width;
            this.height = width;
            this.x = x;
            this.y = y;

            borderColor = Colors.Black;
            fillColor = Colors.White;
            borderBrush = new SolidColorBrush(borderColor);
            fillBrush = new SolidColorBrush(fillColor);

            borderLineWidth = 1;
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

        


        protected void init(System.Windows.Shapes.Shape s)
        {
            s.Fill = fillBrush;
            s.Stroke = borderBrush;
            s.StrokeThickness = borderLineWidth;

            s.IsHitTestVisible = false;

            //if (!settings.isLast && settings.mouseUp!=null)
            //{
            //    s.MouseUp += settings.mouseUp;
            //}
        }

        abstract public System.Windows.UIElement draw(Canvas canvas);

    }
}
