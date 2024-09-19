using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BuildingMaterials
{
    public class BuildingMaterials
    {
        public UserControl GetMaterials(int id, bool Read, bool Write, bool Edit, bool Delete)
        {
            MaterialsControl materialsControl = new MaterialsControl(Read, Write, Edit, Delete);

            return materialsControl;
        }
    }
}
