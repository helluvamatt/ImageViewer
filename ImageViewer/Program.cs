using log4net;
using log4net.Config;
using System;
using System.Windows.Forms;

namespace ImageViewer
{
    static class Program
    {
        private static ILog _Logger;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // Configure logging
            XmlConfigurator.Configure();
            _Logger = LogManager.GetLogger(typeof(Program));

            // Install global exception handling
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            // Start application
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(args));
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = (Exception)e.ExceptionObject;
            _Logger.Fatal(string.Format("Unhandled exception: {0}", ex.Message), ex);
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            _Logger.Fatal(string.Format("Application exception: {0}", e.Exception.Message), e.Exception);
        }
    }
}
