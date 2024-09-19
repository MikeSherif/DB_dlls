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

namespace StreetsAddresses
{
    public partial class StrAdControl : UserControl
    {
        private string oledb_attrs = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BCompany.mdb";

        private void GetInfo()
        {
            itemsGrid.Rows.Clear();
            itemsGrid2.Rows.Clear();

            OleDbConnection connection = new OleDbConnection(oledb_attrs);
            connection.Open();

            string cmd = "select * from улица";
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

            cmd = "select " +
                "адрес.код_адреса, улица.название, адрес.номер " +
                "from улица " +
                "inner join адрес " +
                "on улица.[код_улицы] = адрес.[код_улицы]";
            command = new OleDbCommand(cmd, connection);
            reader = command.ExecuteReader();
            counter = 0;

            while (reader.Read())
            {
                itemsGrid2.Rows.Add();
                itemsGrid2.Rows[counter].Cells[0].Value = reader.GetInt32(0).ToString();
                itemsGrid2.Rows[counter].Cells[1].Value = reader.GetString(1);
                itemsGrid2.Rows[counter].Cells[2].Value = reader.GetString(2);

                counter++;
            }

            connection.Close();
        }

        public StrAdControl(bool Read, bool Write, bool Edit, bool Delete)
        {
            InitializeComponent();

            add_address_b.Enabled = Write;
            add_street_b.Enabled = Write;
            edit_address_b.Enabled = Edit;
            edit_street_b.Enabled = Edit;
            delete_address_b.Enabled = Delete;
            delete_street_b.Enabled = Delete;
            GetInfo();
        }

        private void save_street_b_Click(object sender, EventArgs e)
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

        private void save_address_b_Click(object sender, EventArgs e)
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

                        for (int i = 1; i <= itemsGrid2.Columns.Count; i++)
                        {
                            sheet.Cells[1, i].Value = itemsGrid2.Columns[i - 1].HeaderText;
                            for (int j = 1; j <= itemsGrid2.Rows.Count; j++)
                            {
                                sheet.Cells[j + 1, i].Value = itemsGrid2.Rows[j - 1].Cells[i - 1].Value;
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

        private void add_street_b_Click(object sender, EventArgs e)
        {
            AddStreetForm form = new AddStreetForm();

            form.ShowDialog();
            GetInfo();
        }

        private void edit_street_b_Click(object sender, EventArgs e)
        {
            if (itemsGrid.SelectedCells.Count == 0)
            {
                MessageBox.Show("Выберите улицу!");
                return;
            }

            EditStreetForm form = new EditStreetForm(Convert.ToInt32(itemsGrid.SelectedRows[0].Cells[0].Value), itemsGrid.SelectedRows[0].Cells[1].Value.ToString());

            form.ShowDialog();
            GetInfo();
        }

        private void delete_street_b_Click(object sender, EventArgs e)
        {
            if (itemsGrid.SelectedCells.Count == 0)
            {
                MessageBox.Show("Выберите улицу!");
                return;
            }

            DialogResult result = MessageBox.Show("Вы действительно хотите удалить эту запись?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No)
                return;

            OleDbConnection connection = new OleDbConnection(oledb_attrs);
            connection.Open();

            string cmd = "select count(*) from адрес where код_улицы = @id";
            int id = Convert.ToInt32(itemsGrid.SelectedRows[0].Cells[0].Value);
            OleDbCommand command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", id);
            int count = (int)command.ExecuteScalar();

            connection.Close();

            if (count > 0)
            {
                MessageBox.Show("Невозможно удалить улицу, так как она используется в другой записи!");
                return;
            }

            connection.Open();

            cmd = "delete from улица where код_улицы = @id";
            command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();

            connection.Close();
            GetInfo();
        }

        private void add_address_b_Click(object sender, EventArgs e)
        {
            AddAddressForm form = new AddAddressForm();

            form.ShowDialog();
            GetInfo();
        }

        private void edit_address_b_Click(object sender, EventArgs e)
        {
            if (itemsGrid2.SelectedCells.Count == 0)
            {
                MessageBox.Show("Выберите адрес!");
                return;
            }

            int addressID = Convert.ToInt32(itemsGrid2.SelectedRows[0].Cells[0].Value);
            string street = itemsGrid2.SelectedRows[0].Cells[1].Value.ToString();
            string num = itemsGrid2.SelectedRows[0].Cells[2].Value.ToString();
            EditAddressForm form = new EditAddressForm(addressID, street, num);

            form.ShowDialog();
            GetInfo();
        }

        private void delete_address_b_Click(object sender, EventArgs e)
        {
            if (itemsGrid2.SelectedCells.Count == 0)
            {
                MessageBox.Show("Выберите адрес!");
                return;
            }

            DialogResult result = MessageBox.Show("Вы действительно хотите удалить эту запись?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No)
                return;

            OleDbConnection connection = new OleDbConnection(oledb_attrs);
            connection.Open();

            int count = 0;
            string cmd = "select count(*) from объект_строительства where код_адреса = @id";
            int id = Convert.ToInt32(itemsGrid2.SelectedRows[0].Cells[0].Value);
            OleDbCommand command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", id);
            count += (int)command.ExecuteScalar();

            cmd = "select count(*) from поставщик where код_адреса = @id";
            command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", id);
            count += (int)command.ExecuteScalar();

            cmd = "select count(*) from строитель where код_адреса = @id";
            command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", id);
            count += (int)command.ExecuteScalar();

            cmd = "select count(*) from бригадир where код_адреса = @id";
            command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", id);
            count += (int)command.ExecuteScalar();

            connection.Close();

            if (count > 0)
            {
                MessageBox.Show("Невозможно удалить адрес, так как он используется в другой записи!");
                return;
            }

            connection.Open();

            cmd = "delete from адрес where код_адреса = @id";
            command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();

            connection.Close();
            GetInfo();
        }
    }
}
