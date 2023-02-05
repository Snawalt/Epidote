using Epidote.Forms;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Net;
using System.Windows.Forms;

namespace Epidote.Utils
{
    public class VersionChecker
    {

        // Current version number of the software
        public const string CurrentVersion = "1.0.1";

        // The latest version number as determined by the database
        public static string LatestVersion;

        // Flag to indicate whether the software is outdated
        public static bool isOutdatedVersion = false;

        // Check if the software is up to date
        public static bool isUpdateAvaiable()
        {
            try
            {
                foreach (var doc in Epidote.Database.MongoDBSettings.GetVersion()) //error
                {
                    LatestVersion = (string)doc["_version"];

                    if (doc["_version"] == CurrentVersion)
                        return false;
                    else
                        return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                ExceptionLogger.Write(LogEvent.Warning, $"Error: {ex.ToString()}", false);
                return false;
            }
        }
    }
}
