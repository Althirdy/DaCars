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
using Guna.UI2.WinForms;

namespace CarRent
{
    public partial class EditTransaction : Form
    {

        private int transaction_id;
        private double car_price,new_total_price    ;
        private MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
        private int statusId;
        private DateTime data_rent_date, data_return_data;
        private int car_id;
        public EditTransaction()
        {
            InitializeComponent();
            fetchStatus();
        }

        public void LoadTransactionDetails(int transactionId) {
            this.transaction_id = transactionId;
        }
        private void fetchStatus() {
            string query = $"SELECT * From trans_status";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@trans_id", transaction_id);
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();

                }
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string statusText = reader["status"].ToString();
                        string statusValue = reader["id"].ToString();
                        status_combo.Items.Add(new KeyValuePair<string, string>(statusText, statusValue));

                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("error Fetching" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();

            }
        }

        private void fetchingData() {
            string query = $"SELECT tt.id,tt.added_at,tt.return_at, tt.invoice_no, CONCAT(c.first_name, ' ', c.last_name) as full_name, c.contact_no,ca.id as car_id, ca.plate_no, ca.car_name, tt.total_amount, ca.price, s.status, s.id as status_id FROM transaction_table AS tt JOIN customer AS c ON tt.client_id = c.id JOIN cars AS ca ON tt.car_id = ca.id JOIN trans_status AS s ON tt.status = s.id WHERE tt.id = @trans_id";
            MySqlCommand command = new MySqlCommand(query,connection);
            command.Parameters.AddWithValue("@trans_id", transaction_id);
            try
            {
                if (connection.State != ConnectionState.Open) {
                    connection.Open();

                }

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read()) {
                        invoice_no_text.Text = reader["invoice_no"].ToString();
                        full_name_text.Text = reader["full_name"].ToString().ToUpper();
                        contact_no_text.Text = reader["contact_no"].ToString();
                        plate_no_text.Text = reader["plate_no"].ToString().ToUpper();
                        car_name_text.Text = reader["car_name"].ToString().ToUpper();
                        total_amount_text.Text = "$ "+reader["total_amount"].ToString();
                        rent_date.Value = Convert.ToDateTime(reader["added_at"]);
                        return_date.Value = Convert.ToDateTime(reader["return_at"]);
                        statusId = Convert.ToInt32(reader["status_id"]);
                        transaction_id = Convert.ToInt32(reader["id"]);
                        car_price = Convert.ToDouble(reader["price"]);
                        data_rent_date = Convert.ToDateTime(reader["added_at"]);
                        data_return_data = Convert.ToDateTime(reader["return_at"]);
                        car_id = Convert.ToInt32(reader["car_id"]);
                        foreach (KeyValuePair<string, string> item in status_combo.Items)
                        {
                            if (item.Value == statusId.ToString())
                            {
                                status_combo.SelectedItem = item;
                                break; 
                            }
                        }
                    }
                }
            }
            catch (Exception ex) {

                MessageBox.Show("error Fetching" + ex.Message + ex.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();

            }
        
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Edit_Transaction_Load(object sender, EventArgs e)
        {
            if (transaction_id != 0) {
                fetchingData();
            }
        }


        private void guna2Button3_Click(object sender, EventArgs e)
        {
            Boolean isReadyToUpdate = checkingInputs();

            string query = $"UPDATE `transaction_table` SET `return_at`= @return_at, `added_at`=@added_at{(new_total_price != 0 ? ", `total_amount`=@total_amount" : "")}, `status`=@status WHERE id = @id";

            string update_car = $"UPDATE cars SET `status`= @status WHERE id  = @car_id";

            MySqlCommand Update_Transaction_Command = new MySqlCommand(query, connection);
            MySqlCommand Update_Car_Command = new MySqlCommand(update_car, connection);

            Update_Transaction_Command.Parameters.AddWithValue("@id", transaction_id);
            Update_Transaction_Command.Parameters.AddWithValue("@return_at", return_date.Value.Date.ToString("yyyy-MM-dd"));
            Update_Transaction_Command.Parameters.AddWithValue("@added_at", rent_date.Value.Date.ToString("yyyy-MM-dd"));
            if (new_total_price != 0)
            {
                Update_Transaction_Command.Parameters.AddWithValue("@total_amount", new_total_price);
            }
            Update_Transaction_Command.Parameters.AddWithValue("@status", status_combo.SelectedIndex + 1);

            Update_Car_Command.Parameters.AddWithValue("@car_id", car_id);
            if (status_combo.SelectedIndex + 1 == 3 || status_combo.SelectedIndex + 1 == 2)
            {
                Update_Car_Command.Parameters.AddWithValue("@status", 0);
            }
            else
            {
                Update_Car_Command.Parameters.AddWithValue("@status", status_combo.SelectedIndex + 1);
            }

            if (isReadyToUpdate)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }

                    using (MySqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            int rowsAffected1 = Update_Transaction_Command.ExecuteNonQuery();
                            if (rowsAffected1 > 0)
                            {
                                int rowsAffected2 = Update_Car_Command.ExecuteNonQuery();

                                if (rowsAffected2 > 0)
                                {
                                    transaction.Commit();
                                    fetchingData();
                                    MessageBox.Show("Transaction updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    this.Dispose();
                                }
                                else
                                {
                                    transaction.Rollback();
                                    MessageBox.Show("Error: Car update failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
   
                                transaction.Rollback();
                                MessageBox.Show("Error: Transaction update failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show($"Error occurred while updating: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void Date_Rent_Value_Change(object sender, EventArgs e)
        {
            DateTime selectedDate = rent_date.Value.Date;
            DateTime today = DateTime.Today;
            if (selectedDate > today) {
                status_combo.SelectedIndex = 3;
            }
        }

        private Boolean checkingInputs()
        {
            Boolean isReadyToSave = false;
            DateTime selectedDate = rent_date.Value.Date;
            DateTime selectedReturnDate = return_date.Value.Date;
            DateTime today = DateTime.Today;

            // Overall validation: Rent date and return date cannot be the same
            if (selectedDate == selectedReturnDate)
            {
                MessageBox.Show("Error: Rent date and return date cannot be the same.", "Date Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // Return early as further validation is not needed
            }

            // Overall validation: Rent date cannot be less than today's date
            if (selectedDate < today)
            {
                MessageBox.Show("Error: Rent date cannot be in the past.", "Date Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // Return early as further validation is not needed
            }

            // Overall validation: Rent date cannot be greater than return date
            if (selectedDate > selectedReturnDate)
            {
                MessageBox.Show("Error: Rent date cannot be later than the return date.", "Date Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // Return early as further validation is not needed
            }

            // If the status is 'ON GOING' and the selected status is 'FINISHED'
            if (statusId == 1 && status_combo.SelectedIndex == 1)
            {
                isReadyToSave = true;
            }
            else if (statusId != 1)
            { // If the status is not 'ON GOING'
              // Validate date changes and calculate new total price if dates are changed
                if (selectedDate != data_rent_date || selectedReturnDate != data_return_data)
                {
                    TimeSpan difference = selectedReturnDate - selectedDate;
                    int day_before_return = difference.Days;
                    new_total_price = Math.Round(car_price * day_before_return, 2);

                    // Check if the return date is after the rent date
                    if (selectedReturnDate > selectedDate)
                    {
                        isReadyToSave = true;
                    }

                    // Check if the rent date is in the future and status is 'RESERVED' or 'CANCELLED'
                    if (selectedDate > today && (status_combo.SelectedIndex == 3 || status_combo.SelectedIndex == 2))
                    {
                        isReadyToSave = true;
                    }

                    // Check if the rent date is today and status is 'ON GOING'
                    if (selectedDate == today && status_combo.SelectedIndex == 0)
                    {
                        isReadyToSave = true;
                    }
                }
                else
                {
                    new_total_price = 0;
                }
            }

            // Additional specific checks based on the requirements
            if (statusId == 1 && (selectedDate != data_rent_date || selectedReturnDate != data_return_data))
            {
                MessageBox.Show("Error: You cannot change the date while the rent is ongoing. Please finish the rent instead.", "Date Change Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rent_date.Value = data_rent_date;
                return_date.Value = data_return_data;
                isReadyToSave = false;
            }

            if (selectedDate > today && status_combo.SelectedIndex == 1)
            {
                MessageBox.Show("Error: Cannot select 'ON GOING' status because the selected date is not today.", "Date Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isReadyToSave = false;
                status_combo.SelectedIndex = 3; // Set to 'RESERVED'
            }

            if (statusId != 1 && status_combo.SelectedIndex == 1)
            {
                MessageBox.Show("Error: You cannot finish the rent if the status is not 'ON GOING'. You can cancel it instead.", "Date Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isReadyToSave = false;
                status_combo.SelectedIndex = 2; // Set to 'CANCELLED'
            }

            if (selectedDate == today && status_combo.SelectedIndex == 3)
            {
                MessageBox.Show("Error: You cannot reserve the car because the rental date is today. You can rent it and change the status to 'ON GOING'.", "Date Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isReadyToSave = false;
                status_combo.SelectedIndex = 0; // Set to 'ON GOING'
            }
            if (statusId == 4 && selectedDate == data_rent_date && selectedReturnDate == data_return_data)
            {
                // If the dates are not changed and the status is RESERVED
                // Only allow selecting the 'CANCELLED' status
                if (status_combo.SelectedIndex != 2) // If not already set to 'CANCELLED'
                {
                    MessageBox.Show("Error: If the rental is reserved and dates are not changed, the status must be set to 'CANCELLED'.", "Status Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    status_combo.SelectedIndex = 2; // Set to 'CANCELLED'
                    isReadyToSave = false;
                }
                else {

                    isReadyToSave = true;

                }
            }

            return isReadyToSave;
        }
    }
}
