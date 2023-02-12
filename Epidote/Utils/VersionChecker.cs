using Epidote.Forms;
using System;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Forms;

namespace Epidote.Utils
{
    public class VersionChecker
    {

        // Current version number of the software
        public const string CurrentVersion = "1.0.0";

        // The latest version number as determined by the database
        public static string LatestVersion;

        // Flag to indicate whether the software is outdated
        public static bool isOutdatedVersion = false;

        static string url = "https://raw.githubusercontent.com/Snawalt/Epidote/master/version.txt";


        // Check if the software is up to date
        public static bool isUpdateAvaiable()
        {
            using (WebClient client = new WebClient())
            {
                string latestVersion = client.DownloadString(url).Trim();
                if (CurrentVersion != latestVersion)
                    return true;
                else
                    return false;
            }
        }
    }
}
