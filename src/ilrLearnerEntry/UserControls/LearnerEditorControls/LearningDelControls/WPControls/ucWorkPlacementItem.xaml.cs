using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Windows.Controls;
using ILR;
using ilrLearnerEntry.UserControls.Validations;

namespace ilrLearnerEntry.UserControls.LearnerEditorControls.LearningDelControls.WorkPlacementControls
{
    /// <summary>
    /// Interaction logic for ucWorkPlacementItem.xaml
    /// </summary>
    public partial class ucWorkPlacementItem : UserControl, INotifyPropertyChanged, IDataErrorInfo
    {
        private const String CLASSNAME = "LearningDeliveryWorkPlacement";
        private ILR.Schema XmlSchema = new ILR.Schema();

        private LearningDeliveryWorkPlacement _learningDeliveryWorkPlacement;
        private string _workplaceempid = string.Empty;

        #region Constructor
        public ucWorkPlacementItem()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        #endregion

        #region Public Properties
        public LearningDeliveryWorkPlacement CurrentItem
        {
            get { return _learningDeliveryWorkPlacement; }
            set
            {
                if (value != null)
                {
                    _learningDeliveryWorkPlacement = value;
                    WorkPlaceEmpId = _learningDeliveryWorkPlacement.WorkPlaceEmpId;
                    WorkPlaceHours = _learningDeliveryWorkPlacement.WorkPlaceHours;
                    this.DataContext = this;
                    OnPropertyChanged("CurrentItem");
                    OnPropertyChanged("WorkPlaceEmpId");
                    OnPropertyChanged("WorkPlaceHours");
                    OnPropertyChanged("WorkplacementCodeList");
                    _learningDeliveryWorkPlacement.Refresh();
                }
                else
                {
                    this.DataContext = null;
                }
            }
        }
        public string WorkPlaceEmpId
        {
            get
            {                
                return (_learningDeliveryWorkPlacement!=null)?_learningDeliveryWorkPlacement.WorkPlaceEmpId:string.Empty;
            }
            set
            {
                _learningDeliveryWorkPlacement.WorkPlaceEmpId = value;
                //int number;
                //bool result = Int32.TryParse(System.Convert.ToString(value), out number);
                //if (result)
                //{
                //    CurrentItem.WorkPlaceEmpId = number.ToString();
                //}
                _learningDeliveryWorkPlacement.Refresh();
            }
        }
        public string WorkPlaceHours
        {
            get { return (CurrentItem!=null)?CurrentItem.WorkPlaceHours:string.Empty; }
            set
            {
                CurrentItem.WorkPlaceHours = value;
                _learningDeliveryWorkPlacement.Refresh();
            }
        }
        public DataTable WorkplacementCodeList { set; get; }

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

        #region IDataErrorInfo Members
        public string Error
        {
            get { throw new NotImplementedException(); }
        }
        public string this[string columnName]
        {
            get
            {
                string sReturn = null;
                if (CurrentItem != null)
                {
                    switch (columnName)
                    { 
                        case "WorkPlaceEmpId":
                            if (!string.IsNullOrEmpty(WorkPlaceEmpId))
                            {
                                sReturn += CheckPropertyLength(WorkPlaceEmpId, CLASSNAME, columnName);
                                sReturn += NumericValidations.CheckInt32ValidValue(WorkPlaceEmpId, columnName);
                            }
                            break;
                        case "WorkPlaceHours":
                            if (!string.IsNullOrEmpty(WorkPlaceHours))
                            {
                                sReturn += CheckPropertyLength(WorkPlaceHours, CLASSNAME, columnName);
                                sReturn += NumericValidations.CheckInt32ValidValue(WorkPlaceHours, columnName);
                            }
                            else if (WorkPlaceHours == null || WorkPlaceHours.ToString().Length == 0)
                                return "Work Place Hours is required\r\n";
                            break;
                        default:
                            break;
                    }
                }
                return sReturn;
            }
        }
        public int GetItemSize(string ItemName)
        {
            return XmlSchema.GetMaxLength(ItemName);
        }
        public string CheckPropertyLength(object itemValue, string ClassName, string ItemName)
        {
            String ItemFullName = String.Format("{0}.{1}", ClassName, ItemName);
            int ItemSize = GetItemSize(ItemFullName);
            if (itemValue != null && itemValue.ToString().Length > ItemSize)
            {
                return String.Format("exceeds maximum length ({0} characters). Current length : {1}\r\n", ItemSize, itemValue.ToString().Length);
            }
            return null;
        }
        #endregion


    }
}
