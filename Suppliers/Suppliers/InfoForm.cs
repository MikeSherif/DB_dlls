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

namespace Suppliers
{
    public partial class InfoForm : Form
    {
        private string oledb_attrs = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BCompany.mdb";
        private int supplierID;

        private void GetInfo()
        {
            OleDbConnection connection = new OleDbConnection(oledb_attrs);
            connection.Open();

            string cmd = "select " +
                "поставщик.фио_рук, поставщик.телефон, поставщик.расчетный_счет, поставщик.инн, " +
                "улица.название as улица_название, адрес.номер, банк.название as банк_название " +
                "from (улица inner join адрес on улица.[код_улицы] = адрес.[код_улицы]) " +
                "inner join (банк inner join поставщик on банк.[код_банка] = поставщик.[код_банка]) " +
                "on адрес.[код_адреса] = поставщик.[код_адреса] " +
                "where поставщик.[код_поставщика] = @id";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", supplierID);
            OleDbDataReader reader = command.ExecuteReader();
            reader.Read();

            name_t.Text = reader.GetString(0);
            tel_t.Text = reader.GetString(1);
            payment_t.Text = reader.GetString(2);
            inn_t.Text = reader.GetString(3);
            address_t.Text = reader.GetString(4) + " " + reader.GetString(5);
            bank_t.Text = reader.GetString(6);

            connection.Close();
        }

        public InfoForm(int supplier_id)
        {
            InitializeComponent();

            supplierID = supplier_id;
            GetInfo();
        }
    }
}
