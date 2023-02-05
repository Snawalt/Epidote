using System;
using System.IO;

namespace Epidote.Game
{
    public class LunarJreCheck
    {
        // Constant path to the Lunar jre folder
        private static readonly string LunarJrePath = Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE"), @".lunarclient\jre");

        public static bool isProblemWithJre = false;
        // Method to get the path to the Lunar Jre
        public static string GetLunarJrePath()
        {
            // Check if the Lunar Jre folder exists
            if (Directory.Exists(LunarJrePath))
            {
                // Get the directories within the Lunar Jre folder
                string[] directories = Directory.GetDirectories(LunarJrePath);
                // Check if there is only one directory within the Lunar Jre folder
                if (directories.Length != 1)
                {
                    // Set the bool to true cause error detected
                    isProblemWithJre = true;

                    // If there is not one directory, set the error message and delete the Lunar Jre folder
                    Epidote.Utils.FileVerification.errorMessage = "There are problems with your Lunar jre folder close the program and start lunar";
                    Directory.Delete(LunarJrePath, true);
                }

                // Get the main library path
                string mainLibraryPath = Directory.GetDirectories(directories[0])[0];
                // Return the path to the javaw executable
                return Path.Combine(mainLibraryPath, "bin", "javaw");
            }
            // Return an empty string if the Lunar Jre folder does not exist
            return "";
        }

        
    }
}
