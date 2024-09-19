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

namespace UnitsOfMeasure
{
    public partial class AddForm : Form
    {
        private string oledb_attrs = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BCompany.mdb";

        public AddForm()
        {
            InitializeComponent();
        }

        public static bool IsSameUnit(string name)
        {
            OleDbConnection connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BCompany.mdb");
            connection.Open();

            string cmd = "select count(*) from единица_измерения where название = @name";
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
                MessageBox.Show("Введите название единицы измерения!");
                return;
            }
            else if (name_t.Text.Contains("  ") || name_t.Text.StartsWith(" ") || name_t.Text.EndsWith(" "))
            {
                MessageBox.Show("Название не может содержать двойных пробелов, начинаться с него или заканчиваться на него!");
                return;
            }
            else if (IsSameUnit(name_t.Text))
            {
                MessageBox.Show("Единица измерения с таким названием уже есть!");
                return;
            }

            OleDbConnection connection = new OleDbConnection(oledb_attrs);
            connection.Open();

            string cmd = "insert into единица_измерения (название) values (@name)";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@name", name_t.Text);
            command.ExecuteNonQuery();

            connection.Close();

            MessageBox.Show("Единица измерения успешно добавлена!");
            this.Close();
        }
    }
}
