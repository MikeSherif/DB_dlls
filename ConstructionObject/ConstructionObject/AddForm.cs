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
    public partial class AddForm : Form
    {
        private string oledb_attrs = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BCompany.mdb";

        private void GetInfo()
        {
            addressGrid.Rows.Clear();

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
                addressGrid.Rows.Add();
                addressGrid.Rows[counter].Cells[0].Value = reader.GetInt32(0);
                addressGrid.Rows[counter].Cells[1].Value = reader.GetString(1);
                addressGrid.Rows[counter].Cells[2].Value = reader.GetString(2);

                counter++;
            }

            connection.Close();
        }

        public AddForm()
        {
            InitializeComponent();

            GetInfo();
        }

        private bool IsSameObj()
        {
            OleDbConnection connection = new OleDbConnection(oledb_attrs);
            connection.Open();

            string cmd = "select count(*) from объект_строительства where название = @name and код_адреса = @id";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@name", name_t.Text);
            command.Parameters.AddWithValue("@id", addressGrid.SelectedRows[0].Cells[0].Value);
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

            string cmd = "insert into объект_строительства (название, код_адреса) values (@name, @id)";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@name", name_t.Text);
            command.Parameters.AddWithValue("@id", Convert.ToInt32(addressGrid.SelectedRows[0].Cells[0].Value));
            command.ExecuteNonQuery();

            connection.Close();

            MessageBox.Show("Объект успешно добавлен!");
            this.Close();
        }
    }
}
