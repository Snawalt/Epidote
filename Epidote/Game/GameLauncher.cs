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

        //static string logsPath = Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE") + @"\.lunarclient\offline\multiver\logs");
        //static string CrackedAccountPath = Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE") + @"\.lunarclient\addons\CrackedAccount");
        //static string hitdelayfixPath = Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE") + @"\.lunarclient\addons\HitDelayFix");
        //static string appdataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\");

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

        private static bool IsWaiting = false;

        private static readonly List<string> _GameStartingDetection = new List<string>
        {
            "[Bridge] Found Textures dir:"
        };

        private static readonly List<string> FreezeDetection = new List<string>
        {
            "[Client thread/INFO]: A gérés esetén csatlakozz fel!"
        };

        private static readonly List<string> _CacheDetection = new List<string>
        {
            "[Client thread/INFO]: Building cache"
        };




        private static void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {

            // Detect if the game building a cache
            if (e.Data != null && _CacheDetection.Any(phrase => e.Data.Contains("[IchorPipeline/INFO] Loading Mixin runtime for LUNAR_MIXIN")))
            {
                Console.WriteLine(" Building cache... (This may take longer time depending on your computer)");
            }

            // Check if the output contains any phrases
            if (e.Data != null && _GameStartingDetection.Any(phrase => e.Data.Contains("[Bridge] Found textures dir:")))
            {
                // A phrase was detected - take appropriate action
                Console.WriteLine(" The client has launched");
            }
            else
            {
                //Console.WriteLine(e.Data);
            }
        }

        public static void LaunchLunar()
        {
            ExceptionLogger.Write(LogEvent.Info, "1: " + _AppDataPath + " 2: " + _CrackedAccountPath + " 3: " + _HitDelayFixPath + " 4: " + _LogsPath);

            var userProfile = Environment.GetEnvironmentVariable("USERPROFILE");
            var lunarDirectory = Path.Combine(userProfile, @".lunarclient\offline\multiver");
            var lunarJrePath = Epidote.Game.LunarJreCheck.GetLunarJrePath();
            var processArguments = GetArguments();

            try
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        WorkingDirectory = lunarDirectory,
                        FileName = lunarJrePath,
                        Arguments = processArguments,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        StandardOutputEncoding = Encoding.UTF8,
                    },
                    EnableRaisingEvents = true
                };

                process.OutputDataReceived += Process_OutputDataReceived;
                process.Exited += new EventHandler((sender, args) => Environment.Exit(0));
                AppDomain.CurrentDomain.ProcessExit += new EventHandler((sender, args) =>
                {
                    // Code to close the launched process
                    if (!process.HasExited)
                    {
                        process.Kill();
                    }
                });

                process.Start();
                process.BeginOutputReadLine();
            }
            catch (Exception ex)
            {
                ExceptionLogger.Write(LogEvent.Error, $"An error occured: {ex}", false);
            }
        }



        private static string GetArguments()
        {
            return @"--add-modules jdk.naming.dns --add-exports jdk.naming.dns/com.sun.jndi.dns=java.naming -Djna.boot.library.path=natives -Dlog4j2.formatMsgNoLookups=true --add-opens java.base/java.io=ALL-UNNAMED -Xverify:none -Xms" + Epidote.Game.MemoryCalculator.CalculateJavaMemoryAllocation() + @"m -Xmx" + Epidote.Game.MemoryCalculator.CalculateJavaMemoryAllocation() + @"m -Djava.library.path=natives -XX:+DisableAttachMechanism -javaagent:" + _HitDelayFixPath + @" -javaagent:" + _CrackedAccountPath + "=" + Epidote.Program._username + @" -cp optifine-0.1.0-SNAPSHOT-all.jar;genesis-0.1.0-SNAPSHOT-all.jar;v1_8-0.1.0-SNAPSHOT-all.jar;common-0.1.0-SNAPSHOT-all.jar;lunar-lang.jar;lunar-emote.jar;lunar.jar com.moonsworth.lunar.genesis.Genesis --version 1.8.9 --accessToken 0 --assetIndex 1.8 --userProperties {} --gameDir " + _AppDataPath + @".minecraft --texturesDir " + Environment.GetEnvironmentVariable("USERPROFILE") + @"\.lunarclient\textures --launcherVersion 2.14.0 --hwid 0 --installationId 0 --width 854 --height 480 --workingDirectory . --classpathDir . --ichorClassPath optifine-0.1.0-SNAPSHOT-all.jar,genesis-0.1.0-SNAPSHOT-all.jar,v1_8-0.1.0-SNAPSHOT-all.jar,common-0.1.0-SNAPSHOT-all.jar,lunar-lang.jar,lunar-emote.jar,lunar.jar --ichorExternalFiles OptiFine_v1_8.jar";
        }
    }
}