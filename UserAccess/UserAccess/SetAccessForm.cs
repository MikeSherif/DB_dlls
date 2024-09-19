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

namespace UserAccess
{
    public partial class SetAccessForm : Form
    {
        private string oledb_attrs = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BCompany.mdb";
        private int userID;

        private void GetInfo()
        {
            OleDbConnection connection = new OleDbConnection(oledb_attrs);
            connection.Open();

            string cmd = "select название from меню";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            OleDbDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                menuComboBox.Items.Add(reader.GetString(0));
            }

            connection.Close();
        }

        public SetAccessForm(int userID)
        {
            InitializeComponent();
            this.userID = userID;

            GetInfo();
        }

        private void menuComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (menuComboBox.Text.Length == 0)
                return;

            OleDbConnection connection = new OleDbConnection(oledb_attrs);
            connection.Open();

            string cmd = "select код_пункта from меню where название = @name";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@name", menuComboBox.Text);
            int menuID = (int)command.ExecuteScalar();

            cmd = "select R, W, E, D from права_пользователя where код_пользователя = @id and код_пункта = @id2";
            command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", userID);
            command.Parameters.AddWithValue("@id2", menuID);
            OleDbDataReader reader = command.ExecuteReader();

            reader.Read();
            R_b.Checked = reader.GetBoolean(0);
            W_b.Checked = reader.GetBoolean(1);
            E_b.Checked = reader.GetBoolean(2);
            D_b.Checked = reader.GetBoolean(3);

            connection.Close();
        }

        private void edit_b_Click(object sender, EventArgs e)
        {
            OleDbConnection connection = new OleDbConnection(oledb_attrs);
            connection.Open();

            string cmd = "select код_пункта from меню where название = @name";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@name", menuComboBox.Text);
            int menuID = (int)command.ExecuteScalar();

            cmd = "update права_пользователя set R = @r, W = @w, E = @e, D =@d where код_пользователя = @id and код_пункта = @id2";
            command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@r", R_b.Checked);
            command.Parameters.AddWithValue("@w", W_b.Checked);
            command.Parameters.AddWithValue("@e", E_b.Checked);
            command.Parameters.AddWithValue("@d", D_b.Checked);
            command.Parameters.AddWithValue("@id", userID);
            command.Parameters.AddWithValue("@id2", menuID);
            command.ExecuteNonQuery();

            connection.Close();

            MessageBox.Show("Права успешно изменены!");
        }
    }
}
