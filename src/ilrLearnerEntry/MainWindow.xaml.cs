using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace ilrLearnerEntry
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        #region routed commands
        #endregion

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
            App.Log("MainWindow", " Constructor", "Start");
            HomeScreenControl.OnNewFileImported += HomeScreenControl_OnNewFileImported;
            HomeScreenControl.OnUkprnUpdated += HomeScreenControl_OnUkprnUpdated;
            this.DataContext = this;
            OnPropertyChanged("WindowTitle");
            OnPropertyChanged("ProductVersion");

            if (App.ILRMessage.LearnerCount > 0 || App.ILRMessage.LearnerDestinationandProgressionCount > 0)
            {
                App.Log("MainWindow", " Constructor", "Update Child Controls");
                UpdateChildControlAsNewDataLoaded();
            }
            else
            {
                HomeScreenControl.RefreshData();
            }
            tabControlMain.SelectionChanged += TabControlMain_SelectionChanged;
            App.Log("MainWindow", " Constructor", "End");
        }

        private void TabControlMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        #endregion

        #region Public Properties
        public string WindowTitle
        {
            get
            {
                if (App.ILRMessage.LearningProvider.UKPRN != null)
                {
                    return String.Format("{0} - UKPRN : {1}", App.ApplicationName, App.ILRMessage.LearningProvider.UKPRN.ToString());
                }
                else
                {
                    return App.ApplicationName;
                }
            }
        }

        public String ProductVersion
        {
            get
            {
                string vReturn = string.Empty;
                Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

                if (version != null)
                {
#if DEBUG
                    vReturn = version.ToString();
#else
                                    vReturn = version.Major.ToString() + "." + version.Minor.ToString() + "." + version.Build.ToString();
#endif
                }
                else
                {
                    vReturn = "Unable to get Version.";
                }

                return "v2021.2";
            }
        }
        #endregion

        #region Public Methods
        public void UpdateChildControlAsNewDataLoaded()
        {
            LearnersControl.UpdateChildControlAsNewDataLoaded();
            DPOutcomeControl.UpdateChildControlAsNewDataLoaded();
            HomeScreenControl.RefreshData();
        }
        #endregion

        #region PRIVATE Methods
        private void HomeScreenControl_OnNewFileImported(string newFileName) //(object sender, RoutedEventArgs e)
        {
            App.Log("MainWindow", " HomeScreenControl_OnNewFileImported", $"New file {newFileName} imported");
            UpdateChildControlAsNewDataLoaded();
        }
        public void HomeScreenControl_OnUkprnUpdated(string ukprn)// (object sender, RoutedEventArgs e)
        {
            OnPropertyChanged("WindowTitle");
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (App.ILRMessage != null)
            {
                App.ILRMessage.Save();
            }

        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            Application.Current.Shutdown();
        }

        #endregion

        #region INotifyPropertyChanged Members
        /// <summary>
        /// INotifyPropertyChanged requires a property called PropertyChanged.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Fires the event for the property when it changes.
        /// </summary>
        protected virtual void OnPropertyChanged(string propertyName)
        {
#if DEBUG
            VerifyPropertyName(propertyName);
#endif
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public void VerifyPropertyName(string propertyName)
        {
            // Verify that the property name matches a real,  
            // public, instance property on this object.
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                var msg = "Invalid property name: " + propertyName;

                if (this.ThrowOnInvalidPropertyName)
                {
                    throw new Exception(msg);
                }
                else
                {
                    Debug.Fail(msg);
                }
            }
        }

        protected bool ThrowOnInvalidPropertyName { get; set; }

        #endregion

    }
}
