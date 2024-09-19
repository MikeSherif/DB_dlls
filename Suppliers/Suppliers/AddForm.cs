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

namespace Suppliers
{
    public partial class AddForm : Form
    {
        private string oledb_attrs = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BCompany.mdb";

        private void GetInfo()
        {
            addressesGrid.Rows.Clear();
            bankGrid.Rows.Clear();

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
                addressesGrid.Rows.Add();
                addressesGrid.Rows[counter].Cells[0].Value = reader.GetInt32(0).ToString();
                addressesGrid.Rows[counter].Cells[1].Value = reader.GetString(1);
                addressesGrid.Rows[counter].Cells[2].Value = reader.GetString(2);

                counter++;
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

                counter++;
            }

            connection.Close();
        }

        public AddForm()
        {
            InitializeComponent();

            GetInfo();
        }

        public static bool ValidateName(string name)
        {
            string pattern = @"^[A-Za-zА-Яа-яЁё]+(-[A-Za-zА-Яа-яЁё]+)? [A-Za-zА-Яа-яЁё]+(-[A-Za-zА-Яа-яЁё]+)? [A-Za-zА-Яа-яЁё]+(-[A-Za-zА-Яа-яЁё]+)?$";

            if (Regex.IsMatch(name, pattern))
                return true;

            return false;
        }

        public static bool ValidateTel(string name)
        {
            string pattern = @"^\+7\d{10}$";

            if (Regex.IsMatch(name, pattern))
                return true;

            return false;
        }

        public static bool ValidateINN(string name)
        {
            string pattern = @"^\d{12}$";

            if (Regex.IsMatch(name, pattern))
                return true;

            return false;
        }

        public static bool ValidatePaymentACC(string name)
        {
            string pattern = @"^\d{20}$";

            if (Regex.IsMatch(name, pattern))
                return true;

            return false;
        }

        private bool IsSameSupplier()
        {
            OleDbConnection connection = new OleDbConnection(oledb_attrs);
            connection.Open();

            string cmd = "select count(*) from поставщик where расчетный_счет = @1 or инн = @2 or фио_рук = @3 or телефон = @4";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@1", payment_t.Text);
            command.Parameters.AddWithValue("@2", inn_t.Text);
            command.Parameters.AddWithValue("@3", name_t.Text);
            command.Parameters.AddWithValue("@4", tel_t.Text);
            int count = (int)command.ExecuteScalar();   

            connection.Close();

            if (count > 0)           
                return true;            

            return false;
        }

        private void add_b_Click(object sender, EventArgs e)
        {
            if (name_t.Text.Length == 0 || tel_t.Text.Length == 0 || inn_t.Text.Length == 0 || payment_t.Text.Length == 0 || bankGrid.SelectedRows.Count == 0 || addressesGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Введите все данные!");
                return;
            }
            else if (!ValidateName(name_t.Text))
            {
                MessageBox.Show("ФИО введено некорректно!");
                return;
            }
            else if (!ValidateTel(tel_t.Text))
            {
                MessageBox.Show("Телефон введен некорректно (должен начинаться с +7 без каких-либо других спец. символов)!");
                return;
            }
            else if (!ValidateINN(inn_t.Text))
            {
                MessageBox.Show("ИНН введен неверно!");
                return;
            }
            else if (!ValidatePaymentACC(payment_t.Text))
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

            string cmd = "insert into поставщик (код_адреса, фио_рук, телефон, код_банка, расчетный_счет, инн) values (@1, @2, @3, @4, @5, @6)";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@1", Convert.ToInt32(addressesGrid.SelectedRows[0].Cells[0].Value));
            command.Parameters.AddWithValue("@2", name_t.Text);
            command.Parameters.AddWithValue("@3", tel_t.Text);
            command.Parameters.AddWithValue("@4", Convert.ToInt32(bankGrid.SelectedRows[0].Cells[0].Value));
            command.Parameters.AddWithValue("@5", payment_t.Text);
            command.Parameters.AddWithValue("@6", inn_t.Text);
            command.ExecuteNonQuery();

            connection.Close();

            MessageBox.Show("Поставщик успешно добавлен!");
            this.Close();
        }
    }
}
