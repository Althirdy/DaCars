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
    public partial class transactionPage : Form
    {
        private MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
        private int pageSize = 10;
        private int currentPage = 1;
        private int limit = 10;
        private Add_Transaction trans_page = new Add_Transaction();
       private int totalPages;
        public transactionPage()
        {
            InitializeComponent();
            FetchingData(null);
            trans_page.transactionAdded += ReloadForm;
 
        }
        private void Updated() {
            FetchingData(null);
        }
        private void ReloadForm()
        {
            FetchingData(null);
        }

     
        
        private void FetchingData(string searchTerm)
        {
            try
            {
                page_count.Text = currentPage.ToString();
                connection.Open();



                // Query to count total transactions
                string query = string.IsNullOrEmpty(searchTerm)
                  ? "SELECT COUNT(*) FROM transaction_table"
                  : "SELECT COUNT(*) FROM transaction_table " + 
                    "JOIN cars ON transaction_table.car_id = cars.id " + 
                    "WHERE transaction_table.invoice_no LIKE @invoice OR cars.plate_no LIKE @invoice";

                MySqlCommand countCommand = new MySqlCommand(query, connection);
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    countCommand.Parameters.AddWithValue("@invoice", $"%{searchTerm}%");
                }
                int totalTransactions = Convert.ToInt32(countCommand.ExecuteScalar());
                totalPages = (totalTransactions + pageSize - 1) / pageSize;
                // Query to fetch transaction data with pagination
                string query_select = $@"SELECT tt.id,tt.invoice_no, tt.total_amount, tt.status, c.car_name, c.plate_no, cu.first_name, cu.last_name, cu.contact_no 
                  FROM transaction_table AS tt 
                  JOIN cars AS c ON tt.car_id = c.id 
                  JOIN customer AS cu ON tt.client_id = cu.id 
                  {(string.IsNullOrEmpty(searchTerm) ? "" : "WHERE tt.invoice_no LIKE @invoice OR c.plate_no LIKE @invoice")} 
                  ORDER BY tt.added_at DESC 
                  LIMIT {pageSize} OFFSET {(currentPage - 1) * pageSize}";

                MySqlCommand command = new MySqlCommand(query_select, connection);
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    command.Parameters.AddWithValue("@invoice", $"%{searchTerm}%");
                }
                // Execute query and fill data into DataTable
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                int rowCount = dataTable.Rows.Count;

                // Clear existing controls in the flowLayoutPanel
                flowLayoutPanel1.Controls.Clear();

                // Process retrieved data
                if (rowCount != 0)
                {
                    transactionControl[] transactions = new transactionControl[rowCount];
                    limit = rowCount;
                    for (int i = 0; i < rowCount; i++)
                    {
                        string invoiceNo = dataTable.Rows[i]["invoice_no"].ToString().ToUpper();
                        String car_name = dataTable.Rows[i]["car_name"].ToString().ToUpper();
                        string full_name = $"{dataTable.Rows[i]["first_name"].ToString().ToUpper()} {dataTable.Rows[i]["last_name"].ToString().ToUpper()}";
                        transactions[i] = new transactionControl();
                        transactions[i].invoice_method = invoiceNo.Length > 10 ? invoiceNo.Substring(0, 10) + "..." : invoiceNo;
                        transactions[i].full_name_method = full_name.Length > 10 ? full_name.Substring(0, 10) + "..." : full_name;
                        transactions[i].car_name_method = car_name.Length > 7 ? car_name.Substring(0, 7) + "..." : car_name;
                        transactions[i].plate_no_method = dataTable.Rows[i]["plate_no"].ToString().ToUpper();
                        transactions[i].price_method = Convert.ToDouble(dataTable.Rows[i]["total_amount"]);
                        transactions[i].contact_no_method = dataTable.Rows[i]["contact_no"].ToString();
                        transactions[i].status_method = Convert.ToInt32(dataTable.Rows[i]["status"]);
                        transactions[i].trans_id_method = Convert.ToInt32(dataTable.Rows[i]["id"]);
                        flowLayoutPanel1.Controls.Add(transactions[i]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database Connection Error: Fetching Transaction Data\n{ex.StackTrace}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            trans_page.Show();
        }

        private void Add_pagination(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                string searchTerm = string.IsNullOrWhiteSpace(search_text.Text) ? null : search_text.Text;
                FetchingData(searchTerm);
                limit = 0;

            }
        }

        private void Substract_pagination(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                string searchTerm = string.IsNullOrWhiteSpace(search_text.Text) ? null : search_text.Text;
                FetchingData(searchTerm);
            }
        }

        private void search_(object sender, KeyEventArgs e)
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

        private void search_key_up(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                if (search_text.Text == "")
                {
                    FetchingData(null);
                }
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            FetchingData(null);
        }
    }
}
