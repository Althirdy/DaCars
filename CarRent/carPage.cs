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
        private MySqlConnection connection  = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
        private int pageSize = 10;
        private int currentPage = 1;
        private int limit = 10;
        private AddCars add_car = new AddCars();
        private int totalPages = 0;

     
        public carPage()
        {
            InitializeComponent();
            page_count.Text = currentPage.ToString() + "...";
            add_car.carAdded += ReloadForm;

        }
        private void ReloadForm()
        {
            MessageBox.Show("ReloadForm method called");
            FetchingData(null);
        }

        private void FetchingData(string search)
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                page_count.Text = currentPage.ToString() + "...";
                connection.Open();

                string query;
                MySqlCommand countCommand;

                if (string.IsNullOrWhiteSpace(search))
                {
                    query = "SELECT COUNT(*) FROM cars WHERE car_status = 1";
                    countCommand = new MySqlCommand(query, connection);
                }
                else
                {
                    query = "SELECT COUNT(*) FROM cars WHERE plate_no LIKE @search AND car_status = 1";
                    countCommand = new MySqlCommand(query, connection);
                    countCommand.Parameters.AddWithValue("@search", $"%{search}%");
                }

                int totalCars = Convert.ToInt32(countCommand.ExecuteScalar());
                totalPages = (totalCars + pageSize - 1) / pageSize;

                if (currentPage <= totalPages)
                {
                    // Calculate OFFSET value, ensuring it's non-negative
                    int offset = Math.Max(0, (currentPage - 1) * pageSize);

                    // Construct main query with pagination
                    query = string.IsNullOrWhiteSpace(search) ?
                        $"SELECT * FROM cars WHERE car_status =1 ORDER BY added_at DESC LIMIT {pageSize} OFFSET {offset}" :
                        "SELECT * FROM cars WHERE plate_no LIKE @search AND car_status = 1 ORDER BY added_at DESC LIMIT @pageSize OFFSET @offset";

                    MySqlCommand command = new MySqlCommand(query, connection);

                    if (!string.IsNullOrWhiteSpace(search))
                    {
                        command.Parameters.AddWithValue("@search", $"%{search}%");
                        command.Parameters.AddWithValue("@pageSize", pageSize);
                        command.Parameters.AddWithValue("@offset", offset);
                    }

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CarTableRow car = new CarTableRow();
                            car.car_name = reader["car_name"].ToString().ToUpper();
                            car.id = reader.GetInt32("id");
                            car.car_model = reader["car_model"].ToString().ToUpper();
                            car.plateno = reader["plate_no"].ToString().ToUpper();
                            car.color = reader["color"].ToString().ToUpper();
                            car.price = Convert.ToDouble(reader["price"]);
                            car.stats_method = reader.GetInt32("status");
                            flowLayoutPanel1.Controls.Add(car);
                        }
                    }
                }

                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Connection Error: Fetching Cars Data \n" + ex.Message + ex.StackTrace);
            }
            finally
            {
                connection.Close();
            }
        }

        private void guna2Button3_Click_1(object sender, EventArgs e)
        {
            add_car.Show();
        }

        private void CarPageLoad(object sender, EventArgs e)
        {
            FetchingData(null);

        }
        
        //Add Pagination
        private void AddPagination(object sender, MouseEventArgs e)
        {

            if (currentPage < totalPages)
            {
                currentPage++;

                string searchTerm = string.IsNullOrWhiteSpace(search_text.Text) ? null : search_text.Text;
                FetchingData(searchTerm);
            }
        }

        private void SubtractPagination(object sender, MouseEventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                string searchTerm = string.IsNullOrWhiteSpace(search_text.Text) ? null : search_text.Text;
                FetchingData(searchTerm);
            }
        }

        private void ReloadClick(object sender, MouseEventArgs e)
        {
            FetchingData(null);

        }

        private void search_key_down(object sender, KeyEventArgs e)
        {
            //Check if the Enter key is pressed
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
                if (string.IsNullOrWhiteSpace(search_text.Text))
                {
                    FetchingData(null);
                }
            }
        }
    }
}
