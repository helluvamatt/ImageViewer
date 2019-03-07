using System;

namespace ImageViewer.Models
{
    internal class ComponentErrorEventArgs
    {
        public ComponentErrorEventArgs(Exception ex, string component)
        {
            Timestamp = DateTime.Now;
            Exception = ex;
            Component = component;
        }

        public DateTime Timestamp { get; }

        public string Component { get; }

        public Exception Exception { get; }

        public string Message => Exception.Message;

        public string StackTrace => Exception.StackTrace;
    }
}
