using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;

namespace WpfApp1.UI
{
    class Dialogs
    {
      
        public String? openFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();


            if (openFileDialog.ShowDialog()==true)
            {
                return openFileDialog.FileName;
            }
            return null;
        }


        public String? saveFileDialog()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();


            if (saveFileDialog.ShowDialog() == true)
            {
                return saveFileDialog.FileName;
            }
            return null;
        }

        public void showMessage(String message)
        {
            MessageBox.Show(message);
        }

    }
}
