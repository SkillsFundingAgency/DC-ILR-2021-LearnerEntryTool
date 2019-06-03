﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Data;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using System.Reflection;

namespace ilrLearnerEntry
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, IDisposable
    {       
        private static readonly string appGuid = "SFA-ilrLearnerEntry-A07151B7-8F1A-4423-ABFB-B85387423E98-1920";
        private Mutex mutex = new Mutex(false, appGuid);

        private static readonly string  appYear         = "1920";
        private static readonly string  appYearPrevious = "1819";
        private static bool             NoCheckOnStart  = false;

        #region Public Methods
        public App()
        {

            LogFileName                                             = string.Empty;
            Assembly assembly                                       = System.Reflection.Assembly.GetExecutingAssembly();
            ResourceDictionary myresourcedictionary_Generic         = new ResourceDictionary();
            myresourcedictionary_Generic.Source                     = new Uri(String.Format("/ILRLearnerEntry{0};component/StandardResourceDictionary.xaml", ApplicationYear), UriKind.RelativeOrAbsolute);
            this.Resources.MergedDictionaries.Add(myresourcedictionary_Generic);

            string Path                                             = assembly.Location;
            System.IO.FileInfo fi                                   = new System.IO.FileInfo(Path);
            DataFolder                                              = fi.DirectoryName;
            ExportFolder                                            = fi.DirectoryName;
            string[] args                                           = Environment.GetCommandLineArgs();

            /// added as some people had issues with startup in the past. See if its the mutex causing the issue (Single Version of application)
            /// Also revistied the logging. Still not full working, only use if this is a start up issus.
            foreach (string arg in args)
            {
                Log("App", "Check Arg : ", String.Format("Check Arg : {0}", arg));
                switch (arg.ToUpper())
                {
                    case "NOCHECKONSTART":
                    case "/NOCHECKONSTART":
                        Log("App", "Check Arg : ", "Turn On NOCHECKONSTART - Don't check of already running.");
                        NoCheckOnStart = true;
                        break;
#if DEBUG
                    case "LOGTOFILE":
                    case "/LOGTOFILE":
                        Log("App", "Check Arg : ", "Enable loging to File");
                        LogFileName = System.IO.Path.Combine(fi.DirectoryName, assembly.ManifestModule.Name.Split('.')[0] + "." + DateTime.Now.ToString("yyyy.MM.dd.hhss") + ".Log");
        #if DEBUG
                        LogFileName = System.IO.Path.Combine(fi.DirectoryName, assembly.ManifestModule.Name.Split('.')[0] + ".Log");
        #endif      
                        break;
#endif
                    default:
                        break;
                }
            }

            Log("App", "Start", "");
            if (!NoCheckOnStart)
            {
                Log("App", "Start - App already Running", "Check if App already Running");
                try
                {
                    if (!mutex.WaitOne(TimeSpan.FromSeconds(1), false))
                    {
                        Log("App", "Start - App already Running", "Start Shutdown");
                        ilrLearnerEntry.UserControls.winAppAlreadyRunning winAppRunning = new UserControls.winAppAlreadyRunning();
                        winAppRunning.ShowDialog();
                        Log("App", "Start - App already Running", "Shutdown");
                        return;
                    }
                }
                catch (Exception e)
                {
                    Log("App", "Start - App already Running", "Check - ERROR -" + e.Message);
                    ilrLearnerEntry.UserControls.winAppAlreadyRunning winAppRunning = new UserControls.winAppAlreadyRunning();
                    winAppRunning.ShowDialog();
                    mutex.ReleaseMutex();
                    Log("App", "Start - App already Running", "Shutdown");
                    return;
                }
            }
            GC.Collect();
            SetupApplicationStuff();
        }
        
        private void SetupApplicationStuff()
        {
            Log("App", "SetupApplicationStuff", "Enable loging to File");
            Application.Current.ShutdownMode = System.Windows.ShutdownMode.OnMainWindowClose;
            ILRMessage = new ILR.Message(System.IO.Path.Combine(DataFolder, ApplicationInternalFilename),LogFileName);
        }
        #endregion

        #region Public Properties
        public static Version ApplicationVersion
        {
            get
            {
                Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                return version;
            }
        }
        public static String ApplicationName
        {
            get
            {
                var attributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    var titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title.Length > 0)
                        return titleAttribute.Title;
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().CodeBase);
            }
        }
        public static String AssemblyName
        {
            get
            {
                var attributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    var titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title.Length > 0)
                        return titleAttribute.Title.Replace(".exe", "").Replace("-", "").Replace(" ", "");
                }
                return string.Format("ILRLearnerEntry{0}", ApplicationYear);
            }
        }
        public static string ApplicationYear
        {
            get
            {
                return appYear;
            }
        }
        public static string ApplicationYearPrevious
        {
            get
            {
                return appYearPrevious;
            }
        }
        public static string ApplicationInternalFilename
        {
            get
            {
                return string.Format("internal{0}.ilr", ApplicationYear);
            }
        }
        public static String LogFileName { get; set; }
        public static Boolean IsLoggingEnabled { get { return LogFileName == string.Empty ? false : true; } }

        public static ILR.Message ILRMessage
        { get; set; }

        public static String DataFolder
        { get; set; }
        public static String ExportFolder
        { get; set; }
        #endregion
        
        #region Public Static Methods
        public static void Log(String className, String caller, String message)
        {
#if DEBUG
			if (IsLoggingEnabled)
			{
				using (System.IO.StreamWriter writer = new System.IO.StreamWriter(LogFileName, true))
				{
					writer.WriteLine("{0} | {1} | {2} | {3}", DateTime.Now.ToString("yyyy.MM.dd.hhss"), className, caller, message);
				}
			}
#endif
		}
		#endregion

		private void Application_Exit(object sender, ExitEventArgs e)
        {
            App.Current.Shutdown();
            Log("App", "EXIT", "");
        }

        /// http://stackoverflow.com/questions/4502037/where-is-the-application-doevents-in-wpf
        public static void DoEvents()
        {
            DispatcherFrame frame = new DispatcherFrame();
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, new DispatcherOperationCallback(ExitFrame), frame);
            Dispatcher.PushFrame(frame);
        }

        public static object ExitFrame(object f)
        {
            ((DispatcherFrame)f).Continue = false;

            return null;
        }

        private void Dispose(Boolean disposing)
        {
            if (disposing && (mutex != null))
            {
                mutex.ReleaseMutex();
                mutex.Close();
                mutex = null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Dispose();
            base.OnExit(e);
        }
    }
}
