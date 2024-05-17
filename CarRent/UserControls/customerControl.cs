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
    public partial class customerControl : UserControl
    {
        public customerControl()
        {
            InitializeComponent();
        }

        #region Properties
        private int id;
        private string first_name;
        private string last_name;
        private string contact_no;
        private string email;
        private int no_rent;
        private DateTime added_at;
        [Category("Custom Props")]
        public int id_method
        {
            get { return id; }
            set { id = value; id_text.Text = value.ToString(); }
        }
        [Category("Custom Props")]

        public string first_name_method
        {
            get { return first_name; }
            set { first_name = value; first_name_text.Text = value.ToUpper(); }
        }

        [Category("Custom Props")]

        public string last_name_method
        {
            get { return last_name; }
            set { last_name = value; last_name_text.Text = value.ToUpper(); }
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
        [Category("custom Props")]


        public string added_at_method
        {
            get { return added_at.ToString(); }
            set {
                DateTime parsedDate;
                if (DateTime.TryParseExact(value, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out parsedDate))
                {
                  added_at = parsedDate; // Assign the parsed date to the private field
                    added_at_text.Text = added_at.ToString("dd-MM-yyyy"); // Set the formatted date to added_at_text
                }
            }
        }



        [Category("Custom Props")]

        public string email_method
        {
            get { return email; }
            set { email = value; email_address_text.Text = value.ToUpper(); }
        }
        [Category("Custom Props")]


        public int no_rent_method
        {
            get { return no_rent; }
            set { no_rent = value; rent_text.Text = value.ToString(); }
        }



        #endregion

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            EditCustomer edt_customer = new EditCustomer();
            edt_customer.setClientId(id);
            edt_customer.Show();

        }
    }
}
