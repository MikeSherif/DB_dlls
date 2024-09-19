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

namespace StreetsAddresses
{
    public partial class EditAddressForm : Form
    {
        private string oledb_attrs = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BCompany.mdb";
        private int addressID;

        public EditAddressForm(int address_id, string street, string num)
        {
            InitializeComponent();
            addressID = address_id;

            OleDbConnection connection = new OleDbConnection(oledb_attrs);
            connection.Open();

            string cmd = "select название from улица";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            OleDbDataReader reader = command.ExecuteReader();

            int select_id = 0;
            while (reader.Read())
            {
                string streetName = reader.GetString(0);
                streetBox.Items.Add(streetName);
                
                if (streetName.Equals(street))
                    select_id = streetBox.Items.Count - 1;
            }

            streetBox.SelectedIndex = select_id;
            num_t.Text = num;

            connection.Close();
        }

        private void edit_b_Click(object sender, EventArgs e)
        {
            if (num_t.Text.Length == 0 || streetBox.Text.Length == 0)
            {
                MessageBox.Show("Введите все данные!");
                return;
            }
            else if (!AddAddressForm.ValidateNum(num_t.Text))
            {
                MessageBox.Show("Номер улицы может содержать только цифры, '/' или русские буквы!");
                return;
            }
            else if (AddAddressForm.IsSameAddress(streetBox.Text, num_t.Text))
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

            cmd = "update адрес set код_улицы = @id, номер = @num where код_адреса = @ad";
            command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@num", num_t.Text);
            command.Parameters.AddWithValue("@ad", addressID);
            command.ExecuteNonQuery();

            connection.Close();

            MessageBox.Show("Адрес успешно обновлен!");
            this.Close();
        }
    }
}
