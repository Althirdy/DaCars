﻿using Guna.UI2.WinForms;
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
        private customerPage customer_page = new customerPage();
        private CarArchive archive = new CarArchive();
        private Form currentForm = null;

        public Form1()
        {
            InitializeComponent();
            sidebarButtons.Add(overView);
            sidebarButtons.Add(carGarage);
            sidebarButtons.Add(Transaction);
            sidebarButtons.Add(customer_btn);
            sidebarButtons.Add(offsite);
            sidebarButtons.Add(logout);


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
            if (currentForm == form)
            {
                return; 
            }


            viewPanel.Controls.Clear();

            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            viewPanel.Controls.Add(form);

            currentForm = form;

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
            Dash_page.isActivated();
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
            ShowForm(customer_page);

        }

        private void offsite_Click(object sender, EventArgs e)
        {
            Guna2Button clickedButton = (Guna2Button)sender;
            ChangeColor(clickedButton);
            ShowForm(archive);
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void logout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to log out?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                LoginForm login = new LoginForm();
                login.Show();

                this.Dispose();
            }
        }
    }
}
