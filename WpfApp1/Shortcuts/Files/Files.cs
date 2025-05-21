using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Shapes;
using WpfApp1.UI;

namespace WpfApp1.Shortcuts.Files
{
    public class Files
    {
        Dialogs dialogs;

        Serializer serializer;
        Deserializer deserializer;

        String? curFile = null;



        public Files()
        {
            dialogs = new Dialogs();
            serializer = new Serializer();
            deserializer = new Deserializer();
        }

        
        public ShapeList.ShapeList open()
        {
            if (curFile == null)
            {
                save(Draw.shapeList);
            }

            var fileName = dialogs.openFileDialog();

            if (fileName != null && fileName!="")
            {
                Draw.mainCanvas.Children.Clear();

                String content;
                using (StreamReader reader = new StreamReader(fileName))
                {
                    content = reader.ReadToEnd();
                }

                var res = deserializer.deserialize(content);
                Draw.shapeList = res;
                
                curFile = fileName;
            }

            return null;
        }

        public void newFile()
        {

        }

        public void save(ShapeList.ShapeList shapeList)
        {
            if (curFile == null && shapeList.getList().Count>0)
            {
                curFile = dialogs.saveFileDialog();
            }

            if (curFile != null && curFile != "")
            {
                var res = serializer.serialize(shapeList);
                using (FileStream fs = new FileStream(curFile, FileMode.Create))
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    writer.Write(res);
                }
            }
        }

        public void saveAs(ShapeList.ShapeList shapeList)
        {
            curFile = dialogs.saveFileDialog();

            save(shapeList);
        }

        public void addPlugin()
        {
            var fileName = dialogs.openFileDialog();

            if (fileName != null)
            {
                ShapeFactory.Instance().addPlugins(fileName);
            }
        }

    }
}
