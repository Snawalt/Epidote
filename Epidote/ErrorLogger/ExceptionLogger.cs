using System;
using System.IO;
using System.Text;

namespace Epidote
{
    enum LogEvent
    {
        Info = 0,
        Success = 1,
        Warning = 2,
        Error = 3
    }

    internal static class ExceptionLogger
    {
        private static readonly string LogSession = DateTime.Now.ToLocalTime().ToString("ddMMyyyy_HHmmss");
        private static readonly string LogPath = @"C:\Windows\Temp\Epidote";

        internal static void Write(LogEvent level, string message, bool showConsole = true, bool writeLogFile = true)
        {
            string eventType;

            switch (level)
            {
                case LogEvent.Info:
                    eventType = "Info";
                    break;
                case LogEvent.Success:
                    eventType = "Success";
                    break;
                case LogEvent.Warning:
                    eventType = "Warning";
                    break;
                case LogEvent.Error:
                    eventType = "Error";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(level), level, null);
            }

            if (writeLogFile)
            {
                if (!Directory.Exists(LogPath))
                    Directory.CreateDirectory(LogPath);

                File.AppendAllText(Path.Combine(LogPath, LogSession + ".log"), $"[{DateTime.Now:HH:mm:ss}] - {eventType}: {message}\n");
            }
        }
    }
}



//using System;
//using System.IO;
//using System.Text;

//namespace Epidote
//{
//    public enum LogEvent
//    {
//        Info = 0,
//        Success = 1,
//        Warning = 2,
//        Error = 3
//    }

//    public class ExceptionLogger
//    {
//        private static readonly string logSession = DateTime.Now.ToLocalTime().ToString("yyyyMMdd_HHmmss");
//        private static readonly string logPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Epidote", "Logs");

//        public static void Write(LogEvent level, string message, Exception ex = null, bool showConsole = true, bool writeLogFile = true)
//        {
//            string eventType;

//            switch (level)
//            {
//                case LogEvent.Info:
//                    eventType = "Info";
//                    break;
//                case LogEvent.Success:
//                    eventType = "Success";
//                    break;
//                case LogEvent.Warning:
//                    eventType = "Warning";
//                    break;
//                case LogEvent.Error:
//                    eventType = "Error";
//                    break;
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(level), level, null);
//            }

//            if (writeLogFile)
//            {
//                if (!Directory.Exists(logPath))
//                    Directory.CreateDirectory(logPath);

//                using (var sw = new StreamWriter(Path.Combine(logPath, logSession + ".log"), true, Encoding.UTF8))
//                {
//                    string logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] - {eventType}: {message}";
//                    if (ex != null)
//                        logMessage += Environment.NewLine + ex;
//                    sw.WriteLine(logMessage);
//                }
//            }
//            if (showConsole)
//                Console.WriteLine(message);
//        }
//    }
//}
