using Epidote;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace LunarMeow.Utils
{
    public class FileDownloader
    {
        public static void DownloadAndInstallAddons()
        {
            string addonInstallationDirectory = Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE") + @"\.lunarclient\addons\");

            const string CrackedAccountAddonUrl = "https://github.com/Nilsen84/lunar-client-agents/releases/download/v1.2.0/CrackedAccount.jar";
            const string CrackedAccountAddonChecksum = "8d5b780119246248f5a7f84e83ac37d555ab2b74";
            const string HitDelayFixAddonUrl = "https://github.com/Nilsen84/lunar-client-agents/releases/download/v1.2.0/HitDelayFix.jar";
            const string HitDelayFixAddonChecksum = "ddc379ea73182a25a12fdcfab2ffe9113de3b450";

            // Create the addon installation directory if it doesn't already exist.
            if (!Directory.Exists(addonInstallationDirectory))
            {
                Directory.CreateDirectory(addonInstallationDirectory);
            }

            // Download and verify the addon files.
            if (Directory.Exists(addonInstallationDirectory))
            {
                string crackedAccountAddonPath = Path.Combine(addonInstallationDirectory, "CrackedAccount");
                string hitDelayFixAddonPath = Path.Combine(addonInstallationDirectory, "HitDelayFix");

                // Download and verify the CrackedAccount addon. 
                 DownloadAndVerifyAddon(CrackedAccountAddonUrl, CrackedAccountAddonChecksum, crackedAccountAddonPath);

                // Download and verify the HitDelayFix addon. 
                 DownloadAndVerifyAddon(HitDelayFixAddonUrl, HitDelayFixAddonChecksum, hitDelayFixAddonPath);
            }
        }

        public static void DownloadAndVerifyAddon(string addonUrl, string addonChecksum, string addonPath)
        {
            try
            {
                // Download the addon if it doesn't already exist
                if (!File.Exists(addonPath))
                {
                    Epidote.Utils.SafeWebClient.DownloadFile(addonUrl, addonPath);
                }
                // Verify the addon's checksum
                string actualChecksum = CalculateChecksum(addonPath);
                if (addonChecksum != actualChecksum)
                {
                    Epidote.Utils.SafeWebClient.DownloadFile(addonUrl, addonPath);
                    ExceptionLogger.Write(LogEvent.Warning, $"Invalid checksum for {addonPath} addon.", false);
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.Write(LogEvent.Warning, $"Error: {ex.ToString()}", false);
            }
        }


        private static string CalculateChecksum(string filePath)
        {
            using (var stream = File.OpenRead(filePath))
            {
                var sha1 = SHA1.Create();
                byte[] hash = sha1.ComputeHash(stream);
                return BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();
            }
        }
    }
}