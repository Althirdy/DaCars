using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;


namespace CarRent
{
    public partial class AddCustomer : Form
    {
        private MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
        public delegate void CustomerAddedEventHandler();
        public event CustomerAddedEventHandler CustomerAdded;
        public AddCustomer()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            DateTime date = DateTime.Now;
            String first_name, last_name, contact_no, email, added_at,license_no;
            first_name = first_name_text.Text;
            last_name = last_name_text.Text;
            contact_no = contact_no_text.Text;
            email = email_text.Text;
            added_at = date.ToString("yyyy-MM-dd");
            license_no = license_no_text.Text;
            if (string.IsNullOrWhiteSpace(first_name) ||
                 string.IsNullOrWhiteSpace(last_name) ||
                 string.IsNullOrWhiteSpace(contact_no) ||
                 string.IsNullOrWhiteSpace(email) ||
                 string.IsNullOrWhiteSpace(license_no))
            {
                MessageBox.Show("Error: All fields are required.", "Fields Error");
            }
            else if (!Regex.IsMatch(contact_no, @"^\d{11}$"))
            {
                MessageBox.Show("Error: Contact number must contain exactly 11 numeric characters.", "Validation Error");
            }
            //Checking
            string checkQueryString = "SELECT * FROM customer WHERE driver_license_no = @license_no Or email=@email";
            MySqlCommand checkCommand = new MySqlCommand(checkQueryString, connection);
            checkCommand.Parameters.AddWithValue("@license_no", license_no);
            checkCommand.Parameters.AddWithValue("@email", email);


            //Insert
            string sqlInsert = "INSERT INTO `customer`(`first_name`, `last_name`, `contact_no`, `email`, `driver_license_no`, `added_at`) VALUES (@first_name,@last_name,@contact_no,@email,@driver_license_no,@added_at)";
            MySqlCommand command_query = new MySqlCommand(sqlInsert, connection);
            command_query.Parameters.AddWithValue("@first_name", first_name);
            command_query.Parameters.AddWithValue("@last_name", last_name);
            command_query.Parameters.AddWithValue("@contact_no", contact_no);
            command_query.Parameters.AddWithValue("@email", email);
            command_query.Parameters.AddWithValue("@driver_license_no", license_no);
            command_query.Parameters.AddWithValue("@added_at", added_at);


            try {
                connection.Open();
                MySqlDataReader reader = checkCommand.ExecuteReader();
                if (reader.Read())
                {
                    MessageBox.Show($"The customer that has a Driver License No. {license_no} or Email {email} is already save in the database");
                    return;
                }
            }
            catch (Exception ex) {
                MessageBox.Show(" Error: Checking Error Add Customer \n"  + ex.Message);

            }
            finally {
                connection.Close();
            }


            try {
                connection.Open();
                if (command_query.ExecuteNonQuery() == 1)
                {

                    MessageBox.Show("Successfully added");
                    last_name_text.Text = "";
                    first_name_text.Text = "";
                    contact_no_text.Text = "";
                    license_no_text.Text = "";
                    email_text.Text = "";

                    CustomerAdded?.Invoke();


                }
            } catch (Exception ex) {
                MessageBox.Show("Saving Error" + ex.Message);
            }
            finally {
                connection.Close();
            }

        }
    }
}
