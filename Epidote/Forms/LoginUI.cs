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

        protected override void OnHandleCreated(EventArgs e)
        {
            // Calls the DwmSetWindowAttribute function to set the window attribute
            int result = SetWindowAttribute(Handle, 19, new[] { 1 }, 4);

            // If the result of the function call is not 0, call the function again with a different attribute value
            if (result != 0)
            {
                SetWindowAttribute(Handle, 20, new[] { 1 }, 4);
            }
        }
        public LoginUI()
        {
            // Initializes the UI components
            InitializeComponent();

            // Disables checking for illegal cross-thread calls, which is used to ensure that a control is accessed from the same thread as the one that created it
            CheckForIllegalCrossThreadCalls = false;

            // Clears the text of the error group box and sets its visibility to false
            errorGroupBox.Text = "";
            errorGroupBox.Visible = false;
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            // Retrieves the text entered in the username text box
            string username = username_box.Text;

            // Checks if the username meets the minimum and maximum length requirements and if it only contains valid characters
            if (username.Length < Epidote.LandingUI.MIN_USERNAME_LENGTH ||
                username.Length > Epidote.LandingUI.MAX_USERNAME_LENGTH ||
                !Regex.IsMatch(username, @"^[a-zA-Z0-9_]+$"))
            {
                // If the username is invalid, sets the text of the error group box to display the error message and sets its visibility to true
                errorGroupBox.Text = "The username you have entered is incorrect";
                errorGroupBox.Visible = true;
                return;
            }

            // Starts a new task to handle the rest of the logic
            Task.Run(() =>
            {
                // Assigns the username to the static "_username" field of the Program class
                Epidote.Program._username = username;

                // Retrieves the path of the directory for the user's data
                string usernameDirectory = Epidote.Utils.FileVerification._usernameDirectory;

                // If the directory does not exist, create it
                if (!Directory.Exists(usernameDirectory))
                {
                    Directory.CreateDirectory(usernameDirectory);
                }

                // Retrieves the path of the file that stores the username
                string usernamePath = Epidote.Utils.FileVerification._usernamePath;

                // Writes the username to the file
                File.WriteAllText(usernamePath, username);

                // Get the current process instance
                Process process = Process.GetCurrentProcess();

                // Start a new instance of the same process
                Process.Start(process.MainModule.FileName);

                // Terminate the current process
                process.Kill();
            });
        }

        private void LoginUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Get the directory path and file path where the username is stored
            string usernameDirectory = Epidote.Utils.FileVerification._usernameDirectory;
            string usernamePath = Epidote.Utils.FileVerification._usernamePath;

            // If the directory and file exists, check if the file contains any data
            if (Directory.Exists(usernameDirectory) && File.Exists(usernamePath))
            {
                // Open the file and read its content
                FileStream stream = new FileStream(usernamePath, FileMode.Open, FileAccess.Read, FileShare.None);
                StreamReader reader = new StreamReader(stream);

                // If the file is empty, delete the directory
                if (reader.ReadToEnd().Length == 0)
                {
                    Directory.Delete(usernameDirectory, true);
                }
            }

            // Close the application
            Application.Exit();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            // Open a Discord invite link in the default browser
            string inviteLink = "https://discord.gg/nT73fjuDQp";
            Process.Start(inviteLink);
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            // Open the website in the default browser
            string websiteLink = "https://epidote.lol";
            Process.Start(websiteLink);
        }
    }
}
