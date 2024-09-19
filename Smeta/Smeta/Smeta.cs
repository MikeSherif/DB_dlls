using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Smeta
{
    public class Smeta
    {
        public UserControl GetSmeta(int id, bool Read, bool Write, bool Edit, bool Delete)
        {
            SmetaControl smeta = new SmetaControl(Read, Write, Edit, Delete);

            return smeta;
        }
    }
}
