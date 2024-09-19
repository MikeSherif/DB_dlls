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
    public partial class BrigadeControl : UserControl
    {
        private string oledb_attrs = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BCompany.mdb";

        private void GetInfo()
        {
            itemsGrid.Rows.Clear();

            OleDbConnection connection = new OleDbConnection(oledb_attrs);
            connection.Open();

            string cmd = "select " +
                "бригада.код_бригады, бригада.название, бригадир.ФИО, count(состав_бригады.код_строителя) as Количество_строителей " +
                "FROM (бригадир inner join бригада on бригадир.код_бригадира = бригада.код_бригадира) " +
                "inner join состав_бригады on бригада.код_бригады = состав_бригады.код_бригады " +
                "group by бригада.код_бригады, бригада.название, бригадир.ФИО;";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            OleDbDataReader reader = command.ExecuteReader();
            int counter = 0;

            while (reader.Read())
            {
                itemsGrid.Rows.Add();
                itemsGrid.Rows[counter].Cells[0].Value = reader.GetInt32(0).ToString();
                itemsGrid.Rows[counter].Cells[1].Value = reader.GetString(1);
                itemsGrid.Rows[counter].Cells[2].Value = reader.GetString(2);
                itemsGrid.Rows[counter].Cells[3].Value = reader.GetInt32(3).ToString();

                counter++;
            }

            connection.Close();
        }

        public BrigadeControl(bool Read, bool Write, bool Edit, bool Delete)
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

            int brigadeID = Convert.ToInt32(itemsGrid.SelectedRows[0].Cells[0].Value);
            string name = itemsGrid.SelectedRows[0].Cells[1].Value.ToString();
            EditForm form = new EditForm(brigadeID, name);

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

            int brigadeID = Convert.ToInt32(itemsGrid.SelectedRows[0].Cells[0].Value);
            string name = itemsGrid.SelectedRows[0].Cells[1].Value.ToString();
            string foreman = itemsGrid.SelectedRows[0].Cells[2].Value.ToString();

            InfoForm form = new InfoForm(brigadeID, name, foreman);

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

            cmd = "select count(*) from выполненные_работы where код_бригады = @id";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", id);
            count += (int)command.ExecuteScalar();

            connection.Close();

            if (count > 0)
            {
                MessageBox.Show("Невозможно удалить бригаду, так как она используется в другой записи!");
                return;
            }

            connection.Open();

            cmd = "delete from состав_бригады where код_бригады = @id";
            command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();

            cmd = "delete from бригада where код_бригады = @id";
            command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();

            connection.Close();
            GetInfo();
        }
    }
}
