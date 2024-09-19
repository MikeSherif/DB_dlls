using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Brigades
{
    public partial class InfoForm : Form
    {
        private string oledb_attrs = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BCompany.mdb";
        private int brigadeID;

        private void GetInfo()
        {
            OleDbConnection connection = new OleDbConnection(oledb_attrs);
            connection.Open();

            List<int> workers = new List<int>();
            string cmd = "select код_строителя from состав_бригады where код_бригады = @id";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", brigadeID);
            OleDbDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                workers.Add(reader.GetInt32(0));
            }

            cmd = "select строитель.код_строителя, строитель.фио, специализация.название " +
                "from строитель " +
                "inner join (специализация inner join специализации_строителей on специализация.[код_специализации] = специализации_строителей.[код_специализации]) " +
                "on строитель.[код_строителя] = специализации_строителей.[код_строителя]";
            command = new OleDbCommand(cmd, connection);
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                int workerId = reader.GetInt32(0);
                string workerName = reader.GetString(1);
                string specialization = reader.GetString(2);

                if (workers.Contains(workerId))
                {
                    bool found = false;
                    foreach (DataGridViewRow row in workersGrid.Rows)
                    {
                        if (row.Cells[0].Value != null && (int)row.Cells[0].Value == workerId)
                        {
                            row.Cells[2].Value += ", " + specialization;
                            found = true;
                            break;
                        }
                    }

                    if (!found)
                    {
                        int rowIndex = workersGrid.Rows.Add();
                        workersGrid.Rows[rowIndex].Cells[0].Value = workerId;
                        workersGrid.Rows[rowIndex].Cells[1].Value = workerName;
                        workersGrid.Rows[rowIndex].Cells[2].Value = specialization;
                    }
                }                
            }

            connection.Close();
        }

        public InfoForm(int brigade_id, string name, string foreman)
        {
            InitializeComponent();

            brigadeID = brigade_id;
            name_t.Text = name;
            brig_t.Text = foreman;
            GetInfo();
        }
    }
}
