using System;
using IPALogger = IPA.Logging.Logger;
using LogLevel = IPA.Logging.Logger.Level;

namespace TransparentWall
{
    internal static class Logger
    {
        internal static IPALogger log { private get; set; }

        internal static void Log(string message, LogLevel severity)
        {
            if (Plugin.IsLoggerSet) log.Log(severity, message);
            else Console.WriteLine($"[{Plugin.PluginName}] ({severity.ToString()}): {message}");
        }
    }
}
