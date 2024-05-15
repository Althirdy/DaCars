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

namespace CarRent
{
    public partial class carPage : Form
    {
        private MySqlConnection connection;
        private int pageSize = 10;
        private int currentPage = 1;
        private int limit = 10;
        private AddCars add_car = new AddCars();
        private int totalPages = 0;
        public carPage()
        {
            InitializeComponent();
            DBConnection();
            add_car.carAdded += ReloadForm;

        }
        private void ReloadForm()
        {
            FetchingData(null);
        }
        private void DBConnection() {

            connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
        
        }

        private void FetchingData(string search)
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                page_count.Text = currentPage.ToString() + "...";
                connection.Open();

                string query;
                if (string.IsNullOrWhiteSpace(search))
                {
                    query = $"SELECT COUNT(*) FROM cars"; 
                    MySqlCommand countCommand = new MySqlCommand(query, connection);
                    int totalCars = Convert.ToInt32(countCommand.ExecuteScalar());
                    totalPages = (totalCars + pageSize - 1) / pageSize; 
                }
                else
                {
                    query = "SELECT COUNT(*) FROM cars WHERE plate_no LIKE @search"; 
                    MySqlCommand countCommand = new MySqlCommand(query, connection);
                    countCommand.Parameters.AddWithValue("@search", $"%{search}%");
                    int totalCars = Convert.ToInt32(countCommand.ExecuteScalar());
                    totalPages = (totalCars + pageSize - 1) / pageSize;
                }

                if (currentPage <= totalPages) 
                {
                 
                    query = string.IsNullOrWhiteSpace(search) ?
                        $"SELECT * FROM cars ORDER BY added_at DESC LIMIT {pageSize} OFFSET {(currentPage - 1) * pageSize}" :
                        "SELECT * FROM cars WHERE plate_no LIKE @search ORDER BY added_at DESC LIMIT @pageSize OFFSET @offset";

                    MySqlCommand command = new MySqlCommand(query, connection);

                    if (!string.IsNullOrWhiteSpace(search))
                    {
                        command.Parameters.AddWithValue("@search", $"%{search}%");
                        command.Parameters.AddWithValue("@pageSize", pageSize);
                        command.Parameters.AddWithValue("@offset", (currentPage - 1) * pageSize);
                    }

                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    int rowCount = dataTable.Rows.Count;
                    if (rowCount != 0)
                    {
                        CarTableRow[] car_data = new CarTableRow[rowCount];
                        for (int i = 0; i < rowCount; i++)
                        {
                            car_data[i] = new CarTableRow();
                            car_data[i].car_name = dataTable.Rows[i]["car_name"].ToString().ToUpper();
                            car_data[i].id = (int)dataTable.Rows[i]["id"];
                            car_data[i].car_model = dataTable.Rows[i]["car_model"].ToString().ToUpper();
                            car_data[i].plateno = dataTable.Rows[i]["plate_no"].ToString().ToUpper();
                            car_data[i].color = dataTable.Rows[i]["color"].ToString().ToUpper();
                            car_data[i].price = (double)dataTable.Rows[i]["price"];
                            car_data[i].stats_method = (int)dataTable.Rows[i]["status"];
                            flowLayoutPanel1.Controls.Add(car_data[i]);
                        }
                    }
                }
                else
                {
                    currentPage = totalPages;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Connection Error: Fetching Cars Data" + ex.StackTrace);
            }
            finally
            {
                connection.Close();
            }
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

        

        private void guna2Button3_Click_1(object sender, EventArgs e)
        {
            add_car.Show();
        }

        private void search_text_Enter(object sender, EventArgs e)
        {
            search_text.KeyDown += search_;

        }

        private void search_(object sender, KeyEventArgs e)
        {
            // Check if the Enter key is pressed
            if (e.KeyCode == Keys.Enter)
            {
                if (search_text.Text != "") { 
                 currentPage = 1;
                page_count.Text = currentPage.ToString() + "...";
                string search = search_text.Text;

                FetchingData(search);


                e.Handled = true;
                e.SuppressKeyPress = true;
                }
            }
        }

        private void search_text_Leave(object sender, EventArgs e)
        {
            search_text.KeyDown -= search_;
        }

        private void Up(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                if (search_text.Text == "")
                {
                    FetchingData(null);
                }
            }
        }

        private void CarPageLoad(object sender, EventArgs e)
        {
            FetchingData(null);

        }
    }
}
