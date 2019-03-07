using System;

namespace ImageViewer.Models
{
    internal class LogEventArgs : EventArgs
    {
        public LogEventArgs(string message, LogLevel level)
        {
            Message = message;
            Level = level;
        }

        public string Message { get; }

        public LogLevel Level { get; }
    }

    internal enum LogLevel { INFO, WARN, ERROR };
}
