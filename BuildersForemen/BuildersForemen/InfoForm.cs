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

namespace BuildersForemen
{
    public partial class InfoForm : Form
    {
        private string oledb_attrs = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BCompany.mdb";
        private bool wasBuilder = false;
        private int workerID;

        private void GetInfo()
        {
            OleDbConnection connection = new OleDbConnection(oledb_attrs);
            connection.Open();

            string cmd;
            cmd = wasBuilder ? "select трудовой_стаж from строитель where код_строителя = @id" : "select трудовой_стаж from бригадир where код_бригадира = @id";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", workerID);
            OleDbDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                work_t.Text = reader.GetString(0);
            }

            cmd = wasBuilder ? "select код_адреса from строитель where код_строителя = @id" : "select код_адреса from бригадир where код_бригадира = @id";
            command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", workerID);
            int addressID = (int)command.ExecuteScalar();

            cmd = "select " +
                "адрес.код_адреса, улица.название, адрес.номер " +
                "from улица " +
                "inner join адрес " +
                "on улица.[код_улицы] = адрес.[код_улицы] " +
                "where адрес.[код_адреса] = @id";
            command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", addressID);
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                address_t.Text = reader.GetString(1) + " " + reader.GetString(2);
            }

            if (!wasBuilder)
            {
                connection.Close();
                return;
            }

            cmd = "select " +
                "специализации_строителей.код_специализации, специализация.название " +
                "from специализация " +
                "inner join специализации_строителей " +
                "on специализация.[код_специализации] = специализации_строителей.[код_специализации] " +
                "where специализации_строителей.[код_строителя] = @id";
            command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", workerID);
            reader = command.ExecuteReader();
            int counter = 0;
            
            while (reader.Read())
            {
                specsGrid.Rows.Add();
                specsGrid.Rows[counter].Cells[0].Value = reader.GetInt32(0);
                specsGrid.Rows[counter].Cells[1].Value = reader.GetString(1);
                counter++;
            }            

            connection.Close();
        }

        public InfoForm(bool is_builder, int worker_id, string name, string mf, string birth)
        {
            InitializeComponent();

            wasBuilder = is_builder;
            workerID = worker_id;
            
            name_t.Text = name;
            mfBox.Text = mf;
            birth_t.Text = birth;
            checkBox1.Checked = is_builder;

            GetInfo();
        }
    }
}
