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

namespace Specializations
{
    public partial class AddForm : Form
    {
        private string oledb_attrs = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BCompany.mdb";

        public AddForm()
        {
            InitializeComponent();
        }

        public static bool IsSameSpec(string name)
        {
            OleDbConnection connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BCompany.mdb");
            connection.Open();

            string cmd = "select count(*) from специализация where название = @name";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@name", name);
            int count = (int)command.ExecuteScalar();

            connection.Close();

            if (count > 0)
                return true;

            return false;
        }

        private void add_b_Click(object sender, EventArgs e)
        {
            if (name_t.Text.Length == 0)
            {
                MessageBox.Show("Введите название специализации!");
                return;
            }
            else if (name_t.Text.Contains("  ") || name_t.Text.StartsWith(" ") || name_t.Text.EndsWith(" "))
            {
                MessageBox.Show("Название не может содержать двойной пробел, начинаться с него или заканчиваться им!");
                return;
            }
            else if (IsSameSpec(name_t.Text))
            {
                MessageBox.Show("Специализация с таким названием уже есть!");
                return;
            }

            OleDbConnection connection = new OleDbConnection(oledb_attrs);
            connection.Open();

            string cmd = "insert into специализация (название) values (@name)";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@name", name_t.Text);
            command.ExecuteNonQuery();

            connection.Close();

            MessageBox.Show("Специализация успешно добавлена!");
            this.Close();
        }
    }
}
