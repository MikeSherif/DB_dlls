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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace Suppliers
{
    public partial class SuppliersControl : UserControl
    {
        private string oledb_attrs = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BCompany.mdb";

        private void GetInfo()
        {
            itemsGrid.Rows.Clear();

            OleDbConnection connection = new OleDbConnection(oledb_attrs);
            connection.Open();

            string cmd = "select " +
                "поставщик.код_поставщика, поставщик.фио_рук, улица.название, адрес.номер, поставщик.телефон " +
                "from (улица inner join адрес on улица.[код_улицы] = адрес.[код_улицы]) " +
                "inner join поставщик on адрес.[код_адреса] = поставщик.[код_адреса]";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            OleDbDataReader reader = command.ExecuteReader();
            int counter = 0;

            while (reader.Read())
            {
                itemsGrid.Rows.Add();
                itemsGrid.Rows[counter].Cells[0].Value = reader.GetInt32(0).ToString();
                itemsGrid.Rows[counter].Cells[1].Value = reader.GetString(1);
                itemsGrid.Rows[counter].Cells[2].Value = reader.GetString(2) + " " + reader.GetString(3);
                itemsGrid.Rows[counter].Cells[3].Value = reader.GetString(4);

                counter++;
            }

            connection.Close();
        }

        public SuppliersControl(bool Read, bool Write, bool Edit, bool Delete)
        {
            InitializeComponent();

            info_b.Enabled = Read;
            add_b.Enabled = Write;
            edit_b.Enabled = Edit;
            delete_b.Enabled = Delete;

            GetInfo();
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

            int supplierID = Convert.ToInt32(itemsGrid.SelectedRows[0].Cells[0].Value);
            EditForm form = new EditForm(supplierID);

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

            int supplierID = Convert.ToInt32(itemsGrid.SelectedRows[0].Cells[0].Value);
            InfoForm form = new InfoForm(supplierID);

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
            int supplierID = Convert.ToInt32(itemsGrid.SelectedRows[0].Cells[0].Value);
            int count = 0;

            string cmd = "select count(*) from поставка where код_поставщика = @id";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", supplierID);
            count = (int)command.ExecuteScalar();

            cmd = "select count(*) from склад where код_поставщика = @id";
            command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", supplierID);
            count += (int)command.ExecuteScalar();

            connection.Close();

            if (count > 0)
            {
                MessageBox.Show("Невозможно удалить поставщика, так как он используется в другой записи!");
                return;
            }

            connection.Open();            

            cmd = "delete from поставщик where код_поставщика = @id";
            command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", supplierID);
            command.ExecuteNonQuery();

            connection.Close();
            GetInfo();
        }
    }
}
