using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Brigades
{
    public class Brigades
    {
        public UserControl GetBrigades(int id, bool Read, bool Write, bool Edit, bool Delete)
        {
            BrigadeControl brigadeControl = new BrigadeControl(Read, Write, Edit, Delete);

            return brigadeControl;
        }
    }
}
