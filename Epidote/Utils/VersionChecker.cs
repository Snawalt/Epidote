using Epidote.Forms;
using MongoDB.Bson;
using MongoDB.Driver;
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

        // Check if the software is up to date
        public static bool isUpdateAvaiable()
        {
            if (Epidote.MongoDB.MongoDBSettings.GetVersion() != CurrentVersion)
            {
                LatestVersion = Epidote.MongoDB.MongoDBSettings.GetVersion();
                return true;
            }
            else
                return false;
        }
    }
}
