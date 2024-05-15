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
using System.Windows.Forms;

namespace CarRent
{
    public partial class Add_Transaction : Form
    {
        private int customer_id, transaction_status;
        public delegate void transactionAddedEventHandler();
        public event transactionAddedEventHandler transactionAdded;
        private int car_id;
        private MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
        private double car_price;
        public Add_Transaction()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void fetchCustomerData(string driverLicense)
        {
            string query = "SELECT id, first_name, last_name, contact_no, email FROM customer WHERE driver_license_no = @driverLicense";

            try
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@driverLicense", driverLicense);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            first_name_text.Text = reader["first_name"].ToString();
                            last_name_text.Text = reader["last_name"].ToString();
                            contact_no_text.Text = reader["contact_no"].ToString();
                            email_text.Text = reader["email"].ToString();
                            customer_id = reader.GetInt32("id");
                        }
                        else
                        {
                            MessageBox.Show("No customer found with the provided driver license number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        private void fetchCarData(string plate_no)
        {
            string query = "SELECT id, car_name, color, status, price FROM cars WHERE plate_no = @plate_no";

            try
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@plate_no", plate_no);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            if (reader.GetInt32("status") == 1 || reader.GetInt32("status") == 4)
                            {
                                MessageBox.Show($"The car with plate no. {plate_no} is rented.", "Car Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                plate_no_text.Text = "";
                            }
                            else
                            {
                                car_name_text.Text = reader["car_name"].ToString();
                                car_color_text.Text = reader["color"].ToString();
                                car_id = reader.GetInt32("id");
                                car_price_text.Text = "$" + reader["price"].ToString();
                                car_price = reader.GetDouble("price");
                            }
                        }
                        else
                        {
                            MessageBox.Show("No car found with the provided plate number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        private void Enterclick(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(license_no.Text))
            {
                if (e.KeyCode == Keys.Enter)
                {
                    fetchCustomerData(license_no.Text);
                }
            }
        }



        private void EnterKey(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(plate_no_text.Text))
            {
                if (e.KeyCode == Keys.Enter)
                {
                    fetchCarData(plate_no_text.Text);
                }
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            int day_before_rent;
            DateTime selectedDate = start_date.Value.Date;
            DateTime selectedReturnDate = return_date.Value.Date;
            string selectedDateString = start_date.Value.Date.ToString("yyyy-MM-dd");
            string ReturnDateString = return_date.Value.Date.ToString("yyyy-MM-dd");
            DateTime today = DateTime.Today;
            if (selectedReturnDate <= selectedDate)
            {
                MessageBox.Show("Please select a return date that is after the start date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (selectedDate < today) {
                MessageBox.Show("Please select a rent date that is today or  after today's date for reservation.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            if (selectedDate > today)
            {
                transaction_status = 4; // RESERVED
                TimeSpan difference_reserved = selectedDate - today;
                day_before_rent = difference_reserved.Days;
                //MessageBox.Show($"Days before rent [Booked in]: {day_before_rent}");
            }
            else if (selectedDate == today)
            {
                transaction_status = 1; // ON GOING
            }
            string invoice_no = GenerateInvoiceNumber(customer_id);
            TimeSpan difference = selectedReturnDate - selectedDate;
            int day_before_return = difference.Days;
            double total_price = Math.Round(car_price * day_before_return, 2);
            if (customer_id != 0 && car_id != 0)
            {
                savingTransaction(ReturnDateString, selectedDateString, invoice_no, total_price, transaction_status);
            }
            else {
                MessageBox.Show("Please select a valid car or customer license no..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }


        private void updatingCarStatus(int car_id,int car_status) {

            string query = "UPDATE `cars` SET `status`=@status WHERE id = @id";
            MySqlCommand command_query = new MySqlCommand(query, connection);
            command_query.Parameters.AddWithValue("@status", car_status);
            command_query.Parameters.AddWithValue("@id", car_id);
            try
            {
                command_query.ExecuteNonQuery();
                car_id = 0;
                customer_id = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Updating Cars Error Error" + ex.StackTrace + "\n" + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }


        private void savingTransaction(string return_at,string added_at,string invoice,double total_amount,int status) {
            string sqlInsert = "INSERT INTO `transaction_table`(`client_id`, `car_id`, `return_at`, `added_at`, `invoice_no`, `total_amount`,`status`) VALUES (@client_id, @car_id, @return_at, @added_at, @invoice_no, @total_amount, @status)";
            MySqlCommand command_query = new MySqlCommand(sqlInsert, connection);
            command_query.Parameters.AddWithValue("@client_id", customer_id);
            command_query.Parameters.AddWithValue("@car_id", car_id);
            command_query.Parameters.AddWithValue("@return_at", return_at);
            command_query.Parameters.AddWithValue("@added_at", added_at);
            command_query.Parameters.AddWithValue("@invoice_no", invoice);
            command_query.Parameters.AddWithValue("@total_amount", total_amount);
            command_query.Parameters.AddWithValue("@status", status);

            try
            {
                connection.Open();
                if (command_query.ExecuteNonQuery() == 1)
                {
                    updatingCarStatus(car_id,status);
                    MessageBox.Show("Successfully added");
                    last_name_text.Text = "";
                    first_name_text.Text = "";
                    contact_no_text.Text = "";
                    license_no.Text = "";
                    email_text.Text = "";
                    plate_no_text.Text = "";
                    car_name_text.Text = "";
                    car_color_text.Text = "";
                    car_price_text.Text = "";
                    transactionAdded?.Invoke();

                    //CustomerAdded?.Invoke();


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Saving Error" + ex.StackTrace +"\n" + ex.Message);
            }
            finally
            {
                connection.Close();
            }


        }

        private void TextChange(object sender, EventArgs e)
        {
            string plate_no = plate_no_text.Text;
           
            try
            {
                connection.Open();
                string query = "SELECT plate_no FROM cars WHERE plate_no LIKE @plate_no";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@plate_no", $"%{plate_no}%");

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    AutoCompleteStringCollection autoComplete = new AutoCompleteStringCollection();

                    while (reader.Read())
                    {
                        autoComplete.Add(reader["plate_no"].ToString());
                    }

                    plate_no_text.AutoCompleteCustomSource = autoComplete;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }

        private void Back_space_plate_no(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(plate_no_text.Text))
            {
                car_id = 0;
                car_name_text.Text = "";
                car_color_text.Text = "";
                car_price_text.Text = "";
            }
        }

        private void TransactionForm_Load(object sender, EventArgs e)
        {
            start_date.Value = DateTime.Today;
            return_date.Value = DateTime.Today.AddDays(1);
        }

        private void driver_license_suggestion_textchange(object sender, EventArgs e)
        {
            string license = license_no.Text;

            try
            {
                connection.Open();
                string query = "SELECT driver_license_no FROM customer WHERE driver_license_no LIKE @license_no";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@license_no", $"%{license}%");

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    AutoCompleteStringCollection autoComplete = new AutoCompleteStringCollection();

                    while (reader.Read())
                    {
                        autoComplete.Add(reader["driver_license_no"].ToString());
                    }

                    license_no.AutoCompleteCustomSource = autoComplete;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }

        public string GenerateInvoiceNumber(int customer_id)
        {
            // You can customize the format and prefix/suffix as needed
            string prefix = "INV";
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            string randomSuffix = Guid.NewGuid().ToString().Substring(0, 4); // Generating a random suffix

            // Combine the elements to form the invoice number
            string invoiceNumber = $"{prefix}-{timestamp}-{randomSuffix}-{customer_id}";

            return invoiceNumber;
        }
    }
}
