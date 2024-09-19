using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangePassword
{
    internal class ChangePass
    {
        public void ChangePassword(int id, bool Read, bool Write, bool Edit, bool Delete)
        {
            Form1 form = new Form1(id);

            form.ShowDialog();
        }
    }
}
