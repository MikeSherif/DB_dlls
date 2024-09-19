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

namespace BuildersForemen
{
    public partial class WorkersControl : UserControl
    {
        private string oledb_attrs = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BCompany.mdb";

        private void GetInfo()
        {
            itemsGrid.Rows.Clear();

            OleDbConnection connection = new OleDbConnection(oledb_attrs);
            connection.Open();

            string cmd = "select код_строителя, фио, пол, дата_рождения from строитель";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            OleDbDataReader reader = command.ExecuteReader();
            int counter = 0;

            while (reader.Read())
            {
                itemsGrid.Rows.Add();
                itemsGrid.Rows[counter].Cells[0].Value = reader.GetInt32(0).ToString();
                itemsGrid.Rows[counter].Cells[1].Value = reader.GetString(1);
                itemsGrid.Rows[counter].Cells[2].Value = reader.GetString(2);
                itemsGrid.Rows[counter].Cells[3].Value = reader.GetDateTime(3).ToString("dd/MM/yyyy");
                itemsGrid.Rows[counter].Cells[4].Value = "Строитель";

                counter++;
            }

            cmd = "select код_бригадира, ФИО, пол, дата_рождения from бригадир";
            command = new OleDbCommand(cmd, connection);
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                itemsGrid.Rows.Add();
                itemsGrid.Rows[counter].Cells[0].Value = reader.GetInt32(0).ToString();
                itemsGrid.Rows[counter].Cells[1].Value = reader.GetString(1);
                itemsGrid.Rows[counter].Cells[2].Value = reader.GetString(2);
                itemsGrid.Rows[counter].Cells[3].Value = reader.GetDateTime(3).ToString("dd/MM/yyyy");
                itemsGrid.Rows[counter].Cells[4].Value = "Бригадир";

                counter++;
            }

            connection.Close();
        }

        public WorkersControl(bool Read, bool Write, bool Edit, bool Delete)
        {
            InitializeComponent();

            GetInfo();

            info_b.Enabled = Read;
            add_b.Enabled = Write;
            edit_b.Enabled = Edit;
            delete_b.Enabled = Delete;
        }

        private void add_b_Click(object sender, EventArgs e)
        {
            AddForm form = new AddForm();

            form.ShowDialog();
            GetInfo();
        }

        private void edit_b_Click(object sender, EventArgs e)
        {
            if (itemsGrid.SelectedCells.Count == 0)
            {
                MessageBox.Show("Выберите запись!");
                return;
            }

            bool isBuilder = itemsGrid.SelectedRows[0].Cells[4].Value.Equals("Строитель");
            int workerID = Convert.ToInt32(itemsGrid.SelectedRows[0].Cells[0].Value);
            string name = itemsGrid.SelectedRows[0].Cells[1].Value.ToString();
            string mf = itemsGrid.SelectedRows[0].Cells[2].Value.ToString();
            string birth = itemsGrid.SelectedRows[0].Cells[3].Value.ToString();

            EditForm form = new EditForm(isBuilder, workerID, name, mf, birth);

            form.ShowDialog();
            GetInfo();
        }

        private void info_b_Click(object sender, EventArgs e)
        {
            if (itemsGrid.SelectedCells.Count == 0)
            {
                MessageBox.Show("Выберите запись!");
                return;
            }

            bool isBuilder = itemsGrid.SelectedRows[0].Cells[4].Value.Equals("Строитель");
            int workerID = Convert.ToInt32(itemsGrid.SelectedRows[0].Cells[0].Value);
            string name = itemsGrid.SelectedRows[0].Cells[1].Value.ToString();
            string mf = itemsGrid.SelectedRows[0].Cells[2].Value.ToString();
            string birth = itemsGrid.SelectedRows[0].Cells[3].Value.ToString();

            InfoForm form = new InfoForm(isBuilder, workerID, name, mf, birth);

            form.ShowDialog();
        }

        private void delete_b_Click(object sender, EventArgs e)
        {
            if (itemsGrid.SelectedCells.Count == 0)
            {
                MessageBox.Show("Выберите запись!");
                return;
            }

            DialogResult result = MessageBox.Show("Вы действительно хотите удалить эту запись?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No)
                return;

            OleDbConnection connection = new OleDbConnection(oledb_attrs);
            connection.Open();

            int count = 0;

            string cmd; 
            int id = Convert.ToInt32(itemsGrid.SelectedRows[0].Cells[0].Value);
            
            if (itemsGrid.SelectedRows[0].Cells[4].Value.Equals("Строитель"))
                cmd = "select count(*) from состав_бригады where код_строителя = @id";
            else
                cmd = "select count(*) from бригада where код_бригадира = @id";

            OleDbCommand command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", id);
            count += (int)command.ExecuteScalar();

            connection.Close();

            if (count > 0)
            {
                MessageBox.Show("Невозможно удалить работника, так как он используется в другой записи!");
                return;
            }

            connection.Open();

            if (itemsGrid.SelectedRows[0].Cells[4].Value.Equals("Строитель"))
            {
                cmd = "delete from специализации_строителей where код_строителя = @id";
                command = new OleDbCommand(cmd, connection);
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }

            if (itemsGrid.SelectedRows[0].Cells[4].Value.Equals("Строитель"))
                cmd = "delete from строитель where код_строителя = @id";
            else
                cmd = "delete from бригадир where код_бригадира = @id";

            command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();

            connection.Close();
            GetInfo();
        }
    }
}
