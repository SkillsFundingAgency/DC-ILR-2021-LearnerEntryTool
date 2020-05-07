using System;
using System.ComponentModel;
using System.Data;
using System.Windows;
using ILR;
using ilrLearnerEntry.UserControls.Validations;

namespace ilrLearnerEntry.UserControls.LearnerEditorControls.LearnerControls
{
    /// <summary>
    /// Interaction logic for ucLearnerHEInformation.xaml
    /// </summary>
    public partial class ucLearnerHEInformation : BaseUserControl, INotifyPropertyChanged
    {
        #region Private Variables
        private const String CLASSNAME = "Learner";
        private Learner _learner;
        private String _hefincash = string.Empty;
        private String _hefinnearcash = string.Empty;
        private String _hefinother = string.Empty;
        private String _hefinaccommodationdiscounts = string.Empty;
        #endregion

        #region Constructor
        public ucLearnerHEInformation()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        #endregion

        #region Public Properties
        public Learner CurrentItem
        {
            get { return _learner; }
            set
            {
                _hefincash = string.Empty;
                _hefinnearcash = string.Empty;
                _hefinother = string.Empty;
                _hefinaccommodationdiscounts = string.Empty;
                if (value != null)
                {
                    _learner = value;
                    _hefincash = value.HEFinCash.ToString();
                    _hefinnearcash = value.HEFinNearCash.ToString();
                    _hefinother = value.HEFinOther.ToString();
                    _hefinaccommodationdiscounts = value.HEFinAccommodationDiscounts.ToString();
                    this.DataContext = this;
                    OnPropertyChanged("HEFinCash");
                    OnPropertyChanged("HEFinNearCash");
                    OnPropertyChanged("HEFinAccommodationDiscounts");
                    OnPropertyChanged("HEFinOther");
                    OnPropertyChanged("CurrentItem");
                }
                else
                {
                    this.DataContext = null;
                }
            }
        }
        public String HEFinCash
        {
            get { return _hefincash; }
            set
            {
                _hefincash = value;
                if (String.IsNullOrEmpty(value))
                {
                    if (CurrentItem.HEFinCash != null) CurrentItem.HEFinCash = null;
                }
                else
                {
                    bool result = Int32.TryParse(System.Convert.ToString(value), out var number);
                    if (result) { CurrentItem.HEFinCash = number; }
                }
                OnPropertyChanged("HEFinCash");
            }
        }
        public String HEFinNearCash
        {
            get { return _hefinnearcash; }
            set
            {
                _hefinnearcash = value;
                if (String.IsNullOrEmpty(value))
                {
                    if (CurrentItem.HEFinNearCash!=null) CurrentItem.HEFinNearCash = null;
                }
                else
                {
                    bool result = Int32.TryParse(System.Convert.ToString(value), out var number);
                    if (result) { CurrentItem.HEFinNearCash = number; }
                }
                OnPropertyChanged("HEFinNearCash");
            }
        }
        public String HEFinAccommodationDiscounts
        {
            get { return _hefinaccommodationdiscounts; }
            set
            {
                _hefinaccommodationdiscounts = value;
                if (String.IsNullOrEmpty(value))
                {
                    if (CurrentItem.HEFinAccommodationDiscounts != null) CurrentItem.HEFinAccommodationDiscounts = null;
                }
                else
                {
                    bool result = Int32.TryParse(System.Convert.ToString(value), out var number);
                    if (result) { CurrentItem.HEFinAccommodationDiscounts = number; }
                }
                OnPropertyChanged("HEFinAccommodationDiscounts");
            }
        }
        public String HEFinOther
        {
            get { return _hefinother; }
            set
            {
                _hefinother = value;
                if (String.IsNullOrEmpty(value))
                {
                    if (CurrentItem.HEFinOther != null) CurrentItem.HEFinOther = null;
                }
                else
                {
                    bool result = Int32.TryParse(System.Convert.ToString(value), out var number);
                    if (result) { CurrentItem.HEFinOther = number; }
                }
                OnPropertyChanged("HEFinOther");
            }
        }
        // Change from Number to String here to stopp GI missing blank string !!
        //public string UCASENum { get; set; }
        public DataTable TermTimeAccList { set; get; }
        #endregion

        #region Public Methods
        #endregion

        #region PRIVATE Methods
        private void UserControl_RequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
        {
            OnPropertyChanged("CurrentItem");
            CurrentItem.RefreshData();
        }
        #endregion

        #region IDataErrorInfo Members

        public string this[string columnName]
        {
            get
            {
                string sReturn = null;
                switch (columnName)
                {
                    case "HEFinCash":
                        if (!string.IsNullOrEmpty(HEFinCash))
                        {
                            sReturn += CheckPropertyLength(HEFinCash, CLASSNAME, columnName);
                            sReturn += NumericValidations.CheckInt32ValidValue(HEFinCash, columnName);
                        }
                        break;
                    case "HEFinNearCash":
                        if (!string.IsNullOrEmpty(HEFinNearCash))
                        {
                            sReturn += CheckPropertyLength(HEFinNearCash, CLASSNAME, columnName);
                            sReturn += NumericValidations.CheckInt32ValidValue(HEFinNearCash, columnName);
                        }
                        break;
                    case "HEFinOther":
                        if (!string.IsNullOrEmpty(HEFinOther))
                        {
                            sReturn += CheckPropertyLength(HEFinOther, CLASSNAME, columnName);
                            sReturn += NumericValidations.CheckInt32ValidValue(HEFinOther, columnName);
                        }
                        break;
                    case "HEFinAccommodationDiscounts":
                        if (!string.IsNullOrEmpty(HEFinAccommodationDiscounts))
                        {
                            sReturn += CheckPropertyLength(HEFinAccommodationDiscounts, CLASSNAME, columnName);
                            sReturn += NumericValidations.CheckInt32ValidValue(HEFinAccommodationDiscounts, columnName);
                        }
                        break;
                    default:
                        break;
                }
                return sReturn;
            }
        }
        #endregion

    }
}
