using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epidote.Game
{
    public class ManipulateLunarLogin
    {
        private const string AccountsFileName = "accounts.json";
        private const string BackupFileName = "accountsBackupBeforeDisableAutoLogin.json";

        private static readonly string AccountsPath = Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE"),
            ".lunarclient",
            "settings",
            "game",
            AccountsFileName
            );

        private static readonly string BackupPath = Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE"),
            ".lunarclient",
            "settings",
            "game",
            BackupFileName
            );

        public static void DisableLunarAutoLogin()
        {
            try
            {
                if (File.Exists(AccountsPath))
                {
                    if (!File.Exists(BackupPath))
                    {
                        File.Copy(AccountsPath, BackupPath, true);
                    }
                    File.Delete(AccountsPath);
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.Write(LogEvent.Error, "Something wrong happend: " + ex.ToString(), false);
                // Handle the exception here, for example by logging it or showing an error message to the user.
            }
        }

        public static void EnableLunarAutoLogin()
        {
            try
            {
                if (File.Exists(BackupPath))
                {
                    File.Copy(BackupPath, AccountsPath, true);
                    File.Delete(BackupPath);
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.Write(LogEvent.Error, "Something wrong happend: " + ex.ToString(), false);
                // Handle the exception here, for example by logging it or showing an error message to the user.
            }
        }
    }
}
