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

namespace ChangePassword
{
    public partial class Form1 : Form
    {
        private string oledb_attrs = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BCompany.mdb";
        private int userID;

        public Form1(int id)
        {
            InitializeComponent();
            
            userID = id;
            eng_label.Visible = false;
            rus_label.Visible = false;
            caps_label.Visible = false;
            status_timer.Start();
        }

        public string CreateSHA256(string password)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] hashVal = SHA256.Create().ComputeHash(passwordBytes);

            return BitConverter.ToString(hashVal);
        }

        private bool IsCorrectOldPassword(string passwordHash)
        {
            OleDbConnection connection = new OleDbConnection(oledb_attrs);
            connection.Open();

            string cmd = "select пароль from пользователь where код_пользователя = @id";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", userID);
            OleDbDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                if (!reader.GetString(0).Equals(passwordHash))
                {
                    connection.Close();
                    return false;
                }
            }

            connection.Close();
            return true;
        }

        private bool IsCorrectData()
        {
            if (old_pass.Text.Length == 0 || new_pass.Text.Length == 0)
            {
                MessageBox.Show("Введите все данные!");
                return false;
            }

            string passHash = CreateSHA256(old_pass.Text);
            if (!IsCorrectOldPassword(passHash))
            {
                MessageBox.Show("Старый пароль введен неверно!");
                return false;
            }
            else if (new_pass.Text.Contains(" "))
            {
                MessageBox.Show("Новый пароль не должен содержать пробелы!");
                return false;
            }

            return true;
        }

        private void change_b_Click(object sender, EventArgs e)
        {
            if (!IsCorrectData())
            {
                return;
            }

            string passHash = CreateSHA256(new_pass.Text);
            OleDbConnection connection = new OleDbConnection(oledb_attrs);
            connection.Open();

            string cmd = "update пользователь set пароль = @password where код_пользователя = @id";
            OleDbCommand command = new OleDbCommand(cmd, connection);
            command.Parameters.AddWithValue("@password", passHash);
            command.Parameters.AddWithValue("@id", userID);
            command.ExecuteNonQuery();

            connection.Close();

            MessageBox.Show("Вы успешно сменили пароль!");
            this.Close();
        }

        private void status_timer_Tick(object sender, EventArgs e)
        {
            if (IsKeyLocked(Keys.CapsLock))
            {
                caps_label.Visible = true;
            }
            else
            {
                caps_label.Visible = false;
            }

            if (InputLanguage.CurrentInputLanguage.LayoutName == "США")
            {
                rus_label.Visible = false;
                eng_label.Visible = true;
            }
            else
            {
                eng_label.Visible = false;
                rus_label.Visible = true;
            }
        }
    }
}
