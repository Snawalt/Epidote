using Guna.UI2.WinForms;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Epidote.Forms
{
    public partial class LoginUI : Form
    {
        [DllImport("DwmApi", EntryPoint = "DwmSetWindowAttribute")]
        private static extern int SetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);

        public LoginUI()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            errorGroupBox.Text = "";
            errorGroupBox.Visible = false;
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            int result = SetWindowAttribute(Handle, 19, new[] { 1 }, 4);
            if (result != 0)
            {
                SetWindowAttribute(Handle, 20, new[] { 1 }, 4);
            }
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            string username = username_box.Text;

            if (username.Length < Epidote.LandingUI.MIN_USERNAME_LENGTH ||
                username.Length > Epidote.LandingUI.MAX_USERNAME_LENGTH ||
                !Regex.IsMatch(username, @"^[a-zA-Z0-9_]+$"))
            {
                errorGroupBox.Text = "The username you have entered is incorrect";
                errorGroupBox.Visible = true;
                return;
            }

            Task.Run(() =>
            {
                Epidote.Program._username = username;
                string usernameDirectory = Epidote.Utils.FileVerification._usernameDirectory;

                if (!Directory.Exists(usernameDirectory))
                {
                    Directory.CreateDirectory(usernameDirectory);
                }

                string usernamePath = Epidote.Utils.FileVerification._usernamePath;
                File.WriteAllText(usernamePath, username);

                LandingUI landingUI = new LandingUI();
                Hide();
                landingUI.ShowDialog();
                Close();
            });
        }

        private void LoginUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            string usernameDirectory = Epidote.Utils.FileVerification._usernameDirectory;
            string usernamePath = Epidote.Utils.FileVerification._usernamePath;

            if (Directory.Exists(usernameDirectory) && File.Exists(usernamePath))
            {
                FileStream stream = new FileStream(usernamePath, FileMode.Open, FileAccess.Read, FileShare.None);
                StreamReader reader = new StreamReader(stream);
                if (reader.ReadToEnd().Length == 0)
                {
                    Directory.Delete(usernameDirectory, true);
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
