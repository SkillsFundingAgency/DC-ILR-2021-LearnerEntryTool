
using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Windows.Controls;
using ILR;
using ilrLearnerEntry.UserControls.Validations;

namespace ilrLearnerEntry.UserControls.LearnerEditorControls.LearningDelControls.FinancialDetailControls
{
    /// <summary>
    /// Interaction logic for ucApprenticeshipFinancialRecordItem.xaml
    /// </summary>
    public partial class ucFinancialDetailtem : BaseUserControl, INotifyPropertyChanged
    {
        private const String CLASSNAME = "ApprenticeshipFinancialRecord";
        private ApprenticeshipFinancialRecord _item;
        private string _tbfincode = string.Empty;
        private string _tbfinamount = string.Empty;

        public ucFinancialDetailtem()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        #region Public Properties
        public ApprenticeshipFinancialRecord CurrentItem
        {
            get { return _item; }
            set
            {
                if (value != null)
                {
                    _item = value;
                    this.DataContext = this;
                }
                else
                {
                    this.DataContext = null;
                }
                OnPropertyChanged("AFinCodetList");
                OnPropertyChanged("AFinTypeList");
                OnPropertyChanged("AFinCode");
                OnPropertyChanged("AFinAmount");
                OnPropertyChanged("CurrentItem");
            }
        }
        public string AFinCode
        {
            get { return _tbfincode; }
            set
            {
                _tbfincode = value;
                bool result = Int32.TryParse(System.Convert.ToString(value), out var number);
                if (result)
                {
                    CurrentItem.AFinCode = number;
                }
            }
        }
        public string AFinAmount
        {
            get { return _tbfinamount; }
            set
            {
                _tbfinamount = value;
                bool result = Int32.TryParse(System.Convert.ToString(value), out var number);
                if (result)
                {
                    CurrentItem.AFinAmount = number;
                }
            }
        }
        public DataTable AFinTypeList { set; get; }
        public DataTable AFinCodetList { set; get; }
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

        #region IDataErrorInfo Members
        public string this[string columnName]
        {
            get
            {
                string sReturn = null;
                if (CurrentItem != null)
                {
                    switch (columnName)
                    {
                        case "AFinCode":
                            if (!string.IsNullOrEmpty(AFinCode))
                            {
                                sReturn += CheckPropertyLength(AFinCode, CLASSNAME, columnName);
                                sReturn += NumericValidations.CheckInt32ValidValue(AFinCode, columnName);
                            }
                            break;
                        case "AFinAmount":
                            if (!string.IsNullOrEmpty(AFinAmount))
                            {
                                sReturn += CheckPropertyLength(AFinAmount, CLASSNAME, columnName);
                                sReturn += NumericValidations.CheckInt32ValidValue(AFinAmount, columnName);
                            }
                            break;
                        default:
                            break;
                    }
                }
                return sReturn;
            }
        }
        #endregion
    }
}
