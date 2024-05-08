using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using MySql.Data.MySqlClient;


namespace CarRent
{
    public partial class AddCars : Form
    {
        //Connection String
        private MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
        public AddCars()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            DateTime date = DateTime.Now;
            String car_name, car_model, plate_no, color, added_at;
            double price;
            car_name = car_name_text.Text;
            plate_no = plate_no_text.Text;
            car_model = car_model_text.Text;
            color = car_color_text.Text;
            added_at = date.ToString("yyyy-MM-dd");
            try {
                
                price = Double.Parse(car_rate_text.Text);
                //Checking if the inputs are fill
                if (string.IsNullOrWhiteSpace(car_name) ||
                      string.IsNullOrWhiteSpace(plate_no) ||
                      string.IsNullOrWhiteSpace(car_model) ||
                      string.IsNullOrWhiteSpace(color))
                {
                    MessageBox.Show("Error: All fields are required.","Fields Error");
                }
                else {
                    //Checking first if the plate_no is inside in the database;
                    string checkQueryString = "SELECT plate_no FROM cars WHERE plate_no = @plate_no";
                    MySqlCommand checkCommand = new MySqlCommand(checkQueryString, connection);
                    checkCommand.Parameters.AddWithValue("@plate_no", plate_no);
                    try
                    {
                        connection.Open();
                        MySqlDataReader reader = checkCommand.ExecuteReader();
                        if (reader.Read())
                        {
                            MessageBox.Show($"The car that has a plate No. {plate_no} is already save in the database");
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Checking Error" + ex.Message);

                    }
                    finally {
                        connection.Close();
                    }


                    //*******************  SAVING IN DATABASE *******//
                    string query = "INSERT INTO cars (car_name, car_model, price, plate_no, color, added_at) VALUES (@car_name, @car_model, @price, @plate_no, @color, @added_at)";
                    MySqlCommand command_query = new MySqlCommand(query, connection);
                    command_query.Parameters.AddWithValue("@car_name",car_name);
                    command_query.Parameters.AddWithValue("@car_model", car_model);
                    command_query.Parameters.AddWithValue("@price", price);
                    command_query.Parameters.AddWithValue("@plate_no", plate_no);
                    command_query.Parameters.AddWithValue("@color", color);
                    command_query.Parameters.AddWithValue("@added_at", added_at);
                    try
                    {
                        connection.Open();
                        if (command_query.ExecuteNonQuery() == 1)
                        {

                            MessageBox.Show("Successfully added");
                            car_name_text.Text = "";
                            plate_no_text.Text = "";
                            car_model_text.Text = "";
                            car_color_text.Text = "";
                            car_rate_text.Text = "";
   
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Saving Error" + ex.Message);
                    }
                    finally {
                        connection.Close();
                    }
                    //END SAVING
                }

            } catch(FormatException ex) {
                MessageBox.Show("Invalid Input","ERROR");
            }

        }
    }
}
