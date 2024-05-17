using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;
using System.Configuration;
namespace CarRent
{
    public partial class LoginForm : Form
    {
        private MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);

        public LoginForm()
        {
            InitializeComponent();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            string username = username_text.Text;
            string database_password = Checkuser(username);

            if (database_password == "User not found")
            {
                MessageBox.Show("Invalid username", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (database_password == "Error")
            {
                MessageBox.Show("An error occurred while fetching user data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {  
                string new_passsword = GetMd5Hash(password_text.Text);
                if (database_password == new_passsword)
                {
                    Form1 Dashboard = new Form1();
                    Dashboard.Show();
                    this.Hide();

                }
                else {
                    MessageBox.Show("Error Username or Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
        private string Checkuser(string username)
        {
            string checksql = $"SELECT password FROM user WHERE username = @username";
            MySqlCommand cmd = new MySqlCommand(checksql, connection);
            cmd.Parameters.AddWithValue("@username", username);

            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return reader["password"].ToString();
                    }
                    else
                    {
                        return "User not found";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred Fetching the user: " + ex.Message);
                return "Error";
            }
            finally
            {
                connection.Close();
            }
        }


        private static string GetMd5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
   
                StringBuilder builder = new StringBuilder();


                for (int i = 0; i < data.Length; i++)
                {
                    builder.Append(data[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
