﻿using System;
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
    public partial class ucProviderSpecifiedLearningMonitorInformation : UserControl, INotifyPropertyChanged
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

		private void UserControl_RequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
		{
			OnPropertyChanged("CurrentItem");
			CurrentItem.RefreshData();
		}



    }
}