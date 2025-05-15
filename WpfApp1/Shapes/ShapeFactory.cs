using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static WpfApp1.Draw;

namespace WpfApp1.Shapes
{
    public class ShapeFactory
    {
        private static volatile ShapeFactory instance;
        private static readonly object syncRoot = new object();
        
        Type baseType = typeof(Shape);
        Type[]? shapeTypeList;

        private ShapeFactory()
        {
            update();
        }

        public static ShapeFactory Instance()
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                    {
                        instance = new ShapeFactory();
                    }
                }
            }
            return instance;
        }

        public void update()
        {
            lock (syncRoot) 
            {
                shapeTypeList = Assembly.GetAssembly(baseType).GetTypes().Where(type => type.IsSubclassOf(baseType) && !type.IsAbstract).ToArray<Type>();
                shapeTypeList = shapeTypeList.OrderBy(type =>
                {
                    PropertyInfo idProperty = type.GetProperty("id", BindingFlags.Public | BindingFlags.Static);
                    if (idProperty == null)
                        throw new InvalidOperationException($"Class {type.Name} does not have a static 'id' field.");

                    return (int)idProperty.GetValue(null);
                }).ToArray();
            }
        }

        public Shape get(int id, object[] parametrs)
        {
            ConstructorInfo constructor = shapeTypeList[id].GetConstructors().Where(_ => _.GetParameters().Length == parametrs.Length).First();
            return (Shape)constructor.Invoke(parametrs);

        }

        public Type[]? getTypeList()
        {
            return shapeTypeList;
        }





    }
}
