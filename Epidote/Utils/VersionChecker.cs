using System;

namespace Epidote.Utils
{
    public static class VersionChecker
    {
        public static void CheckVersion()
        {
            string currentVersion = "1.0.0";
            string latestVersion = DownloadLatestVersion();

            if (currentVersion != latestVersion)
            {
                Console.WriteLine("Error: Version mismatch");
                Console.WriteLine("Current version: " + currentVersion);
                Console.WriteLine("Latest version: " + latestVersion);
            }
            else
            {
                Console.WriteLine("Program version is up to date.");
            }
        }

        private static string DownloadLatestVersion()
        {
            string url = "https://pastebin.com/raw/1rrD5DCN";
            return SafeWebClient.DownloadString(url);
        }
    }
}
