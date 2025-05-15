using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WpfApp1.Shapes;

namespace WpfApp1.Shortcuts.Files
{
    class Serializer
    {
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            Converters = { new CustomJsonConverter() }
        };

        public Serializer()
        {

        }

        public String serialize(ShapeList.ShapeList shapeList)
        {

            var res = "";
            var list = shapeList.getList();

            Type[]? shapeTypeList = ShapeFactory.Instance().getTypeList();

            
            for (int i = 0; i< list.Count(); i++)
            {
                var id = (int)list[i].GetType().GetProperty("id", BindingFlags.Static | BindingFlags.Public).GetValue(null);
                res += id + "\n";

                var json = JsonSerializer.Serialize(list[i], shapeTypeList[id], options);

                res += json + "\n";
            }

            res.Trim();

            return res;

        }
    }
}
