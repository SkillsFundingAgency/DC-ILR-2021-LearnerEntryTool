using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using ILR;

namespace ilrLearnerEntry.UserControls.LearnerEditorControls.LearnerControls
{
    /// <summary>
    /// Interaction logic for ucProviderSpecifiedLearningMonitorInformation.xaml
    /// </summary>
    public partial class ucProviderSpecifiedLearningMonitorInformation : BaseUserControl, INotifyPropertyChanged
    {
        
        #region Private Variables
        private Learner _learner;
        #endregion

        #region Constructor
        public ucProviderSpecifiedLearningMonitorInformation()
        {
            InitializeComponent();
        }
        #endregion

        #region Public Properties
        public Learner CurrentItem
        {
            get { return _learner; }
            set
            {
                if (value != null)
                {
                    _learner = value;
                    this.DataContext = this;
                    OnPropertyChanged("CurrentItem");
                }
                else
                {
                    this.DataContext = null;
                }
            }
        }
        #endregion

        #region Public Methods
        #endregion

        #region PRIVATE Methods
        #endregion

		private void UserControl_RequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
		{
			OnPropertyChanged("CurrentItem");
			CurrentItem.RefreshData();
		}



    }
}