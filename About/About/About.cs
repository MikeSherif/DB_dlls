using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace About
{
    public class About
    {
        public void GetAbout(int id, bool Read, bool Write, bool Edit, bool Delete)
        {
            string helpFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "HelpDB.chm");

            if (File.Exists(helpFilePath))
            {
                Help.ShowHelp(null, helpFilePath, HelpNavigator.Topic, "o_prilozhenii.htm");
            }
            else
            {
                MessageBox.Show("Файл справки не найден.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
