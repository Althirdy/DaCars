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
    public partial class DashBoardLoagControl : UserControl
    {
        public DashBoardLoagControl()
        {
            InitializeComponent();
        }

        #region Properties
        [Category("Custom Props")]
        private string invoice_no;

        public string invoice_no_method
        {
            get { return invoice_no; }
            set { invoice_no = value; invoice_no_text.Text = value.Length > 15 ? value.Substring(0, 15) + "..." : value; }
        }
        [Category("Custom Props")]
        private string plate_no;

        public string plate_no_method
        {
            get { return plate_no; }
            set { plate_no = value; plate_no_text.Text = value.ToUpper(); }
        }

        [Category("Custom Props")]
        private Double total_amount;

        public Double total_amoun_method
        {
            get { return total_amount; }
            set { total_amount = value; price_text.Text = $"$ {value.ToString()}"; }
        }
        [Category("Custom Props")]
        private string rent_at;

        public string rent_at_method
        {
            get { return rent_at; }
            set { rent_at = value; rent_at_text.Text = TranslateDate(value); }
        }
        [Category("Custom Props")]
        private string return_at;

        public string return_at_method
        {
            get { return return_at; }
            set { return_at = value; return_at_text.Text = TranslateDate(value); }
        }
        private int status_id;

        public int status_id_method
        {
            get { return status_id; }
            set { status_id = value;
                if (status_id == 1)
                {
                    status_text.Text = "ON GOING";
                    status_text.FillColor = Color.FromArgb(50, 89, 117);
                    status_text.HoverState.FillColor = Color.FromArgb(50, 89, 117);
                    status_text.PressedColor = Color.FromArgb(50, 89, 117);
                }
                else if (status_id == 2)
                {
                    status_text.Text = "FINISHED";
                    status_text.FillColor = Color.FromArgb(96, 150, 254);
                    status_text.HoverState.FillColor = Color.FromArgb(96, 150, 254);
                    status_text.PressedColor = Color.FromArgb(96, 150, 254);
                }
                else if (status_id == 4)
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



        #endregion
        public static string TranslateDate(string inputDate)
        {
                DateTime date = DateTime.Parse(inputDate);

            string formattedDate = date.ToString("dd MMM yyyy");

            return formattedDate;
        }

    }
}
