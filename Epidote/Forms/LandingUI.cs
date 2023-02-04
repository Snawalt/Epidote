using Epidote.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace Epidote
{
    public partial class LandingUI : Form
    {
        [DllImport("DwmApi")] //System.Runtime.InteropServices
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);

        protected override void OnHandleCreated(EventArgs e)
        {
            if (DwmSetWindowAttribute(Handle, 19, new[] { 1 }, 4) != 0)
                DwmSetWindowAttribute(Handle, 20, new[] { 1 }, 4);
        }

        // Blur things

        private bool isBlurred = false;

        [DllImport("dwmapi.dll", PreserveSig = false)]
        private static extern void DwmEnableBlurBehindWindow(IntPtr hWnd, ref DWM_BLURBEHIND blurBehind);

        [StructLayout(LayoutKind.Sequential)]
        private struct DWM_BLURBEHIND
        {
            public uint dwFlags;
            public bool fEnable;
            public IntPtr hRgnBlur;
            public bool fTransitionOnMaximized;
        }


        // Constants for minimum and maximum username length
        public const int MIN_USERNAME_LENGTH = 3;
        public const int MAX_USERNAME_LENGTH = 16;

        // Flag to indicate whether the username is valid
        public static bool _isUsernameValid = false;



        public LandingUI()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void LandingUI_Load(object sender, EventArgs e)
        {
            guna2Button5.Text = Program._username;
            var request = WebRequest.Create("https://mc-heads.net/avatar/" + Program._username);

            using (var response = request.GetResponse())
            using (var stream = response.GetResponseStream())
            {
                guna2PictureBox3.Image = Bitmap.FromStream(stream);
            }

            Epidote.Utils.VersionChecker.CheckVersion();

            Console.WriteLine("Memory used before collection:       {0:N0}",
                              GC.GetTotalMemory(false));

            // Collect all generations of memory.
            GC.Collect();
            Console.WriteLine("Memory used after full collection:   {0:N0}",
                              GC.GetTotalMemory(true));
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {

                Epidote.Game.ManipulateLunarLogin.DisableLunarAutoLogin();
                Epidote.Game.GameLauncher.LaunchLunar();

                // if client exited go back to the launch page

                //code

            });
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            Directory.Delete(Epidote.Program._usernameDirectory, true);
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


        private void LandingUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Directory.Exists(Program._usernameDirectory))
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

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            string _namemcProfile = "https://namemc.com/profile/"+Program._username;
            Process.Start(_namemcProfile);
        }
    }
}
