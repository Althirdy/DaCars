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
    public partial class transactionControl : UserControl
    {

        public transactionControl()
        {
            InitializeComponent();
       
        }

        #region Properties
        private string full_name;
        private string invoice;
        private string car_name;
        private string plate_no;
        private double price;
        private string contact_no;
        private int transaction_status;
        private int transaction_id;
        [Category("Custom Props")]
        public string full_name_method
        {
            get { return full_name; }
            set { full_name = value; full_name_text.Text = value.ToUpper(); }
        }
        [Category("Custom Props")]

        public string invoice_method
        {
            get { return invoice; }
            set { invoice = value;invoice_text.Text = value; }
        }

        [Category("Custom Props")]

        public string car_name_method
        {
            get { return car_name; }
            set { car_name = value; car_name_text.Text = value.ToUpper(); }
        }
        [Category("Custom Props")]    
        public string plate_no_method
        {
            get { return plate_no; }
            set { plate_no = value; plate_no_text.Text = value; }
        }
        [Category("Custom Props")]

        public Double price_method
        {
            get { return price; }
            set { price = value; rate_text.Text = $"${value.ToString()}"; }
        }
        [Category("Custom Props")]
        public string contact_no_method
        {
            get { return contact_no; }
            set { contact_no = value;
                // Remove any non-digit characters from the value
                string digitsOnly = new string(value.Where(char.IsDigit).ToArray());

                // Ensure the length is at most 10 characters
                digitsOnly = digitsOnly.Substring(0, Math.Min(digitsOnly.Length, 10));

                // Format the contact number as xxxx-xxx-xxxx
                string formattedContactNo = $"{digitsOnly.Substring(0, 4)}-{digitsOnly.Substring(4, 3)}-{digitsOnly.Substring(7)}";

                // Set the formatted value to the private field
                contact_no = formattedContactNo;

                // Set the formatted value to the contact_no_text.Text
                contact_no_text.Text = formattedContactNo;

            }
        }

        [Category("Custom Props")]

        public int status_method
        {
            get { return transaction_status; }
            set { transaction_status = value;
                if (transaction_status == 2)
                {
                    status_text.Text = "ON GOING";
                    status_text.FillColor = Color.FromArgb(50, 89, 117);
                    status_text.HoverState.FillColor = Color.FromArgb(50, 89, 117);
                    status_text.PressedColor = Color.FromArgb(50, 89, 117);
                }
                else if (transaction_status == 3)
                {
                    status_text.Text = "FINISHED";
                    status_text.FillColor = Color.FromArgb(96, 150, 254);
                    status_text.HoverState.FillColor = Color.FromArgb(96, 150, 254);
                    status_text.PressedColor = Color.FromArgb(96, 150, 254);
                    guna2GradientButton1.Hide();
                }
                else if (transaction_status == 1)
                {
                    status_text.Text = "RESERVED";
                    status_text.FillColor = Color.FromArgb(88, 88, 88);
                    status_text.HoverState.FillColor = Color.FromArgb(88, 88, 88);
                    status_text.PressedColor = Color.FromArgb(88, 88, 88);

                }
                else {
                    status_text.Text = "CANCELED";
                    status_text.FillColor = Color.FromArgb(155, 76, 21);
                    status_text.HoverState.FillColor = Color.FromArgb(155, 76, 21);
                    status_text.PressedColor = Color.FromArgb(155, 76, 21);
                    guna2GradientButton1.Hide();

                }

            }
        }
        [Category("Custom Props")]

        public int trans_id_method
        {
            get { return transaction_id; }
            set { transaction_id = value; }
        }






        #endregion

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            EditTransaction transaction = new EditTransaction();
            transaction.LoadTransactionDetails(this.transaction_id);
            transaction.Show();
        }

    }
}
