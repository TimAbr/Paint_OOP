using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using WpfApp1.FrameShapeFiles;

namespace WpfApp1.Plugins
{
    
    public class ShapePluginResolver 
    {

        public ShapePluginResolver()
        {
        }

        public List<Type> AddPlugin(string pluginPath)
        {
            var result = new List<Type>();
            var notIncudedModules = new List<String>();
            var errorList = new List<String>();
            try
            {
                Type baseType = typeof(Shape);
                Type frameType = typeof(FrameShape);
                Type pointShape = typeof(PointShapeFiles.PointShape);
                Assembly assembly = Assembly.LoadFrom(pluginPath);
                Type[] types = assembly.GetTypes().Where(type => type.IsSubclassOf(baseType) && !type.IsAbstract).ToArray<Type>();

                HashSet<int> idSet = new HashSet<int>();

                foreach (Type type in types)
                {
                    if (type.IsSubclassOf(frameType))
                    {
                        result.Add(type);
                    } else if (type.IsSubclassOf(pointShape))
                    {
                        result.Add(type);
                    } else
                    {
                        errorList.Add("");
                    }
                     
                }
                
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Error creating shape from plugin: {ex.Message}");
                //return (null, null);
            }

            return result;

        }
    }
    
}
