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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace UnitsOfMeasure
{
    public partial class EditForm : Form
    {
        private string oledb_attrs = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BCompany.mdb";
        private int unitID;

        public EditForm(int unit_id, string old_name)
        {
            InitializeComponent();

            unitID = unit_id;
            name_t.Text = old_name;
        }

        private void edit_b_Click(object sender, EventArgs e)
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
            else if (AddForm.IsSameUnit(name_t.Text))
            {
                MessageBox.Show("Единица измерения с таким названием уже есть!");
                return;
            }

            OleDbConnection connection = new OleDbConnection(oledb_attrs);
            connection.Open();

            string cmd = "update единица_измерения set название = @name where код_единицы = @id";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@name", name_t.Text);
            command.Parameters.AddWithValue("@id", unitID);
            command.ExecuteNonQuery();

            connection.Close();

            MessageBox.Show("Единица измерения успешно обновлена!");
            this.Close();
        }
    }
}
