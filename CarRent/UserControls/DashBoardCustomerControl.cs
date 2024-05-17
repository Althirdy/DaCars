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
    public partial class DashBoardCustomerControl : UserControl
    {
        public DashBoardCustomerControl()
        {
            InitializeComponent();
        }

        #region Properties
        [Category("Custom Props")]
        private string first_name;

        public string first_name_method
        {
            get { return first_name; }
            set { first_name = value; first_name_text.Text = value.Length > 10 ? value.Substring(0, 10) + "..." : value; }
        }
        [Category("Custom Props")]
        private string last_name;

        public string last_name_method
        {
            get { return last_name; }
            set { last_name = value; last_name_text.Text = value.Length > 10 ? value.Substring(0, 10) + "..." : value; }
        }

        [Category("Custom Props")]
        private string contact_no;

        public string contact_no_method
        {
            get { return contact_no; }
            set { contact_no = value; 
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
        private string driver_license_no;

        public string license_no_method
        {
            get { return driver_license_no; }
            set { driver_license_no = value; license_no_text.Text = value; }
        }
        [Category("Custom Props")]
        private string added_at;

        public string added_at_method {
            get { return added_at; }
            set { added_at = value; added_at_text.Text = TranslateDate(value); }
        }


        #endregion

        public static string TranslateDate(string inputDate)
        {
            DateTime date = DateTime.Parse(inputDate);

            string formattedDate = date.ToString("dd MMM yyyy");

            return formattedDate;
        }
    }
}
