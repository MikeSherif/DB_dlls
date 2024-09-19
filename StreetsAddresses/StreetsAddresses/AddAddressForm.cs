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

namespace StreetsAddresses
{
    public partial class AddAddressForm : Form
    {
        private string oledb_attrs = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BCompany.mdb";

        public AddAddressForm()
        {
            InitializeComponent();

            OleDbConnection connection = new OleDbConnection(oledb_attrs);
            connection.Open();

            string cmd = "select название from улица";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            OleDbDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                streetBox.Items.Add(reader.GetString(0));
            }

            connection.Close();
        }

        public static bool ValidateNum(string num)
        {
            string pattern = @"^\d+(\/\d+)?[А-Яа-я]?$";

            if (Regex.IsMatch(num, pattern))
                return true;

            return false;
        }

        public static bool IsSameAddress(string street, string num) 
        {
            OleDbConnection connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BCompany.mdb");
            connection.Open();

            string cmd = "select " +
                "count(*)" +
                "from улица " +
                "inner join адрес " +
                "on улица.[код_улицы] = адрес.[код_улицы] " +
                "where улица.[название] = @name and адрес.[номер] = @num";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@name", street);
            command.Parameters.AddWithValue("@num", num);
            int count = (int)command.ExecuteScalar();

            connection.Close();

            if (count > 0)
                return true;

            return false;
        }

        private void add_b_Click(object sender, EventArgs e)
        {
            if (num_t.Text.Length == 0 || streetBox.Text.Length == 0)
            {
                MessageBox.Show("Введите все данные!");
                return;
            }
            else if (!ValidateNum(num_t.Text))
            {
                MessageBox.Show("Номер улицы может содержать только цифры, '/' или русские буквы!");
                return;
            }
            else if (IsSameAddress(streetBox.Text, num_t.Text))
            {
                MessageBox.Show("Такой адрес уже есть!");
                return;
            }

            OleDbConnection connection = new OleDbConnection(oledb_attrs);
            connection.Open();

            string cmd = "select код_улицы from улица where название = @name";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@name", streetBox.Text);
            int id = (int)command.ExecuteScalar();

            cmd = "insert into адрес (код_улицы, номер) values (@id, @num)";
            command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@num", num_t.Text);
            command.ExecuteNonQuery();

            connection.Close();

            MessageBox.Show("Адрес успешно добавлен!");
            this.Close();
        }
    }
}
