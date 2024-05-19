using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Configuration;
using System.Data;
using System;

namespace CarRent
{
    public partial class customerPage : Form
    {

        private MySqlConnection connection;
        private int pageSize = 10;
        private int currentPage = 1;
        private int limit = 10;
        private AddCustomer add_customer = new AddCustomer();
        int totalPages;
        public customerPage()
        {
            InitializeComponent();
            DBConnection();
            add_customer.CustomerAdded += ReloadForm;

        }

        private void ReloadForm() {
            FetchingData(null);
        }
        private void DBConnection()
        {

            connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);

        }



        //Fetchin Data of Customers
        private void FetchingData(string search)
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                page_count.Text = currentPage.ToString() + "...";
                connection.Open();

                // Query to count total customers
                string countQuery = "SELECT COUNT(*) FROM customer";
                // Query to fetch customer data
                string query = @"SELECT customer.id, customer.first_name, customer.last_name, customer.contact_no, 
                               customer.email, customer.added_at, COUNT(transaction_table.client_id) AS rent_count 
                        FROM customer 
                        LEFT JOIN transaction_table ON customer.id = transaction_table.client_id";

                // Add search conditions if search term is provided
                if (!string.IsNullOrWhiteSpace(search))
                {
                    countQuery += " WHERE customer.driver_license_no LIKE @driver OR customer.first_name LIKE @first_name OR customer.last_name LIKE @last_name";
                    query += @" WHERE customer.driver_license_no LIKE @driver 
                         OR customer.first_name LIKE @first_name 
                         OR customer.last_name LIKE @last_name";
                }

                // Execute count query to get total customers
                MySqlCommand countCommand = new MySqlCommand(countQuery, connection);
                if (!string.IsNullOrWhiteSpace(search))
                {
                    countCommand.Parameters.AddWithValue("@driver", $"%{search}%");
                    countCommand.Parameters.AddWithValue("@first_name", $"%{search}%");
                    countCommand.Parameters.AddWithValue("@last_name", $"%{search}%");
                }
                int totalCustomers = Convert.ToInt32(countCommand.ExecuteScalar());
                // Calculate total pages
                totalPages = (totalCustomers + pageSize - 1) / pageSize;

                // Add pagination conditions to the fetch query
                query += " GROUP BY customer.id ORDER BY customer.added_at DESC LIMIT @pageSize OFFSET @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                if (!string.IsNullOrWhiteSpace(search))
                {
                    command.Parameters.AddWithValue("@driver", $"%{search}%");
                    command.Parameters.AddWithValue("@first_name", $"%{search}%");
                    command.Parameters.AddWithValue("@last_name", $"%{search}%");
                }
                command.Parameters.AddWithValue("@pageSize", pageSize);
                command.Parameters.AddWithValue("@offset", (currentPage - 1) * pageSize);

                // Execute fetch query
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Process retrieved data
                int rowCount = dataTable.Rows.Count;
                if (rowCount != 0)
                {
                    customerControl[] customer_data = new customerControl[rowCount];
                    limit = rowCount;
                    for (int i = 0; i < rowCount; i++)
                    {
                        customer_data[i] = new customerControl();
                        customer_data[i].first_name_method = dataTable.Rows[i]["first_name"].ToString().ToUpper();
                        customer_data[i].id_method = (int)dataTable.Rows[i]["id"];
                        customer_data[i].last_name_method = dataTable.Rows[i]["last_name"].ToString().ToUpper();
                        customer_data[i].contact_no_method = dataTable.Rows[i]["contact_no"].ToString().ToUpper();
                        customer_data[i].email_method = dataTable.Rows[i]["email"].ToString().ToUpper();
                        object rentCountObj = dataTable.Rows[i]["rent_count"];
                        if (rentCountObj != DBNull.Value && int.TryParse(rentCountObj.ToString(), out int rentCount))
                        {
                            customer_data[i].no_rent_method = rentCount;
                        }
                        customer_data[i].added_at_method = dataTable.Rows[i]["added_at"].ToString();
                        flowLayoutPanel1.Controls.Add(customer_data[i]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Connection Error: Fetching Customer Data " + ex.Message + ex.StackTrace);
            }
            finally
            {
                connection.Close();
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            add_customer.Show();
        }

        private void CustomerLoad(object sender, EventArgs e)
        {
            FetchingData(null);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

            if (currentPage < totalPages)
            {
                currentPage++;
                string searchTerm = string.IsNullOrWhiteSpace(search_text.Text) ? null : search_text.Text;
                FetchingData(searchTerm);
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {

                currentPage--;
                string searchTerm = string.IsNullOrWhiteSpace(search_text.Text) ? null : search_text.Text;
                FetchingData(searchTerm);

            }
        }

        private void searchDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (search_text.Text != "")
                {
                    currentPage = 1;
                    page_count.Text = currentPage.ToString() + "...";
                    string search = search_text.Text;

                    FetchingData(search);


                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
            }
        }



        private void KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                // Check if the text box is empty
                if (search_text.Text == "")
                {
                    // Call the FetchingData method with null parameter
                    FetchingData(null);
                }
            }
        }

        private void ReloadClick(object sender, MouseEventArgs e)
        {
            FetchingData(null);
        }
    }
}
