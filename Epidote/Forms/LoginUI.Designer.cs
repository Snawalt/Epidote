namespace Epidote.Forms
{
    partial class LoginUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginUI));
            this.login_button = new Guna.UI2.WinForms.Guna2GradientButton();
            this.username_box = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2HtmlLabel6 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel5 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.errorGroupBox = new Guna.UI2.WinForms.Guna2GroupBox();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.tos_button = new Guna.UI2.WinForms.Guna2Button();
            this.website_button = new Guna.UI2.WinForms.Guna2Button();
            this.discord_button = new Guna.UI2.WinForms.Guna2Button();
            this.guna2PictureBox3 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.guna2HtmlLabel3 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2Separator1 = new Guna.UI2.WinForms.Guna2Separator();
            this.guna2Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // login_button
            // 
            this.login_button.Animated = true;
            this.login_button.AnimatedGIF = true;
            this.login_button.BackColor = System.Drawing.Color.Transparent;
            this.login_button.BorderRadius = 5;
            this.login_button.BorderThickness = 1;
            this.login_button.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.login_button.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.login_button.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.login_button.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.login_button.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.login_button.FillColor = System.Drawing.Color.White;
            this.login_button.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.login_button.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.login_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.login_button.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.login_button.HoverState.BorderColor = System.Drawing.Color.White;
            this.login_button.HoverState.FillColor = System.Drawing.Color.White;
            this.login_button.HoverState.FillColor2 = System.Drawing.Color.White;
            this.login_button.Location = new System.Drawing.Point(263, 318);
            this.login_button.Name = "login_button";
            this.login_button.ShadowDecoration.BorderRadius = 4;
            this.login_button.ShadowDecoration.Color = System.Drawing.Color.White;
            this.login_button.ShadowDecoration.Enabled = true;
            this.login_button.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(1);
            this.login_button.Size = new System.Drawing.Size(130, 32);
            this.login_button.TabIndex = 36;
            this.login_button.Text = "Login";
            this.login_button.UseTransparentBackground = true;
            this.login_button.Click += new System.EventHandler(this.guna2GradientButton1_Click);
            // 
            // username_box
            // 
            this.username_box.Animated = true;
            this.username_box.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(149)))), ((int)(((byte)(149)))));
            this.username_box.BorderRadius = 5;
            this.username_box.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.username_box.DefaultText = "";
            this.username_box.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.username_box.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.username_box.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.username_box.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.username_box.FillColor = System.Drawing.Color.Black;
            this.username_box.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(149)))), ((int)(((byte)(149)))));
            this.username_box.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.username_box.ForeColor = System.Drawing.Color.White;
            this.username_box.HoverState.BorderColor = System.Drawing.Color.White;
            this.username_box.Location = new System.Drawing.Point(54, 318);
            this.username_box.Name = "username_box";
            this.username_box.PasswordChar = '\0';
            this.username_box.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.username_box.PlaceholderText = "Enter your Username";
            this.username_box.SelectedText = "";
            this.username_box.Size = new System.Drawing.Size(193, 32);
            this.username_box.TabIndex = 35;
            // 
            // guna2HtmlLabel6
            // 
            this.guna2HtmlLabel6.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel6.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.guna2HtmlLabel6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(149)))), ((int)(((byte)(149)))));
            this.guna2HtmlLabel6.IsSelectionEnabled = false;
            this.guna2HtmlLabel6.Location = new System.Drawing.Point(54, 274);
            this.guna2HtmlLabel6.Name = "guna2HtmlLabel6";
            this.guna2HtmlLabel6.Size = new System.Drawing.Size(320, 19);
            this.guna2HtmlLabel6.TabIndex = 33;
            this.guna2HtmlLabel6.Text = " access all the features and benefits we have to offer.";
            // 
            // guna2HtmlLabel5
            // 
            this.guna2HtmlLabel5.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel5.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.guna2HtmlLabel5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(149)))), ((int)(((byte)(149)))));
            this.guna2HtmlLabel5.IsSelectionEnabled = false;
            this.guna2HtmlLabel5.Location = new System.Drawing.Point(54, 251);
            this.guna2HtmlLabel5.Name = "guna2HtmlLabel5";
            this.guna2HtmlLabel5.Size = new System.Drawing.Size(358, 19);
            this.guna2HtmlLabel5.TabIndex = 32;
            this.guna2HtmlLabel5.Text = "Your username is your unique identifier that allows you to";
            // 
            // guna2HtmlLabel2
            // 
            this.guna2HtmlLabel2.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel2.Font = new System.Drawing.Font("Segoe UI Semibold", 15F, System.Drawing.FontStyle.Bold);
            this.guna2HtmlLabel2.ForeColor = System.Drawing.Color.White;
            this.guna2HtmlLabel2.IsSelectionEnabled = false;
            this.guna2HtmlLabel2.Location = new System.Drawing.Point(54, 211);
            this.guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            this.guna2HtmlLabel2.Size = new System.Drawing.Size(205, 30);
            this.guna2HtmlLabel2.TabIndex = 31;
            this.guna2HtmlLabel2.Text = "Login to Your account";
            // 
            // errorGroupBox
            // 
            this.errorGroupBox.BackColor = System.Drawing.Color.Transparent;
            this.errorGroupBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(54)))), ((int)(((byte)(127)))));
            this.errorGroupBox.BorderRadius = 6;
            this.errorGroupBox.BorderThickness = 0;
            this.errorGroupBox.CustomBorderColor = System.Drawing.Color.Transparent;
            this.errorGroupBox.FillColor = System.Drawing.Color.Transparent;
            this.errorGroupBox.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.errorGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(54)))), ((int)(((byte)(127)))));
            this.errorGroupBox.Location = new System.Drawing.Point(43, 360);
            this.errorGroupBox.Name = "errorGroupBox";
            this.errorGroupBox.ShadowDecoration.BorderRadius = 8;
            this.errorGroupBox.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(54)))), ((int)(((byte)(127)))));
            this.errorGroupBox.ShadowDecoration.Depth = 1;
            this.errorGroupBox.ShadowDecoration.Enabled = true;
            this.errorGroupBox.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(3);
            this.errorGroupBox.Size = new System.Drawing.Size(260, 25);
            this.errorGroupBox.TabIndex = 41;
            this.errorGroupBox.Text = "The username you have entered is incorrect";
            this.errorGroupBox.TextOffset = new System.Drawing.Point(0, -6);
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.Controls.Add(this.tos_button);
            this.guna2Panel1.Controls.Add(this.website_button);
            this.guna2Panel1.Controls.Add(this.discord_button);
            this.guna2Panel1.Controls.Add(this.guna2PictureBox3);
            this.guna2Panel1.Controls.Add(this.guna2HtmlLabel3);
            this.guna2Panel1.Location = new System.Drawing.Point(-1, 1);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(734, 55);
            this.guna2Panel1.TabIndex = 43;
            // 
            // tos_button
            // 
            this.tos_button.Animated = true;
            this.tos_button.AnimatedGIF = true;
            this.tos_button.BackColor = System.Drawing.Color.Transparent;
            this.tos_button.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.tos_button.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.tos_button.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.tos_button.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.tos_button.FillColor = System.Drawing.Color.Transparent;
            this.tos_button.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.tos_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(149)))), ((int)(((byte)(149)))));
            this.tos_button.HoverState.ForeColor = System.Drawing.Color.White;
            this.tos_button.Location = new System.Drawing.Point(348, 18);
            this.tos_button.Name = "tos_button";
            this.tos_button.PressedColor = System.Drawing.Color.Transparent;
            this.tos_button.PressedDepth = 0;
            this.tos_button.Size = new System.Drawing.Size(54, 21);
            this.tos_button.TabIndex = 36;
            this.tos_button.Text = "Tos";
            this.tos_button.UseTransparentBackground = true;
            // 
            // website_button
            // 
            this.website_button.Animated = true;
            this.website_button.AnimatedGIF = true;
            this.website_button.BackColor = System.Drawing.Color.Transparent;
            this.website_button.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.website_button.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.website_button.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.website_button.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.website_button.FillColor = System.Drawing.Color.Transparent;
            this.website_button.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.website_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(149)))), ((int)(((byte)(149)))));
            this.website_button.HoverState.ForeColor = System.Drawing.Color.White;
            this.website_button.Location = new System.Drawing.Point(269, 18);
            this.website_button.Name = "website_button";
            this.website_button.PressedColor = System.Drawing.Color.Transparent;
            this.website_button.PressedDepth = 0;
            this.website_button.Size = new System.Drawing.Size(70, 21);
            this.website_button.TabIndex = 35;
            this.website_button.Text = "Website";
            this.website_button.UseTransparentBackground = true;
            this.website_button.Click += new System.EventHandler(this.guna2Button3_Click);
            // 
            // discord_button
            // 
            this.discord_button.Animated = true;
            this.discord_button.AnimatedGIF = true;
            this.discord_button.BackColor = System.Drawing.Color.Transparent;
            this.discord_button.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.discord_button.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.discord_button.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.discord_button.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.discord_button.FillColor = System.Drawing.Color.Transparent;
            this.discord_button.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.discord_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(149)))), ((int)(((byte)(149)))));
            this.discord_button.HoverState.ForeColor = System.Drawing.Color.White;
            this.discord_button.Location = new System.Drawing.Point(192, 18);
            this.discord_button.Name = "discord_button";
            this.discord_button.PressedColor = System.Drawing.Color.Transparent;
            this.discord_button.PressedDepth = 0;
            this.discord_button.Size = new System.Drawing.Size(68, 21);
            this.discord_button.TabIndex = 34;
            this.discord_button.Text = "Discord";
            this.discord_button.UseTransparentBackground = true;
            this.discord_button.Click += new System.EventHandler(this.guna2Button2_Click);
            // 
            // guna2PictureBox3
            // 
            this.guna2PictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.guna2PictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("guna2PictureBox3.Image")));
            this.guna2PictureBox3.ImageRotate = 0F;
            this.guna2PictureBox3.Location = new System.Drawing.Point(55, 8);
            this.guna2PictureBox3.Name = "guna2PictureBox3";
            this.guna2PictureBox3.ShadowDecoration.BorderRadius = 26;
            this.guna2PictureBox3.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(0)))), ((int)(((byte)(235)))));
            this.guna2PictureBox3.ShadowDecoration.Depth = 1;
            this.guna2PictureBox3.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(12);
            this.guna2PictureBox3.Size = new System.Drawing.Size(38, 38);
            this.guna2PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.guna2PictureBox3.TabIndex = 1;
            this.guna2PictureBox3.TabStop = false;
            this.guna2PictureBox3.UseTransparentBackground = true;
            // 
            // guna2HtmlLabel3
            // 
            this.guna2HtmlLabel3.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel3.Font = new System.Drawing.Font("Segoe UI Semibold", 15F, System.Drawing.FontStyle.Bold);
            this.guna2HtmlLabel3.ForeColor = System.Drawing.Color.White;
            this.guna2HtmlLabel3.IsSelectionEnabled = false;
            this.guna2HtmlLabel3.Location = new System.Drawing.Point(95, 12);
            this.guna2HtmlLabel3.Name = "guna2HtmlLabel3";
            this.guna2HtmlLabel3.Size = new System.Drawing.Size(72, 30);
            this.guna2HtmlLabel3.TabIndex = 2;
            this.guna2HtmlLabel3.Text = "Epidote";
            // 
            // guna2Separator1
            // 
            this.guna2Separator1.BackColor = System.Drawing.Color.Transparent;
            this.guna2Separator1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.guna2Separator1.Location = new System.Drawing.Point(-1, 56);
            this.guna2Separator1.Name = "guna2Separator1";
            this.guna2Separator1.Size = new System.Drawing.Size(734, 1);
            this.guna2Separator1.TabIndex = 42;
            this.guna2Separator1.UseTransparentBackground = true;
            // 
            // LoginUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(734, 561);
            this.Controls.Add(this.guna2Panel1);
            this.Controls.Add(this.guna2Separator1);
            this.Controls.Add(this.errorGroupBox);
            this.Controls.Add(this.login_button);
            this.Controls.Add(this.username_box);
            this.Controls.Add(this.guna2HtmlLabel6);
            this.Controls.Add(this.guna2HtmlLabel5);
            this.Controls.Add(this.guna2HtmlLabel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "LoginUI";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LoginUI_FormClosing);
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2GradientButton login_button;
        private Guna.UI2.WinForms.Guna2TextBox username_box;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel6;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel5;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private Guna.UI2.WinForms.Guna2GroupBox errorGroupBox;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2Button tos_button;
        private Guna.UI2.WinForms.Guna2Button website_button;
        private Guna.UI2.WinForms.Guna2Button discord_button;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox3;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel3;
        private Guna.UI2.WinForms.Guna2Separator guna2Separator1;
    }
}