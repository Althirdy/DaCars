using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace CarRent
{
    public partial class EditCustomer : Form
    {
        private MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
        private int pageSize = 6;
        private int currentPage = 1;
        private int totalPages = 0;
        private int client_id;
        public EditCustomer()
        {
            InitializeComponent();
        }

        public void setClientId(int clientid) {
            this.client_id = clientid;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        private void FetchingData()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                page_count.Text = currentPage.ToString() + "...";
                connection.Open();

                string countQuery = $"SELECT COUNT(*) FROM transaction_table WHERE client_id = @client_id";
                MySqlCommand countCommand = new MySqlCommand(countQuery, connection);
                countCommand.Parameters.AddWithValue("@client_id", client_id);
                int totalCars = Convert.ToInt32(countCommand.ExecuteScalar());
                totalPages = (totalCars + pageSize - 1) / pageSize;

                if (currentPage <= totalPages)
                {
                    int offset = Math.Max(0, (currentPage - 1) * pageSize);

                    // Construct main query with pagination
                    string query = @"SELECT tt.id, tt.added_at, tt.invoice_no, tt.status, cars.plate_no 
                             FROM transaction_table as tt 
                             JOIN cars ON tt.car_id = cars.id 
                             WHERE tt.client_id = @client_id 
                             ORDER BY tt.added_at DESC 
                             LIMIT @pageSize OFFSET @offset";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@client_id", client_id);
                    command.Parameters.AddWithValue("@pageSize", pageSize);
                    command.Parameters.AddWithValue("@offset", offset);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            EditCustomerControl customer_control = new EditCustomerControl();
                            customer_control.invoice_method = reader["invoice_no"].ToString().ToUpper();
                            customer_control.plate_no_method = reader["plate_no"].ToString().ToUpper();
                            customer_control.trans_id_method = reader.GetInt32("id");
                            customer_control.rented_at_method = Convert.ToDateTime(reader["added_at"]); // Convert DateTime to string
                            customer_control.status_method = reader.GetInt32("status");
                            flowLayoutPanel1.Controls.Add(customer_control);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Connection Error: Fetching Cars Data \n" + ex.Message + ex.StackTrace);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }


        private void FetchCustomerData()
        {
            string query = $"SELECT c.first_name, c.last_name, c.contact_no, c.email, c.driver_license_no FROM customer as c WHERE c.id = {client_id}";
            MySqlCommand command = new MySqlCommand(query, connection);

            try
            {
                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        first_name_text.Text = reader["first_name"].ToString().ToUpper();
                        last_name_text.Text = reader["last_name"].ToString().ToUpper();
                        contact_no_text.Text = FormatContactNumber(reader["contact_no"].ToString()); 
                        email_text.Text = reader["email"].ToString().ToUpper();
                        license_no_text.Text = reader["driver_license_no"].ToString().ToUpper();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching customer data: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close(); // Ensure the connection is closed even if an error occurs
                }
            }
        }
        private void EditCustomerLoad(object sender, EventArgs e)
        {
            FetchingData();
            FetchCustomerData();
        }

        private void AddPagination(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                FetchingData();
            }
        }

        private void SubtractPagination(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                FetchingData();
            }
        }

        private string FormatContactNumber(string contactNumber)
        {
            string digitsOnly = new string(contactNumber.Where(char.IsDigit).ToArray());

            if (digitsOnly.Length == 11)
            {
                return string.Format("{0:0###-###-####}", long.Parse(digitsOnly));
            }
            else
            {
                return contactNumber;
            }
        }
    }
}
