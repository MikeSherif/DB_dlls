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

namespace Suppliers
{
    public partial class EditForm : Form
    {
        private string oledb_attrs = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BCompany.mdb";
        private int supplierID;

        private void GetInfo()
        {
            addressesGrid.Rows.Clear();
            bankGrid.Rows.Clear();

            OleDbConnection connection = new OleDbConnection(oledb_attrs);
            connection.Open();

            string cmd = "select код_адреса, фио_рук, телефон, код_банка, расчетный_счет, инн from поставщик where код_поставщика = @id";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", supplierID);
            OleDbDataReader reader = command.ExecuteReader();
            reader.Read();

            int addressID = reader.GetInt32(0);
            name_t.Text = reader.GetString(1);
            tel_t.Text = reader.GetString(2);
            int bankID = reader.GetInt32(3);
            payment_t.Text = reader.GetString(4);
            inn_t.Text = reader.GetString(5);

            cmd = "select " +
                "адрес.код_адреса, улица.название, адрес.номер " +
                "from улица " +
                "inner join адрес " +
                "on улица.[код_улицы] = адрес.[код_улицы]";
            command = new OleDbCommand(cmd, connection);
            reader = command.ExecuteReader();
            int counter = 0;
            int select_id = 0;

            while (reader.Read())
            {
                addressesGrid.Rows.Add();
                addressesGrid.Rows[counter].Cells[0].Value = reader.GetInt32(0).ToString();
                addressesGrid.Rows[counter].Cells[1].Value = reader.GetString(1);
                addressesGrid.Rows[counter].Cells[2].Value = reader.GetString(2);
                select_id = Convert.ToInt32(addressesGrid.Rows[counter].Cells[0].Value) == addressID ? counter : select_id;

                counter++;
            }

            if (addressesGrid.Rows.Count > 0)
            {
                addressesGrid.CurrentCell = addressesGrid.Rows[select_id].Cells[0];
            }

            cmd = "select код_банка, название from банк";
            command = new OleDbCommand(cmd, connection);
            reader = command.ExecuteReader();
            counter = 0;

            while (reader.Read())
            {
                bankGrid.Rows.Add();
                bankGrid.Rows[counter].Cells[0].Value = reader.GetInt32(0).ToString();
                bankGrid.Rows[counter].Cells[1].Value = reader.GetString(1);
                select_id = Convert.ToInt32(bankGrid.Rows[counter].Cells[0].Value) == bankID ? counter : select_id;

                counter++;
            }

            if (bankGrid.Rows.Count > 0)
            {
                bankGrid.CurrentCell = bankGrid.Rows[select_id].Cells[0];
            }

            connection.Close();
        }

        public EditForm(int supplier_id)
        {
            InitializeComponent();
            supplierID = supplier_id;

            GetInfo();
        }

        private bool IsSameSupplier()
        {
            OleDbConnection connection = new OleDbConnection(oledb_attrs);
            connection.Open();

            string cmd = "select count(*) from поставщик where (расчетный_счет = @1 or инн = @2 or фио_рук = @3 or телефон = @4) and код_поставщика <> @id";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@1", payment_t.Text);
            command.Parameters.AddWithValue("@2", inn_t.Text);
            command.Parameters.AddWithValue("@3", name_t.Text);
            command.Parameters.AddWithValue("@4", tel_t.Text);
            command.Parameters.AddWithValue("@id", supplierID);
            int count = (int)command.ExecuteScalar();

            connection.Close();

            if (count > 0)
                return true;

            return false;
        }

        private void edit_b_Click(object sender, EventArgs e)
        {
            if (name_t.Text.Length == 0 || tel_t.Text.Length == 0 || inn_t.Text.Length == 0 || payment_t.Text.Length == 0 || bankGrid.SelectedRows.Count == 0 || addressesGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Введите все данные!");
                return;
            }
            else if (!AddForm.ValidateName(name_t.Text))
            {
                MessageBox.Show("ФИО введено некорректно!");
                return;
            }
            else if (!AddForm.ValidateTel(tel_t.Text))
            {
                MessageBox.Show("Телефон введен некорректно (должен начинаться с +7 без каких-либо других спец. символов)!");
                return;
            }
            else if (!AddForm.ValidateINN(inn_t.Text))
            {
                MessageBox.Show("ИНН введен неверно!");
                return;
            }
            else if (!AddForm.ValidatePaymentACC(payment_t.Text))
            {
                MessageBox.Show("Расчетный счет введен неверно!");
                return;
            }
            else if (IsSameSupplier())
            {
                MessageBox.Show("Поставщик с такими данными уже внесен в базу данных!");
                return;
            }

            OleDbConnection connection = new OleDbConnection(oledb_attrs);
            connection.Open();

            string cmd = "update поставщик set код_адреса = @1, фио_рук = @2, телефон = @3, код_банка = @4, расчетный_счет = @5, инн = @6 where код_поставщика = @id";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@1", Convert.ToInt32(addressesGrid.SelectedRows[0].Cells[0].Value));
            command.Parameters.AddWithValue("@2", name_t.Text);
            command.Parameters.AddWithValue("@3", tel_t.Text);
            command.Parameters.AddWithValue("@4", Convert.ToInt32(bankGrid.SelectedRows[0].Cells[0].Value));
            command.Parameters.AddWithValue("@5", payment_t.Text);
            command.Parameters.AddWithValue("@6", inn_t.Text);
            command.Parameters.AddWithValue("@id", supplierID);
            command.ExecuteNonQuery();

            connection.Close();

            MessageBox.Show("Поставщик успешно обновлен!");
            this.Close();
        }
    }
}
