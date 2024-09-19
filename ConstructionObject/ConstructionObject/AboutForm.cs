using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConstructionObject
{
    public partial class AboutForm : Form
    {
        private string oledb_attrs = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BCompany.mdb";
        private int objectID, workID, work2ID;
        private string workName;

        private void GetInfo()
        {
            requestGrid.Rows.Clear();

            OleDbConnection connection = new OleDbConnection(oledb_attrs);
            connection.Open();

            string cmd = "SELECT заявка.код_заявки, стройматериал.название AS стройматериал_название, единица_измерения.название AS единица_измерения_название, партия_материала.количество, поставщик.фио_рук, заявка.дата\r\nFROM (объект_строительства INNER JOIN (поставщик INNER JOIN ((единица_измерения INNER JOIN (заявка INNER JOIN (партия_материала INNER JOIN перечень_в_заявках ON партия_материала.[код_партии] = перечень_в_заявках.[код_партии]) ON заявка.[код_заявки] = перечень_в_заявках.[код_заявки]) ON единица_измерения.[код_единицы] = партия_материала.[код_единицы]) INNER JOIN (стройматериал INNER JOIN склад ON стройматериал.[код_материала] = склад.[код_материала]) ON (склад.код_на_складе = партия_материала.код_на_складе) AND (единица_измерения.[код_единицы] = склад.[код_единицы])) ON поставщик.[код_поставщика] = склад.[код_поставщика]) ON объект_строительства.код_объекта = заявка.код_объекта) INNER JOIN перечень_работ ON (перечень_работ.код_работы = заявка.код_работы) AND (объект_строительства.код_объекта = перечень_работ.код_объекта)\r\nWHERE (((заявка.код_объекта)=@id) AND ((перечень_работ.код_работы)=@id2));\r\n";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", objectID);
            command.Parameters.AddWithValue("@id2", work2ID);
            OleDbDataReader reader = command.ExecuteReader();
            int counter = 0;

            while (reader.Read())
            {
                requestGrid.Rows.Add();
                requestGrid.Rows[counter].Cells[0].Value = reader.GetInt32(0).ToString();
                requestGrid.Rows[counter].Cells[1].Value = reader.GetString(1);
                requestGrid.Rows[counter].Cells[2].Value = reader.GetString(2);
                requestGrid.Rows[counter].Cells[3].Value = reader.GetString(3);
                requestGrid.Rows[counter].Cells[4].Value = reader.GetString(4);
                requestGrid.Rows[counter].Cells[5].Value = reader.GetDateTime(5).ToString("dd/MM/yyyy");

                counter++;
            }

            cmd = "select код_бригады from выполненные_работы where код_выполненных = @id";
            command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", workID);
            int brigadeID = (int)command.ExecuteScalar();

            cmd = "select " +
                "бригада.код_бригадира, бригадир.ФИО " +
                "from (бригадир " +
                "inner join бригада " +
                "on бригадир.[код_бригадира] = бригада.[код_бригадира]) " +
                "inner join выполненные_работы " +
                "on бригада.[код_бригады] = выполненные_работы.[код_бригады] " +
                "where выполненные_работы.код_бригады = @id";
            command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", brigadeID);
            reader = command.ExecuteReader();
            reader.Read();

            int brigID = reader.GetInt32(0);
            string brigName = reader.GetString(1);

            workersGrid.Rows.Add();
            workersGrid.Rows[0].Cells[0].Value = brigID;
            workersGrid.Rows[0].Cells[1].Value = brigName;
            workersGrid.Rows[0].Cells[2].Value = "Бригадир";

            List<int> workers = new List<int>();
            cmd = "select код_строителя from состав_бригады where код_бригады = @id";
            command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", brigadeID);
            reader = command.ExecuteReader();

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

            name_t.Text = workName;
            cmd = "select название from бригада where код_бригады = @id";
            command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", brigadeID);
            reader = command.ExecuteReader();
            reader.Read();

            label2.Text += reader.GetString(0);

            cmd = "select дата, описание_работ from выполненные_работы where код_выполненных = @id";
            command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", workID);
            reader = command.ExecuteReader();
            reader.Read();

            date_t.Text = reader.GetDateTime(0).ToString("dd/MM/yyyy");
            about_t.Text += reader.GetString(1);

            connection.Close();
        }

        public AboutForm(int object_id, int work_id, string name, int work2ID)
        {
            InitializeComponent();

            name_t.Enabled = false;
            date_t.Enabled = false;
            about_t.ReadOnly = true;
            objectID = object_id;
            workID = work_id;
            workName = name;
            this.work2ID = work2ID;
            GetInfo();
        }
    }
}
