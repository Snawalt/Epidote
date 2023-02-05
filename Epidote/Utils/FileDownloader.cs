using System;
using System.IO;
using System.Security.Cryptography;

namespace Epidote.Utils
{
    public class FileDownloader
    {

        // A static variable indicating whether the addons have been downloaded
        public static bool IsDownloaded = false;

        // Method to download and install addons
        public static void DownloadAndInstallAddons()
        {
            // Path to the addon installation directory
            string addonInstallationDirectory = Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE") + @"\.lunarclient\addons\");

            // URLs and checksums of the addons to be downloaded
            const string CrackedAccountAddonUrl = "https://github.com/Nilsen84/lunar-client-agents/releases/download/v1.2.0/CrackedAccount.jar";
            const string CrackedAccountAddonChecksum = "8d5b780119246248f5a7f84e83ac37d555ab2b74";
            const string HitDelayFixAddonUrl = "https://github.com/Nilsen84/lunar-client-agents/releases/download/v1.2.0/HitDelayFix.jar";
            const string HitDelayFixAddonChecksum = "ddc379ea73182a25a12fdcfab2ffe9113de3b450";

            // Check if the addon installation directory exists, and create it if it doesn't
            if (!Directory.Exists(addonInstallationDirectory))
            {
                Directory.CreateDirectory(addonInstallationDirectory);
            }

            // If the addon installation directory exists, download and verify the addon files
            if (Directory.Exists(addonInstallationDirectory))
            {
                // Path to the CrackedAccount addon file
                string crackedAccountAddonPath = Path.Combine(addonInstallationDirectory, "CrackedAccount");
                // Path to the HitDelayFix addon file
                string hitDelayFixAddonPath = Path.Combine(addonInstallationDirectory, "HitDelayFix");

                // Download and verify the CrackedAccount addon
                DownloadAndVerifyAddon(CrackedAccountAddonUrl, CrackedAccountAddonChecksum, crackedAccountAddonPath);

                // Download and verify the HitDelayFix addon
                DownloadAndVerifyAddon(HitDelayFixAddonUrl, HitDelayFixAddonChecksum, hitDelayFixAddonPath);

                // Set the IsDownloaded variable to true
                IsDownloaded = true;
            }
        }

        // Method to download and verify addons

        public static void DownloadAndVerifyAddon(string addonUrl, string addonChecksum, string addonPath)
        {
            try
            {
                // Download the addon if it doesn't already exist
                if (!File.Exists(addonPath))
                {
                    // Call the `DownloadFile` method from the `SafeWebClient` class to download the file from the specified URL
                    Epidote.Utils.SafeWebClient.DownloadFile(addonUrl, addonPath);
                }
                // Verify the addon's checksum
                string actualChecksum = CalculateChecksum(addonPath);
                if (addonChecksum != actualChecksum)
                {
                    // Download the file again if the checksum doesn't match
                    Epidote.Utils.SafeWebClient.DownloadFile(addonUrl, addonPath);
                    // Log a warning message indicating that the checksum is invalid
                    ExceptionLogger.Write(LogEvent.Warning, $"Invalid checksum for {addonPath} addon.", false);
                }
            }
            catch (Exception ex)
            {
                // Log an error message if there is an exception while downloading or verifying the addon
                ExceptionLogger.Write(LogEvent.Warning, $"Error: {ex.ToString()}", false);
            }
        }

        private static string CalculateChecksum(string filePath)
        {
            // Use a `using` statement to open a stream for reading the file
            using (var stream = File.OpenRead(filePath))
            {
                // Create an instance of the SHA1 hash algorithm
                var sha1 = SHA1.Create();
                // Compute the hash of the file contents
                byte[] hash = sha1.ComputeHash(stream);
                // Convert the hash to a string representation and remove the hyphens
                return BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();
            }
        }
    }
}