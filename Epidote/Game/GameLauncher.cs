using Guna.UI2;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Epidote.Game
{
    public class GameLauncher
    {
        // Constants to store file paths for logs, addons, and the AppData folder
        private static readonly string _LogsPath = Path.Combine(
            Environment.GetEnvironmentVariable("USERPROFILE"),
            ".lunarclient",
            "offline",
            "multiver",
            "logs"
        );

        private static readonly string _CrackedAccountPath = Path.Combine(
            Environment.GetEnvironmentVariable("USERPROFILE"),
            ".lunarclient",
            "addons",
            "CrackedAccount"
        );

        private static readonly string _HitDelayFixPath = Path.Combine(
            Environment.GetEnvironmentVariable("USERPROFILE"),
            ".lunarclient",
            "addons",
            "HitDelayFix"
        );

        private static readonly string _AppDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
            "\\"
        );

        // Event handler for when the process outputs data

        public static bool isBuidilingChache;
        public static bool isClientLaunched;

        private static void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            // Detect if the game is building a cache
            if (e.Data != null && e.Data.Contains("LUNARCLIENT_STATUS_BUILD_CACHE"))
            {
                isBuidilingChache = true;
            }


            // Check if the output contains any of the phrases in the _GameStartingDetection list
            if (e.Data != null && e.Data.Contains("[Genesis/INFO] Starting game!"))
            {
                isClientLaunched = true;
            }

             ExceptionLogger.Write(LogEvent.Info, e.Data, false);
             Console.WriteLine(e.Data);
        }

        public static void LaunchLunar()
        {
            // Get the path to the user profile directory
            var userProfile = Environment.GetEnvironmentVariable("USERPROFILE");

            // Combine the user profile path with the path to the LunarClient directory
            var lunarDirectory = Path.Combine(userProfile, @".lunarclient\offline\multiver");

            // Get the path to the Lunar JRE
            var lunarJrePath = Epidote.Game.LunarJreCheck.GetLunarJrePath();

            // Get the process arguments
            var processArguments = GetArguments();

            try
            {
                // Create a new process object
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        // Set the working directory to the LunarClient directory
                        WorkingDirectory = lunarDirectory,

                        // Set the file name to the path of the Lunar JRE
                        FileName = lunarJrePath,

                        // Set the arguments for the process
                        Arguments = processArguments,

                        // Do not use shell execution
                        UseShellExecute = false,

                        // Redirect the standard output
                        RedirectStandardOutput = true,

                        // Set the encoding for the standard output to UTF8
                        StandardOutputEncoding = Encoding.UTF8,
                    },
                    // Enable raising events for the process
                    EnableRaisingEvents = true
                };

                // Subscribe to the OutputDataReceived event
                process.OutputDataReceived += Process_OutputDataReceived;

                // Subscribe to the Exited event and close the launched process when it exits
                process.Exited += new EventHandler((sender, args) => Environment.Exit(0));

                // Subscribe to the ProcessExit event and close the launched process if it is still running when the application exits
                AppDomain.CurrentDomain.ProcessExit += new EventHandler((sender, args) =>
                {
                    // Code to close the launched process
                    if (!process.HasExited)
                    {
                        process.Kill();
                    }
                });

                // Start the process
                process.Start();

                // Begin reading the standard output
                process.BeginOutputReadLine();
            }
            catch (Exception ex)
            {
                // Log the exception to the exception logger
                ExceptionLogger.Write(LogEvent.Error, $"An error occured: {ex}", false);
            }
        }


        private static string GetArguments()
        {
            // Return the process arguments

            return @"--add-modules jdk.naming.dns --add-exports jdk.naming.dns/com.sun.jndi.dns=java.naming -Djna.boot.library.path=natives -Dlog4j2.formatMsgNoLookups=true --add-opens java.base/java.io=ALL-UNNAMED -Xverify:none -Xms" + Epidote.Game.MemoryCalculator.CalculateJavaMemoryAllocation() + @"m -Xmx" + Epidote.Game.MemoryCalculator.CalculateJavaMemoryAllocation() + @"m -Djava.library.path=natives -XX:+DisableAttachMechanism -javaagent:" + _HitDelayFixPath + @" -javaagent:" + _CrackedAccountPath + "=" + Epidote.Program._username + @" -cp optifine-0.1.0-SNAPSHOT-all.jar;genesis-0.1.0-SNAPSHOT-all.jar;v1_8-0.1.0-SNAPSHOT-all.jar;common-0.1.0-SNAPSHOT-all.jar;lunar-lang.jar;lunar-emote.jar;lunar.jar com.moonsworth.lunar.genesis.Genesis --version 1.8.9 --accessToken 0 --assetIndex 1.8 --userProperties {} --gameDir " + _AppDataPath + @".minecraft --texturesDir " + Environment.GetEnvironmentVariable("USERPROFILE") + @"\.lunarclient\textures --launcherVersion 2.14.0 --hwid 0 --installationId 0 --width 854 --height 480 --workingDirectory . --classpathDir . --ichorClassPath optifine-0.1.0-SNAPSHOT-all.jar,genesis-0.1.0-SNAPSHOT-all.jar,v1_8-0.1.0-SNAPSHOT-all.jar,common-0.1.0-SNAPSHOT-all.jar,lunar-lang.jar,lunar-emote.jar,lunar.jar --ichorExternalFiles OptiFine_v1_8.jar";
        }
    }
}