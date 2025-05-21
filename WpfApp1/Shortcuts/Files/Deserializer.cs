using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Shapes;
using WpfApp1.Shortcuts.ShapeList;

namespace WpfApp1.Shortcuts.Files
{
    class Deserializer
    {
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            Converters = { new CustomJsonConverter() }
        };

        public Deserializer()
        {

        }

        public ShapeList.ShapeList deserialize(String input)
        {
            var tempArr = input.Split("\n");


            var shapeTypeList = ShapeFactory.Instance().getTypeMap();

            var usedKeys = shapeTypeList.Keys.ToArray();

            var shapeList = new ShapeList.ShapeList(0, Draw.mainCanvas);

            var errorId = new List<int>();


            for (int i = 0; i < tempArr.Length / 2; i++)
            {
                int id = int.Parse(tempArr[i * 2]);

                if (usedKeys.Contains(id))
                {
                    Shape s = (Shape)JsonSerializer.Deserialize(tempArr[i * 2 + 1], shapeTypeList[id]);

                    shapeList.add(s);
                } else
                {
                    if (!errorId.Contains(id))
                    {
                        errorId.Add(id);
                    }
                }
            }

            if (errorId.Count != 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(errorId[0]);

                for (int i = 1; i<errorId.Count; i++)
                {
                    sb.Append(", ");
                    sb.Append(errorId[i]);
                }

                MessageBox.Show("Some plugins were not added to your programm: " + sb.ToString() + ".\nAdd these plugins and reopen the file.");

            }

            return shapeList;

        }
    }
}
