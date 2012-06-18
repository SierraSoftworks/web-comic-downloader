using System;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using SierraLib.Windows.Win7API.ApplicationServices;
using SierraLib.Windows.Win7API.Dialogs;
using SierraLib.Analytics.GoogleAnalytics;
using SierraLib.Net.CrashReporting;
using SierraLib.Updates.Automatic;

namespace WebcomicDownloader
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();

            CrashManager.Application = WebCrashReport.Applications.WebComicDownloader;
            CrashManager.ApplicationName = "Web Comic Downloader";
            CrashManager.CrashLogFolder = Environment.ExpandEnvironmentVariables("%AppData%\\Sierra Softworks\\Web Comic Downloader\\Crash Logs");
            CrashManager.Server = "http://sierrasoftworks.com/ReportCrash.php";
            CrashManager.Username = "apptracking";
            CrashManager.Password = "password";
            CrashManager.Referrer = "apptracking.sierrasoftworks.com";

            AppDomain.CurrentDomain.UnhandledException += CrashManager.OnUnhandledException;

            //Allow this app to restart if it crashed
            if(Environment.OSVersion.Version.Major >= 6)
                ApplicationRestartRecoveryManager.RegisterForApplicationRestart(new RestartSettings("", RestartRestrictions.NotOnReboot | RestartRestrictions.NotOnPatch));

            AutomaticUpdates updateManager = new AutomaticUpdates("Sierra Softworks - WKD");
            updateManager.ApplicationName = "Web Comic Downloader";
            
            try
            {
                updateManager.ProcessCommandLine(Environment.CommandLine);

                if (updateManager.PerformingUpdate)                
                    return;
                
                else if (updateManager.MultipleInstances)                
                    return;
                
            }
            finally
            {
                if (updateManager.Mutex != null)
                    updateManager.Mutex.ReleaseMutex();
            }

            Application.Run(new Main());

        }
    }
}