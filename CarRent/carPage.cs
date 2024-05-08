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
        public carPage()
        {
            InitializeComponent();
            DBConnection();
            FetchingData();
        }

        private void DBConnection() {

            connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
        
        }

        private void FetchingData() {
            try
            {
              


                //Fetching the data with pagination 
                page_count.Text = currentPage.ToString();
                connection.Open();
                MySqlCommand command = new MySqlCommand($"SELECT * FROM cars ORDER BY added_at DESC LIMIT {pageSize} OFFSET {(currentPage - 1) * pageSize}", connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                int rowCount = dataTable.Rows.Count;
                //iterate sa lahat ng data and pass sa props 
                if (rowCount != 0) {
                    flowLayoutPanel1.Controls.Clear();
                    CarTableRow[] car_data = new CarTableRow[rowCount];
                    limit = rowCount;
                    for (int i = 0; i < rowCount; i++) {
                       
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
            catch (Exception ex)
            {
               
                MessageBox.Show("Database Connection Error: Fetching Cars Data");
            }
            finally {
                connection.Close();
            }
        
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

            if (limit == 10) {
                currentPage++;
                FetchingData();
            }

                
            
            
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                FetchingData();
            }
        }

        

        //For Searching and filtering of data base on CarModel, CarName, CarPlate
        private void Search(object sender, KeyEventArgs e)
        {
            MessageBox.Show(search_text.Text);
        }

        private void guna2Button3_Click_1(object sender, EventArgs e)
        {
            add_car.Show();
        }
    }
}
