using ILR;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace ilrLearnerEntry.UserControls.LearnerEditorControls.LearnerControls
{
    /// <summary>
    /// Interaction logic for ucLearnerContacePreferences.xaml
    /// </summary>
    public partial class ucLearnerContacePreferences : BaseUserControl, INotifyPropertyChanged
    {

        #region Private Variables
        private Learner _learner;
        #endregion

        #region constructor
        public ucLearnerContacePreferences()
        {
            InitializeComponent();
        }
        #endregion

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

        private void UserControl_RequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
        {
            OnPropertyChanged("CurrentItem");
            CurrentItem.RefreshData();
        }


    }
}
