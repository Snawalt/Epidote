﻿using LunarMeow;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epidote.Game
{
    public class LunarJreCheck
    {
        private static readonly string LunarJrePath = Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE"), @".lunarclient\jre");

        public static string GetLunarJrePath()
        {
            if (!Directory.Exists(LunarJrePath))
            {
                ExceptionLogger.Write(LogEvent.Info, "The lunar jre folder does not exist, you have to start lunar.", false);
                Console.ReadKey();
                Environment.Exit(0);
            }

            string[] directories = Directory.GetDirectories(LunarJrePath);
            if (directories.Length != 1)
            {
                ExceptionLogger.Write(LogEvent.Warning, "There are problems with your Lunar jre folder, close the program and start lunar.", false);
                Directory.Delete(LunarJrePath, true);
                Console.ReadKey();
                Environment.Exit(0);
            }

            string mainLibraryPath = Directory.GetDirectories(directories[0])[0];
            return Path.Combine(mainLibraryPath, "bin", "javaw");
        }
    }
}
