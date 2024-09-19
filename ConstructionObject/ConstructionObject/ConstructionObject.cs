using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConstructionObject
{
    public class ConstructionObject
    {
        public UserControl GetObjects(int id, bool Read, bool Write, bool Edit, bool Delete)
        {
            ObjectsControl objectsControl = new ObjectsControl(Read, Write, Edit, Delete);

            return objectsControl;
        }
    }
}
