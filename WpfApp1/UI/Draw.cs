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
using WpfApp1.PointShapeFiles;
using WpfApp1.Shortcuts.ShapeList;


namespace WpfApp1
{
    
    public static class Draw
    {
        public static Shape curShape = null;

        public static Canvas mainCanvas = null;

        private static int xStart;
        private static int yStart;
        private static int xFinish;
        private static int yFinish;
        private static bool onDrawing = false;

        public static ShapeList shapeList = null;


        public static void reDraw()
        {

        }

        
        public static void onMouseDown(MouseButtonEventArgs e)
        {
            if (!onDrawing)
            {
                xStart = (int)e.GetPosition(mainCanvas).X;
                yStart = (int)e.GetPosition(mainCanvas).Y;
                onDrawing = true;
            }
        }

        public static void onMouseMove(int xFinish, int yFinish, ConstructorInfo constructor, ShapeSettings s)
        {
            if (onDrawing) {
                                
                if (curShape != null)
                {
                    shapeList.removeLast();
                }

                setShape(xFinish, yFinish, constructor, s);
            }

        }

        public struct ShapeSettings
        {
            public Brush borderColor;
            public Brush fillColor;
            public double lineWidth;
            public bool isLast;
            public MouseButtonEventHandler mouseUp;
        }

        private static void setShape(int xFinish, int yFinish, ConstructorInfo constructor, ShapeSettings s)
        {
            Shape temp = (Shape)constructor.Invoke(new object[] { mainCanvas, xStart, yStart, xFinish, yFinish });


            temp.Settings = s;


            shapeList.add(temp);
            //shapeList.reDraw();

            curShape = temp;

        }

    

        public static void onMouseUp(int xFinish, int yFinish, ConstructorInfo constructor, ShapeSettings s)
        {
            if (onDrawing)
            {
                onMouseMove(xFinish, yFinish, constructor, s);
            }
            onDrawing = false;
            curShape = null;

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

        public static void onPolyMouseMove(int xFinish, int yFinish, ConstructorInfo constructor, ShapeSettings s)
        {
            if (curShape != null)
            {
                if (((PointShape)curShape).pointCollection.Count > 1)
                {
                    ((PointShape)curShape).RemoveLastPoint();
                }

                ((PointShape)curShape).AddPoint(xFinish, yFinish);

               shapeList.update();
            }

        }



        public static void onPolyMouseUp(int xFinish, int yFinish, ConstructorInfo constructor, ShapeSettings s)
        {
            bool isEnd = false;
            if (curShape != null)
            {
                isEnd = ((PointShape)Draw.curShape).AddPoint(xFinish, yFinish);
                shapeList.update();
            }
            else
            {
                (Draw.curShape) = (PointShape)constructor.Invoke(new object[] { Draw.mainCanvas, xFinish, yFinish });
                shapeList.add(curShape);
            }
            curShape.Settings = s;
            shapeList.update();

            if (isEnd)
            {
                s.isLast = true;
                ((PointShape)Draw.curShape).RemoveLastPoint();
                shapeList.update();
                
                curShape = null;

            }
        
        }

    }
}
