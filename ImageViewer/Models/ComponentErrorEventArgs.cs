using System;

namespace ImageViewer.Models
{
    internal class ComponentErrorEventArgs
    {
        //public ComponentErrorEventArgs(Exception ex, string component) : this(ex.Message, ex, component) { }

        public ComponentErrorEventArgs(string message, Exception ex, string component)
        {
            Timestamp = DateTime.Now;
            Message = message;
            Exception = ex;
            Component = component;
        }

        public DateTime Timestamp { get; }

        public string Component { get; }

        public Exception Exception { get; }

        public string Message { get; }

        public string StackTrace => Exception.StackTrace;
    }
}
