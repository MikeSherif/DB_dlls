using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkersList
{
    public class WorkersList
    {
        public UserControl GetWorkersList(int id, bool Read, bool Write, bool Edit, bool Delete)
        {
            WorkersControl smeta = new WorkersControl(Read, Write, Edit, Delete);

            return smeta;
        }
    }
}
