using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BuildersForemen
{
    public class BuildersForemen
    {
        public UserControl GetWorkers(int id, bool Read, bool Write, bool Edit, bool Delete)
        {
            WorkersControl workers = new WorkersControl(Read, Write, Edit, Delete);

            return workers;
        }
    }
}
