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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BuildersForemen
{
    public partial class EditForm : Form
    {
        private string oledb_attrs = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BCompany.mdb";
        private bool wasBuilder = false;
        private int workerID;

        private void GetInfo()
        {
            itemsGrid.Rows.Clear();

            OleDbConnection connection = new OleDbConnection(oledb_attrs);
            connection.Open();

            string cmd;
            cmd = wasBuilder ? "select код_адреса from строитель where код_строителя = @id" : "select код_адреса from бригадир where код_бригадира = @id";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", workerID);
            int addressID = (int)command.ExecuteScalar();

            cmd = "select " +
                "адрес.код_адреса, улица.название, адрес.номер " +
                "from улица " +
                "inner join адрес " +
                "on улица.[код_улицы] = адрес.[код_улицы]";
            command = new OleDbCommand(cmd, connection);
            OleDbDataReader reader = command.ExecuteReader();
            int counter = 0;
            int select_id = 0;

            while (reader.Read())
            {
                itemsGrid.Rows.Add();
                itemsGrid.Rows[counter].Cells[0].Value = reader.GetInt32(0).ToString();
                itemsGrid.Rows[counter].Cells[1].Value = reader.GetString(1);
                itemsGrid.Rows[counter].Cells[2].Value = reader.GetString(2);                
                select_id = Convert.ToInt32(itemsGrid.Rows[counter].Cells[0].Value) == addressID ? counter : select_id;

                counter++;
            }

            if (itemsGrid.Rows.Count > 0)
            {
                itemsGrid.CurrentCell = itemsGrid.Rows[select_id].Cells[0];
            }            
            
            cmd = wasBuilder ? "select трудовой_стаж from строитель where код_строителя = @id" : "select трудовой_стаж from бригадир where код_бригадира = @id";
            command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", workerID);
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                string[] parts = reader.GetString(0).Split(new char[] { ' ' }, 2);

                work_t.Text = parts[0];

                int id = workTimeBox.Items.IndexOf(parts[1]);
                workTimeBox.SelectedIndex = id;
            }

            if (!wasBuilder)
            {
                cmd = "select код_специализации, название from специализация";
                command = new OleDbCommand(cmd, connection);
                reader = command.ExecuteReader();
                counter = 0;

                while (reader.Read())
                {
                    specsGrid.Rows.Add();
                    specsGrid.Rows[counter].Cells[0].Value = reader.GetInt32(0).ToString();
                    specsGrid.Rows[counter].Cells[1].Value = reader.GetString(1);

                    counter++;
                }
            }
            else
            {
                List<int> specs = new List<int>();
                cmd = "select код_специализации from специализации_строителей where код_строителя = @id";
                command = new OleDbCommand(cmd, connection);
                command.Parameters.AddWithValue("@id", workerID);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    specs.Add(reader.GetInt32(0));
                }

                cmd = "select код_специализации, название from специализация";
                command = new OleDbCommand(cmd, connection);
                reader = command.ExecuteReader();
                counter = 0;
                int counter2 = 0;

                while (reader.Read())
                {
                    int specID = reader.GetInt32(0);

                    if (specs.Contains(specID))
                    {
                        specsGrid2.Rows.Add();
                        specsGrid2.Rows[counter2].Cells[0].Value = specID;
                        specsGrid2.Rows[counter2].Cells[1].Value = reader.GetString(1);
                        counter2++;
                    }
                    else
                    {
                        specsGrid.Rows.Add();
                        specsGrid.Rows[counter].Cells[0].Value = specID;
                        specsGrid.Rows[counter].Cells[1].Value = reader.GetString(1);
                        counter++;
                    }
                }
            }            

            connection.Close();
        }

        public EditForm(bool is_builder, int worker_id, string name, string mf, string birth)
        {
            InitializeComponent();

            wasBuilder = is_builder;
            checkBox1.Checked = is_builder;
            workerID = worker_id;
            add_spec_b.Enabled = is_builder;
            remove_spec_b.Enabled = is_builder;
            specsGrid.Enabled = is_builder;
            specsGrid2.Enabled = is_builder;

            name_t.Text = name;
            int index = mfBox.Items.IndexOf(mf);
            mfBox.SelectedIndex = index;

            if (DateTime.TryParseExact(birth, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
            {
                dateTimePicker1.Value = parsedDate;
            }

            GetInfo();

            dateTimePicker1.MinDate = DateTime.Now.AddYears(-100);
            dateTimePicker1.MaxDate = DateTime.Now.AddYears(-18);
        }

        private bool IsSameWorker()
        {
            OleDbConnection connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BCompany.mdb");
            connection.Open();

            string cmd;
            int count = 0;
            OleDbCommand command;
            if (wasBuilder)
            {
                cmd = "select count(*) from строитель where фио = @name and дата_рождения = @date and код_строителя <> @id";
                command = new OleDbCommand(cmd, connection);
                command.Parameters.AddWithValue("@name", name_t.Text);
                command.Parameters.AddWithValue("@date", dateTimePicker1.Value.ToString("dd/MM/yyyy"));
                command.Parameters.AddWithValue("@id", workerID);                
                count = (int)command.ExecuteScalar();

                cmd = "select count(*) from бригадир where ФИО = @name and дата_рождения = @date";
                command = new OleDbCommand(cmd, connection);
                command.Parameters.AddWithValue("@name", name_t.Text);
                command.Parameters.AddWithValue("@date", dateTimePicker1.Value.ToString("dd/MM/yyyy"));
                count += (int)command.ExecuteScalar();
            }
            else if (!wasBuilder)
            {
                cmd = "select count(*) from строитель where фио = @name and дата_рождения = @date";
                command = new OleDbCommand(cmd, connection);
                command.Parameters.AddWithValue("@name", name_t.Text);
                command.Parameters.AddWithValue("@date", dateTimePicker1.Value.ToString("dd/MM/yyyy"));
                count = (int)command.ExecuteScalar();

                cmd = "select count(*) from бригадир where ФИО = @name and дата_рождения = @date and код_бригадира <> @id";
                command = new OleDbCommand(cmd, connection);
                command.Parameters.AddWithValue("@name", name_t.Text);
                command.Parameters.AddWithValue("@date", dateTimePicker1.Value.ToString("dd/MM/yyyy"));
                command.Parameters.AddWithValue("@id", workerID);
                count += (int)command.ExecuteScalar();
            }                       

            connection.Close();

            if (count > 0)
                return true;

            return false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                add_spec_b.Enabled = true;
                remove_spec_b.Enabled = true;
                specsGrid.Enabled = true;
                specsGrid2.Enabled = true;
            }
            else
            {
                add_spec_b.Enabled = false;
                remove_spec_b.Enabled = false;
                specsGrid.Enabled = false;
                specsGrid2.Enabled = false;
            }
        }

        private void add_spec_b_Click(object sender, EventArgs e)
        {
            if (specsGrid.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = specsGrid.SelectedRows[0];

                int newRowIndex = specsGrid2.Rows.Add();
                DataGridViewRow newRow = specsGrid2.Rows[newRowIndex];

                foreach (DataGridViewCell cell in selectedRow.Cells)
                {
                    newRow.Cells[cell.ColumnIndex].Value = cell.Value;
                }

                specsGrid.Rows.Remove(selectedRow);
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите доступную специализацию!");
            }
        }

        private void remove_spec_b_Click(object sender, EventArgs e)
        {
            if (specsGrid2.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = specsGrid2.SelectedRows[0];

                int newRowIndex = specsGrid.Rows.Add();
                DataGridViewRow newRow = specsGrid.Rows[newRowIndex];

                foreach (DataGridViewCell cell in selectedRow.Cells)
                {
                    newRow.Cells[cell.ColumnIndex].Value = cell.Value;
                }

                specsGrid2.Rows.Remove(selectedRow);
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите специализацию рабочего (правая таблица)!");
            }
        }

        private void edit_b_Click(object sender, EventArgs e)
        {
            if (!AddForm.ValidateName(name_t.Text) || !AddForm.ValidateWorkTime(work_t.Text))
            {
                MessageBox.Show("Неправильное ФИО или трудовой стаж!");
                return;
            }
            else if (name_t.Text.Length == 0 || mfBox.Text.Length == 0 || itemsGrid.SelectedRows.Count == 0 || work_t.Text.Length == 0 || workTimeBox.Text.Length == 0)
            {
                MessageBox.Show("Введите всю информацию!");
                return;
            }
            else if (IsSameWorker())
            {
                MessageBox.Show("Работник с такими данными уже занесен в базу данных!");
                return;
            }
            else if (checkBox1.Checked && specsGrid2.Rows.Count == 0)
            {
                MessageBox.Show("Для строителя необходимо выбрать как минимум одну специализацию!");
                return;
            }

            OleDbConnection connection = new OleDbConnection(oledb_attrs);
            connection.Open();

            string cmd;
            OleDbCommand command;
            if (wasBuilder && checkBox1.Checked)
            {
                cmd = "update строитель set фио = @1, пол = @2, дата_рождения = @3, код_адреса = @4, трудовой_стаж = @5 where код_строителя = @id";
                command = new OleDbCommand(cmd, connection);
                command.Parameters.AddWithValue("@1", name_t.Text);
                command.Parameters.AddWithValue("@2", mfBox.Text);
                command.Parameters.AddWithValue("@3", dateTimePicker1.Value.ToString("dd/MM/yyyy"));
                command.Parameters.AddWithValue("@4", Convert.ToInt32(itemsGrid.SelectedRows[0].Cells[0].Value));
                command.Parameters.AddWithValue("@5", work_t.Text + " " + workTimeBox.Text);
                command.Parameters.AddWithValue("@id", workerID);
                command.ExecuteNonQuery();

                cmd = "delete from специализации_строителей where код_строителя = @id";
                command = new OleDbCommand(cmd, connection);
                command.Parameters.AddWithValue("@id", workerID);
                command.ExecuteNonQuery();

                cmd = "insert into специализации_строителей (код_строителя, код_специализации) values (@1, @2)";
                foreach (DataGridViewRow row in specsGrid2.Rows)
                {
                    command = new OleDbCommand(cmd, connection);
                    command.Parameters.AddWithValue("@1", workerID);
                    command.Parameters.AddWithValue("@2", Convert.ToInt32(row.Cells[0].Value));
                    command.ExecuteNonQuery();
                }
            }
            else if (wasBuilder && !checkBox1.Checked)
            {
                cmd = "delete from специализации_строителей where код_строителя = @id";
                command = new OleDbCommand(cmd, connection);
                command.Parameters.AddWithValue("@id", workerID);
                command.ExecuteNonQuery();

                cmd = "delete from строитель where код_строителя = @id";
                command = new OleDbCommand(cmd, connection);
                command.Parameters.AddWithValue("@id", workerID);
                command.ExecuteNonQuery();
               
                cmd = "insert into бригадир (ФИО, пол, дата_рождения, код_адреса, трудовой_стаж) values (@1, @2, @3, @4, @5)";
                command = new OleDbCommand(cmd, connection);
                command.Parameters.AddWithValue("@1", name_t.Text);
                command.Parameters.AddWithValue("@2", mfBox.Text);
                command.Parameters.AddWithValue("@3", dateTimePicker1.Value.ToString("dd/MM/yyyy"));
                command.Parameters.AddWithValue("@4", Convert.ToInt32(itemsGrid.SelectedRows[0].Cells[0].Value));
                command.Parameters.AddWithValue("@5", work_t.Text + " " + workTimeBox.Text);
                command.ExecuteNonQuery();
            }
            else if (!wasBuilder && checkBox1.Checked)
            {
                cmd = "delete from бригадир where код_бригадира = @id";
                command = new OleDbCommand(cmd, connection);
                command.Parameters.AddWithValue("@id", workerID);
                command.ExecuteNonQuery();

                cmd = "insert into строитель (фио, пол, дата_рождения, код_адреса, трудовой_стаж) values (@1, @2, @3, @4, @5)";
                command = new OleDbCommand(cmd, connection);
                command.Parameters.AddWithValue("@1", name_t.Text);
                command.Parameters.AddWithValue("@2", mfBox.Text);
                command.Parameters.AddWithValue("@3", dateTimePicker1.Value.ToString("dd/MM/yyyy"));
                command.Parameters.AddWithValue("@4", Convert.ToInt32(itemsGrid.SelectedRows[0].Cells[0].Value));
                command.Parameters.AddWithValue("@5", work_t.Text + " " + workTimeBox.Text);
                command.ExecuteNonQuery();

                command = new OleDbCommand("SELECT MAX(код_строителя) FROM строитель", connection);
                int newID = (int)command.ExecuteScalar();

                cmd = "insert into специализации_строителей (код_строителя, код_специализации) values (@1, @2)";
                foreach (DataGridViewRow row in specsGrid2.Rows)
                {
                    command = new OleDbCommand(cmd, connection);
                    command.Parameters.AddWithValue("@1", newID);
                    command.Parameters.AddWithValue("@2", Convert.ToInt32(row.Cells[0].Value));
                    command.ExecuteNonQuery();
                }
            }
            else
            {
                cmd = "update бригадир set ФИО = @1, пол = @2, дата_рождения = @3, код_адреса = @4, трудовой_стаж = @5 where код_бригадира = @id";
                command = new OleDbCommand(cmd, connection);
                command.Parameters.AddWithValue("@1", name_t.Text);
                command.Parameters.AddWithValue("@2", mfBox.Text);
                command.Parameters.AddWithValue("@3", dateTimePicker1.Value.ToString("dd/MM/yyyy"));
                command.Parameters.AddWithValue("@4", Convert.ToInt32(itemsGrid.SelectedRows[0].Cells[0].Value));
                command.Parameters.AddWithValue("@5", work_t.Text + " " + workTimeBox.Text);
                command.Parameters.AddWithValue("@id", workerID);
                command.ExecuteNonQuery();
            }

            connection.Close();

            MessageBox.Show("Информация успешно изменена!");
            this.Close();
        }
    }
}
