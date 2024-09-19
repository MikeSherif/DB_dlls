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
    public partial class EditForm : Form
    {
        private string oledb_attrs = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BCompany.mdb";
        private int objectID;

        private void GetInfo()
        {
            addressGrid.Rows.Clear();

            OleDbConnection connection = new OleDbConnection(oledb_attrs);
            connection.Open();

            string cmd = "select код_адреса from объект_строительства where код_объекта = @id";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", objectID);
            OleDbDataReader reader = command.ExecuteReader();
            reader.Read();

            int addressID = reader.GetInt32(0);

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
                addressGrid.Rows.Add();
                addressGrid.Rows[counter].Cells[0].Value = reader.GetInt32(0);
                addressGrid.Rows[counter].Cells[1].Value = reader.GetString(1);
                addressGrid.Rows[counter].Cells[2].Value = reader.GetString(2);
                select_id = Convert.ToInt32(addressGrid.Rows[counter].Cells[0].Value) == addressID ? counter : select_id;

                counter++;
            }

            if (addressGrid.Rows.Count > 0)
            {
                addressGrid.CurrentCell = addressGrid.Rows[select_id].Cells[0];
            }

            connection.Close();
        }

        public EditForm(int object_id, string name)
        {
            InitializeComponent();

            name_t.Text = name;
            objectID = object_id;
            GetInfo();
        }

        private bool IsSameObj()
        {
            OleDbConnection connection = new OleDbConnection(oledb_attrs);
            connection.Open();

            string cmd = "select count(*) from объект_строительства where (название = @name and код_адреса = @id) and код_объекта <> @id2";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@name", name_t.Text);
            command.Parameters.AddWithValue("@id", addressGrid.SelectedRows[0].Cells[0].Value);
            command.Parameters.AddWithValue("@id2", objectID);
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
                MessageBox.Show("Введите корректное название объекта!");
                return;
            }
            else if (addressGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите адрес!");
                return;
            }
            else if (IsSameObj())
            {
                MessageBox.Show("Объект с таким названием и адресом уже занесен в базу данных!");
                return;
            }

            OleDbConnection connection = new OleDbConnection(oledb_attrs);
            connection.Open();

            string cmd = "update объект_строительства set название = @name, код_адреса = @id where код_объекта = @id2";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@name", name_t.Text);
            command.Parameters.AddWithValue("@id", Convert.ToInt32(addressGrid.SelectedRows[0].Cells[0].Value));
            command.Parameters.AddWithValue("@id2", objectID);
            command.ExecuteNonQuery();

            connection.Close();

            MessageBox.Show("Объект успешно обновлен!");
            this.Close();
        }
    }
}
