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
    public partial class EditWork : Form
    {
        private string oledb_attrs = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BCompany.mdb";
        private int objectID, workID;

        public EditWork(int object_id, int work_id, string work_name)
        {
            InitializeComponent();

            objectID = object_id;
            workID = work_id;
            name_t.Text = work_name;
        }

        private bool isSameWork()
        {
            OleDbConnection connection = new OleDbConnection(oledb_attrs);
            connection.Open();

            string cmd = "select count(*) from перечень_работ where (наименование = @name and код_объекта = @id) and код_работы <> @id2";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@name", name_t.Text);
            command.Parameters.AddWithValue("@id", objectID);
            command.Parameters.AddWithValue("@id2", workID);
            int count = (int)command.ExecuteScalar();

            connection.Close();

            if (count > 0)
                return true;

            return false;
        }

        private void edit_b_Click(object sender, EventArgs e)
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

            string cmd = "update перечень_работ set наименование = @name where код_работы = @id";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@name", name_t.Text);
            command.Parameters.AddWithValue("@id", workID);
            command.ExecuteNonQuery();

            connection.Close();

            MessageBox.Show("Работа успешно обновлена!");
            this.Close();
        }
    }
}
