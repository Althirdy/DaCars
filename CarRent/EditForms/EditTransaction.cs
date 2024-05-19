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
        private double car_price,new_total_price = 0;
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
            this.Close();
        }

        private void Edit_Transaction_Load(object sender, EventArgs e)
        {
            if (transaction_id != 0) {
                fetchingData();
            }
        }


        private void guna2Button3_Click(object sender, EventArgs e)
        {
            DateTime selectedDate = rent_date.Value.Date;
            DateTime selectedReturnDate = return_date.Value.Date;
            if (selectedDate != data_rent_date || selectedReturnDate != data_return_data || status_combo.SelectedIndex + 1 != statusId) {
                UpdatingTheData();
            }
        }

        private void UpdatingTheData() {
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
            if (status_combo.SelectedIndex + 1 == 3 || status_combo.SelectedIndex + 1 == 4)
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
                                    transactionPage trans_page = new transactionPage();
                                    trans_page.Updated();
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


        private Boolean checkingInputs()
        {
            Boolean isReadyToSave = false;
            DateTime selectedDate = rent_date.Value.Date;
            DateTime selectedReturnDate = return_date.Value.Date;
            DateTime today = DateTime.Today;
            


            // SelectedDate cannot be greater than or equal to the SelectedReturnDate
            // selectedDate cannot be less than today
            if (selectedDate >= selectedReturnDate || selectedDate < today)
            {
                rent_date.Value = data_rent_date;
                return_date.Value = data_return_data;
                MessageBox.Show("Error: Rent date and return date cannot be the same \n Or Rent date can not be less than today.", "Date Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (statusId == 2) {
                if (selectedDate == data_rent_date && selectedReturnDate == data_return_data && status_combo.SelectedIndex == 2)
                {
                    isReadyToSave = true;
                }
                else
                {
                    MessageBox.Show("Error: The Rent is On Going you cannot cancel it. Instead, you can finish the rent \n Error: You cannot change the Dates if the Rent is On Going.", "Date Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }


            // If the Rent is in Reserved and selectedDate is equal today. The user can update the status to [ON GOING, CANCELLED] they cannot finish it
            if (statusId == 1) {
                if (selectedDate > data_rent_date || selectedReturnDate != data_return_data || selectedDate == today)
                {
                    TimeSpan difference = selectedReturnDate - selectedDate;
                    int day_before_return = difference.Days;
                    new_total_price = Math.Round(car_price * day_before_return, 2);
                }
                if ((selectedDate == today && (status_combo.SelectedIndex == 1 || status_combo.SelectedIndex == 3)) || (selectedDate > today && status_combo.SelectedIndex == 0))
                {
                    isReadyToSave = true;
                }
                else
                {
                    MessageBox.Show("Error: The car is Reserved you can cancel it or make it on going.\n Error: You can make it On Going if the Rent Date is Today", "Updating Transaction Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
           

            // If the Rent status is ON GOING they cannot change the dates, they can update the status to [FINISHED] only
           

            return isReadyToSave;
        }
    }
}
