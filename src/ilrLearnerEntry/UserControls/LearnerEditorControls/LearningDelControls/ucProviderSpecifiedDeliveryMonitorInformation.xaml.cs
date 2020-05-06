using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Controls;

namespace ilrLearnerEntry.UserControls.LearnerEditorControls.LearningDelControls
{
    /// <summary>
    /// Interaction logic for ucProviderSpecifiedDeliveryMonitorInformation.xaml
    /// </summary>
    public partial class ucProviderSpecifiedDeliveryMonitorInformation : UserControl, INotifyPropertyChanged
    {


        #region Private Variables
        private ILR.LearningDelivery _learningDelivery;
        #endregion

        #region Constructor
        public ucProviderSpecifiedDeliveryMonitorInformation()
        {
            InitializeComponent();
        }
        #endregion

        #region Public Properties

        public ILR.LearningDelivery CurrentLearningDelivery
        {
            get { return _learningDelivery; }
            set
            {
                if (value != null)
                {
                    _learningDelivery = value;

                    this.DataContext = this;
                    OnPropertyChanged("CurrentLearningDelivery");
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

    }
}
