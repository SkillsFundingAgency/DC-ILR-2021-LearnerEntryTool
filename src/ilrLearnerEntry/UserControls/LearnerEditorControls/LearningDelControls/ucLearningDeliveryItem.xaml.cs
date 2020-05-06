using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Controls;

namespace ilrLearnerEntry.UserControls.LearnerEditorControls.LearningDelControls
{
    /// <summary>
    /// Interaction logic for ucLearningDeliveryItem.xaml
    /// </summary>
    public partial class ucLearningDeliveryItem : UserControl, INotifyPropertyChanged
    {
        #region Private Variables
        private ILR.LearningDelivery _learningDelivery;
        #endregion

        #region Constructor
        public ucLearningDeliveryItem()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        #endregion

        #region Public Properties
        public ILR.LearningDelivery CurrentItem
        {
            get { return _learningDelivery; }
            set
            {
                _learningDelivery = value;
                LearningStartInformationControl.CurrentItem = _learningDelivery;
                LearnerFundingAndMonitoringControl.CurrentItem = _learningDelivery;
                ProviderSpecifiedDeliveryMonitorInformationControl.CurrentLearningDelivery = _learningDelivery;
                LearningEndInformationControl.CurrentItem = _learningDelivery;
                FinancialDetailsListControl.CurrentItem = _learningDelivery;
                WorkplaceListControls.CurrentItem = _learningDelivery;
                HEControl.CurrentItem = _learningDelivery;
				OnPropertyChanged("CurrentItem");
                OnPropertyChanged("ShowControl");
            }
        }
        public System.Windows.Visibility ShowControl
        {
            get
            {
                if (this.CurrentItem == null)
                { return this.Visibility = System.Windows.Visibility.Collapsed; }
                else
                { return this.Visibility = System.Windows.Visibility.Visible; }
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
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

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
