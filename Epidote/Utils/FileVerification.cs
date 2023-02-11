using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Epidote.Utils
{
    public class FileVerification
    {
        // Constants for various directories used in the verification process
        private static readonly string GameReadyDirectory = Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE"), ".lunarclient", "offline");
        private static readonly string NativesDirectory = Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE"), ".lunarclient", "offline", "multiver", "natives");
        private static readonly string LocalProgramData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Programs\lunarclient";
        // Constant path to the Lunar jre folder
        public static readonly string LunarJrePath = Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE"), @".lunarclient\jre");

        // Constants for the username directory and file
        public static string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        public static string _usernamePath = Path.Combine(userProfile, "Epidote", "epidote-usernames.dat");
        public static string _usernameDirectory = Path.Combine(userProfile, "Epidote");

        // Minimum size of the GameReadyDirectory in bytes
        private static readonly long MinimumSize = 90000 * 1024; // 90MB in bytes

        // A string to store the error message if the verification fails
        public static string errorMessage;

        // Asynchronously checks if the required files and directories are present and in the correct state
        public static async Task<bool> IsReadyAsync()
        {
            // Check if the user have a bad environment username

            if (isUsernameContainsSpace())
            {
                errorMessage = "Your user account contains space characters.";
                return false;
            }

            // Check if the lunar jre path is invalid

            if (isProblemWithLunarJrePath())
            {
                errorMessage = "Problems detected in the lunar jre folder.";
                return false;
            }

            // Check if the user file is damaged
            if (IsFileDamaged())
            {
                errorMessage = "The folder associated with the player username is empty.";
                return false;
            }

            // Check if the GameReadyDirectory exists
            if (!Directory.Exists(GameReadyDirectory))
            {
                errorMessage = "The Game Directory does not exist.";
                return false;
            }

            // Check if the NativesDirectory exists
            if (!Directory.Exists(NativesDirectory))
            {
                errorMessage = "The Natives Directory does not exist.";
                return false;
            }

            // Check if there are at least 15 .dll files in the NativesDirectory
            int dllCount = Directory.EnumerateFiles(NativesDirectory, "*.dll", SearchOption.AllDirectories).Count();
            if (dllCount < 15)
            {
                errorMessage = "The number of .dll files in the Natives Directory is less than 15.";
                return false;
            }

            // Check if the size of the GameReadyDirectory is at least 90MB
            var offlineSize = await DirSizeAsync(new DirectoryInfo(GameReadyDirectory));
            if (offlineSize < MinimumSize)
            {
                errorMessage = "The size of the Lunar directory is less than 90MB.";
                return false;
            }

            // Check if the LocalProgramData directory exists
            if (!Directory.Exists(LocalProgramData))
            {
                errorMessage = "The Local ProgramData directory does not exist.";
                return false;
            }

            // If all checks pass, return true
            return true;
        }

        // Calculate the size of a directory asynchronously
        private static async Task<long> DirSizeAsync(DirectoryInfo d)
        {
            var size = 0L;
            // Enumerate all files in the directory and get their sizes
            var fileTasks = d.EnumerateFiles().Select(f => f.Length);
            // Recursively calculate the size of all subdirectories
            var dirTasks = d.EnumerateDirectories().Select(DirSizeAsync);

            // Wait for all tasks to complete
            await Task.WhenAll(dirTasks);
            // Add up the sizes of the files and subdirectories
            return size + fileTasks.Sum() + dirTasks.Sum(t => t.Result);
        }

        public static bool isUsernameContainsSpace()
        {
            if (Environment.GetEnvironmentVariable("USERPROFILE").ToString().Contains(" "))
                return true;
            else
                return false;
        }

        // Check if a file is damaged
        public static bool IsFileDamaged()
        {
            // Check if the user directory and file exist
            if (Directory.Exists(_usernameDirectory) && File.Exists(_usernamePath))
            {
                using (FileStream stream = new FileStream(_usernamePath, FileMode.Open, FileAccess.Read, FileShare.None))
                using (StreamReader reader = new StreamReader(stream))
                {
                    // Return true if the file is empty
                    if (reader.ReadToEnd().Length == 0)
                    {
                        return true;
                    }
                    else
                        return false;
                }
            }
            else
            {
                // Create the user directory if it does not exist
                Directory.CreateDirectory(_usernameDirectory);
            }
            return false;
        }

        // Check if both the directory and the file exist
        public static bool IsFileAndDirectoryExist()
        {
            // Return true if both the directory and the file exist, false otherwise
            if (Directory.Exists(_usernameDirectory) && File.Exists(_usernamePath))
                return true;
            else
                return false;
        }

        // Get the error message
        public static string GetErrorMessage()
        {
            // Return the error message
            return errorMessage;
        }

        public static bool isProblemWithLunarJrePath()
        {
            // Check if the Lunar Jre folder exists
            if (Directory.Exists(LunarJrePath))
            {
                // Get the directories within the Lunar Jre folder
                string[] directories = Directory.GetDirectories(LunarJrePath);
                // Check if there is only one directory within the Lunar Jre folder
                if (directories.Length != 1)
                    return true;
                else
                    return false;
            }
            return false;
        }
    }
}
