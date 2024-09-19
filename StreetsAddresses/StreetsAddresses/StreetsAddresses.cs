using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StreetsAddresses
{
    public class StreetsAddresses
    {
        public UserControl GetAddresses(int id, bool Read, bool Write, bool Edit, bool Delete)
        {
            StrAdControl ad = new StrAdControl(Read, Write, Edit, Delete);

            return ad;
        }
    }
}
