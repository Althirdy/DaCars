
namespace CarRent
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2ControlBox1 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.guna2DragControl1 = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.guna2Panel2 = new Guna.UI2.WinForms.Guna2Panel();
            this.customer_btn = new Guna.UI2.WinForms.Guna2Button();
            this.Transaction = new Guna.UI2.WinForms.Guna2Button();
            this.carGarage = new Guna.UI2.WinForms.Guna2Button();
            this.overView = new Guna.UI2.WinForms.Guna2Button();
            this.label3 = new System.Windows.Forms.Label();
            this.viewPanel = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2Panel1.SuspendLayout();
            this.guna2Panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.TargetControl = this;
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
            this.guna2Panel1.Controls.Add(this.guna2ControlBox1);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.ShadowDecoration.Parent = this.guna2Panel1;
            this.guna2Panel1.Size = new System.Drawing.Size(1025, 39);
            this.guna2Panel1.TabIndex = 0;
            // 
            // guna2ControlBox1
            // 
            this.guna2ControlBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2ControlBox1.BackColor = System.Drawing.Color.Transparent;
            this.guna2ControlBox1.BorderRadius = 5;
            this.guna2ControlBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.guna2ControlBox1.FillColor = System.Drawing.Color.Transparent;
            this.guna2ControlBox1.HoverState.Parent = this.guna2ControlBox1;
            this.guna2ControlBox1.IconColor = System.Drawing.Color.Black;
            this.guna2ControlBox1.Location = new System.Drawing.Point(973, 6);
            this.guna2ControlBox1.Name = "guna2ControlBox1";
            this.guna2ControlBox1.ShadowDecoration.Parent = this.guna2ControlBox1;
            this.guna2ControlBox1.Size = new System.Drawing.Size(45, 29);
            this.guna2ControlBox1.TabIndex = 0;
            this.guna2ControlBox1.UseTransparentBackground = true;
            // 
            // guna2DragControl1
            // 
            this.guna2DragControl1.TargetControl = this.guna2Panel1;
            // 
            // guna2Panel2
            // 
            this.guna2Panel2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.guna2Panel2.BorderRadius = 10;
            this.guna2Panel2.Controls.Add(this.customer_btn);
            this.guna2Panel2.Controls.Add(this.Transaction);
            this.guna2Panel2.Controls.Add(this.carGarage);
            this.guna2Panel2.Controls.Add(this.overView);
            this.guna2Panel2.Controls.Add(this.label3);
            this.guna2Panel2.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel2.Name = "guna2Panel2";
            this.guna2Panel2.ShadowDecoration.Parent = this.guna2Panel2;
            this.guna2Panel2.Size = new System.Drawing.Size(168, 615);
            this.guna2Panel2.TabIndex = 1;
            // 
            // customer_btn
            // 
            this.customer_btn.BackColor = System.Drawing.Color.Transparent;
            this.customer_btn.BorderRadius = 5;
            this.customer_btn.CheckedState.Parent = this.customer_btn;
            this.customer_btn.CustomImages.Parent = this.customer_btn;
            this.customer_btn.FillColor = System.Drawing.Color.Transparent;
            this.customer_btn.Font = new System.Drawing.Font("Roboto", 10F);
            this.customer_btn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.customer_btn.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.customer_btn.HoverState.Parent = this.customer_btn;
            this.customer_btn.Image = global::CarRent.Properties.Resources.customer;
            this.customer_btn.ImageOffset = new System.Drawing.Point(-4, 0);
            this.customer_btn.ImageSize = new System.Drawing.Size(23, 23);
            this.customer_btn.Location = new System.Drawing.Point(10, 257);
            this.customer_btn.Name = "customer_btn";
            this.customer_btn.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.customer_btn.ShadowDecoration.Parent = this.customer_btn;
            this.customer_btn.Size = new System.Drawing.Size(148, 37);
            this.customer_btn.TabIndex = 4;
            this.customer_btn.Text = "Customer";
            this.customer_btn.TextOffset = new System.Drawing.Point(-1, 0);
            this.customer_btn.Click += new System.EventHandler(this.customer_btn_Click);
            // 
            // Transaction
            // 
            this.Transaction.BackColor = System.Drawing.Color.Transparent;
            this.Transaction.BorderRadius = 5;
            this.Transaction.CheckedState.Parent = this.Transaction;
            this.Transaction.CustomImages.Parent = this.Transaction;
            this.Transaction.FillColor = System.Drawing.Color.Transparent;
            this.Transaction.Font = new System.Drawing.Font("Roboto", 10F);
            this.Transaction.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Transaction.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Transaction.HoverState.Parent = this.Transaction;
            this.Transaction.Image = global::CarRent.Properties.Resources.transact;
            this.Transaction.ImageOffset = new System.Drawing.Point(-2, 0);
            this.Transaction.ImageSize = new System.Drawing.Size(23, 23);
            this.Transaction.Location = new System.Drawing.Point(10, 209);
            this.Transaction.Name = "Transaction";
            this.Transaction.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Transaction.ShadowDecoration.Parent = this.Transaction;
            this.Transaction.Size = new System.Drawing.Size(148, 37);
            this.Transaction.TabIndex = 3;
            this.Transaction.Text = "Transaction";
            this.Transaction.TextOffset = new System.Drawing.Point(2, 0);
            this.Transaction.Click += new System.EventHandler(this.Transaction_Click);
            // 
            // carGarage
            // 
            this.carGarage.BackColor = System.Drawing.Color.Transparent;
            this.carGarage.BorderRadius = 5;
            this.carGarage.CheckedState.Parent = this.carGarage;
            this.carGarage.CustomImages.Parent = this.carGarage;
            this.carGarage.FillColor = System.Drawing.Color.Transparent;
            this.carGarage.Font = new System.Drawing.Font("Roboto", 10F);
            this.carGarage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.carGarage.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.carGarage.HoverState.Parent = this.carGarage;
            this.carGarage.Image = global::CarRent.Properties.Resources.garage;
            this.carGarage.ImageOffset = new System.Drawing.Point(-3, 0);
            this.carGarage.ImageSize = new System.Drawing.Size(22, 22);
            this.carGarage.Location = new System.Drawing.Point(10, 161);
            this.carGarage.Name = "carGarage";
            this.carGarage.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.carGarage.ShadowDecoration.Parent = this.carGarage;
            this.carGarage.Size = new System.Drawing.Size(148, 37);
            this.carGarage.TabIndex = 2;
            this.carGarage.Text = "Car Garage";
            this.carGarage.TextOffset = new System.Drawing.Point(1, 0);
            this.carGarage.Click += new System.EventHandler(this.carGarage_Click);
            // 
            // overView
            // 
            this.overView.BackColor = System.Drawing.Color.Transparent;
            this.overView.BorderRadius = 5;
            this.overView.CheckedState.Parent = this.overView;
            this.overView.CustomImages.Parent = this.overView;
            this.overView.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.overView.Font = new System.Drawing.Font("Roboto", 10F);
            this.overView.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.overView.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.overView.HoverState.Parent = this.overView;
            this.overView.Image = global::CarRent.Properties.Resources.dashboard;
            this.overView.ImageOffset = new System.Drawing.Point(-5, 0);
            this.overView.Location = new System.Drawing.Point(10, 113);
            this.overView.Name = "overView";
            this.overView.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.overView.ShadowDecoration.Parent = this.overView;
            this.overView.Size = new System.Drawing.Size(148, 37);
            this.overView.TabIndex = 1;
            this.overView.Text = "Overview";
            this.overView.TextOffset = new System.Drawing.Point(-1, 0);
            this.overView.Click += new System.EventHandler(this.overView_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(37, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 27);
            this.label3.TabIndex = 0;
            this.label3.Text = "DaCars";
            // 
            // viewPanel
            // 
            this.viewPanel.BackColor = System.Drawing.Color.Transparent;
            this.viewPanel.Location = new System.Drawing.Point(199, 46);
            this.viewPanel.Name = "viewPanel";
            this.viewPanel.ShadowDecoration.Parent = this.viewPanel;
            this.viewPanel.Size = new System.Drawing.Size(814, 555);
            this.viewPanel.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1025, 613);
            this.Controls.Add(this.guna2Panel2);
            this.Controls.Add(this.viewPanel);
            this.Controls.Add(this.guna2Panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel2.ResumeLayout(false);
            this.guna2Panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2DragControl guna2DragControl1;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel2;
        private System.Windows.Forms.Label label3;
        private Guna.UI2.WinForms.Guna2Button overView;
        private Guna.UI2.WinForms.Guna2Button Transaction;
        private Guna.UI2.WinForms.Guna2Button carGarage;
        private Guna.UI2.WinForms.Guna2Panel viewPanel;
        private Guna.UI2.WinForms.Guna2Button customer_btn;
    }
}

