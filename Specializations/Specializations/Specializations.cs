using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Specializations
{
    public class Specializations
    {
        public UserControl GetSpecs(int id, bool Read, bool Write, bool Edit, bool Delete)
        {
            SpecsControl specs = new SpecsControl(Read, Write, Edit, Delete);

            return specs;
        }
    }
}
