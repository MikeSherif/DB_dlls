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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace StreetsAddresses
{
    public partial class EditStreetForm : Form
    {
        private string oledb_attrs = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BCompany.mdb";
        private int streetID;

        public EditStreetForm(int street_id, string old_name)
        {
            InitializeComponent();

            streetID = street_id;
            name_t.Text = old_name;
        }

        private void edit_b_Click(object sender, EventArgs e)
        {
            if (name_t.Text.Length == 0)
            {
                MessageBox.Show("Введите название улицы!");
                return;
            }
            else if (!AddStreetForm.ValidateName(name_t.Text))
            {
                MessageBox.Show("Название может содержать только буквы, цифры и одинарные пробелы!");
                return;
            }
            else if (AddStreetForm.IsSameStreet(name_t.Text))
            {
                MessageBox.Show("Улица с таким названием уже есть!");
                return;
            }

            OleDbConnection connection = new OleDbConnection(oledb_attrs);
            connection.Open();

            string cmd = "update улица set название = @name where код_улицы = @id";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@name", name_t.Text);
            command.Parameters.AddWithValue("@id", streetID);
            command.ExecuteNonQuery();

            connection.Close();

            MessageBox.Show("Улица успешно обновлена!");
            this.Close();
        }
    }
}
