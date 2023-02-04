using Epidote.Forms;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Epidote
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 


        public static string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        public static string _usernamePath = Path.Combine(userProfile, "Epidote", "epidote-usernames.dat");
        public static string _usernameDirectory = Path.Combine(userProfile, "Epidote");

        public static string _username;
        [STAThread]
        static void Main()
        {
            if(Epidote.Game.LunarFilesCheck.IsReadyAsync().Result == false)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new ErrorUI());
            }
            else
            {
                if (!Directory.Exists(_usernameDirectory) || !File.Exists(_usernamePath) || IsFileDamaged())
                {
                    Directory.CreateDirectory(_usernameDirectory);

                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new LoginUI());
                }
                else
                {

                    _username = File.ReadAllText(_usernamePath);

                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new LandingUI());
                }
            }
           
        }


        public static bool IsFileDamaged()
        {
            if (Directory.Exists(Program._usernameDirectory) && File.Exists(Program._usernamePath))
            {
                using (FileStream stream = new FileStream(Epidote.Program._usernamePath, FileMode.Open, FileAccess.Read, FileShare.None))
                using (StreamReader reader = new StreamReader(stream))
                {
                    if (reader.ReadToEnd().Length == 0)
                    {
                        return true;
                    }
                    else
                        return false;
                }
            }
            return false;
        }
    }
}
