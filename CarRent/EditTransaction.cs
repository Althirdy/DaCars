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

        private Boolean checkingInputs() {
            Boolean isReadyToSave = true;
            DateTime selectedDate = rent_date.Value.Date;
            DateTime selectedReturnDate = return_date.Value.Date;
            string selectedDateString = rent_date.Value.Date.ToString("yyyy-MM-dd");
            string ReturnDateString = return_date.Value.Date.ToString("yyyy-MM-dd");
            DateTime today = DateTime.Today;
            int status_value = status_combo.SelectedIndex + 1;
            //MessageBox.Show(status_value.ToString());
            if (selectedReturnDate <= selectedDate)
            {
                MessageBox.Show("Please select a return date that is after the start date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isReadyToSave = false;
            }
            if (selectedDate < today)
            {
                MessageBox.Show("Please select a rent date that is today or  after today's date for reservation.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isReadyToSave = false;
            }
            if (selectedDate > today && status_value == 1)
            {
                MessageBox.Show("Error: Cannot select 'ON GOING' status because the selected date is not today.", "Date Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isReadyToSave = false;
                status_combo.SelectedIndex = 3;

            }
            else if (statusId != 1 && status_value == 2)
            {
                MessageBox.Show("Error: You cannot finish the rent if the status is not 'ON GOING'. You can cancel it instead.", "Date Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isReadyToSave = false;
                status_combo.SelectedIndex = 2;
            }
            else if (selectedDate == today && status_value == 4) {
                MessageBox.Show("Error: You cannot reserve the car because the rental date is today. You can rent it and change the status to 'ON GOING'.", "Date Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isReadyToSave = false;
                status_combo.SelectedIndex = 0;


            }
            if (statusId == 1 && status_combo.SelectedIndex == 2 || (selectedDate != data_rent_date || selectedReturnDate != data_return_data)  ) 
            {
                MessageBox.Show("Error: You cannot change the date while the rent is ongoing or you cannot cancel the On Going Rent. Please finish the rent instead.", "Date Change Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                status_combo.SelectedIndex = 1;
                rent_date.Value = data_rent_date;
                return_date.Value = data_return_data;
                isReadyToSave = false;
            }
            if (selectedDate != data_rent_date || selectedReturnDate != data_return_data)
            {
                TimeSpan difference = selectedReturnDate - selectedDate;
                int day_before_return = difference.Days;
                new_total_price = Math.Round(car_price * day_before_return, 2);
            }
            else {
                new_total_price = 0;
            }
            return isReadyToSave;
        }
    }
}
