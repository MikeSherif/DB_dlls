using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeOpenXml;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace Smeta
{
    public partial class SmetaControl : UserControl
    {
        private string oledb_attrs = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BCompany.mdb";

        private void SetBrigades()
        {
            OleDbConnection connection = new OleDbConnection(oledb_attrs);
            connection.Open();

            string cmd = "select название from объект_строительства";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            OleDbDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                objComboBox.Items.Add(reader.GetString(0));
            }

            connection.Close();
        }

        private void brigadesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (objComboBox.Text.Length == 0)
            {
                itemsGrid.Rows.Clear();
                return;
            }

            itemsGrid.Rows.Clear();
            GetInfo(objComboBox.Text);
        }

        private void GetInfo(string objectName)
        {
            OleDbConnection connection = new OleDbConnection(oledb_attrs);
            connection.Open();

            string cmd = "select код_объекта from объект_строительства where название = @name";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@name", objectName);
            int objID = (int)command.ExecuteScalar();

            cmd = "SELECT выполненные_работы.код_работы, перечень_работ.наименование, выполненные_работы.дата, выполненные_работы.код_бригады, бригада.название AS бригада_название, смета.код_заявки, перечень_в_заявках.код_партии, стройматериал.название, партия_материала.количество, партия_материала.код_единицы, единица_измерения.название AS единица_измерения_название, склад.код_на_складе\r\nFROM стройматериал INNER JOIN (объект_строительства INNER JOIN (((заявка INNER JOIN ((единица_измерения INNER JOIN партия_материала ON единица_измерения.[код_единицы] = партия_материала.[код_единицы]) INNER JOIN перечень_в_заявках ON партия_материала.[код_партии] = перечень_в_заявках.[код_партии]) ON заявка.[код_заявки] = перечень_в_заявках.[код_заявки]) INNER JOIN ((перечень_работ INNER JOIN (бригада INNER JOIN выполненные_работы ON бригада.[код_бригады] = выполненные_работы.[код_бригады]) ON перечень_работ.[код_работы] = выполненные_работы.[код_работы]) INNER JOIN смета ON выполненные_работы.[код_выполненных] = смета.[код_вып_работ]) ON заявка.[код_заявки] = смета.[код_заявки]) INNER JOIN склад ON (склад.код_на_складе = партия_материала.код_на_складе) AND (единица_измерения.код_единицы = склад.код_единицы)) ON (объект_строительства.код_объекта = смета.код_объекта) AND (объект_строительства.код_объекта = перечень_работ.код_объекта) AND (объект_строительства.код_объекта = заявка.код_объекта)) ON стройматериал.код_материала = склад.код_материала where смета.код_объекта = @id";
            command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", objID);
            OleDbDataReader reader = command.ExecuteReader();
            int counter = 0;

            while (reader.Read())
            {
                itemsGrid.Rows.Add();
                itemsGrid.Rows[counter].Cells[0].Value = reader.GetString(1);
                itemsGrid.Rows[counter].Cells[1].Value = reader.GetDateTime(2).ToString("dd/MM/yyyy");
                itemsGrid.Rows[counter].Cells[2].Value = reader.GetString(4);
                itemsGrid.Rows[counter].Cells[3].Value = reader.GetString(7);
                itemsGrid.Rows[counter].Cells[4].Value = reader.GetString(8);
                itemsGrid.Rows[counter].Cells[5].Value = reader.GetString(10);

                int storageID = reader.GetInt32(11);
                string cmd2 = "SELECT склад.код_на_складе, партия_материала.код_партии, партия_материала.количество, перечень_в_поставках.закупочная_цена\r\nFROM (склад INNER JOIN партия_материала ON склад.[код_на_складе] = партия_материала.[код_на_складе]) INNER JOIN перечень_в_поставках ON партия_материала.[код_партии] = перечень_в_поставках.[код_партии] where склад.код_на_складе = @id";
                OleDbCommand com = new OleDbCommand(cmd2, connection);
                com.Parameters.AddWithValue("@id", storageID);
                OleDbDataReader read = com.ExecuteReader();

                read.Read();

                int requestCount = Convert.ToInt32(itemsGrid.Rows[counter].Cells[4].Value.ToString());
                int storageCount = Convert.ToInt32(read.GetString(2));
                int price = Convert.ToInt32(read.GetString(3));
                float summary = (price / storageCount) * requestCount;

                itemsGrid.Rows[counter].Cells[6].Value = summary;

                counter++;
            }

            connection.Close();
        }

        public SmetaControl(bool Read, bool Write, bool Edit, bool Delete)
        {
            InitializeComponent();

            SetBrigades();
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
                OleDbConnection connection = new OleDbConnection(oledb_attrs);
                connection.Open();

                string cmd = "SELECT улица.название, адрес.номер\r\nFROM (улица INNER JOIN адрес ON улица.[код_улицы] = адрес.[код_улицы]) INNER JOIN объект_строительства ON адрес.код_адреса = объект_строительства.код_адреса where объект_строительства.название = @name";
                OleDbCommand command = new OleDbCommand(cmd, connection);
                command.Parameters.AddWithValue("@name", objComboBox.Text);
                OleDbDataReader reader = command.ExecuteReader();
                reader.Read();

                string address = reader.GetString(0) + " " + reader.GetString(1);

                connection.Close();                

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Word Files (*.docx)|*.docx|All files (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (var doc = DocX.Create(saveFileDialog.FileName))
                    {
                        doc.PageLayout.Orientation = Xceed.Document.NET.Orientation.Landscape;

                        Paragraph title = doc.InsertParagraph("Смета на стройматериалы");
                        Paragraph info = doc.InsertParagraph("Объект: " + objComboBox.Text + ". Адрес: " + address);
                        title.Font(new Xceed.Document.NET.Font("Times New Roman")).FontSize(14).Bold();
                        info.Font(new Xceed.Document.NET.Font("Times New Roman")).FontSize(14).Bold();

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
