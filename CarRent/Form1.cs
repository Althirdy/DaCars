using Guna.UI2.WinForms;
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
    public partial class Form1 : Form
    {
        private List<Guna2Button> sidebarButtons = new List<Guna2Button>();
        private DashPage Dash_page = new DashPage();
        private carPage Car_page = new carPage();
        private transactionPage transact_page = new transactionPage();
        public Form1()
        {
            InitializeComponent();
            sidebarButtons.Add(overView);
            sidebarButtons.Add(carGarage);
            sidebarButtons.Add(Transaction);
            sidebarButtons.Add(customer_btn);
        }

        //For Button Click and focus color
        private void ChangeColor(Guna2Button clickedButton)
        {
            clickedButton.FillColor = Color.FromArgb(192, 192, 255);
            // Set the background color of other buttons to transparent
            foreach (Guna2Button button in sidebarButtons)
            {
                if (button != clickedButton)
                {
                    button.FillColor = Color.Transparent;
                }
            }
        }


        //For Changing the content in the panel
        private void ShowForm(Form form)
        {
            // Clear any existing controls in the panel
            viewPanel.Controls.Clear();

            // Add the new form to the panel
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            viewPanel.Controls.Add(form);
            // Show the form
            form.Show();
        }



        //*****************************//
        private void Form1_Load(object sender, EventArgs e)
        {
            ShowForm(Dash_page);
        }

        private void overView_Click(object sender, EventArgs e)
        {
            Guna2Button clickedButton = (Guna2Button)sender;
            ChangeColor(clickedButton);
            ShowForm(Dash_page);

        }

        private void carGarage_Click(object sender, EventArgs e)
        {
            Guna2Button clickedButton = (Guna2Button)sender;
            ChangeColor(clickedButton);
            ShowForm(Car_page);
        }

        private void Transaction_Click(object sender, EventArgs e)
        {
            Guna2Button clickedButton = (Guna2Button)sender;
            ChangeColor(clickedButton);
            ShowForm(transact_page);
        }

        private void customer_btn_Click(object sender, EventArgs e)
        {
            Guna2Button clickedButton = (Guna2Button)sender;
            ChangeColor(clickedButton);
        }
    }
}
