﻿using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;


namespace ilrLearnerEntry.UserControls
{
    /// <summary>
    /// Interaction logic for ucHomeScreen.xaml
    /// </summary>
    public partial class ucHomeScreen : BaseUserControl, INotifyPropertyChanged
    {
        #region Private Variables

        private String _windowTitle = string.Empty;
        private String _loadMessage = string.Empty;
        private String _statsMessage = string.Empty;

        private Boolean IsProcessing { get; set; }
        private string ImportFilename { get; set; }
        private string FilenameOnly { get; set; }
        private BackgroundWorker _workerStats;

        #endregion

        #region Constructor

        public ucHomeScreen()
        {
            InitializeComponent();
            txtUKPRM.TextChanged += TxtUKPRM_TextChanged;
            IsProcessing = false;
            this.DataContext = this;
            OnPropertyChanged("Statistics");
            OnPropertyChanged("UKPRN");
        }

        private void TxtUKPRM_TextChanged(object sender, TextChangedEventArgs e)
        {
            //var newValue = txtUKPRM.Text;
            //TextBox txt = e.Source as TextBox;
            //UInt32 number;

            //bool result = UInt32.TryParse(System.Convert.ToString(newValue), out number);
            //if (result) { UKPRN = (int)number; }
            //else { UKPRN = null; }
            //UKPRNWasUpdated(null, null);
        }

        #endregion



        #region routed events - UKPRN


        private void UKPRNWasUpdated(object sender, RoutedEventArgs e)
        {
            OnPropertyChanged("UKPRN");
            string ukprnValue = (UKPRN.HasValue) ? UKPRN.ToString() : string.Empty;
            OnUkprnUpdated?.Invoke(ukprnValue);

            //RaiseEvent(new RoutedEventArgs(OnUkprnUpdateEvent, String.Empty));
        }

        public delegate void HomeScreenEventHandler(string someValue);

        public event HomeScreenEventHandler OnUkprnUpdated;
        public event HomeScreenEventHandler OnNewFileImported;

        #endregion

        #region Public Properties

        public string WindowTitle
        {
            get { return App.ApplicationName; }
        }

        public string LogFileName
        {
            get
            {
                if ((App.LogFileName != null) && (App.LogFileName != string.Empty))
                {
                    System.IO.FileInfo fi = new System.IO.FileInfo(App.LogFileName);
                    return String.Format(" LogFile : {0}", fi.Name);
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public String ApplicationVersion
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
                    vReturn =
 version.Major.ToString() + "." + version.Minor.ToString() + "." + version.Build.ToString();
#endif
                }
                else
                {
                    vReturn = "Unable to get Version.";
                }

                return vReturn;
            }
        }

        public String LoadMessage
        {
            get { return _loadMessage; }
            set
            {
                _loadMessage = value;
                OnPropertyChanged("LoadMessage");
            }
        }

        public String StatsMessage
        {
            get { return _statsMessage; }
            set
            {
                _statsMessage = value;
                OnPropertyChanged("StatsMessage");
                OnPropertyChanged("StatsMessageVisibility");
            }
        }

        public Visibility StatsMessageVisibility
        {
            get { return _statsMessage == string.Empty ? Visibility.Collapsed : Visibility.Visible; }

        }

        public int? UKPRN
        {

            get { return App.ILRMessage.LearningProvider.UKPRN; }
            set
            {
                if (value.HasValue)
                {
                    App.ILRMessage.LearningProvider.UKPRN = value.Value;
                }
                else
                {
                    App.ILRMessage.LearningProvider.UKPRN = null;
                }

                UKPRNWasUpdated(null, null);
            }
        }

        public System.Data.DataTable Statistics
        {
            get { return App.ILRMessage.Statistics; }
        }

        #endregion

        #region Public Methods

        public void RefreshData()
        {
            // Not sure DoEvents is needed anymore?
            //App.DoEvents();
            SetupBackgroundWorkerStats();
        }

        #endregion

        #region PRIVATE Methods

        private void SetupBackgroundWorkerStats()
        {
            if (_workerStats == null)
            {
                _workerStats = new BackgroundWorker();
            }

            if (!_workerStats.IsBusy)
            {
                StatsMessage = String.Format("  Refreshing Stats. Please Wait...");

                App.Log("Home Screen", "ImportWorker", "Setup Worker");
                _workerStats.DoWork += new DoWorkEventHandler(workerStats_DoWork);
                _workerStats.RunWorkerCompleted += new RunWorkerCompletedEventHandler(workerStats_RunWorkerCompleted);
                _workerStats.WorkerReportsProgress = false;
                _workerStats.WorkerSupportsCancellation = true;
                _workerStats.RunWorkerAsync();
            }
            else
            {
                App.Log("Home Screen", "ImportWorker", "Worker is Busy. ");
            }
        }

        private void workerStats_DoWork(object sender, DoWorkEventArgs e)
        {
            this.IsProcessing = true;

            BackgroundWorker worker = sender as BackgroundWorker;

            try
            {
                App.Log("Home Screen", "WorkerStats", "ReFreshStats.");
                Application.Current.Dispatcher.BeginInvoke(
                    DispatcherPriority.Background,
                    new Action(() =>
                    {
                        App.ILRMessage.ReFreshStats();
                        OnPropertyChanged("Statistics");
                    }));

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                e.Cancel = _workerStats.CancellationPending;
                this.IsProcessing = false;
            }
        }

        private void workerStats_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                App.Log("Home Screen", "ImportWorke r- Progress Update", "Update Here");
                // Not used just here incase!!
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void workerStats_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            App.Log("Home Screen", "WorkerStats - Complete", "");
            try
            {
                if (e.Cancelled == true)
                {
                    App.Log("Home Screen", "WorkerStats - Complete", "was Cancelled");
                    //  throw (new Exception("Cancelled Background"));
                }
                else if (e.Error != null)
                {
                    App.Log("Home Screen", "WorkerStats - Complete", "Had Error : " + e.Error.Message);
                    throw (new Exception(e.Error.Message));
                }
                else // Completed without error !!
                {
                    StatsMessage = String.Empty;
                    OnPropertyChanged("Statistics");
                    App.Log("Home Screen", "WorkerStats - Complete", "Completed OK.");
                }
            }
            catch (Exception ex)
            {
                App.Log("HomeScreen", "WorkerStats",
                    "Error : " + String.Format("Error while  processing : {0}{1}", Environment.NewLine, ex.Message));
            }
            finally
            {
                if (_workerStats != null)
                {
                    if (_workerStats.IsBusy)
                    {
                        workerStats_CancelIfProcessing();
                    }

                    _workerStats.DoWork -= new DoWorkEventHandler(workerStats_DoWork);
                    _workerStats.RunWorkerCompleted -=
                        new RunWorkerCompletedEventHandler(workerStats_RunWorkerCompleted);
                }

                _workerStats = null;
                this.IsProcessing = false;
                StatsMessage = String.Empty;
                App.Log("Home Screen", "WorkerStats - Complete", "UI Update - End");
                App.DoEvents();
            }
        }

        private void workerStats_CancelIfProcessing()
        {
            App.Log("Home Screen", "WorkerStats - Cancel", "");
            if (_workerStats != null)
            {
                if (_workerStats.WorkerSupportsCancellation == true && _workerStats.IsBusy)
                {
                    if (!(_workerStats.CancellationPending))
                    {
                        App.Log("Home Screen", "WorkerStats - Cancel", "CancellationPending");
                        _workerStats.CancelAsync();
                    }
                }
            }
            else
            {
                this.IsProcessing = false;
            }
        }

        /// <summary>
        /// 'Import' button event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // get confirmation to import file and overwrite any existing data
                if (ShowConfirmImportFileMessageBox() == MessageBoxResult.Yes)
                {
                    ShowProgressLabel();
                    ImportIlrFile();
                }
            }
            catch (Exception ex)
            {
                ShowImportErrorMessageBox(ex);
            }
            finally
            {
                HideControlWhileLoadingFile(Visibility.Visible);
                UKPRNWasUpdated(null, null);
            }
        }

        private static MessageBoxResult ShowConfirmImportFileMessageBox()
        {
            App.Log("Home Screen", "Import", "Show Warning Message");

            MessageBoxResult result = MessageBox.Show(String.Format(
                    "      Importing a new file will OVERWRITE the current data." +
                    "{0}{0}      Any changes you have made WILL BE LOST." +
                    "{0}{0}{0}      Are you sure you want to do this ?", Environment.NewLine)
                , "    Warning!"
                , MessageBoxButton.YesNo
                , MessageBoxImage.Warning
                , MessageBoxResult.No);
            return result;
        }

        private void ShowProgressLabel()
        {
            LoadMessage = String.Empty;
            this.lbloadingMsg.Visibility = Visibility.Visible;
        }

        private void ImportIlrFile()
        {
            App.Log("Home Screen", "Import", "Accepted Warning Message");

            ImportFilename = ilrLearnerEntry.Utils.FileStuff.getFileName("XML |*.xml", "");
            App.Log("Home Screen", "Import", String.Format("Selected File : {0}", ImportFilename));

            if (System.IO.File.Exists(ImportFilename))
            {
                App.Log("Home Screen", "Import", "File Exists");
                UpdateProgressLabel_PleaseWaitMessage();

                System.IO.FileInfo fi = new System.IO.FileInfo(this.ImportFilename);
                this.FilenameOnly = fi.Name;
                fi = null;

                App.DoEvents();
                App.Log("Home Screen", "Import", "Call Import File");

                App.ILRMessage.Import(ImportFilename);

                LoadMessage = String.Format("Loading File : {0}  {1} {1}Completed.", FilenameOnly, Environment.NewLine);
                OnPropertyChanged("LoadMessage");
                OnPropertyChanged("UKPRN");
                App.DoEvents();
                OnNewFileImported?.Invoke(string.Empty);
            }
            else
            {
                LoadMessage = String.Empty;
                OnPropertyChanged("LoadMessage");
                App.DoEvents();
            }

            OnPropertyChanged("LoadMessage");
        }

        private void UpdateProgressLabel_PleaseWaitMessage()
        {
            LoadMessage =
                String.Format(
                    "  Loading File : {0} {1}    Please Wait...{1} {1}    This may take a few minutes depending on size of file.",
                    FilenameOnly, Environment.NewLine);
            OnPropertyChanged("LoadMessage");

            HideControlWhileLoadingFile(Visibility.Collapsed);
            App.DoEvents();
        }

        private static void ShowImportErrorMessageBox(Exception ex)
        {
            MessageBox.Show(String.Format("Error while loading your file.{0}{0}{1}", Environment.NewLine,
                    ex.Message)
                , "Error loading File"
                , MessageBoxButton.OK
                , MessageBoxImage.Error);
        }

        private bool HasInvalidLdp()
        {
            return App.ILRMessage.LearnerDestinationandProgressionList.Any(ldp => ((!ldp.ULN.HasValue || string.IsNullOrEmpty(ldp.LearnRefNumber)) && !ldp.ExcludeFromExport));
        }
        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            LoadMessage = String.Format("Export Started : {0}", this.UKPRN.ToString());
            if (String.IsNullOrEmpty(this.UKPRN.ToString()))
            {
                MessageBox.Show(String.Format("UKPRN Not Supplied. {0} Please Enter UKPRN on Home Screen", Environment.NewLine)
                                                           , "UKPRN Not Supplied"
                                                           , MessageBoxButton.OK
                                                           , MessageBoxImage.Error
                                                           , MessageBoxResult.OK);

                LoadMessage = String.Empty;
            }
            else if (App.ILRMessage.LearnerExportCount == 0)
            {
                MessageBox.Show(String.Format("There are No learners in a completed State marked for export.", Environment.NewLine)
                                                           , "Nothing to Export"
                                                           , MessageBoxButton.OK
                                                           , MessageBoxImage.Error
                                                           , MessageBoxResult.OK);

                LoadMessage = String.Empty;
            }
            else if (HasInvalidLdp())
            {
                MessageBox.Show(String.Format("There are invalid Learner Destination Progression records, please correct them", Environment.NewLine)
                                                          , "Unable to Export"
                                                          , MessageBoxButton.OK
                                                          , MessageBoxImage.Error
                                                          , MessageBoxResult.OK);
                LoadMessage = String.Empty;
            }
            else
            {
                this.lbloadingMsg.Visibility = Visibility.Visible;
                App.Log("Home Screen", "Export", "");
                using (
                var dialog = new System.Windows.Forms.FolderBrowserDialog())
                {
                    dialog.ShowNewFolderButton = true;
                    dialog.Description = "Select folder to Export file to.";
                    dialog.SelectedPath = App.ExportFolder;
                    System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                    if ((result == System.Windows.Forms.DialogResult.OK) || (result == System.Windows.Forms.DialogResult.OK))
                    {
                        App.Log("Home Screen", "Export", "Start");
                        if (App.ExportFolder != dialog.SelectedPath)
                        {
                            App.ExportFolder = dialog.SelectedPath;
                        }
                        LoadMessage = String.Format("Export Started : {0}.", this.UKPRN.ToString());
                        App.DoEvents();
                        OnPropertyChanged("LoadMessage");
                        App.ILRMessage.Save();
                        LoadMessage = String.Format("Export Started : {0}..", this.UKPRN.ToString());
                        App.DoEvents();

                        App.ILRMessage.Export(App.ExportFolder,App.ApplicationVersion.ToString());
                        LoadMessage = String.Format("Export Started : {0}...", this.UKPRN.ToString());
                       
                        App.Log("Home Screen", "Export", "Complete");
                        StatsMessage = String.Format("Export Complete..");
                        App.DoEvents();
                        OnPropertyChanged("LoadMessage");
                    }
                }
            }
            this.lbloadingMsg.Visibility = Visibility.Collapsed;

        }
        private void btnOpenExportFolder_Click(object sender, RoutedEventArgs e)
        {        
            ilrLearnerEntry.Utils.FileStuff.OpenFolderInExplorer(App.ExportFolder);
        }
        private void UserControl_RequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
        {
            RefreshData();
            LoadMessage = String.Empty;
            this.lbloadingMsg.Visibility = Visibility.Collapsed;
        }
        private void HideControlWhileLoadingFile(Visibility ShowOrHide)
        {
            this.dgStats.Visibility = ShowOrHide;
            this.ImportExportGrid.Visibility = ShowOrHide;
            this.UKPRNGrid.Visibility = ShowOrHide;
        }
        #endregion
    }
}
