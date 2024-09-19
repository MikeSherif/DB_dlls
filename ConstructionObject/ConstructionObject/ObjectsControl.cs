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
    public partial class ObjectsControl : UserControl
    {
        private string oledb_attrs = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BCompany.mdb";
        private bool R, W, E, D;

        private void GetInfo()
        {
            itemsGrid.Rows.Clear();

            OleDbConnection connection = new OleDbConnection(oledb_attrs);
            connection.Open();

            string cmd = "select " +
                "объект_строительства.код_объекта, объект_строительства.название as объект_строительства_название, " +
                "улица.название as улица_название, адрес.номер " +
                "from (улица inner join адрес on улица.[код_улицы] = адрес.[код_улицы]) " +
                "inner join объект_строительства on адрес.[код_адреса] = объект_строительства.[код_адреса]";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            OleDbDataReader reader = command.ExecuteReader();
            int counter = 0;

            while (reader.Read())
            {
                itemsGrid.Rows.Add();
                itemsGrid.Rows[counter].Cells[0].Value = reader.GetInt32(0).ToString();
                itemsGrid.Rows[counter].Cells[1].Value = reader.GetString(1);
                itemsGrid.Rows[counter].Cells[2].Value = reader.GetString(2) + " " + reader.GetString(3);

                counter++;
            }

            connection.Close();
        }

        public ObjectsControl(bool Read, bool Write, bool Edit, bool Delete)
        {
            InitializeComponent();

            GetInfo();

            info_b.Enabled = Read;
            add_b.Enabled = Write;
            edit_b.Enabled = Edit;
            delete_b.Enabled = Delete;

            R = Read;
            W = Write;
            E = Edit;
            D = Delete;
        }

        private void add_b_Click(object sender, EventArgs e)
        {
            AddForm form = new AddForm();

            form.ShowDialog();
            GetInfo();
        }

        private void edit_b_Click(object sender, EventArgs e)
        {
            if (itemsGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите запись!");
                return;
            }

            int objectID = Convert.ToInt32(itemsGrid.SelectedRows[0].Cells[0].Value.ToString());
            string name = itemsGrid.SelectedRows[0].Cells[1].Value.ToString();
            EditForm form = new EditForm(objectID, name);

            form.ShowDialog();
        }

        private void info_b_Click(object sender, EventArgs e)
        {
            if (itemsGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите запись!");
                return;
            }

            int objectID = Convert.ToInt32(itemsGrid.SelectedRows[0].Cells[0].Value.ToString());
            InfoForm form = new InfoForm(objectID, R, W, E, D);

            form.ShowDialog();
        }

        private void delete_b_Click(object sender, EventArgs e)
        {
            if (itemsGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите запись!");
                return;
            }

            DialogResult result = MessageBox.Show("Вы действительно хотите удалить эту запись?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No)
                return;

            OleDbConnection connection = new OleDbConnection(oledb_attrs);
            connection.Open();

            int count = 0;

            string cmd;
            int id = Convert.ToInt32(itemsGrid.SelectedRows[0].Cells[0].Value);            

            cmd = "select count(*) from перечень_работ where код_объекта = @id";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", id);
            count += (int)command.ExecuteScalar();

            cmd = "select count(*) from заявка where код_объекта = @id";
            command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", id);
            count += (int)command.ExecuteScalar();

            connection.Close();

            if (count > 0)
            {
                MessageBox.Show("Невозможно удалить объект, так как он используется в другой записи!");
                return;
            }

            connection.Open();
             
            cmd = "select count(*) from смета where код_объекта = @id";
            command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", id);
            bool isDocument = (int)command.ExecuteScalar() > 0;

            if (isDocument)
            {
                cmd = "delete from смета where код_объекта = @id";
                command = new OleDbCommand(cmd, connection);
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }

            cmd = "delete from объект_строительства where код_объекта = @id";
            command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();

            connection.Close();
            GetInfo();
        }
    }
}
