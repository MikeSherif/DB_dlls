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

namespace ConstructionObject
{
    public partial class AddWork : Form
    {
        private string oledb_attrs = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BCompany.mdb";
        private int objectID;

        public AddWork(int object_id)
        {
            InitializeComponent();
            objectID = object_id;
        }

        private bool isSameWork()
        {
            OleDbConnection connection = new OleDbConnection(oledb_attrs);
            connection.Open();

            string cmd = "select count(*) from перечень_работ where наименование = @name and код_объекта = @id";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@name", name_t.Text);
            command.Parameters.AddWithValue("@id", objectID);
            int count = (int)command.ExecuteScalar();

            connection.Close();

            if (count > 0)
                return true;

            return false;
        }

        private void add_b_Click(object sender, EventArgs e)
        {
            if (name_t.Text.Length == 0 || name_t.Text.StartsWith(" ") || name_t.Text.EndsWith(" "))
            {
                MessageBox.Show("Введите корректное наименование (проверьте наличие лишних пробелов)!");
                return;
            }
            else if (isSameWork())
            {
                MessageBox.Show("Работа с таким наименованием уже закреплена за объектом!");
                return;
            }

            OleDbConnection connection = new OleDbConnection(oledb_attrs);
            connection.Open();

            string cmd = "insert into перечень_работ (код_объекта, наименование) values (@id, @name)";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", objectID);
            command.Parameters.AddWithValue("@name", name_t.Text);
            command.ExecuteNonQuery();

            connection.Close();

            MessageBox.Show("Работа успешно добавлена в список!");
            this.Close();
        }
    }
}
