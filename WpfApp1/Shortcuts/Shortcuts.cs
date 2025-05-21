using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApp1.Shortcuts
{
    public class Shortcuts
    {

        private Files.Files filesController;

        public Shortcuts()
        {
            filesController = new Files.Files();
        }

        public void Open()
        {
            filesController.open();
        }
        public void Save()
        {
            filesController.save(Draw.shapeList);
        }

        public void SaveAs()
        {
            filesController.saveAs(Draw.shapeList);
        }

        public void Undo()
        {
            Draw.shapeList.undo();
        }

        public void Redo()
        {
            Draw.shapeList.redo();
        }
        public void NewFile()
        {

        }

        public void addPlugin()
        {
            filesController.addPlugin();
        }
    }
}
