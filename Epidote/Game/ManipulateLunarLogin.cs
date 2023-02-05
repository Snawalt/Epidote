using System;
using System.IO;

namespace Epidote.Game
{
    public class ManipulateLunarLogin
    {
        // Constants representing the file names for the accounts file and the backup file
        private const string AccountsFileName = "accounts.json";
        private const string BackupFileName = "accountsBackupBeforeDisableAutoLogin.json";

        // Constant representing the path of the accounts file
        private static readonly string AccountsPath = Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE"),
            ".lunarclient",
            "settings",
            "game",
            AccountsFileName
            );

        // Constant representing the path of the backup file
        private static readonly string BackupPath = Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE"),
            ".lunarclient",
            "settings",
            "game",
            BackupFileName
            );

        // Method to disable the lunar client auto-login feature
        public static void DisableLunarAutoLogin()
        {
            try
            {
                // Check if the accounts file exists
                if (File.Exists(AccountsPath))
                {
                    // Check if the backup file does not exist
                    if (!File.Exists(BackupPath))
                    {
                        // If it does not exist, create a backup of the accounts file
                        File.Copy(AccountsPath, BackupPath, true);
                    }

                    // Delete the accounts file
                    File.Delete(AccountsPath);
                }
            }
            catch (Exception ex)
            {
                // Log the exception and handle it in some way, for example by showing an error message to the user
                ExceptionLogger.Write(LogEvent.Error, "Something wrong happend: " + ex.ToString(), false);
            }
        }

        // Method to enable the lunar client auto-login feature
        public static void EnableLunarAutoLogin()
        {
            try
            {
                // Check if the backup file exists
                if (File.Exists(BackupPath))
                {
                    // If it exists, copy the contents of the backup file to the accounts file
                    File.Copy(BackupPath, AccountsPath, true);

                    // Delete the backup file
                    File.Delete(BackupPath);
                }
            }
            catch (Exception ex)
            {
                // Log the exception and handle it in some way, for example by showing an error message to the user
                ExceptionLogger.Write(LogEvent.Error, "Something wrong happend: " + ex.ToString(), false);
            }
        }
    }
}
