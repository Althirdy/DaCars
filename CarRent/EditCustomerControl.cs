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
    public partial class EditCustomerControl : UserControl
    {
        public EditCustomerControl()
        {
            InitializeComponent();
        }

        #region Properties
        private string invoice;
        private string plate_no;
        private int transaction_status;
        private int transaction_id;
        private DateTime rented_at;
       
        [Category("Custom Props")]

        public string invoice_method
        {
            get { return invoice; }
            set
            {
                invoice = value;
                if (invoice.Length > 10)
                {
                    invoice_no_text.Text = invoice.Substring(0, 10) + "...";
                }
                else
                {
                    invoice_no_text.Text = invoice;
                }

            }
        }

        [Category("Custom Props")]
        public string plate_no_method
        {
            get { return plate_no; }
            set { plate_no = value; plate_no_text.Text = value; }
        }
     
        [Category("Custom Props")]

        public int status_method
        {
            get { return transaction_status; }
            set
            {
                transaction_status = value;
                if (transaction_status == 1)
                {
                    status_text.Text = "ON GOING";
                    status_text.FillColor = Color.FromArgb(50, 89, 117);
                    status_text.HoverState.FillColor = Color.FromArgb(50, 89, 117);
                    status_text.PressedColor = Color.FromArgb(50, 89, 117);
                }
                else if (transaction_status == 2)
                {
                    status_text.Text = "FINISHED";
                    status_text.FillColor = Color.FromArgb(96, 150, 254);
                    status_text.HoverState.FillColor = Color.FromArgb(96, 150, 254);
                    status_text.PressedColor = Color.FromArgb(96, 150, 254);
                }
                else if (transaction_status == 4)
                {
                    status_text.Text = "RESERVED";
                    status_text.FillColor = Color.FromArgb(88, 88, 88);
                    status_text.HoverState.FillColor = Color.FromArgb(88, 88, 88);
                    status_text.PressedColor = Color.FromArgb(88, 88, 88);

                }
                else
                {
                    status_text.Text = "CANCELED";
                    status_text.FillColor = Color.FromArgb(155, 76, 21);
                    status_text.HoverState.FillColor = Color.FromArgb(155, 76, 21);
                    status_text.PressedColor = Color.FromArgb(155, 76, 21);
                }

            }
        }
        [Category("Custom Props")]

        public int trans_id_method
        {
            get { return transaction_id; }
            set { transaction_id = value; trans_id.Text = value.ToString("D2"); }
        }

        [Category("Custom Props")]


        public DateTime rented_at_method
        {
            get { return rented_at; }
            set { rented_at = value; 
                rented_at_text.Text = value.ToShortDateString(); }
        }





        #endregion

    }
}
