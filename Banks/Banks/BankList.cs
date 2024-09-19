using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Banks
{
    public class BankList
    {
        public UserControl GetBanks(int id, bool Read, bool Write, bool Edit, bool Delete)
        {
            BankControl bank = new BankControl(Read, Write, Edit, Delete);

            return bank;
        }
    }
}
