using Microsoft.Maui.Controls.Shapes;
using Microsoft.UI.Xaml.Shapes;
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


namespace WpfApp1
{
    using PointShapeFiles;
    static class Draw
    {
        public static Shape curShape = null;

        public static Canvas mainCanvas = null;

        private static int xStart;
        private static int yStart;
        private static int xFinish;
        private static int yFinish;
        private static bool onDrawing = false;
        public static System.Windows.UIElement? tempShape = null;

        
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
            public double lineWidth;
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


        public static void onPolyMouseDown(MouseButtonEventArgs e)
        {
            if (!onDrawing)
            {
                xStart = (int)e.GetPosition(mainCanvas).X;
                yStart = (int)e.GetPosition(mainCanvas).Y;
                onDrawing = true;
            }
        }

        public static System.Windows.UIElement? onPolyMouseMove(int xFinish, int yFinish, ConstructorInfo constructor, ShapeSettings s)
        {
            if (curShape != null)
            {
                if (((PointShape)curShape).pointCollection.Count > 1)
                {
                    ((PointShape)curShape).RemoveLastPoint();
                }
                ((PointShape)curShape).AddPoint(xFinish, yFinish);
                mainCanvas.Children.Remove(tempShape);
                curShape.Pen.Brush = s.borderColor;
                curShape.Pen.Thickness = s.lineWidth;
                curShape.Brush = s.fillColor;
                tempShape = ((PointShape)curShape).draw();
                return tempShape;
            }
            return null;

        }



        public static System.Windows.UIElement? onPolyMouseUp(int xFinish, int yFinish, ConstructorInfo constructor, ShapeSettings s)
        {
            bool isEnd = false;
            if (curShape != null)
            {
                isEnd = ((PointShape)Draw.curShape).AddPoint(xFinish, yFinish);
                Draw.mainCanvas.Children.Remove(Draw.tempShape);

            }
            else
            {
                (Draw.curShape) = (PointShape)constructor.Invoke(new object[] { Draw.mainCanvas, xFinish, yFinish });
            }
            curShape.Pen.Brush = s.borderColor;
            curShape.Pen.Thickness = s.lineWidth;
            curShape.Brush = s.fillColor;
            Draw.tempShape = ((PointShape)Draw.curShape).draw();

            System.Windows.UIElement? temp = tempShape;
            if (isEnd)
            {
                ((PointShape)Draw.curShape).RemoveLastPoint();
                Draw.mainCanvas.Children.Remove(Draw.tempShape);
                curShape.Pen.Brush = s.borderColor;
                curShape.Pen.Thickness = s.lineWidth;
                curShape.Brush = s.fillColor;
                Draw.tempShape = ((PointShape)Draw.curShape).draw();
                temp = tempShape;
                curShape = null;
                tempShape = null;

            }
            return temp;

        


        }

    }
}
