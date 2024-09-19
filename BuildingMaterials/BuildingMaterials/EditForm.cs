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

namespace BuildingMaterials
{
    public partial class EditForm : Form
    {
        private string oledb_attrs = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BCompany.mdb";
        private int materialID;

        public EditForm(int material_id, string old_name)
        {
            InitializeComponent();

            materialID = material_id;
            name_t.Text = old_name;
        }

        private void edit_b_Click(object sender, EventArgs e)
        {
            if (name_t.Text.Length == 0)
            {
                MessageBox.Show("Введите название материала!");
                return;
            }
            else if (!AddForm.ValidateName(name_t.Text))
            {
                MessageBox.Show("Название может содержать только буквы, цифры, одинарные пробелы или дефисы!");
                return;
            }
            else if (AddForm.IsSameMaterial(name_t.Text))
            {
                MessageBox.Show("Материал с таким названием уже есть!");
                return;
            }

            OleDbConnection connection = new OleDbConnection(oledb_attrs);
            connection.Open();

            string cmd = "update стройматериал set название = @name where код_материала = @id";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@name", name_t.Text);
            command.Parameters.AddWithValue("@id", materialID);
            command.ExecuteNonQuery();

            connection.Close();

            MessageBox.Show("Стройматериал успешно обновлен!");
            this.Close();
        }
    }
}
