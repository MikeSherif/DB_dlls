using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Storage
{
    public partial class StorageControl : UserControl
    {
        private string oledb_attrs = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BCompany.mdb";

        private void GetInfo()
        {
            itemsGrid.Rows.Clear();

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
                itemsGrid.Rows.Add();
                itemsGrid.Rows[counter].Cells[0].Value = reader.GetInt32(0).ToString();
                itemsGrid.Rows[counter].Cells[1].Value = reader.GetString(1);
                itemsGrid.Rows[counter].Cells[2].Value = reader.GetString(2);
                itemsGrid.Rows[counter].Cells[3].Value = reader.GetString(3);
                itemsGrid.Rows[counter].Cells[4].Value = reader.GetString(4);
                itemsGrid.Rows[counter].Cells[5].Value = reader.GetDateTime(5).ToString("dd/MM/yyyy");

                counter++;
            }

            connection.Close();
        }

        public StorageControl(bool Read, bool Write, bool Edit, bool Delete)
        {
            InitializeComponent();

            add_b.Enabled = Write;
            edit_b.Enabled = Edit;
            delete_b.Enabled = Delete;
            GetInfo();
        }

        private void add_b_Click(object sender, EventArgs e)
        {
            AddForm form = new AddForm();

            form.ShowDialog();
            GetInfo();
        }

        private void edit_b_Click(object sender, EventArgs e)
        {
            if (itemsGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите запись!");
                return;
            }

            OleDbConnection connection = new OleDbConnection(oledb_attrs);
            connection.Open();

            string cmd = "select count(заявка.код_заявки) as Количество_заявок " +
                "from партия_материала " +
                "inner join (заявка " +
                "inner join перечень_в_заявках on заявка.код_заявки = перечень_в_заявках.код_заявки) " +
                "on партия_материала.код_партии = перечень_в_заявках.код_партии " +
                "where партия_материала.код_на_складе = @id";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", Convert.ToInt32(itemsGrid.SelectedRows[0].Cells[0].Value));
            int count = (int)command.ExecuteScalar();

            connection.Close();

            if (count > 0)
            {
                MessageBox.Show("Материалы из данной поставки уже были задействованы, редактирование записи невозможно!");
                return;
            }

            int storageID = Convert.ToInt32(itemsGrid.SelectedRows[0].Cells[0].Value);
            string material = itemsGrid.SelectedRows[0].Cells[1].Value.ToString();
            string unit = itemsGrid.SelectedRows[0].Cells[2].Value.ToString();
            string counts = itemsGrid.SelectedRows[0].Cells[3].Value.ToString();
            string supplier = itemsGrid.SelectedRows[0].Cells[4].Value.ToString();
            string date = itemsGrid.SelectedRows[0].Cells[5].Value.ToString();
            EditForm form = new EditForm(storageID, material, unit, counts, supplier, date);

            form.ShowDialog();
            GetInfo();
        }

        private void delete_b_Click(object sender, EventArgs e)
        {
            if (itemsGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите запись!");
                return;
            }

            DialogResult result = MessageBox.Show("Вы действительно хотите удалить эту запись?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No)
                return;

            OleDbConnection connection = new OleDbConnection(oledb_attrs);
            connection.Open();

            string cmd = "select count(заявка.код_заявки) as Количество_заявок " +
                "from партия_материала " +
                "inner join (заявка " +
                "inner join перечень_в_заявках on заявка.код_заявки = перечень_в_заявках.код_заявки) " +
                "on партия_материала.код_партии = перечень_в_заявках.код_партии " +
                "where партия_материала.код_на_складе = @id";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", Convert.ToInt32(itemsGrid.SelectedRows[0].Cells[0].Value));
            int count = (int)command.ExecuteScalar();

            connection.Close();

            if (count > 0)
            {
                MessageBox.Show("Материалы из данной поставки уже были задействованы, редактирование записи невозможно!");
                return;
            }

            connection.Open();

            cmd = "select код_партии from партия_материала where код_на_складе = @id";
            command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", itemsGrid.SelectedRows[0].Cells[0].Value);
            int lotID = (int)command.ExecuteScalar();

            cmd = "select код_поставки from перечень_в_поставках where код_партии = @id";
            command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", lotID);
            int supplyID = (int)command.ExecuteScalar();

            cmd = "delete from перечень_в_поставках where код_партии = @id and код_поставки = @id2";
            command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", lotID);
            command.Parameters.AddWithValue("@id2", supplyID);
            command.ExecuteNonQuery();

            cmd = "delete from поставка where код_поставки = @id";
            command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", supplyID);
            command.ExecuteNonQuery();

            cmd = "delete from партия_материала where код_партии = @id";
            command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", lotID);
            command.ExecuteNonQuery();

            cmd = "delete from склад where код_на_складе = @id";
            command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", itemsGrid.SelectedRows[0].Cells[0].Value);
            command.ExecuteNonQuery();

            connection.Close();

            MessageBox.Show("Запись успешно удалена!");
            GetInfo();
        }
    }
}
