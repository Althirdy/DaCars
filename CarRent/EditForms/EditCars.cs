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
    public partial class EditCars : Form
    {
        private int carId;
        private MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
        private double old_car_price;
        private string plate_no;
        public EditCars()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void setCarId(int car_id) {
            this.carId = car_id;
        }


        public void fetchCar() {
            string query = "SELECT * FROM cars WHERE id=@id";
            MySqlCommand fetch_car = new MySqlCommand(query, connection);
            fetch_car.Parameters.AddWithValue("@id", carId);

            try {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                using (MySqlDataReader reader = fetch_car.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        car_name_text.Text = reader["car_name"].ToString().ToUpper();
                        car_model_text.Text = reader["car_model"].ToString().ToUpper();
                        plate_no_text.Text = reader["plate_no"].ToString().ToUpper();
                        plate_no = reader["plate_no"].ToString().ToUpper();
                        car_price_text.Text = reader["price"].ToString();
                        car_color_text.Text = reader["color"].ToString().ToUpper();
                        old_car_price = Convert.ToDouble(reader["price"]);
                    }
                }
            } catch (Exception ex) {
                MessageBox.Show("error Fetching" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally {
                if (connection.State != ConnectionState.Closed)
                {
                    connection.Close();

                }
            }

        
        }

        private void EditCarsLoad(object sender, EventArgs e)
        {
            fetchCar();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            double new_car_price;
            try
            {
                new_car_price = Convert.ToDouble(car_price_text.Text);
                if (old_car_price != new_car_price)
                {
                    string query = $"UPDATE `cars` SET price = @price WHERE id = @id";
                    MySqlCommand update_sql = new MySqlCommand(query, connection);
                    update_sql.Parameters.AddWithValue("@price", new_car_price);
                    update_sql.Parameters.AddWithValue("@id", carId);

                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }

                    int rowsAffected = update_sql.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Car price updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        fetchCar();
                    }
                }
                else
                {
                    MessageBox.Show(" No changes needed.", "No Changes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Error: Invalid input for car price. Please enter a valid number.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error occurred while updating car price: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show($"Are you sure you want to remove this car [Plate No. {plate_no}] from the garage?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string sql = "UPDATE `cars` SET car_status = 2 WHERE id = @id";
                MySqlCommand up_car_garage = new MySqlCommand(sql, connection);
                up_car_garage.Parameters.AddWithValue("@id", carId);
                try {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    int rowsAffected = up_car_garage.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show($"Car with Plate No. {plate_no} Moved out from the garage.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Dispose();
                    }
                }
                catch (Exception ex) {
                    MessageBox.Show($"Error occurred while moving  car out from the garage: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                finally {
                    connection.Close();
                }

            }
        }
    }
}
