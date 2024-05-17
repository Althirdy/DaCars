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
    public partial class OffSiteCar : UserControl
    {
        private MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
        public OffSiteCar()
        {
            InitializeComponent();
        }

        #region Properties

        private int car_id_props;
        private string car_name_props;
        private string car_model_props;
        private string car_plateno_props;
        private string color_props;
        private double price_props;

        [Category("Custom Props")]
        public string car_name
        {
            get { return car_name_props; }
            set { car_name_props = value; name_car.Text = value.ToUpper(); }
        }


        [Category("Custom Props")]

        public double price
        {
            get { return price_props; }
            set { price_props = value; priceLabel.Text = "$ " + value; }
        }


        [Category("Custom Props")]

        public string car_model
        {
            get { return car_model_props; }
            set { car_model_props = value; brand.Text = value.ToUpper(); }
        }

        [Category("Custom Props")]
        public string plateno
        {
            get { return car_plateno_props; }
            set { car_plateno_props = value; plateNoLabel.Text = value.ToUpper(); }
        }


        [Category("Custom Props")]
        public int id
        {
            get { return car_id_props; }
            set { car_id_props = value; idLabel.Text = value.ToString().PadLeft(2, '0'); }
        }

        [Category("Custom Props")]
        public string color
        {
            get { return color_props; }
            set { color_props = value; colorLabel.Text = value.ToUpper(); }
        }

        #endregion

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show($"Do you want to put the car that has plate no. '{car_plateno_props}' again in the garage?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }

                    string query = "UPDATE cars SET car_status = 1 WHERE id = @carId";

                    MySqlCommand command = new MySqlCommand(query, connection);

                    command.Parameters.AddWithValue("@carId", car_id_props);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Car has been put back in the garage successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                    }
                    else
                    {
                        MessageBox.Show("Failed to update car status.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}
