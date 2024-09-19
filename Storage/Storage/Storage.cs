using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Storage
{
    public class Storage
    {
        public UserControl GetStorage(int id, bool Read, bool Write, bool Edit, bool Delete)
        {
            StorageControl storageControl = new StorageControl(Read, Write, Edit, Delete);

            return storageControl;
        }
    }
}
