using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeOpenXml;

namespace Banks
{
    public partial class BankControl : UserControl
    {
        private string oledb_attrs = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BCompany.mdb";

        private void GetInfo()
        {
            itemsGrid.Rows.Clear();

            OleDbConnection connection = new OleDbConnection(oledb_attrs);
            connection.Open();

            string cmd = "select * from банк";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            OleDbDataReader reader = command.ExecuteReader();
            int counter = 0;

            while (reader.Read())
            {
                itemsGrid.Rows.Add();
                itemsGrid.Rows[counter].Cells[0].Value = reader.GetInt32(0).ToString();
                itemsGrid.Rows[counter].Cells[1].Value = reader.GetString(1);

                counter++;
            }

            connection.Close();
        }

        public BankControl(bool Read, bool Write, bool Edit, bool Delete)
        {
            InitializeComponent();

            GetInfo();
            
            add_b.Enabled = Write;
            edit_b.Enabled = Edit;
            delete_b.Enabled = Delete;
        }

        private void save_b_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Excel Files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                sfd.FilterIndex = 1;
                sfd.RestoreDirectory = true;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (var package = new ExcelPackage())
                    {
                        ExcelWorksheet sheet = package.Workbook.Worksheets.Add("Sheet1");

                        for (int i = 1; i <= itemsGrid.Columns.Count; i++)
                        {
                            sheet.Cells[1, i].Value = itemsGrid.Columns[i - 1].HeaderText;
                            for (int j = 1; j <= itemsGrid.Rows.Count; j++)
                            {
                                sheet.Cells[j + 1, i].Value = itemsGrid.Rows[j - 1].Cells[i - 1].Value;
                            }
                        }

                        sheet.Cells.AutoFitColumns();

                        FileInfo file = new FileInfo(sfd.FileName);
                        package.SaveAs(file);
                    }

                    MessageBox.Show("Сохранение прошло успешно", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось сохранить файл: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void add_b_Click(object sender, EventArgs e)
        {
            AddForm form = new AddForm();
            
            form.ShowDialog();
            GetInfo();
        }

        private void edit_b_Click(object sender, EventArgs e)
        {
            if (itemsGrid.SelectedCells.Count == 0)
            {
                MessageBox.Show("Выберите банк!");
                return;
            }

            EditForm form = new EditForm(Convert.ToInt32(itemsGrid.SelectedRows[0].Cells[0].Value), itemsGrid.SelectedRows[0].Cells[1].Value.ToString());
            
            form.ShowDialog();
            GetInfo();
        }

        private void delete_b_Click(object sender, EventArgs e)
        {
            if (itemsGrid.SelectedCells.Count == 0)
            {
                MessageBox.Show("Выберите банк!");
                return;
            }

            DialogResult result = MessageBox.Show("Вы действительно хотите удалить эту запись?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No)
                return;

            OleDbConnection connection = new OleDbConnection(oledb_attrs);
            connection.Open();

            string cmd = "select count(*) from поставщик where код_банка = @id";
            int id = Convert.ToInt32(itemsGrid.SelectedRows[0].Cells[0].Value);
            OleDbCommand command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", id);
            int count = (int)command.ExecuteScalar();

            connection.Close();

            if (count > 0)
            {
                MessageBox.Show("Невозможно удалить банк, так как он используется в другой записи!");
                return;
            }

            connection.Open();

            cmd = "delete from банк where код_банка = @id";
            command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();

            connection.Close();
            GetInfo();
        }
    }
}
