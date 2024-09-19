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
    public partial class CompleteWorkForm : Form
    {
        private string oledb_attrs = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BCompany.mdb";
        private int objectID, workID;
        private bool isComplete = true;

        private void GetInfo()
        {
            storageGrid.Rows.Clear();
            requestGrid.Rows.Clear();

            OleDbConnection connection = new OleDbConnection(oledb_attrs);
            connection.Open();

            string cmd = "select склад.код_на_складе, стройматериал.название as стройматериал_название, " +
                "единица_измерения.название as единица_измерения_название, склад.остаток, поставщик.фио_рук, поставка.дата " +
                "from ((поставка inner join перечень_в_поставках on поставка.код_поставки = перечень_в_поставках.код_поставки) " +
                "inner join (поставщик inner join (единица_измерения inner join " +
                "(стройматериал inner join склад on стройматериал.[код_материала] = склад.[код_материала]) " +
                "on единица_измерения.[код_единицы] = склад.[код_единицы]) " +
                "on поставщик.[код_поставщика] = склад.[код_поставщика]) " +
                "on поставка.код_поставщика = поставщик.код_поставщика) " +
                "inner join партия_материала " +
                "on (склад.код_на_складе = партия_материала.код_на_складе) " +
                "and (партия_материала.код_партии = перечень_в_поставках.код_партии) and " +
                "(единица_измерения.код_единицы = партия_материала.код_единицы)";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            OleDbDataReader reader = command.ExecuteReader();
            int counter = 0;

            while (reader.Read())
            {
                storageGrid.Rows.Add();
                storageGrid.Rows[counter].Cells[0].Value = reader.GetInt32(0).ToString();
                storageGrid.Rows[counter].Cells[1].Value = reader.GetString(1);
                storageGrid.Rows[counter].Cells[2].Value = reader.GetString(2);
                storageGrid.Rows[counter].Cells[3].Value = reader.GetString(3);
                storageGrid.Rows[counter].Cells[4].Value = reader.GetString(4);
                storageGrid.Rows[counter].Cells[5].Value = reader.GetDateTime(5).ToString("dd/MM/yyyy");

                counter++;
            }

            cmd = "SELECT заявка.код_заявки, стройматериал.название AS стройматериал_название, единица_измерения.название AS единица_измерения_название, партия_материала.количество, поставщик.фио_рук, заявка.дата\r\nFROM (объект_строительства INNER JOIN (поставщик INNER JOIN ((единица_измерения INNER JOIN (заявка INNER JOIN (партия_материала INNER JOIN перечень_в_заявках ON партия_материала.[код_партии] = перечень_в_заявках.[код_партии]) ON заявка.[код_заявки] = перечень_в_заявках.[код_заявки]) ON единица_измерения.[код_единицы] = партия_материала.[код_единицы]) INNER JOIN (стройматериал INNER JOIN склад ON стройматериал.[код_материала] = склад.[код_материала]) ON (склад.код_на_складе = партия_материала.код_на_складе) AND (единица_измерения.[код_единицы] = склад.[код_единицы])) ON поставщик.[код_поставщика] = склад.[код_поставщика]) ON объект_строительства.код_объекта = заявка.код_объекта) INNER JOIN перечень_работ ON (перечень_работ.код_работы = заявка.код_работы) AND (объект_строительства.код_объекта = перечень_работ.код_объекта)\r\nWHERE (((заявка.код_объекта)=@id) AND ((перечень_работ.код_работы)=@id2));\r\n";
            command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", objectID);
            command.Parameters.AddWithValue("@id2", workID);
            reader = command.ExecuteReader();
            counter = 0;

            while (reader.Read())
            {
                requestGrid.Rows.Add();
                requestGrid.Rows[counter].Cells[0].Value = reader.GetInt32(0).ToString();
                requestGrid.Rows[counter].Cells[1].Value = reader.GetString(1);
                requestGrid.Rows[counter].Cells[2].Value = reader.GetString(2);
                requestGrid.Rows[counter].Cells[3].Value = reader.GetString(3);
                requestGrid.Rows[counter].Cells[4].Value = reader.GetString(4);
                requestGrid.Rows[counter].Cells[5].Value = reader.GetDateTime(5).ToString("dd/MM/yyyy");

                counter++;
            }

            connection.Close();
        }

        private void GetBrigades()
        {
            OleDbConnection connection = new OleDbConnection(oledb_attrs);
            connection.Open();

            string cmd = "select название from бригада";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            OleDbDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                brigadesComboBox.Items.Add(reader.GetString(0));
            }

            connection.Close();
        }

        public CompleteWorkForm(int object_id, int work_id)
        {
            InitializeComponent();

            objectID = object_id;
            workID = work_id;
            requestDate.MinDate = DateTime.Now; 
            workDate.MinDate = DateTime.Now;
            GetBrigades();
            GetInfo();
        }

        public static bool ValidateCount(string count)
        {
            foreach (char ch in count)
            {
                if (!Char.IsDigit(ch) || ch == ' ')
                {
                    return false;
                }
            }

            return true;
        }

        private void add_b_Click(object sender, EventArgs e)
        {            
            if (storageGrid.SelectedRows.Count > 0)
            {
                DateTime parsedDate1 = DateTime.ParseExact(storageGrid.SelectedRows[0].Cells[5].Value.ToString(), "dd/MM/yyyy", null);
                DateTime parsedDate2 = DateTime.ParseExact(requestDate.Value.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null);
                int residue = Convert.ToInt32(storageGrid.SelectedRows[0].Cells[3].Value);
                int request_count;

                if (count_t.Text.Length == 0 || !ValidateCount(count_t.Text))
                {
                    MessageBox.Show("Введите корректное количество материала!");
                    return;
                }
                else if (residue < Convert.ToInt32(count_t.Text))
                {
                    MessageBox.Show("Количество запрашиваемого материала не может быть больше остатка на складе!");
                    return;
                }
                else if (parsedDate2 < parsedDate1)
                {
                    MessageBox.Show("Дата оформления заявки не может быть меньше даты поставки материала на склад!");
                    return;
                }

                request_count = Convert.ToInt32(count_t.Text);

                OleDbConnection connection = new OleDbConnection(oledb_attrs);
                connection.Open();

                string cmd = "insert into заявка (код_объекта, код_работы, дата) values (@1, @2, @3)";
                OleDbCommand command = new OleDbCommand(cmd, connection);
                command.Parameters.AddWithValue("@1", objectID);
                command.Parameters.AddWithValue("@2", workID);
                command.Parameters.AddWithValue("@3", requestDate.Value.ToString("dd/MM/yyyy"));
                command.ExecuteNonQuery();

                cmd = "select MAX(код_заявки) from заявка";
                command = new OleDbCommand(cmd, connection);
                int requestID = (int)command.ExecuteScalar();

                cmd = "select код_материала from стройматериал where название = @name";
                command = new OleDbCommand(cmd, connection);
                command.Parameters.AddWithValue("@name", storageGrid.SelectedRows[0].Cells[1].Value);
                int materialID = (int)command.ExecuteScalar();

                cmd = "select код_единицы from единица_измерения where название = @name";
                command = new OleDbCommand(cmd, connection);
                command.Parameters.AddWithValue("@name", storageGrid.SelectedRows[0].Cells[2].Value);
                int unitID = (int)command.ExecuteScalar();
                
                cmd = "update склад set остаток = @1 where код_на_складе = @id";
                command = new OleDbCommand(cmd, connection);
                command.Parameters.AddWithValue("@1", (residue - request_count).ToString());
                command.Parameters.AddWithValue("@id", Convert.ToInt32(storageGrid.SelectedRows[0].Cells[0].Value));
                command.ExecuteNonQuery();

                cmd = "insert into партия_материала (код_на_складе, количество, код_единицы) values (@1, @2, @3)";
                command = new OleDbCommand(cmd, connection);
                command.Parameters.AddWithValue("@1", Convert.ToInt32(storageGrid.SelectedRows[0].Cells[0].Value));
                command.Parameters.AddWithValue("@2", request_count.ToString());
                command.Parameters.AddWithValue("@3", unitID);
                command.ExecuteNonQuery();             

                cmd = "select MAX(код_партии) from партия_материала";
                command = new OleDbCommand(cmd, connection);
                int lotID = (int)command.ExecuteScalar();

                cmd = "insert into перечень_в_заявках (код_заявки, код_партии) values (@1, @2)";
                command = new OleDbCommand(cmd, connection);
                command.Parameters.AddWithValue("@1", requestID);
                command.Parameters.AddWithValue("@2", lotID);
                command.ExecuteNonQuery();

                connection.Close();
                isComplete = false;
                GetInfo();
            }
            else
            {
                MessageBox.Show("Выберите материал со склада!");
            }
        }

        private void RemoveSelectedRequest()
        {
            OleDbConnection connection = new OleDbConnection(oledb_attrs);
            connection.Open();

            string cmd;
            OleDbCommand command;

            cmd = "select склад.код_на_складе, склад.остаток " +
                "from (склад " +
                "inner join партия_материала " +
                "on склад.[код_на_складе] = партия_материала.[код_на_складе]) " +
                "inner join (заявка " +
                "inner join перечень_в_заявках " +
                "on заявка.[код_заявки] = перечень_в_заявках.[код_заявки]) " +
                "on партия_материала.[код_партии] = перечень_в_заявках.[код_партии] " +
                "where заявка.код_заявки = @id";
            command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", Convert.ToInt32(requestGrid.SelectedRows[0].Cells[0].Value));
            OleDbDataReader reader = command.ExecuteReader();
            reader.Read();

            int storageID = reader.GetInt32(0);
            int residue = Convert.ToInt32(reader.GetString(1));
            int requestCount = Convert.ToInt32(requestGrid.SelectedRows[0].Cells[3].Value);

            cmd = "SELECT партия_материала.код_партии AS партия_материала_код_партии " +
                "FROM партия_материала " +
                "INNER JOIN перечень_в_заявках ON партия_материала.[код_партии] = перечень_в_заявках.[код_партии] " +
                "where перечень_в_заявках.код_заявки = @id";
            command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", Convert.ToInt32(requestGrid.SelectedRows[0].Cells[0].Value));
            int lotID = (int)command.ExecuteScalar();

            cmd = "delete from перечень_в_заявках where код_партии = @id and код_заявки = @id2";
            command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", lotID);
            command.Parameters.AddWithValue("@id2", Convert.ToInt32(requestGrid.SelectedRows[0].Cells[0].Value));
            command.ExecuteNonQuery();

            cmd = "delete from заявка where код_заявки = @id";
            command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", Convert.ToInt32(requestGrid.SelectedRows[0].Cells[0].Value));
            command.ExecuteNonQuery();

            cmd = "delete from партия_материала where код_партии = @id";
            command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", lotID);
            command.ExecuteNonQuery();

            cmd = "update склад set остаток = @res where код_на_складе = @id";
            command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@res", (residue + requestCount).ToString());
            command.Parameters.AddWithValue("@id", storageID);
            command.ExecuteNonQuery();

            connection.Close();
            isComplete = false;
            GetInfo();
        }

        private void remove_b_Click(object sender, EventArgs e)
        {
            if (requestGrid.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Вы действительно хотите убрать эту заявку?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                    return;

                RemoveSelectedRequest();
            }
            else
            {
                MessageBox.Show("Выберите заявку, которую хотите убрать!");
            }
        }

        private bool ValidateDate()
        {
            if (requestGrid.Rows.Count == 0)
            {
                return true;
            }

            foreach (DataGridViewRow row in requestGrid.Rows)
            {
                DateTime parsedDate1 = DateTime.ParseExact(row.Cells[5].Value.ToString(), "dd/MM/yyyy", null);
                DateTime parsedDate2 = DateTime.ParseExact(workDate.Value.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null);

                if (parsedDate1 > parsedDate2)
                {
                    return false;
                }
            }

            return true;
        }

        private bool ValidateBrigade()
        {
            OleDbConnection connection = new OleDbConnection(oledb_attrs);
            connection.Open();

            string cmd = "select " +
                "count(выполненные_работы.код_бригады) " +
                "from (объект_строительства " +
                "inner join перечень_работ " +
                "on объект_строительства.[код_объекта] = перечень_работ.[код_объекта]) " +
                "inner join выполненные_работы " +
                "on перечень_работ.[код_работы] = выполненные_работы.[код_работы] " +
                "where объект_строительства.код_объекта <> @id and выполненные_работы.код_работы <> @id2 and выполненные_работы.дата = @date";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", objectID);
            command.Parameters.AddWithValue("@id2", workID);
            command.Parameters.AddWithValue("@date", workDate.Value.ToString("dd/MM/yyyy"));
            int count = (int)command.ExecuteScalar();

            connection.Close();

            return count == 0;
        }

        private void complete_b_Click(object sender, EventArgs e)
        {
            if (brigadesComboBox.Text.Length == 0)
            {
                MessageBox.Show("Выберите бригаду!");
                return;
            }
            else if (!ValidateBrigade())
            {
                MessageBox.Show("Данная бригада работает на другом объекте в эту дату!");
                return;
            }
            else if (!ValidateDate())
            {
                MessageBox.Show("Дата выполнения работ не может быть меньше даты заявки!");
                return;
            }

            OleDbConnection connection = new OleDbConnection(oledb_attrs);
            connection.Open();

            string cmd = "select код_бригады from бригада where название = @name";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@name", brigadesComboBox.Text);
            int brigadeID = (int)command.ExecuteScalar();

            string about = about_t.Text.Replace(" ", "").Length > 0 ? about_t.Text : "Нет описания";
            cmd = "insert into выполненные_работы (код_работы, дата, код_бригады, описание_работ) values (@1, @2, @3, @4)";
            command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@1", workID);
            command.Parameters.AddWithValue("@2", workDate.Value.ToString("dd/MM/yyyy"));
            command.Parameters.AddWithValue("@3", brigadeID);
            command.Parameters.AddWithValue("@4", about);
            command.ExecuteNonQuery();

            cmd = "select max(код_выполненных) from выполненные_работы";
            command = new OleDbCommand(cmd, connection);
            int idw = (int)command.ExecuteScalar();

            cmd = "insert into смета (код_вып_работ, код_заявки, код_объекта) values (@1, @2, @3)";
            if (requestGrid.Rows.Count == 0)
            {
                command = new OleDbCommand(cmd, connection);
                command.Parameters.AddWithValue("@1", idw);
                command.Parameters.AddWithValue("@2", 0);
                command.Parameters.AddWithValue("@3", objectID);
                command.ExecuteNonQuery();
            }
            else
            {
                foreach (DataGridViewRow row in requestGrid.Rows)
                {
                    command = new OleDbCommand(cmd, connection);
                    command.Parameters.AddWithValue("@1", idw);
                    command.Parameters.AddWithValue("@2", Convert.ToInt32(row.Cells[0].Value));
                    command.Parameters.AddWithValue("@3", objectID);
                    command.ExecuteNonQuery();
                }
            }
            
            connection.Close();

            isComplete = true;
            MessageBox.Show("Работа выполнена!");
            this.Close();
        }

        private void CompleteWorkForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isComplete)
            {
                while (requestGrid.Rows.Count > 0)
                {
                    requestGrid.CurrentCell = requestGrid.Rows[0].Cells[0];
                    RemoveSelectedRequest();
                }                
            }
        }
    }
}
