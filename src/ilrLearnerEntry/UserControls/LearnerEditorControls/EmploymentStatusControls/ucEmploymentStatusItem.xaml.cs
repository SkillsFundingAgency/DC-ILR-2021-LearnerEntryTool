
using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Windows.Controls;
using ILR;

namespace ilrLearnerEntry.UserControls.EmploymentStatus
{
    /// <summary>
    /// Interaction logic for ucEmploymentStatusItem.xaml
    /// </summary>
    public partial class ucEmploymentStatusItem : UserControl, INotifyPropertyChanged
    {
        private LearnerEmploymentStatus _learnerEmploymentStatus;

        #region Constructor
        public ucEmploymentStatusItem()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        #endregion

        #region Public Properties
        public LearnerEmploymentStatus CurrentItem
        {
            get { return _learnerEmploymentStatus; }
            set
            {
                if (value != null)
                {
                    _learnerEmploymentStatus = value;
                    this.DataContext = this; OnPropertyChanged("LengthOfUnemploymentList");
                    OnPropertyChanged("BenifitStatusIndicatorList");
                    OnPropertyChanged("EmploymentIntensityIndicatorList");
                    OnPropertyChanged("LengthOfEmploymentList");
                    OnPropertyChanged("CurrentItem");
                }
                else
                {
                    this.DataContext = null;
                }

            }
        }
        public DataTable EmpStatList { set; get; }
        public DataTable LengthOfUnemploymentList { set; get; }
        public DataTable BenifitStatusIndicatorList { set; get; }
        public DataTable EmploymentIntensityIndicatorList { set; get; }
        public DataTable LengthOfEmploymentList { set; get; }
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
