using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
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


            Type[]? shapeTypeList = ShapeFactory.Instance().getTypeList();

            var shapeList = new ShapeList.ShapeList(0, Draw.mainCanvas);


            for (int i = 0; i < tempArr.Length/2; i++)
            {
                int id = int.Parse(tempArr[i * 2]);

                Shape s = (Shape) JsonSerializer.Deserialize(tempArr[i * 2+1], shapeTypeList[id]);

                shapeList.add(s);
            }

            return shapeList;

        }
    }
}
