using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Epidote;

namespace Epidote.Forms
{
    public partial class LoginUI : Form
    {
        public LoginUI()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            guna2GroupBox1.Text = "";
            guna2GroupBox1.Visible = false;
        }

        [DllImport("DwmApi")] //System.Runtime.InteropServices
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);


        protected override void OnHandleCreated(EventArgs e)
        {
            if (DwmSetWindowAttribute(Handle, 19, new[] { 1 }, 4) != 0)
                DwmSetWindowAttribute(Handle, 20, new[] { 1 }, 4);
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            string username = guna2TextBox1.Text;


            if (username.Length < Epidote.LandingUI.MIN_USERNAME_LENGTH || username.Length > Epidote.LandingUI.MAX_USERNAME_LENGTH || !Regex.IsMatch(username, @"^[a-zA-Z0-9_]+$"))
            {
                guna2GroupBox1.Text = "The username you have entered is incorrect";
                guna2GroupBox1.Visible = true;
            }
            else
            {
                Task.Run(() => {
                    Epidote.Program._username = username;
                    if(!Directory.Exists(Program._usernameDirectory))
                    {
                        Directory.CreateDirectory(Program._usernameDirectory);
                    }
                    File.WriteAllText(Epidote.Program._usernamePath, username);
                    LandingUI LandingUI = new LandingUI();
                    this.Hide();
                    LandingUI.ShowDialog();
                    this.Close();
                });
       
            }
        }

        private void LoginUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Directory.Exists(Program._usernameDirectory) && File.Exists(Program._usernamePath))
            {
                using (FileStream stream = new FileStream(Epidote.Program._usernamePath, FileMode.Open, FileAccess.Read, FileShare.None))
                using (StreamReader reader = new StreamReader(stream))
                {
                    if (reader.ReadToEnd().Length == 0)
                    {
                        Directory.Delete(Program._usernameDirectory, true);
                    }
                }
            }
            Application.Exit();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            string inviteLink = "https://discord.gg/nT73fjuDQp";
            Process.Start(inviteLink);
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            string websiteLink = "https://epidote.lol";
            Process.Start(websiteLink);
        }
    }
}
