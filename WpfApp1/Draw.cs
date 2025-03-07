using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using WpfApp1;

namespace WpfApp1
{
    public enum ShapeType { line, rect, ellipse, polygon, polyline }
    static class Draw
    {
        public static Canvas mainCanvas = null;

        private static int xStart;
        private static int yStart;
        private static int xFinish;
        private static int yFinish;
        private static bool onDrawing = false;
        private static System.Windows.UIElement? tempShape = null;

        public static void onMouseDown(MouseButtonEventArgs e)
        {
            if (!onDrawing)
            {
                xStart = (int)e.GetPosition(mainCanvas).X;
                yStart = (int)e.GetPosition(mainCanvas).Y;
                onDrawing = true;
            }
        }

        public static System.Windows.UIElement? onMouseMove(int xFinish, int yFinish, ConstructorInfo constructor, ShapeSettings s)
        {
            if (onDrawing) {
                if (tempShape != null)
                {
                    if (mainCanvas.Children.Contains(tempShape))
                    {
                        mainCanvas.Children.Remove(tempShape);
                    }
                }

                setShape(xFinish, yFinish, constructor, s);
            }
            return tempShape;
        }

        public struct ShapeSettings
        {
            public Brush borderColor;
            public Brush fillColor;
            public int lineWidth;
        }

        private static void setShape(int xFinish, int yFinish, ConstructorInfo constructor, ShapeSettings s)
        {
            Shape temp = (Shape)constructor.Invoke(new object[] { mainCanvas, xStart, yStart, xFinish, yFinish });
            temp.Pen.Brush = s.borderColor;
            temp.Pen.Thickness = s.lineWidth;
            temp.Brush = s.fillColor;
            tempShape = (temp).draw();
        }

    

        public static System.Windows.UIElement? onMouseUp(int xFinish, int yFinish, ConstructorInfo constructor, ShapeSettings s)
        {
            if (onDrawing)
            {
                onMouseMove(xFinish, yFinish, constructor, s);
            }
            onDrawing = false;
            System.Windows.UIElement? temp = tempShape;
            tempShape = null;
            return temp;


        }

    }
}
