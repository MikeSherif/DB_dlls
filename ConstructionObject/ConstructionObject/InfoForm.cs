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
    public partial class InfoForm : Form
    {
        private string oledb_attrs = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BCompany.mdb";
        private int objectID;

        private void GetInfo()
        {
            itemsGrid.Rows.Clear();

            OleDbConnection connection = new OleDbConnection(oledb_attrs);
            connection.Open();

            Dictionary<int, string> jobs = new Dictionary<int, string>();
            string cmd = "select код_работы, наименование from перечень_работ where код_объекта = @id";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", objectID);
            OleDbDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                jobs.Add(reader.GetInt32(0), reader.GetString(1));
            }

            int counter = 0;
            cmd = "select count(*) from выполненные_работы where код_работы = @id";
            foreach (var job in jobs)
            {
                itemsGrid.Rows.Add();

                command = new OleDbCommand(cmd, connection);
                command.Parameters.AddWithValue("@id", job.Key);
                if ((int)command.ExecuteScalar() > 0)
                {
                    itemsGrid.Rows[counter].Cells[0].Value = job.Key;
                    itemsGrid.Rows[counter].Cells[1].Value = job.Value;
                    itemsGrid.Rows[counter].Cells[2].Value = "Выполнена";
                }
                else
                {
                    itemsGrid.Rows[counter].Cells[0].Value = job.Key;
                    itemsGrid.Rows[counter].Cells[1].Value = job.Value;
                    itemsGrid.Rows[counter].Cells[2].Value = "Не выполнена";
                }

                counter++;
            }

            connection.Close();
        }

        public InfoForm(int object_id, bool Read, bool Write, bool Edit, bool Delete)
        {
            InitializeComponent();

            add_b.Enabled = Write;
            edit_b.Enabled = Edit;
            delete_b.Enabled = Delete;
            complete_b.Enabled = Write;
            cancel_b.Enabled = Delete;

            objectID = object_id;

            GetInfo();
        }

        private void add_b_Click(object sender, EventArgs e)
        {
            AddWork form = new AddWork(objectID);

            form.ShowDialog();
            GetInfo();
        }

        private void edit_b_Click(object sender, EventArgs e)
        {
            if (itemsGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите работу из списка!");
                return;
            }

            int workID = Convert.ToInt32(itemsGrid.SelectedRows[0].Cells[0].Value);
            string workName = itemsGrid.SelectedRows[0].Cells[1].Value.ToString();
            EditWork form = new EditWork(objectID, workID, workName);

            form.ShowDialog();
            GetInfo();
        }

        private void complete_b_Click(object sender, EventArgs e)
        {
            if (itemsGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите работу из списка!");
                return;
            }
            else if (itemsGrid.SelectedRows[0].Cells[2].Value.ToString().Equals("Выполнена"))
            {
                return;
            }

            int workID = Convert.ToInt32(itemsGrid.SelectedRows[0].Cells[0].Value);
            CompleteWorkForm form = new CompleteWorkForm(objectID, workID);

            form.ShowDialog();
            GetInfo();
        }

        private void cancel_b_Click(object sender, EventArgs e)
        {
            if (itemsGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите работу из списка!");
                return;
            }
            else if (itemsGrid.SelectedRows[0].Cells[2].Value.ToString().Equals("Не выполнена"))
            {
                return;
            }

            DialogResult result = MessageBox.Show("Вы действительно хотите изменить статус работы?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No)
                return;

            OleDbConnection connection = new OleDbConnection(oledb_attrs);
            connection.Open();

            string cmd = "select код_выполненных from выполненные_работы where код_работы = @id";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", Convert.ToInt32(itemsGrid.SelectedRows[0].Cells[0].Value));
            int workID = (int)command.ExecuteScalar();

            cmd = "select " +
                "смета.код_заявки, перечень_в_заявках.код_перечня, партия_материала.код_партии, " +
                "партия_материала.код_на_складе, партия_материала.количество, склад.остаток " +
                "from (заявка " +
                "inner join смета " +
                "on заявка.[код_заявки] = смета.[код_заявки]) " +
                "inner join ((склад " +
                "inner join партия_материала " +
                "on склад.[код_на_складе] = партия_материала.[код_на_складе]) " +
                "inner join перечень_в_заявках " +
                "on партия_материала.[код_партии] = перечень_в_заявках.[код_партии]) " +
                "on заявка.[код_заявки] = перечень_в_заявках.[код_заявки] " +
                "where смета.код_вып_работ = @id";
            command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", workID);
            OleDbDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int requestID = reader.GetInt32(0);
                int perId = reader.GetInt32(1);
                int lotID = reader.GetInt32(2);
                int storageID = reader.GetInt32(3);
                int requestCount = Convert.ToInt32(reader.GetString(4));
                int residue = Convert.ToInt32(reader.GetString(5));

                string cmd2 = "delete from перечень_в_заявках where код_перечня = @id";
                OleDbCommand command2 = new OleDbCommand(cmd2, connection);
                command2.Parameters.AddWithValue("@id", perId);
                command2.ExecuteNonQuery();

                cmd2 = "delete from партия_материала where код_партии = @id";
                command2 = new OleDbCommand(cmd2, connection);
                command2.Parameters.AddWithValue("@id", lotID);
                command2.ExecuteNonQuery();

                cmd2 = "update склад set остаток = @res where код_на_складе = @id";
                command2 = new OleDbCommand(cmd2, connection);
                command2.Parameters.AddWithValue("@res", (residue + requestCount).ToString());
                command2.Parameters.AddWithValue("@id", storageID);
                command2.ExecuteNonQuery();

                cmd2 = "delete from заявка where код_заявки = @id";
                command2 = new OleDbCommand(cmd2, connection);
                command2.Parameters.AddWithValue("@id", requestID);
                command2.ExecuteNonQuery();
            }

            cmd = "delete from смета where код_вып_работ = @id";
            command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", workID);
            command.ExecuteNonQuery();

            cmd = "delete from выполненные_работы where код_выполненных = @id";
            command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", workID);
            command.ExecuteNonQuery();

            connection.Close();
            GetInfo();
        }

        private void info_b_Click(object sender, EventArgs e)
        {
            if (itemsGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите работу из списка!");
                return;
            }
            else if (itemsGrid.SelectedRows[0].Cells[2].Value.ToString().Equals("Не выполнена"))
            {
                MessageBox.Show("Работа ещё не выполнена!");
                return;
            }

            OleDbConnection connection = new OleDbConnection(oledb_attrs);
            connection.Open();

            string cmd = "select код_выполненных from выполненные_работы where код_работы = @id";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", Convert.ToInt32(itemsGrid.SelectedRows[0].Cells[0].Value));
            int workID = (int)command.ExecuteScalar();

            connection.Close();

            AboutForm form = new AboutForm(objectID, workID, itemsGrid.SelectedRows[0].Cells[1].Value.ToString(), Convert.ToInt32(itemsGrid.SelectedRows[0].Cells[0].Value));
            
            form.ShowDialog();
        }

        private void delete_b_Click(object sender, EventArgs e)
        {
            if (itemsGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите работу из списка!");
                return;
            }

            DialogResult result = MessageBox.Show("Вы действительно хотите удалить эту запись?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No)
                return;

            OleDbConnection connection = new OleDbConnection(oledb_attrs);
            connection.Open();

            int count = 0;

            string cmd;
            int id = Convert.ToInt32(itemsGrid.SelectedRows[0].Cells[0].Value);

            cmd = "select count(*) from выполненные_работы where код_работы = @id";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", id);
            count = (int)command.ExecuteScalar();

            connection.Close();

            if (count > 0)
            {
                MessageBox.Show("Невозможно удалить выполненную работу!");
                return;
            }

            connection.Open();

            cmd = "delete from перечень_работ where код_работы = @id";
            command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();

            connection.Close();
            GetInfo();
        }
    }
}
