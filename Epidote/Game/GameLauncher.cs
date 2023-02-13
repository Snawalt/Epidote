using Guna.UI2;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        private static readonly string _multiverDirectory = Path.Combine(
           Environment.GetEnvironmentVariable("USERPROFILE"),
           ".lunarclient",
           "offline",
           "multiver"
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

        private static readonly string _texturesDirectory = Path.Combine(
            Environment.GetEnvironmentVariable("USERPROFILE"),
            ".lunarclient",
            "textures"
        );
        private static readonly string _AppDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
            "\\"
        );

        private static readonly string _assetsDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.minecraft\assets");

        private static readonly string _minecraftDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.minecraft");

        public static string _solarEnginePath = Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE"), "Epidote", "solar-engine.jar");
        public static string _solarEngineConfig = Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE"), "Epidote", "config.json");


        // Event handler for when the process outputs data

        public static bool isBuidilingChache;
        public static bool isClientLaunched;


        [STAThread]
        private static void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            string freezeDetection1 = "[Client thread/INFO]:";
            string freezeDetection2 = "A g�rd�l�keny megold�s miatt csatlakozz fel!";


            // Detect if the game is building a cache
            if (e.Data != null && e.Data.Contains("LUNARCLIENT_STATUS_BUILD_CACHE"))
            {
                isBuidilingChache = true;
            }


            // Check if the output contains any of the phrases in the _GameStartingDetection list
            if (e.Data != null && e.Data.Contains("[Bridge] Setting Minecraft Client instance to net.minecraft.client.Minecraft"))
            {
                isClientLaunched = true;
            }


            // Check if the player freezed
            if (e.Data != null && e.Data.Contains(freezeDetection1) && e.Data.Contains(freezeDetection2))
            {
                if (Epidote.LandingUI.isAutoFreezeSupportEnabled)
                {
                    Thread thread = new Thread(new ThreadStart(() =>
                    {
                        Clipboard.SetText(Epidote.LandingUI.setAnyDeskIDtoClipboard());
                    }));
                    thread.SetApartmentState(ApartmentState.STA);
                    thread.Start();
                    thread.Join();
                }
            }


            ExceptionLogger.Write(LogEvent.Info, e.Data, false);
            Console.WriteLine(e.Data);
        }

        private static Process process;
        public static void LaunchLunar()
        {
            // Write the arguments to be passed to the process to the exception logger
            ExceptionLogger.Write(LogEvent.Info, Epidote.Game.LunarJreCheck.GetLunarJrePath() + " -> " + GetBasicArguments().ToString(), false);

            // Get the environment variable for the user profile, which is the location of the user's profile folder in Windows
            var userProfile = Environment.GetEnvironmentVariable("USERPROFILE");

            // Create the path to the LunarClient directory by combining the user profile path with the subdirectories for LunarClient
            var lunarDirectory = Path.Combine(userProfile, @".lunarclient\offline\multiver");

            // Get the path to the Lunar JRE, which is the Java runtime environment required to run LunarClient
            var lunarJrePath = Epidote.Game.LunarJreCheck.GetLunarJrePath();

            // Get the process arguments
            var processArguments = GetBasicArguments();

            try
            {
                // Create a new process object
                process = new Process
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

                // Subscribe to the OutputDataReceived event to read and process the standard output
                process.OutputDataReceived += Process_OutputDataReceived;

                // Subscribe to the Exited event and close the launched process when it exits
                process.Exited += new EventHandler((sender, args) => Environment.Exit(0));

                // Subscribe to the ProcessExit event to close the launched process if it is still running when the application exits
                AppDomain.CurrentDomain.ProcessExit += new EventHandler((sender, args) =>
                {
                    // Close the launched process if it is still running
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


        private static string GetBasicArguments()
        {
            //Return the process arguments
            string args = @"--add-modules jdk.naming.dns --add-exports jdk.naming.dns/com.sun.jndi.dns=java.naming -Djna.boot.library.path=natives -Dlog4j2.formatMsgNoLookups=true --add-opens java.base/java.io=ALL-UNNAMED -Xms" + Epidote.LandingUI.sliderSetMemoryValue + @"m -Xmx" + Epidote.LandingUI.sliderSetMemoryValue + @"m -Djava.library.path=natives -XX:+DisableAttachMechanism -cp ""common-0.1.0-SNAPSHOT-all.jar;v1_8-0.1.0-SNAPSHOT-all.jar;optifine-0.1.0-SNAPSHOT-all.jar;genesis-0.1.0-SNAPSHOT-all.jar;lunar-lang.jar;lunar-emote.jar;lunar.jar"" ""-javaagent:" + _HitDelayFixPath + @""" ""-javaagent:" + _CrackedAccountPath + "=" + Epidote.Program._username + @""" com.moonsworth.lunar.genesis.Genesis --version 1.8.9 --accessToken 0 --assetIndex 1.8 --userProperties {} --gameDir """ + _minecraftDirectory + @""" --texturesDir """ + _texturesDirectory + @""" --launcherVersion 2.15.1 --hwid 0 --installationId 0 --width 640 --height 480 --workingDirectory . --classpathDir . --ichorClassPath common-0.1.0-SNAPSHOT-all.jar,v1_8-0.1.0-SNAPSHOT-all.jar,optifine-0.1.0-SNAPSHOT-all.jar,genesis-0.1.0-SNAPSHOT-all.jar,lunar-lang.jar,lunar-emote.jar,lunar.jar --ichorExternalFiles OptiFine_v1_8.jar";

            return args;
        }

    }
}