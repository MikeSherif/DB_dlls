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
    public partial class AccessForm : UserControl
    {
        private string oledb_attrs = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BCompany.mdb";
        private int userID;

        private void Getinfo()
        {
            OleDbConnection connection = new OleDbConnection(oledb_attrs);
            connection.Open();

            string cmd = "select код_пользователя, логин from пользователь where код_пользователя <> @id";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", userID);
            OleDbDataReader reader = command.ExecuteReader();
            int counter = 0;

            while (reader.Read())
            {
                itemsGrid.Rows.Add();
                itemsGrid.Rows[counter].Cells[0].Value = reader.GetInt32(0);
                itemsGrid.Rows[counter].Cells[1].Value = reader.GetString(1);
                itemsGrid.Rows[counter].Cells[2].Value = itemsGrid.Rows[counter].Cells[1].Value.Equals("root") ? "Администратор" : "Обычный пользователь";

                counter++;
            }

            connection.Close();
        }

        public AccessForm(int id, bool Read, bool Write, bool Edit, bool Delete)
        {
            InitializeComponent();

            edit_b.Enabled = Write || Edit || Delete;
            userID = id;
            Getinfo();
        }

        private void edit_b_Click(object sender, EventArgs e)
        {
            if (itemsGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите пользователя!");
                return;
            }
            else if (itemsGrid.SelectedRows[0].Cells[1].Value.ToString().Equals("Администратор"))
            {
                MessageBox.Show("У вас нет прав для редактирования прав этого пользователя!");
                return;
            }

            SetAccessForm form = new SetAccessForm(Convert.ToInt32(itemsGrid.SelectedRows[0].Cells[0].Value));

            form.ShowDialog();
        }
    }
}
