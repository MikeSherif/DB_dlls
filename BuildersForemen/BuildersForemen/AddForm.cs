using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace BuildersForemen
{
    public partial class AddForm : Form
    {
        private string oledb_attrs = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BCompany.mdb";
        private bool isBuilder = false;

        private void GetInfo()
        {
            itemsGrid.Rows.Clear();

            OleDbConnection connection = new OleDbConnection(oledb_attrs);
            connection.Open();

            string cmd = "select " +
                "адрес.код_адреса, улица.название, адрес.номер " +
                "from улица " +
                "inner join адрес " +
                "on улица.[код_улицы] = адрес.[код_улицы]";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            OleDbDataReader reader = command.ExecuteReader();
            int counter = 0;

            while (reader.Read())
            {
                itemsGrid.Rows.Add();
                itemsGrid.Rows[counter].Cells[0].Value = reader.GetInt32(0).ToString();
                itemsGrid.Rows[counter].Cells[1].Value = reader.GetString(1);
                itemsGrid.Rows[counter].Cells[2].Value = reader.GetString(2);

                counter++;
            }

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

            connection.Close();
        }

        public AddForm()
        {
            InitializeComponent();

            GetInfo();

            dateTimePicker1.MinDate = DateTime.Now.AddYears(-100);
            dateTimePicker1.MaxDate = DateTime.Now.AddYears(-18);            
            add_spec_b.Enabled = false;
            remove_spec_b.Enabled = false;
            specsGrid.Enabled = false;
            specsGrid2.Enabled = false;
            isBuilder = checkBox1.Checked;
        }

        public static bool ValidateName(string name)
        {
            string pattern = @"^[A-Za-zА-Яа-яЁё]+(-[A-Za-zА-Яа-яЁё]+)? [A-Za-zА-Яа-яЁё]+(-[A-Za-zА-Яа-яЁё]+)? [A-Za-zА-Яа-яЁё]+(-[A-Za-zА-Яа-яЁё]+)?$";

            if (Regex.IsMatch(name, pattern))
                return true;

            return false;
        }

        public static bool ValidateWorkTime(string name)
        {
            string pattern = @"^\d+(\.\d+)?$";

            if (Regex.IsMatch(name, pattern))
                return true;

            return false;
        }

        private bool IsSameWorker()
        {
            OleDbConnection connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BCompany.mdb");
            connection.Open();

            string cmd = "select count(*) from строитель where фио = @name and дата_рождения = @date";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@name", name_t.Text);
            command.Parameters.AddWithValue("@date", dateTimePicker1.Value.ToString("dd/MM/yyyy"));
            int count = (int)command.ExecuteScalar();

            cmd = "select count(*) from бригадир where ФИО = @name and дата_рождения = @date";
            command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@name", name_t.Text);
            command.Parameters.AddWithValue("@date", dateTimePicker1.Value.ToString("dd/MM/yyyy"));
            count += (int)command.ExecuteScalar();

            connection.Close();

            if (count > 0)
                return true;

            return false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            isBuilder = checkBox1.Checked;

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

        private void add_b_Click(object sender, EventArgs e)
        {            
            if (!ValidateName(name_t.Text) || !ValidateWorkTime(work_t.Text))
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
            else if (isBuilder && specsGrid2.Rows.Count == 0)
            {
                MessageBox.Show("Для строителя необходимо выбрать как минимум одну специализацию!");
                return;
            }

            OleDbConnection connection = new OleDbConnection(oledb_attrs);
            connection.Open();

            string cmd;
            OleDbCommand command;

            if (isBuilder)
            {
                cmd = "insert into строитель (фио, пол, дата_рождения, код_адреса, трудовой_стаж) values (@1, @2, @3, @4, @5)";
                command = new OleDbCommand(cmd, connection);
                command.Parameters.AddWithValue("@1", name_t.Text);
                command.Parameters.AddWithValue("@2", mfBox.Text);
                command.Parameters.AddWithValue("@3", dateTimePicker1.Value.ToString("dd/MM/yyyy"));
                command.Parameters.AddWithValue("@4", Convert.ToInt32(itemsGrid.SelectedRows[0].Cells[0].Value));
                command.Parameters.AddWithValue("@5", work_t.Text + " " + workTimeBox.Text);
                command.ExecuteNonQuery();

                command = new OleDbCommand("SELECT MAX(код_строителя) FROM строитель", connection);
                int workerID = (int)command.ExecuteScalar();

                cmd = "insert into специализации_строителей (код_строителя, код_специализации) values (@1, @2)";
                foreach (DataGridViewRow row in specsGrid2.Rows)
                {
                    command = new OleDbCommand(cmd, connection);
                    command.Parameters.AddWithValue("@1", workerID);
                    command.Parameters.AddWithValue("@2", Convert.ToInt32(row.Cells[0].Value));
                    command.ExecuteNonQuery();
                }
            }
            else
            {
                cmd = "insert into бригадир (ФИО, пол, дата_рождения, код_адреса, трудовой_стаж) values (@1, @2, @3, @4, @5)";
                command = new OleDbCommand(cmd, connection);
                command.Parameters.AddWithValue("@1", name_t.Text);
                command.Parameters.AddWithValue("@2", mfBox.Text);
                command.Parameters.AddWithValue("@3", dateTimePicker1.Value.ToString("dd/MM/yyyy"));
                command.Parameters.AddWithValue("@4", Convert.ToInt32(itemsGrid.SelectedRows[0].Cells[0].Value));
                command.Parameters.AddWithValue("@5", work_t.Text + " " + workTimeBox.Text);
                command.ExecuteNonQuery();
            }

            connection.Close();

            MessageBox.Show("Работник успешно добавлен!");
            this.Close();
        }
    }
}
