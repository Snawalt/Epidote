using Epidote.Forms;
using System;
using System.Net;
using System.Windows.Forms;

namespace Epidote.Utils
{
    public class VersionChecker
    {

        // URL to retrieve the latest version number from

        public const string LatestVersionUrl = "https://pastebin.com/raw/zdZHgrqP";

        // Current version number of the software
        public const string CurrentVersion = "1.0.0";

        // The latest version number as determined by the URL
        public static string LatestVersion { get; private set; }

        // Flag to indicate whether the server is available
        public static bool _isServerAvailable;

        // Flag to indicate whether the software is outdated
        public static bool isOutdatedVersion = false;


        // Check if the software is up to date
        public static void CheckForUpdates()
        {
            // Try to retrieve the latest version number from the URL
            try
            {
                using (var client = new WebClient())
                {
                    LatestVersion = client.DownloadString(LatestVersionUrl);
                    _isServerAvailable = true;

                    // Compare the latest version with the current version
                    if (LatestVersion != CurrentVersion)
                    {
                        // Set the outdated flag and show the version form if the software is outdated
                        isOutdatedVersion = true;
                        ShowVersionForm();
                    }
                }
            }
            catch (WebException ex)
            {
                // If the exception is a protocol error with a forbidden response, set the server unavailable flag and error message
                if (ex.Status == WebExceptionStatus.ProtocolError && ex.Response != null && (ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.Forbidden)
                {
                    _isServerAvailable = false;
                    Epidote.Utils.FileVerification.errorMessage = "Access to the update server is forbidden.";
                }
                else
                {
                    // Otherwise, set the server unavailable flag and a general error message
                    _isServerAvailable = false;
                    Epidote.Utils.FileVerification.errorMessage = "Something wrong happend.";
                }
            }
            catch (Exception ex)
            {
                // Show the error form and write the exception to the log
                ShowErrorForm();
                ExceptionLogger.Write(LogEvent.Error, "Something wrong happend: " + ex.ToString(), false);
                _isServerAvailable = false;
                Epidote.Utils.FileVerification.errorMessage = "Something wrong happend.";
            }
        }

        // Show the error form
        private static void ShowErrorForm()
        {
            // TODO: implement the error form and pass it the error message
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ErrorUI());
        }

        // Show the version form
        private static void ShowVersionForm()
        {
            // TODO: implement the error form and pass it the error message
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new VersionUI());
        }
    }
}
