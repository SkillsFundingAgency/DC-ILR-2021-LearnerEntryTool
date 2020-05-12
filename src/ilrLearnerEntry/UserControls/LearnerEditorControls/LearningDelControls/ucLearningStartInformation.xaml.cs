using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ILR;
using ilrLearnerEntry.UserControls.Validations;

namespace ilrLearnerEntry.UserControls.LearnerEditorControls.LearningDelControls
{
    /// <summary>
    /// Interaction logic for ucLearningStartInformation.xaml
    /// </summary>
    public partial class ucLearningStartInformation : BaseUserControl, INotifyPropertyChanged, IDataErrorInfo
    {
        #region Private Variables

        private const String CLASSNAME = "LearningDelivery";

        private ILR.LearningDelivery _learningDelivery;
        private const Int32 _maxHHSItem = 4;

        private string _priorlearnfundadj = string.Empty;
        private string _progtype = string.Empty;
        //private string _fworkcode = string.Empty;
        private string _pwaycode = string.Empty;
        private string _otherfundadj = string.Empty;
        private string _addhours = string.Empty;
        private string _phours = string.Empty;
        private string _otjActhours = string.Empty;
        private string _partnerukprn = string.Empty;
        #endregion

        #region Constructor
        public ucLearningStartInformation()
        {
            InitializeComponent();
        }
        #endregion

        #region Public Properties
        private Boolean IsClearHSSSelection = false;
        public ILR.LearningDelivery CurrentItem
        {
            get { return _learningDelivery; }
            set
            {
                lv_HHS.SelectionChanged -= lv_HHS_SelectionChanged;
                this.DataContext = null;
                IsClearHSSSelection = true;
                _learningDelivery = null;
                _priorlearnfundadj = string.Empty;
                _progtype = string.Empty;
                //_fworkcode = string.Empty;
                _pwaycode = string.Empty;
                _otherfundadj = string.Empty;
                _addhours = string.Empty;
                _phours = string.Empty;
                _otjActhours = string.Empty;
                _partnerukprn = string.Empty;
                ClearAllHHSSelected();
                if (value != null)
                {
                    _learningDelivery = value;
                    this.DataContext = this;
                    _priorlearnfundadj = _learningDelivery.PriorLearnFundAdj.ToString();
                    _progtype = _learningDelivery.ProgType.ToString();
                    //_fworkcode = _learningDelivery.FworkCode;
                    _pwaycode = _learningDelivery.PwayCode.ToString();
                    _otherfundadj = _learningDelivery.OtherFundAdj.ToString();
                    _addhours = _learningDelivery.AddHours.ToString();
                    _phours = _learningDelivery.PHours.ToString();
                    _otjActhours = _learningDelivery.OTJActhours.ToString();
                    _partnerukprn = _learningDelivery.PartnerUKPRN.ToString();
                    foreach (LearningDeliveryFAM HHSFam in _learningDelivery.HHS)
                    {
                        SetHHSAsSelected(HHSFam);
                    }
                    lv_HHS.SelectionChanged += lv_HHS_SelectionChanged;
                }
                OnPropertyChanged("AimTypeList");
                OnPropertyChanged("FundModelList");
                OnPropertyChanged("PriorLearnFundAdj");
                OnPropertyChanged("ProgType");
                //OnPropertyChanged("FworkCode");
                OnPropertyChanged("PwayCode");
                OnPropertyChanged("OtherFundAdj");
                OnPropertyChanged("AddHours");
                OnPropertyChanged("OTJActhours");
                OnPropertyChanged("PHours");
                OnPropertyChanged("PartnerUKPRN");
                OnPropertyChanged("ProgTypeList");
                OnPropertyChanged("HHSList");
                OnPropertyChanged("CurrentItem");
            }
        }
     

        public string PriorLearnFundAdj
        {
            get { return _priorlearnfundadj; }
            set
            {
                _priorlearnfundadj = value;
                bool result = Int32.TryParse(System.Convert.ToString(value), out var number);
                if (result) { CurrentItem.PriorLearnFundAdj = number; }
                else { CurrentItem.PriorLearnFundAdj = null; }
            }
        }
        public string ProgType
        {
            get { return _progtype; }
            set
            {
                _progtype = value;
                bool result = Int32.TryParse(System.Convert.ToString(value), out var number);
                if (result) { CurrentItem.ProgType = number; }
                else { CurrentItem.ProgType = null; }
            }
        }

        public string PwayCode
        {
            get { return _pwaycode; }
            set
            {
                _pwaycode = value;
                bool result = Int32.TryParse(System.Convert.ToString(value), out var number);
                if (result)
                { CurrentItem.PwayCode = number; }
                else            
                { CurrentItem.PwayCode = null; }
            }
        }
        public string OtherFundAdj
        {
            get { return _otherfundadj; }
            set
            {
                _otherfundadj = value;
                bool result = Int32.TryParse(System.Convert.ToString(value), out var number);
                if (result)
                { CurrentItem.OtherFundAdj = number; }
                else
                { CurrentItem.OtherFundAdj = null; }
            }
        }
        public string AddHours
        {
            get { return _addhours; }
            set
            {
                _addhours = value;
                bool result = Int32.TryParse(System.Convert.ToString(value), out var number);
                if (result)
                { CurrentItem.AddHours = number; }
                else
                { CurrentItem.AddHours = null; }
            }
        }

        public string PHours
        {
            get { return _phours; }
            set
            {
                _phours = value;
                bool result = Int32.TryParse(System.Convert.ToString(value), out var number);
                if (result)
                { CurrentItem.PHours = number; }
                else
                { CurrentItem.PHours = null; }
            }
        }

        public string OTJActhours
        {
            get { return _otjActhours; }
            set
            {
                _otjActhours = value;
                bool result = Int32.TryParse(System.Convert.ToString(value), out var number);
                if (result)
                { CurrentItem.OTJActhours = number; }
                else
                { CurrentItem.OTJActhours = null; }
            }
        }

        public string PartnerUKPRN
        {
            get { return _partnerukprn; }
            set
            {
                _partnerukprn = value;
                bool result = Int32.TryParse(System.Convert.ToString(value), out var number);
                if (result)
                { CurrentItem.PartnerUKPRN = number; }
                else
                { CurrentItem.PartnerUKPRN = null; }
            }
        }
        #region Datatables for Combo box values
        public DataTable AimTypeList { set; get; }
        public DataTable FundModelList { set; get; }
        public DataTable ProgTypeList { set; get; }
        private DataTable _dtHHS;
        public DataTable HHSList
        {
            get { return _dtHHS; }
            set
            {
                DataTable tmpDT = value;
                foreach (DataRow dr in tmpDT.Rows)
                {
                    if (String.IsNullOrWhiteSpace(dr["Code"].ToString()))
                    {
                        dr.Delete();
                        break;
                    }
                }
                _dtHHS = tmpDT;
                _dtHHS.Columns.Add(new DataColumn("IsSelected", typeof(bool)));
            }
        }
        #endregion
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        private void lv_HHS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (DataRowView x in e.AddedItems)
            {
                LearningDeliveryFAM tmp = _learningDelivery.HHS.Where(f => f.LearnDelFAMType == "HHS" && f.LearnDelFAMCode == x["Code"].ToString()).FirstOrDefault();
                if (tmp == null)
                {
                    if (_learningDelivery.HHS.Count < _maxHHSItem)
                    {
                        _learningDelivery.AddFAM(LearningDeliveryFAM.MultiOccurrenceFAMs.HHS, x["Code"].ToString());
                    }
                    else
                    {
                        MessageBox.Show(String.Format("   You may only select {0} items.", _maxHHSItem.ToString())
                                                               , "Max number of selectable items reached."
                                                               , MessageBoxButton.OK
                                                               , MessageBoxImage.Information
                                                               , MessageBoxResult.OK);
                        x["IsSelected"] = Convert.ToBoolean(false);
                        OnPropertyChanged("HHSList");
                    }
                }
            }
            foreach (DataRowView x in e.RemovedItems)
            {
                if (!IsClearHSSSelection)
                {
                    Console.WriteLine(string.Format("Remove {0}", x["Code"].ToString()));
                    foreach (LearningDeliveryFAM fam in _learningDelivery.HHS)
                    {
                        if (fam.LearnDelFAMCode.ToString() == x["Code"].ToString())
                        {
                            _learningDelivery.RemoveFAM(LearningDeliveryFAM.MultiOccurrenceFAMs.HHS, x["Code"].ToString());
                            break;
                        }
                    }
                }
            }
        }
        private void SetHHSAsSelected(LearningDeliveryFAM Filter)
        {
            if (_dtHHS != null)
            {
                foreach (DataRow dr in _dtHHS.Rows)
                {
                    if (dr["Code"].ToString() == Filter.LearnDelFAMCode)
                    {
                        dr["IsSelected"] = Convert.ToBoolean(true);
                        break;
                    }
                }
            }
        }

        private void ClearAllHHSSelected()
        {
            IsClearHSSSelection = true;

            if (_dtHHS != null)
            {
                foreach (DataRow dr in _dtHHS.Rows)
                {
                    dr["IsSelected"] = Convert.ToBoolean(false);
                }
            }
            IsClearHSSSelection = false;

        }

        private void ValidateToNumeric(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
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
                    case "PriorLearnFundAdj":
                        if (!string.IsNullOrEmpty(PriorLearnFundAdj))
                        {
                            sReturn += CheckPropertyLength(PriorLearnFundAdj, CLASSNAME, columnName);
                            sReturn += NumericValidations.CheckInt32ValidValue(PriorLearnFundAdj, columnName);
                        }
                        break;
                    case "OTJActhours":
                        if (!string.IsNullOrEmpty(AddHours))
                        {
                            sReturn += CheckPropertyLength(AddHours, CLASSNAME, columnName);
                            sReturn += NumericValidations.CheckInt32ValidValue(AddHours, columnName);
                        }
                        break;
                    case "ProgType":
                        if (!string.IsNullOrEmpty(ProgType))
                        {
                            sReturn += CheckPropertyLength(ProgType, CLASSNAME, columnName);
                            sReturn += NumericValidations.CheckInt32ValidValue(ProgType, columnName);
                        }
                        break;
                    case "PwayCode":
                        if (!string.IsNullOrEmpty(PwayCode))
                        {
                            sReturn += CheckPropertyLength(PwayCode, CLASSNAME, columnName);
                            sReturn += NumericValidations.CheckInt32ValidValue(PwayCode, columnName);
                        }
                        break;
                    case "OtherFundAdj":
                        if (!string.IsNullOrEmpty(OtherFundAdj))
                        {
                            sReturn += CheckPropertyLength(OtherFundAdj, CLASSNAME, columnName);
                            sReturn += NumericValidations.CheckInt32ValidValue(OtherFundAdj, columnName);
                        }
                        break;
                    case "AddHours":
                        if (!string.IsNullOrEmpty(AddHours))
                        {
                            sReturn += CheckPropertyLength(AddHours, CLASSNAME, columnName);
                            sReturn += NumericValidations.CheckInt32ValidValue(AddHours, columnName);
                        }
                        break;
                    case "PartnerUKPRN":
                        if (!string.IsNullOrEmpty(PartnerUKPRN))
                        {
                            sReturn += CheckPropertyLength(PartnerUKPRN, CLASSNAME, columnName);
                            sReturn += NumericValidations.CheckInt32ValidValue(PartnerUKPRN, columnName);
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
