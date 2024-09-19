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

namespace Banks
{
    public partial class EditForm : Form
    {
        private string oledb_attrs = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BCompany.mdb";
        private int bankID;

        public EditForm(int bank_id, string old_name)
        {
            InitializeComponent();

            bankID = bank_id;
            name_t.Text = old_name;
        }

        private void edit_bank_b_Click(object sender, EventArgs e)
        {
            if (name_t.Text.Length == 0)
            {
                MessageBox.Show("Введите название банка!");
                return;
            }
            else if (!AddForm.ValidateName(name_t.Text))
            {
                MessageBox.Show("Название может содержать только буквы, цифры и одинарные пробелы!");
                return;
            }
            else if (AddForm.IsSameBank(name_t.Text))
            {
                MessageBox.Show("Банк с таким названием уже есть!");
                return;
            }

            OleDbConnection connection = new OleDbConnection(oledb_attrs);
            connection.Open();

            string cmd = "update банк set название = @name where код_банка = @id";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@name", name_t.Text);
            command.Parameters.AddWithValue("@id", bankID);
            command.ExecuteNonQuery();

            connection.Close();

            MessageBox.Show("Банк успешно обновлен!");
            this.Close();
        }
    }
}
