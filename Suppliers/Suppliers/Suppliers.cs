using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Suppliers
{
    public class Suppliers
    {
        public UserControl GetSuppliers(int id, bool Read, bool Write, bool Edit, bool Delete)
        {
            SuppliersControl suppliersControl = new SuppliersControl(Read, Write, Edit, Delete);

            return suppliersControl;
        }
    }
}
