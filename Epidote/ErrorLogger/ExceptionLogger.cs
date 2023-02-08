using System;
using System.IO;

namespace Epidote
{
    // Enum to represent different types of log events
    enum LogEvent
    {
        Info = 0,        // Indicates a general information log message
        Success = 1,     // Indicates a successful operation log message
        Warning = 2,     // Indicates a warning log message
        Error = 3        // Indicates an error log message
    }

    // Utility class for logging exceptions
    internal static class ExceptionLogger
    {
        // The log session is a string representation of the current date and time
        private static readonly string LogSession = DateTime.Now.ToLocalTime().ToString("ddMMyyyy_HHmmss");
        // The log path is a fixed string representing the path to the log directory
        private static readonly string LogPath = @"C:\Windows\Temp\Epidote";

        // Method for writing a log message
        internal static void Write(LogEvent level, string message, bool showConsole = true, bool writeLogFile = true)
        {
            // String representation of the event type based on the enum value passed as an argument
            string eventType;

            // Switch statement to set the event type string based on the enum value
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
                    // Throw an exception if an invalid enum value is passed
                    throw new ArgumentOutOfRangeException(nameof(level), level, null);
            }

            // Check if the writeLogFile argument is true
            if (writeLogFile)
            {
                // If the log directory does not exist, create it
                if (!Directory.Exists(LogPath))
                    Directory.CreateDirectory(LogPath);

                // Append the log message to the log file
                File.AppendAllText(Path.Combine(LogPath, LogSession + ".log"), $"[{DateTime.Now:HH:mm:ss}] - {eventType}: {message}\n");
            }
        }
    }
}
