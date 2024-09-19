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
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace WorkersList
{
    public partial class WorkersControl : UserControl
    {
        private string oledb_attrs = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BCompany.mdb";

        private void GetInfo()
        {
            OleDbConnection connection = new OleDbConnection(oledb_attrs);
            connection.Open();

            string cmd = "SELECT бригадир.код_бригадира, бригадир.ФИО, бригадир.пол, бригадир.дата_рождения, бригадир.код_адреса, улица.название, адрес.номер, бригадир.трудовой_стаж\r\nFROM (улица INNER JOIN адрес ON улица.[код_улицы] = адрес.[код_улицы]) INNER JOIN бригадир ON адрес.[код_адреса] = бригадир.[код_адреса];\r\n";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            OleDbDataReader reader = command.ExecuteReader();
            int counter = 0;

            while (reader.Read())
            {
                itemsGrid.Rows.Add();

                int workerID = reader.GetInt32(0);
                itemsGrid.Rows[counter].Cells[0].Value = reader.GetString(1);
                itemsGrid.Rows[counter].Cells[1].Value = reader.GetString(2);
                itemsGrid.Rows[counter].Cells[2].Value = reader.GetDateTime(3).ToString("dd/MM/yyyy");
                itemsGrid.Rows[counter].Cells[3].Value = reader.GetString(5) + " " + reader.GetString(6);
                itemsGrid.Rows[counter].Cells[4].Value = reader.GetString(7);

                string cmd2 = "select count(*) from бригада where код_бригадира = @id";
                OleDbCommand command1 = new OleDbCommand(cmd2, connection);
                command1.Parameters.AddWithValue("@id", workerID);
                int brigCount = (int)command1.ExecuteScalar();

                if (brigCount > 0)
                {
                    cmd2 = "select название from бригада where код_бригадира = @id";
                    command1 = new OleDbCommand(cmd2,connection);
                    command1.Parameters.AddWithValue("@id", workerID);
                    OleDbDataReader read = command1.ExecuteReader();

                    while (read.Read())
                    {
                        itemsGrid.Rows[counter].Cells[5].Value += read.GetString(0) + "; ";
                    }
                }
                else
                {
                    itemsGrid.Rows[counter].Cells[5].Value = "Нет бригады";
                }

                itemsGrid.Rows[counter].Cells[6].Value = "Бригадир";

                counter++;
            }

            cmd = "SELECT строитель.код_строителя, строитель.фио, строитель.пол, строитель.дата_рождения, строитель.код_адреса, улица.название, адрес.номер, строитель.трудовой_стаж\r\nFROM (улица INNER JOIN адрес ON улица.[код_улицы] = адрес.[код_улицы]) INNER JOIN строитель ON адрес.[код_адреса] = строитель.[код_адреса];\r\n";
            command = new OleDbCommand(cmd, connection);
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                itemsGrid.Rows.Add();

                int workerID = reader.GetInt32(0);
                itemsGrid.Rows[counter].Cells[0].Value = reader.GetString(1);
                itemsGrid.Rows[counter].Cells[1].Value = reader.GetString(2);
                itemsGrid.Rows[counter].Cells[2].Value = reader.GetDateTime(3).ToString("dd/MM/yyyy");
                itemsGrid.Rows[counter].Cells[3].Value = reader.GetString(5) + " " + reader.GetString(6);
                itemsGrid.Rows[counter].Cells[4].Value = reader.GetString(7);

                string cmd2 = "select count(*) from состав_бригады where код_строителя = @id";
                OleDbCommand command1 = new OleDbCommand(cmd2, connection);
                command1.Parameters.AddWithValue("@id", workerID);
                int brigCount = (int)command1.ExecuteScalar();

                if (brigCount > 0)
                {
                    cmd2 = "SELECT бригада.название\r\nFROM строитель INNER JOIN (бригада INNER JOIN состав_бригады ON бригада.[код_бригады] = состав_бригады.[код_бригады]) ON строитель.[код_строителя] = состав_бригады.[код_строителя] where строитель.код_строителя = @id";
                    command1 = new OleDbCommand(cmd2, connection);
                    command1.Parameters.AddWithValue("@id", workerID);
                    OleDbDataReader read = command1.ExecuteReader();

                    while (read.Read())
                    {
                        itemsGrid.Rows[counter].Cells[5].Value += read.GetString(0) + "; ";
                    }
                }
                else
                {
                    itemsGrid.Rows[counter].Cells[5].Value = "Нет бригады";
                }

                itemsGrid.Rows[counter].Cells[6].Value = "Строитель: ";
                cmd2 = "SELECT специализация.название\r\nFROM строитель INNER JOIN (специализация INNER JOIN специализации_строителей ON специализация.[код_специализации] = специализации_строителей.[код_специализации]) ON строитель.[код_строителя] = специализации_строителей.[код_строителя] where строитель.код_строителя = @id";
                command1 = new OleDbCommand(cmd2, connection);
                command1.Parameters.AddWithValue("@id", workerID);
                OleDbDataReader read1 = command1.ExecuteReader();

                while (read1.Read())
                {
                    itemsGrid.Rows[counter].Cells[6].Value += read1.GetString(0) + "; ";
                }
                counter++;
            }


            connection.Close();
        }

        public WorkersControl(bool Read, bool Write, bool Edit, bool Delete)
        {
            InitializeComponent();

            GetInfo();
        }

        private void saveWord_Click(object sender, EventArgs e)
        {
            if (itemsGrid.Rows.Count == 0)
            {
                MessageBox.Show("Нет информации для сохранения!");
                return;
            }

            try
            {                
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Word Files (*.docx)|*.docx|All files (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (var doc = DocX.Create(saveFileDialog.FileName))
                    {
                        doc.PageLayout.Orientation = Xceed.Document.NET.Orientation.Landscape;

                        Paragraph title = doc.InsertParagraph("Рабочий состав организации");
                        title.Font(new Xceed.Document.NET.Font("Times New Roman")).FontSize(14).Bold();

                        var table = doc.AddTable(itemsGrid.Rows.Count + 1, itemsGrid.Columns.Count);
                        table.Design = TableDesign.TableGrid;

                        for (int i = 0; i < itemsGrid.Columns.Count; i++)
                        {
                            table.Rows[0].Cells[i].Paragraphs.First().Append(itemsGrid.Columns[i].HeaderText).FontSize(6);
                        }

                        for (int i = 0; i < itemsGrid.Rows.Count; i++)
                        {
                            for (int j = 0; j < itemsGrid.Columns.Count; j++)
                            {
                                table.Rows[i + 1].Cells[j].Paragraphs.First().Append(itemsGrid.Rows[i].Cells[j].Value.ToString()).FontSize(6);
                            }
                        }

                        doc.InsertTable(table);

                        doc.Save();
                    }

                    MessageBox.Show("Файл успешно сохранен.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении файла: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void saveExcel_Click(object sender, EventArgs e)
        {
            if (itemsGrid.Rows.Count == 0)
            {
                MessageBox.Show("Нет информации для сохранения!");
                return;
            }

            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (var package = new ExcelPackage())
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet1");

                        for (int i = 1; i <= itemsGrid.Columns.Count; i++)
                        {
                            worksheet.Cells[1, i].Value = itemsGrid.Columns[i - 1].HeaderText;
                            for (int j = 1; j <= itemsGrid.Rows.Count; j++)
                            {
                                worksheet.Cells[j + 1, i].Value = itemsGrid.Rows[j - 1].Cells[i - 1].Value;
                            }
                        }

                        worksheet.Cells.AutoFitColumns();

                        FileInfo excelFile = new FileInfo(saveFileDialog.FileName);
                        package.SaveAs(excelFile);
                    }

                    MessageBox.Show("Файл успешно сохранен.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении файла: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
