﻿using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

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

        // Constants for minimum and maximum username length
        public const int MIN_USERNAME_LENGTH = 3;
        public const int MAX_USERNAME_LENGTH = 16;

        // Flag to indicate whether the username is valid
        public static bool _isUsernameValid = false;

        public LandingUI()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            //guna2HtmlLabel7.Text = "stable-"+Epidote.Utils.VersionChecker.CurrentVersion+"-release";
        }

        //static void UploadPlayerData()
        //{
        //    BsonDocument document = new BsonDocument
        //        {
        //            { "username", Epidote.Program._username }
        //        };

        //    var documents = Epidote.Database.MongoDBSettings.Collection.Find(new BsonDocument()).ToList();
        //    foreach (var doc in documents)
        //    {
        //        if (doc["username"] != Epidote.Program._username)
        //        {
        //            Epidote.Database.MongoDBSettings.Collection.InsertOne(doc);
        //        }
        //    }

        //}

        private void LandingUI_Load(object sender, EventArgs e)
        {
            bool stop = false;
            Task.Run(() =>
            {
                //UploadPlayerData();
                launch_button.Visible = false;
                progressbar_waiting.Visible = true;

                Epidote.Utils.FileDownloader.DownloadAndInstallAddons();
                username_text.Text = Program._username;
                var request = WebRequest.Create("https://mc-heads.net/avatar/" + Program._username);

                using (var response = request.GetResponse())
                using (var stream = response.GetResponseStream())
                {
                    profile_freame_picturebox.Image = Bitmap.FromStream(stream);
                }
                while (!stop)
                {
                    launch_button.Visible = true;
                    progressbar_waiting.Visible = false;
                    stop = true;
                }




                Console.WriteLine("Memory used before collection:       {0:N0}",
                                  GC.GetTotalMemory(false));

                // Collect all generations of memory.
                GC.Collect();
                Console.WriteLine("Memory used after full collection:   {0:N0}",
                                  GC.GetTotalMemory(true));


            });

        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {

                Epidote.Game.ManipulateLunarLogin.DisableLunarAutoLogin();
                //Epidote.Game.MemoryCalculator.CalculateJavaMemoryAllocation();
                Epidote.Game.GameLauncher.LaunchLunar();

                // if client exited go back to the launch page

                //code

            });
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            Directory.Delete(Epidote.Utils.FileVerification._usernameDirectory, true);
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
            if (Directory.Exists(Epidote.Utils.FileVerification._usernameDirectory))
            {
                using (FileStream stream = new FileStream(Epidote.Utils.FileVerification._usernamePath, FileMode.Open, FileAccess.Read, FileShare.None))
                using (StreamReader reader = new StreamReader(stream))
                {
                    if (reader.ReadToEnd().Length == 0)
                    {
                        Directory.Delete(Epidote.Utils.FileVerification._usernameDirectory, true);
                    }
                }
            }
            Application.Exit();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            string _namemcProfile = "https://namemc.com/profile/" + Program._username;
            Process.Start(_namemcProfile);
        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
