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

namespace Brigades
{
    public partial class AddForm : Form
    {
        private string oledb_attrs = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BCompany.mdb";

        private void GetInfo()
        {
            brigGrid.Rows.Clear();

            OleDbConnection connection = new OleDbConnection(oledb_attrs);
            connection.Open();

            string cmd = "select код_бригадира, фио from бригадир";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            OleDbDataReader reader = command.ExecuteReader();
            int counter = 0;

            while (reader.Read())
            {
                brigGrid.Rows.Add();
                brigGrid.Rows[counter].Cells[0].Value = reader.GetInt32(0).ToString();
                brigGrid.Rows[counter].Cells[1].Value = reader.GetString(1);

                counter++;
            }

            cmd = "select строитель.код_строителя, строитель.фио, специализация.название " +
                "from строитель " +
                "inner join (специализация inner join специализации_строителей on специализация.[код_специализации] = специализации_строителей.[код_специализации]) " +
                "on строитель.[код_строителя] = специализации_строителей.[код_строителя]";
            command = new OleDbCommand(cmd, connection);
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                int workerId = reader.GetInt32(0);
                string workerName = reader.GetString(1);
                string specialization = reader.GetString(2);

                bool found = false;
                foreach (DataGridViewRow row in workersGrid.Rows)
                {
                    if (row.Cells[0].Value != null && (int)row.Cells[0].Value == workerId)
                    {
                        row.Cells[2].Value += ", " + specialization;
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    int rowIndex = workersGrid.Rows.Add();
                    workersGrid.Rows[rowIndex].Cells[0].Value = workerId;
                    workersGrid.Rows[rowIndex].Cells[1].Value = workerName;
                    workersGrid.Rows[rowIndex].Cells[2].Value = specialization;
                }
            }

            connection.Close();
        }

        public AddForm()
        {
            InitializeComponent();

            GetInfo();
        }

        public static bool IsSameName(string name)
        {
            OleDbConnection connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BCompany.mdb");
            connection.Open();

            string cmd = "select count(*) from бригада where название = @name";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@name", name);
            int count = (int)command.ExecuteScalar();

            connection.Close();

            if (count > 0)
                return true;

            return false;
        }

        private void add_spec_b_Click(object sender, EventArgs e)
        {
            if (workersGrid.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = workersGrid.SelectedRows[0];

                int newRowIndex = workersGrid2.Rows.Add();
                DataGridViewRow newRow = workersGrid2.Rows[newRowIndex];

                foreach (DataGridViewCell cell in selectedRow.Cells)
                {
                    newRow.Cells[cell.ColumnIndex].Value = cell.Value;
                }

                workersGrid.Rows.Remove(selectedRow);
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите строителя!");
            }
        }

        private void remove_spec_b_Click(object sender, EventArgs e)
        {
            if (workersGrid2.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = workersGrid2.SelectedRows[0];

                int newRowIndex = workersGrid.Rows.Add();
                DataGridViewRow newRow = workersGrid.Rows[newRowIndex];

                foreach (DataGridViewCell cell in selectedRow.Cells)
                {
                    newRow.Cells[cell.ColumnIndex].Value = cell.Value;
                }

                workersGrid2.Rows.Remove(selectedRow);
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите строителя из бригады (правая таблица)!");
            }
        }

        private void add_b_Click(object sender, EventArgs e)
        {
            if (name_t.Text.Length == 0 || name_t.Text.Contains("  ") || name_t.Text.StartsWith(" ") || name_t.Text.EndsWith(" "))
            {
                MessageBox.Show("Введите корректное название бригады!");
                return;
            }
            else if (IsSameName(name_t.Text))
            {
                MessageBox.Show("Бригада с таким названием уже есть!");
                return;
            }
            else if (brigGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите бригадира!");
                return;
            }
            else if (workersGrid2.Rows.Count == 0)
            {
                MessageBox.Show("Выберите хотя бы одного строителя!");
                return;
            }

            OleDbConnection connection = new OleDbConnection(oledb_attrs);
            connection.Open();

            string cmd = "insert into бригада (название, код_бригадира) values (@1, @2)";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@1", name_t.Text);
            command.Parameters.AddWithValue("@2", Convert.ToInt32(brigGrid.SelectedRows[0].Cells[0].Value));
            command.ExecuteNonQuery();

            cmd = "select max(код_бригады) from бригада";
            command = new OleDbCommand(cmd, connection);
            int brigadeID = (int)command.ExecuteScalar();

            cmd = "insert into состав_бригады (код_бригады, код_строителя) values (@1, @2)";
            foreach (DataGridViewRow row in workersGrid2.Rows)
            {
                command = new OleDbCommand(cmd, connection);
                command.Parameters.AddWithValue("@1", brigadeID);
                command.Parameters.AddWithValue("@2", Convert.ToInt32(row.Cells[0].Value));
                command.ExecuteNonQuery();
            }

            connection.Close();

            MessageBox.Show("Бригада успешно добавлена!");
            this.Close();
        }
    }
}
