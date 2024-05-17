using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRent
{
    public partial class CarTableRow : UserControl
    {
        public CarTableRow()
        {
            InitializeComponent();
        }

        private void PanelHover(object sender, EventArgs e)
        {
            guna2Panel1.FillColor = Color.FromArgb(224, 228, 255);
        }

        private void PanelLeave(object sender, EventArgs e)
        {
            guna2Panel1.FillColor = Color.Transparent;

        }


        #region Properties

        private int car_id_props;
        private string car_name_props;
        private string car_model_props;
        private string car_plateno_props;
        private string color_props;
        private double price_props;
        private int status_props;

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
            set { car_id_props = value; idLabel.Text = value.ToString(); }
        }

        [Category("Custom Props")]
        public string color
        {
            get { return color_props; }
            set { color_props = value; colorLabel.Text = value.ToUpper(); }
        }

        [Category("Custom Props")]

        public int stats_method
        {
            get { return status_props; }
            set {
                status_props = value;
                if (value == 1)
                {
                    statusLabel.Text = "RENTED";
                    statusLabel.FillColor = Color.FromArgb(225, 75, 86);
                    statusLabel.HoverState.FillColor = Color.FromArgb(225, 75, 86);
                    statusLabel.PressedColor = Color.FromArgb(225, 75, 86);
                    guna2Panel1.FillColor = Color.FromArgb(224, 228, 255);
                    guna2GradientButton1.Hide();
                }
                else if (value == 4) {
                    statusLabel.Text = "RESERVED";
                    statusLabel.FillColor = Color.FromArgb(88, 88, 88);
                    statusLabel.HoverState.FillColor = Color.FromArgb(88, 88, 88);
                    statusLabel.PressedColor = Color.FromArgb(88, 88, 88);
                    guna2Panel1.FillColor = Color.FromArgb(224, 228, 255);
                    guna2GradientButton1.Hide();

                }
                else
                    statusLabel.Text = "AVAILABLE";
            }
        }




        #endregion

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            EditCars edit_car = new EditCars();
            edit_car.setCarId(car_id_props);
            edit_car.Show();
        }
    }
}
