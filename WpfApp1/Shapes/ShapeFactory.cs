using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.UI;
using static WpfApp1.Draw;

namespace WpfApp1.Shapes
{
    public class ShapeFactory
    {
        private static volatile ShapeFactory instance;
        private static readonly object syncRoot = new object();
        private Plugins.ShapePluginResolver pluginResolver;
        
        Type baseType = typeof(Shape);
        Type[]? shapeTypeList;
        Dictionary<int, Type> shapeTypeMap;

        private ShapeFactory()
        {
            update();
            pluginResolver = new Plugins.ShapePluginResolver();
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

                if (checkTypeList(shapeTypeList))
                {
                    shapeTypeMap = shapeListToMap(shapeTypeList);
                } else
                {
                    MessageBox.Show("Initialisation error. Some types have the same id");
                }


            }
        }

        public bool checkTypeList(Type[] shapeTypeList)
        {
            for (int i = 0; i < shapeTypeList.Length - 1; i++)
            {
                if ((int) shapeTypeList[i].GetProperty("id", BindingFlags.Public | BindingFlags.Static).GetValue(0) ==
                    (int)shapeTypeList[i + 1].GetProperty("id", BindingFlags.Public | BindingFlags.Static).GetValue(0))
                {
                    return false;
                }
            }

            return true;
        }

        public Dictionary<int, Type> shapeListToMap(Type[] shapeTypeList)
        {
            var res = new Dictionary<int, Type>();
            for (int i = 0; i<shapeTypeList.Length; i++)
            {
                res.Add((int) shapeTypeList[i].GetProperty("id", BindingFlags.Public | BindingFlags.Static).GetValue(0), shapeTypeList[i]);
            }
            return res;
        }

        public Shape get(int id, object[] parametrs)
        {
            ConstructorInfo constructor = shapeTypeMap[id].GetConstructors().Where(_ => _.GetParameters().Length == parametrs.Length).First();
            return (Shape)constructor.Invoke(parametrs);

        }

        public Dictionary<int, Type> getTypeMap()
        {
            return shapeTypeMap;
        }


        public void addPlugins(String path)
        {
            Type[] temp = pluginResolver.AddPlugin(path).ToArray();

            Type[] test = shapeTypeList.Concat(temp).ToArray();

            test = test.OrderBy(type =>
            {
                PropertyInfo idProperty = type.GetProperty("id", BindingFlags.Public | BindingFlags.Static);
                if (idProperty == null)
                    throw new InvalidOperationException($"Class {type.Name} does not have a static 'id' field.");

                return (int)idProperty.GetValue(null);
            }).ToArray();

            if (checkTypeList(test))
            {
                shapeTypeList = test;

                shapeTypeMap = shapeListToMap(shapeTypeList);

                MainWindow.updateShapeButtons();
            } else
            {
                
                 MessageBox.Show("Plugins weren't added. Some types have the same id");
                
            }
        }

    }
}
