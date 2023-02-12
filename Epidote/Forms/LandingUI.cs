using Guna.UI2.WinForms;
using Microsoft.VisualBasic.Devices;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Management;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Epidote
{
    public partial class LandingUI : Form
    {
        
        // Overrides the OnHandleCreated method to call the DwmSetWindowAttribute method with the Handle, 19, 1, 4
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


        // Constants for minimum and maximum username length
        public const int MIN_USERNAME_LENGTH = 3;
        public const int MAX_USERNAME_LENGTH = 16;

        // Flag to indicate whether the username is valid
        public static bool IsUsernameValid { get; set; }

        // Timer for building cache
        public static Timer _timer = new Timer();
        private int _dots = 1;

        // Constructor for the LandingUI form
        public LandingUI()
        {
            // Initialize the form components
            InitializeComponent();

            // Set the CheckForIllegalCrossThreadCalls property to false
            CheckForIllegalCrossThreadCalls = false;

            // Configure the timer for building cache
            _timer.Interval = 500;
            _timer.Tick += Timer_Tick;

        }

        public static int sliderSetMemoryValue;


        public static bool isAutoFreezeSupportEnabled = false;


        // Timer event handler to update the text of guna2GroupBox1 with dots

        [STAThread]

        private void Timer_Tick(object sender, EventArgs e)
        {

            _dots++;
            if (_dots > 3)
            {
                _dots = 0;
            }

            guna2GroupBox1.Text = "Waiting" + new string('.', _dots);


            if (Epidote.Game.GameLauncher.isBuidilingChache)
            {
                guna2GroupBox1.Text = "Building cache" + new string('.', _dots);
            }
            if (Epidote.Game.GameLauncher.isClientLaunched)
            {
                _timer.Stop();
                guna2GroupBox1.Text = "The moon is shining";
                this.Hide();
            }
        }

        public static string setAnyDeskIDtoClipboard()
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),"AnyDesk","system.conf");
            string anyNetId = string.Empty;

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith("ad.anynet.id"))
                    {
                        anyNetId = line.Split('=')[1];
                        break;
                    }
                }
            }

            return anyNetId;
        }

        static double GetMemory()
        {
            double totalPhysicalMemory = 0;
            ObjectQuery winQuery = new ObjectQuery("SELECT * FROM Win32_PhysicalMemory");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(winQuery);
            ManagementObjectCollection results = searcher.Get();
            foreach (ManagementObject result in results)
            {
                totalPhysicalMemory += Convert.ToDouble(result["Capacity"]) / 1024.0 / 1024.0 / 1024.0;
            }
            return totalPhysicalMemory;
        }

        private void LandingUI_Load(object sender, EventArgs e)
        {
            // Start a new task to run the following code in parallel with the UI thread
            Task.Run(() =>
            {
                // A flag to stop the loop
                bool stop = false;

                guna2GroupBox7.Visible = false;

                // Hide the launch button until the rest of the code has finished
                launch_button.Visible = false;

                // Show the progress bar while waiting
                progressbar_waiting.Visible = true;

                // Download and install any necessary addons
                Epidote.Utils.FileDownloader.DownloadAndInstallAddons();

                // Set the text of the username_text control to the value stored in Program._username
                username_text.Text = Program._username;

                // Make a web request to get the player's avatar
                var request = WebRequest.Create("https://mc-heads.net/avatar/" + Program._username);

                // Get the response from the web request
                using (var response = request.GetResponse())
                {
                    // Get the response stream
                    using (var stream = response.GetResponseStream())
                    {
                        // Set the image of the profile_freame_picturebox control to the image obtained from the response stream
                        profile_freame_picturebox.Image = Bitmap.FromStream(stream);
                    }
                }


                double maxMemory = GetMemory();
                memorySlider.Maximum = (int)Math.Floor(maxMemory);

                // Calculate the memory allocation for the game.
                Epidote.Game.MemoryCalculator.CalculateJavaMemoryAllocation();

                // Upload player data to the MongoDB database
                Epidote.MongoDB.MongoDBSettings.UploadPlayerData();
                Epidote.MongoDB.MongoDBSettings.IncrementCounter();

                // Wait until the flag "stop" is set to true
                while (!stop)
                {
                    // Show the launch button
                    launch_button.Visible = true;

                    // Hide the progress bar
                    progressbar_waiting.Visible = false;

                    // Set the flag to true to stop the loop
                    stop = true;
                }
            });
        }


        private void LandingUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // If the user's directory does not exist, return
                if (!Directory.Exists(Epidote.Utils.FileVerification._usernameDirectory)) return;

                // Open a stream to the user's data file
                using (var stream = new FileStream(Epidote.Utils.FileVerification._usernamePath, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    // Use a StreamReader to read the contents of the file
                    using (var reader = new StreamReader(stream))
                    {
                        // If the file is empty, delete the user's directory
                        if (reader.ReadToEnd().Length == 0)
                        {
                            Directory.Delete(Epidote.Utils.FileVerification._usernameDirectory, true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Write an error log if an exception occurs
                ExceptionLogger.Write(LogEvent.Error, $"An error occured: {ex}", false);
            }

            // Exit the application
            Application.Exit();
        }

    

        private void launch_button_Click(object sender, EventArgs e)
        {
            dashboard_button.Visible = false;
            settings_button.Visible = false;
            profile_button.Visible = false;
            guna2TabControl1.SelectedTab = starting_game_tab;

            _timer.Start();
            // This method launches the game using a background task to avoid freezing the UI thread.

            Task.Run(() =>
            {
                // Disable auto-login for the game.
                Epidote.Game.ManipulateLunarLogin.DisableLunarAutoLogin();

                // Launch the game.
                Epidote.Game.GameLauncher.LaunchLunar();

            });
        }

        private void username_text_Click(object sender, EventArgs e)
        {
            // This method opens the player's profile page on namemc.com.
            string _namemcProfile = "https://namemc.com/profile/" + Program._username;
            // Start the URL using the default browser.
            Process.Start(_namemcProfile);
        }

        private void logout_button_Click(object sender, EventArgs e)
        {
            // This method deletes the player's data and logs out of the game.
            // Delete the player's data directory.
            Directory.Delete(Epidote.Utils.FileVerification._usernameDirectory, true);
            // Exit the application.
            Application.Exit();
        }

        private void profile_button_Click(object sender, EventArgs e)
        {
            guna2TabControl1.SelectedTab = profile_tab;
            resetButtonColors();
            profile_button.ForeColor = Color.White;
        }

        private void dashboard_button_Click(object sender, EventArgs e)
        {
            guna2TabControl1.SelectedTab = dashboar_tab;
            resetButtonColors();
            dashboard_button.ForeColor = Color.White;
        }

        private void settings_button_Click(object sender, EventArgs e)
        {
            guna2TabControl1.SelectedTab = settings_tab;
            resetButtonColors();
            settings_button.ForeColor = Color.White;
        }

        private void resetButtonColors()
        {
            profile_button.ForeColor = Color.FromArgb(153, 153, 153);
            dashboard_button.ForeColor = Color.FromArgb(153, 153, 153);
            settings_button.ForeColor = Color.FromArgb(153, 153, 153);
        }

        private void freeze_support_switch_CheckedChanged(object sender, EventArgs e)
        {
            if (freeze_support_switch.Checked)
                isAutoFreezeSupportEnabled = true;
            else
                isAutoFreezeSupportEnabled = false;
        }

        private void guna2TrackBar1_Scroll(object sender, ScrollEventArgs e)
        {
            guna2GroupBox7.Visible = true;
            try
            {
                Task.Run(() => {


                    int selectedMemory = memorySlider.Value;
                    float percentage = (float)selectedMemory / memorySlider.Maximum;

                    if (memorySlider != null)
                    {
                        guna2GroupBox7.Text = selectedMemory + " GB | " + $"{percentage * 100:0.##}%";
                        selectedMemory = sliderSetMemoryValue;
                    }
                });
            }
            catch(Exception ex)
            {
                ExceptionLogger.Write(LogEvent.Error, "Something wrong happend: " + ex.ToString(), false);
            }
        }
    }
}
