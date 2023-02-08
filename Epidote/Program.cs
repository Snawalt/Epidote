using Epidote.Forms;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Windows.Forms;

namespace Epidote
{
    internal static class Program
    {
        // EntryPoint for the program

        // The publuc username

        public static string _username;

        [STAThread]
        static void Main()
        {
            
            // Check if debugger is attached, put ! the start of the line
            if (!Protection.AntiDebugging.DebuggingDetector.IsDebuggerAttached())
            {
                // Check if there is an outdated version of the software
                if (!Epidote.Utils.VersionChecker.isUpdateAvaiable())
                {

                    // Check if the LunarFiles are ready to be used
                    if (Epidote.Utils.FileVerification.IsReadyAsync().Result == false)
                    {

                        // Show the error form if the LunarFiles are not ready
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        Application.Run(new ErrorUI());
                    }
                    else
                    {

                        if (!Epidote.Utils.FileVerification.IsFileAndDirectoryExist())
                        {

                            // Show the login form
                            Application.EnableVisualStyles();
                            Application.SetCompatibleTextRenderingDefault(false);
                            Application.Run(new LoginUI());
                        }
                        else
                        {
                            // Read the username from the file
                            _username = File.ReadAllText(Epidote.Utils.FileVerification._usernamePath);

                            // Show the landing form
                            Application.EnableVisualStyles();
                            Application.SetCompatibleTextRenderingDefault(false);
                            Application.Run(new LandingUI());
                        }
                    }
                }
                else
                {
                    // Show the version form if there is an outdated version of the software
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new VersionUI());
                }
            }
            else
            {
                // Exit the application if the debugger is attached
                Application.Exit();
            }
        }
    }
}
