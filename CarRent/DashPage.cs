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
    public partial class DashPage : Form
    {
        private MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
        public DashPage()
        {
            InitializeComponent();
            FetchRentLogs();
            FetchingCounts();
            isActivated();
            newCustomer();
        }

     

        public void isActivated() {
            FetchingCounts();
            FetchRentLogs();
            newCustomer();
        }


        private void FetchRentLogs() {
            string query = "SELECT tt.invoice_no,tt.return_at,tt.added_at,tt.total_amount,tt.status,c.plate_no FROM transaction_table as tt JOIN cars as c ON tt.car_id = c.id ORDER BY tt.id DESC LIMIT 4 ";
            MySqlCommand fetchLog = new MySqlCommand(query, connection);
            
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                using (MySqlDataReader reader = fetchLog.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DashBoardLoagControl dashboard_log = new DashBoardLoagControl();
                        dashboard_log.invoice_no_method = reader["invoice_no"].ToString().ToUpper();
                        dashboard_log.return_at_method = reader["return_at"].ToString();
                        dashboard_log.rent_at_method = reader["added_at"].ToString();
                        dashboard_log.plate_no_method = reader["plate_no"].ToString().ToUpper();
                        dashboard_log.total_amoun_method = Convert.ToDouble(reader["total_amount"]);
                        dashboard_log.status_id_method = reader.GetInt32("status");
                        flowLayoutPanel1.Controls.Add(dashboard_log);
                    }
                }


            }
            catch (Exception ex)
            {

                MessageBox.Show("Error fetching new Rent: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }


        }
        int ExecuteScalarInt(MySqlConnection connection, string query)
        {
            MySqlCommand command = new MySqlCommand(query, connection);
            object result = command.ExecuteScalar();
            if (result != null && int.TryParse(result.ToString(), out int value))
            {
                return value;
            }
            else
            {
                throw new Exception("Unexpected result for query: " + query);
            }
        }
        private void FetchingCounts() {
            string count_cars = $"SELECT COUNT(*) FROM cars WHERE status = 0";
            string count_rented_cars = $"SELECT COUNT(*) FROM cars WHERE status = 1 OR status = 4";
            string count_customer = $"SELECT COUNT(*) FROM customer";
            string count_revenue = "SELECT SUM(total_amount) AS revenue FROM transaction_table WHERE added_at >= DATE_SUB(NOW(), INTERVAL 7 DAY) AND status != 3";
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                int totalCars = ExecuteScalarInt(connection, count_cars);
                total_car_text.Text = totalCars.ToString("D2");
                circle_chart.Maximum = totalCars;
                int rentedCars = ExecuteScalarInt(connection, count_rented_cars);
                total_rented_car_text.Text = rentedCars.ToString("D2");
                circle_chart.Value = rentedCars;
                int totalCustomers = ExecuteScalarInt(connection, count_customer);
                customer_text.Text = totalCustomers.ToString("D2");
                int revenue_7Days = ExecuteScalarInt(connection, count_revenue);
                revenue_text.Text = "$ "+revenue_7Days.ToString("D2");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

        }

        private void newCustomer() {
            string sql = "SELECT first_name,last_name,contact_no,driver_license_no,added_at FROM customer ORDER BY id DESC LIMIT 2";
            MySqlCommand command = new MySqlCommand(sql, connection);

            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DashBoardCustomerControl dashboard_customer = new DashBoardCustomerControl();
                        dashboard_customer.license_no_method = reader["driver_license_no"].ToString().ToUpper();
                        dashboard_customer.contact_no_method = reader["contact_no"].ToString();
                        dashboard_customer.first_name_method = reader["first_name"].ToString().ToUpper();
                        dashboard_customer.last_name_method = reader["last_name"].ToString().ToUpper();
                        dashboard_customer.added_at_method = reader["added_at"].ToString();
                        flowLayoutPanel2.Controls.Add(dashboard_customer);
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching new customer: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

        }
    }
}
