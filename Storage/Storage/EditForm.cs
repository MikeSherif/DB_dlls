﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Storage
{
    public partial class EditForm : Form
    {
        private string oledb_attrs = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BCompany.mdb";
        private int storageID;
        private string curMaterial, curDate, curSupplier, curUnit, countM;

        private void GetInfo()
        {
            materialGrid.Rows.Clear();
            suppliersGrid.Rows.Clear();

            OleDbConnection connection = new OleDbConnection(oledb_attrs);
            connection.Open();

            string cmd = "select код_материала, название from стройматериал";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            OleDbDataReader reader = command.ExecuteReader();
            int counter = 0;
            int select_id = 0;

            while (reader.Read())
            {
                materialGrid.Rows.Add();
                materialGrid.Rows[counter].Cells[0].Value = reader.GetInt32(0).ToString();
                materialGrid.Rows[counter].Cells[1].Value = reader.GetString(1);

                counter++;
            }

            cmd = "select код_поставщика, фио_рук from поставщик";
            command = new OleDbCommand(cmd, connection);
            reader = command.ExecuteReader();
            counter = 0;

            while (reader.Read())
            {
                suppliersGrid.Rows.Add();
                suppliersGrid.Rows[counter].Cells[0].Value = reader.GetInt32(0).ToString();
                suppliersGrid.Rows[counter].Cells[1].Value = reader.GetString(1);
                select_id = suppliersGrid.Rows[counter].Cells[1].Value.Equals(curSupplier) ? counter : select_id;

                counter++;
            }

            if (suppliersGrid.Rows.Count > 0)
            {
                suppliersGrid.CurrentCell = suppliersGrid.Rows[select_id].Cells[0];
            }

            cmd = "select название from единица_измерения";
            command = new OleDbCommand(cmd, connection);
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                unitsComboBox.Items.Add(reader.GetString(0));
            }

            cmd = "select перечень_в_поставках.закупочная_цена " +
                "from (склад inner join партия_материала on склад.[код_на_складе] = партия_материала.[код_на_складе]) " +
                "inner join перечень_в_поставках on партия_материала.[код_партии] = перечень_в_поставках.[код_партии] " +
                "where склад.код_на_складе = @id";
            command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", storageID);
            reader = command.ExecuteReader();
            reader.Read();
            string price = reader.GetString(0);

            connection.Close();

            itemsGrid.Rows.Add();
            itemsGrid.Rows[0].Cells[0].Value = curMaterial;
            itemsGrid.Rows[0].Cells[1].Value = curUnit;
            itemsGrid.Rows[0].Cells[2].Value = countM;
            itemsGrid.Rows[0].Cells[3].Value = curSupplier;
            itemsGrid.Rows[0].Cells[4].Value = curDate;
            itemsGrid.Rows[0].Cells[5].Value = price;
        }

        public EditForm(int storage_id, string material, string unit, string count, string supplier, string date)
        {
            InitializeComponent();

            storageID = storage_id;
            countM = count;
            curMaterial = material;
            curUnit = unit;
            curSupplier = supplier;
            curDate = date;
            GetInfo();

            dateTimePicker1.MinDate = DateTime.Now;
            hideLabel.Visible = itemsGrid.Rows.Count > 0;
            suppliersGrid.Enabled = itemsGrid.Rows.Count == 0;
        }

        private void add_b_Click(object sender, EventArgs e)
        {
            if (count_t.Text.Length == 0 || price_t.Text.Length == 0 || unitsComboBox.Text.Length == 0 || materialGrid.SelectedRows.Count == 0 || suppliersGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Введите все данные!");
                return;
            }
            else if (!AddForm.ValidatePriceAndCount(count_t.Text) || !AddForm.ValidatePriceAndCount(price_t.Text))
            {
                MessageBox.Show("Все числа должны быть целочисленными и писаться без специальных знаков и пробелов!");
                return;
            }

            int id = itemsGrid.Rows.Add();
            itemsGrid.Rows[id].Cells[0].Value = materialGrid.SelectedRows[0].Cells[1].Value.ToString();
            itemsGrid.Rows[id].Cells[1].Value = unitsComboBox.Text;
            itemsGrid.Rows[id].Cells[2].Value = count_t.Text;
            itemsGrid.Rows[id].Cells[3].Value = suppliersGrid.SelectedRows[0].Cells[1].Value.ToString();
            itemsGrid.Rows[id].Cells[4].Value = dateTimePicker1.Value.ToString("dd/MM/yyyy");
            itemsGrid.Rows[id].Cells[5].Value = price_t.Text;

            hideLabel.Visible = itemsGrid.Rows.Count > 0;
            suppliersGrid.Enabled = itemsGrid.Rows.Count == 0;
        }

        private void remove_b_Click(object sender, EventArgs e)
        {
            if (itemsGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите запись в таблице!");
                return;
            }

            DialogResult result = MessageBox.Show("Вы действительно хотите убрать эту запись?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No)
                return;

            itemsGrid.Rows.Remove(itemsGrid.SelectedRows[0]);

            hideLabel.Visible = itemsGrid.Rows.Count > 0;
            suppliersGrid.Enabled = itemsGrid.Rows.Count == 0;
        }

        private void complete_b_Click(object sender, EventArgs e)
        {
            if (itemsGrid.Rows.Count == 0)
            {
                MessageBox.Show("Выберите материалы для оформления поставки!");
                return;
            }

            OleDbConnection connection = new OleDbConnection(oledb_attrs);
            connection.Open();

            foreach (DataGridViewRow row in itemsGrid.Rows)
            {
                string cmd;
                OleDbCommand command;
                int materialID, unitID, lotID, supplyID, storageID2;

                if (row == itemsGrid.Rows[0])
                {                    
                    cmd = "select код_материала from стройматериал where название = @name";
                    command = new OleDbCommand(cmd, connection);
                    command.Parameters.AddWithValue("@name", row.Cells[0].Value.ToString());
                    materialID = (int)command.ExecuteScalar();

                    cmd = "select код_единицы from единица_измерения where название = @name";
                    command = new OleDbCommand(cmd, connection);
                    command.Parameters.AddWithValue("@name", row.Cells[1].Value.ToString());
                    unitID = (int)command.ExecuteScalar();

                    cmd = "update склад set код_материала = @1, код_единицы = @2, остаток = @3, код_поставщика = @4 where код_на_складе = @id";
                    command = new OleDbCommand(cmd, connection);
                    command.Parameters.AddWithValue("@1", materialID);
                    command.Parameters.AddWithValue("@2", unitID);
                    command.Parameters.AddWithValue("@3", row.Cells[2].Value.ToString());
                    command.Parameters.AddWithValue("@4", Convert.ToInt32(suppliersGrid.SelectedRows[0].Cells[0].Value));
                    command.Parameters.AddWithValue("@id", storageID);
                    command.ExecuteNonQuery();

                    cmd = "select код_партии from партия_материала where код_на_складе = @id";
                    command = new OleDbCommand(cmd, connection);
                    command.Parameters.AddWithValue("@id", storageID);
                    lotID = (int)command.ExecuteScalar();

                    cmd = "update партия_материала set количество = @1, код_единицы = @2 where код_партии = @id";
                    command = new OleDbCommand(cmd, connection);
                    command.Parameters.AddWithValue("@1", row.Cells[2].Value.ToString());
                    command.Parameters.AddWithValue("@2", unitID);
                    command.Parameters.AddWithValue("@id", lotID);
                    command.ExecuteNonQuery();

                    cmd = "select код_поставки from перечень_в_поставках where код_партии = @id";
                    command = new OleDbCommand(cmd, connection);
                    command.Parameters.AddWithValue("@id", lotID);
                    supplyID = (int)command.ExecuteScalar();

                    cmd = "update перечень_в_поставках set закупочная_цена = @1 where код_партии = @id and код_поставки = @id2";
                    command = new OleDbCommand(cmd, connection);
                    command.Parameters.AddWithValue("@1", row.Cells[5].Value.ToString());
                    command.Parameters.AddWithValue("@id", lotID);
                    command.Parameters.AddWithValue("@id2", supplyID);
                    command.ExecuteNonQuery();

                    cmd = "update поставка set дата = @1, код_поставщика = @2 where код_поставки = @id";
                    command = new OleDbCommand(cmd, connection);
                    command.Parameters.AddWithValue("@1", row.Cells[4].Value.ToString());
                    command.Parameters.AddWithValue("@2", Convert.ToInt32(suppliersGrid.SelectedRows[0].Cells[0].Value));
                    command.Parameters.AddWithValue("@id", supplyID);
                    command.ExecuteNonQuery();

                    continue;
                }

                cmd = "insert into поставка (дата, код_поставщика) values (@1, @2)";
                command = new OleDbCommand(cmd, connection);
                command.Parameters.AddWithValue("@1", row.Cells[4].Value.ToString());
                command.Parameters.AddWithValue("@2", Convert.ToInt32(suppliersGrid.SelectedRows[0].Cells[0].Value));
                command.ExecuteNonQuery();

                cmd = "select MAX(код_поставки) from поставка";
                command = new OleDbCommand(cmd, connection);
                supplyID = (int)command.ExecuteScalar();

                cmd = "select код_материала from стройматериал where название = @name";
                command = new OleDbCommand(cmd, connection);
                command.Parameters.AddWithValue("@name", row.Cells[0].Value.ToString());
                materialID = (int)command.ExecuteScalar();

                cmd = "select код_единицы from единица_измерения where название = @name";
                command = new OleDbCommand(cmd, connection);
                command.Parameters.AddWithValue("@name", row.Cells[1].Value.ToString());
                unitID = (int)command.ExecuteScalar();

                cmd = "insert into склад (код_материала, код_единицы, остаток, код_поставщика) values (@1, @2, @3, @4)";
                command = new OleDbCommand(cmd, connection);
                command.Parameters.AddWithValue("@1", materialID);
                command.Parameters.AddWithValue("@2", unitID);
                command.Parameters.AddWithValue("@3", row.Cells[2].Value.ToString());
                command.Parameters.AddWithValue("@4", Convert.ToInt32(suppliersGrid.SelectedRows[0].Cells[0].Value));
                command.ExecuteNonQuery();

                cmd = "select MAX(код_на_складе) from склад";
                command = new OleDbCommand(cmd, connection);
                storageID2 = (int)command.ExecuteScalar();

                cmd = "insert into партия_материала (код_на_складе, количество, код_единицы) values (@1, @2, @3)";
                command = new OleDbCommand(cmd, connection);
                command.Parameters.AddWithValue("@1", storageID2);
                command.Parameters.AddWithValue("@2", row.Cells[2].Value.ToString());
                command.Parameters.AddWithValue("@3", unitID);
                command.ExecuteNonQuery();

                cmd = "select MAX(код_партии) from партия_материала";
                command = new OleDbCommand(cmd, connection);
                lotID = (int)command.ExecuteScalar();

                cmd = "insert into перечень_в_поставках (код_поставки, код_партии, закупочная_цена) values (@1, @2, @3)";
                command = new OleDbCommand(cmd, connection);
                command.Parameters.AddWithValue("@1", supplyID);
                command.Parameters.AddWithValue("@2", lotID);
                command.Parameters.AddWithValue("@3", row.Cells[5].Value.ToString());
                command.ExecuteNonQuery();
            }

            connection.Close();

            MessageBox.Show("Запись успешно изменена!");
            this.Close();
        }
    }
}