using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UnitsOfMeasure
{
    public class UnitsOfMeasure
    {
        public UserControl GetUnits(int id, bool Read, bool Write, bool Edit, bool Delete)
        {
            UnitsControl uc = new UnitsControl(Read, Write, Edit, Delete);

            return uc;
        }
    }
}
