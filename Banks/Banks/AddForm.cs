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

namespace Banks
{
    public partial class AddForm : Form
    {
        private string oledb_attrs = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BCompany.mdb";

        public AddForm()
        {
            InitializeComponent();
        }

        public static bool ValidateName(string name)
        {
            string pattern = @"^[a-zA-ZА-Яа-я0-9]+( [a-zA-ZА-Яа-я0-9]+)*$";
            
            if (Regex.IsMatch(name, pattern))
                return true;
            
            return false;
        }

        public static bool IsSameBank(string name)
        {
            OleDbConnection connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BCompany.mdb");
            connection.Open();

            string cmd = "select count(*) from банк where название = @name";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@name", name);
            int count = (int)command.ExecuteScalar();

            connection.Close();

            if (count > 0)
                return true;

            return false;
        }

        private void add_bank_b_Click(object sender, EventArgs e)
        {
            if (name_t.Text.Length == 0)
            {
                MessageBox.Show("Введите название банка!");
                return;
            }
            else if (!ValidateName(name_t.Text))
            {
                MessageBox.Show("Название может содержать только буквы, цифры и одинарные пробелы!");
                return;
            }
            else if (IsSameBank(name_t.Text))
            {
                MessageBox.Show("Банк с таким названием уже есть!");
                return;
            }

            OleDbConnection connection = new OleDbConnection(oledb_attrs);
            connection.Open();

            string cmd = "insert into банк (название) values (@name)";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@name", name_t.Text);
            command.ExecuteNonQuery();

            connection.Close();

            MessageBox.Show("Банк успешно добавлен!");
            this.Close();
        }
    }
}
