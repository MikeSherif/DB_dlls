using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserAccess
{
    public class UserAccess
    {
        public UserControl GetAccess(int id, bool Read, bool Write, bool Edit, bool Delete)
        {
            AccessForm form = new AccessForm(id, Read, Write, Edit, Delete);

            return form;
        }
    }
}
