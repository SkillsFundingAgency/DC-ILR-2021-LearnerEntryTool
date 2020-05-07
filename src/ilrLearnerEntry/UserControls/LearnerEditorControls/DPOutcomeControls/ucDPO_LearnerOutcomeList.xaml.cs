using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using ILR;
using ilrLearnerEntry.UserControls.Validations;

namespace ilrLearnerEntry.UserControls.LearnerEditorControls.DPOutcomeControls
{
    /// <summary>
    /// Interaction logic for ucDPO_LearnerOutcomeList.xaml
    /// </summary>
    public partial class ucDPO_LearnerOutcomeList : BaseUserControl, INotifyPropertyChanged, IDataErrorInfo
    {
        #region Private Variables
        private LearnerDestinationandProgression _learnerDP;
        private const String CLASSNAME = "LearnerDestinationandProgression";
        #endregion

        #region Constructor
        public ucDPO_LearnerOutcomeList()
        {
            InitializeComponent();
            lv.Visibility = System.Windows.Visibility.Visible;
            OutcomeDetailControl.Visibility = System.Windows.Visibility.Collapsed;
        }
        #endregion

        #region Public Properties
        public LearnerDestinationandProgression CurrentItem
        {
            get { return _learnerDP; }
            set
            {
                _learnerDP = null;
                if (value == null)
                {
                    this.DataContext = null;
                    if (LearnerOutcomeItemsCV != null)
                    {
                        LearnerOutcomeItemsCV.CurrentChanged -= LearnerOutcomeItemsCV_CurrentChanged;
                    }
                    LearnerOutcomeItemsCV = null;
                    ULN = string.Empty;
                    OutcomeDetailControl.Visibility = System.Windows.Visibility.Collapsed;
                }
                else
                {
                    _learnerDP = value;
                    ULN = _learnerDP.ULN.ToString();
                    if (LearnerOutcomeItemsCV == null)
                    {
                        LearnerOutcomeItemsCV = CollectionViewSource.GetDefaultView(_learnerDP.DPOutcomeList);
                        LearnerOutcomeItemsCV.CurrentChanged += LearnerOutcomeItemsCV_CurrentChanged;
                    }
                    if (value.OutcomeCount > 0)
                    {
                        foreach (ILR.DPOutcome xDp in _learnerDP.DPOutcomeList)
                        {
                            xDp.IsSelected = false;
                        }
                        LearnerOutcomeItemsCV = CollectionViewSource.GetDefaultView(_learnerDP.DPOutcomeList);
                        LearnerOutcomeItemsCV.MoveCurrentToFirst();
                        if (_learnerDP.DPOutcomeList.Count > 0)
                        {
                            (LearnerOutcomeItemsCV.CurrentItem as ILR.DPOutcome).IsSelected = true;
                        }
                        SetSubControl(LearnerOutcomeItemsCV.CurrentItem as ILR.DPOutcome);
                    }
                    else
                    {
                        LearnerOutcomeItemsCV = CollectionViewSource.GetDefaultView(_learnerDP.DPOutcomeList);
                        SetSubControl(null);
                    }
                    LearnerOutcomeItemsCV.Refresh();
                    this.DataContext = this;
                }
                OnPropertyChanged("CurrentItem");
                OnPropertyChanged("LearnerOutcomeItemsCV");
                ShowHidChildControls();
            }
        }
        private string _uln;
        public string ULN
        {
            get { return _uln; }
            set
            {
                _uln = value;
                bool result = Int64.TryParse(System.Convert.ToString(value), out var number);
                if (result)
                {
                    _learnerDP.ULN = number;
                }
                else if (string.IsNullOrEmpty(value) && _learnerDP != null)
                {
                    _learnerDP.ULN = null;
                }
                OnPropertyChanged("ULN");
            }
        }
        void LearnerOutcomeItemsCV_CurrentChanged(object sender, EventArgs e)
        {
            if (LearnerOutcomeItemsCV.CurrentItem != null)
            {
                if (!(LearnerOutcomeItemsCV.CurrentItem as ILR.DPOutcome).IsSelected)
                {
                    (LearnerOutcomeItemsCV.CurrentItem as ILR.DPOutcome).IsSelected = true;
                }
                SetSubControl((LearnerOutcomeItemsCV.CurrentItem as ILR.DPOutcome));
            }
        }
        public ICollectionView LearnerOutcomeItemsCV
        {
            get;
            private set;
        }
        #endregion

        #region PRIVATE Methods
        private void SetSubControl(ILR.DPOutcome outcome)
        {
            if (LearnerOutcomeItemsCV.CurrentItem != null)
            {
                OutcomeDetailControl.CurrentItem = outcome;
            }
            else
            {
                OutcomeDetailControl.CurrentItem = null;
            }
        }
        private void ShowHidChildControls()
        {
            if (_learnerDP != null && _learnerDP.DPOutcomeList.Count > 0)
            {
                OutcomeDetailControl.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                OutcomeDetailControl.Visibility = System.Windows.Visibility.Collapsed;
            }
        }
        private void AddNewOutcome(object sender, RoutedEventArgs e)
        {
            ILR.DPOutcome dpo = _learnerDP.CreateOutcome();
            if (LearnerOutcomeItemsCV == null)
            {

                LearnerOutcomeItemsCV = CollectionViewSource.GetDefaultView(_learnerDP.DPOutcomeList);
                LearnerOutcomeItemsCV.CurrentChanged += LearnerOutcomeItemsCV_CurrentChanged;
            }
            else
            {
                LearnerOutcomeItemsCV = CollectionViewSource.GetDefaultView(_learnerDP.DPOutcomeList);
            }
            LearnerOutcomeItemsCV.MoveCurrentTo(dpo);
            foreach (ILR.DPOutcome xDp in LearnerOutcomeItemsCV)
            {
                xDp.IsSelected = false;
            }
            SetSubControl(LearnerOutcomeItemsCV.CurrentItem as ILR.DPOutcome);
            if (LearnerOutcomeItemsCV.CurrentItem != dpo)
            {

                if (!dpo.IsSelected)
                {
                    dpo.IsSelected = true;
                }
            }
            ShowHidChildControls();
            LearnerOutcomeItemsCV.Refresh();
            OnPropertyChanged("CurrentItem");
            OnPropertyChanged("LearnerOutcomeItemsCV");
        }
        private void RemoveOutcome(object sender, RoutedEventArgs e)
        {
            if (LearnerOutcomeItemsCV.CurrentItem != null)
            {
                ILR.DPOutcome ld2Remove = LearnerOutcomeItemsCV.CurrentItem as ILR.DPOutcome;
                if (ld2Remove != null)
                {
                    _learnerDP.Delete(ld2Remove);
                    if (!LearnerOutcomeItemsCV.IsEmpty)
                    {
                        if (!LearnerOutcomeItemsCV.MoveCurrentToPrevious())
                        {
                            LearnerOutcomeItemsCV.MoveCurrentToFirst();
                            LearnerOutcomeItemsCV.Refresh();
                            OnPropertyChanged("LearnerOutcomeItemsCV");
                        }
                        if ((LearnerOutcomeItemsCV.CurrentItem != null) && (LearnerOutcomeItemsCV.CurrentItem != ld2Remove))
                        {
                            ILR.DPOutcome lr = LearnerOutcomeItemsCV.CurrentItem as ILR.DPOutcome;
                            foreach (ILR.DPOutcome xDp in LearnerOutcomeItemsCV)
                            {
                                xDp.IsSelected = false;
                            }
                            lr.IsSelected = true;
                        }
                        else
                        {
                            LearnerOutcomeItemsCV.MoveCurrentToNext();
                            if (LearnerOutcomeItemsCV.CurrentItem != null)
                            {
                                ILR.DPOutcome lr = LearnerOutcomeItemsCV.CurrentItem as ILR.DPOutcome;
                                lr.IsSelected = true;
                            }
                        }
                    }
                }
                LearnerOutcomeItemsCV.Refresh();
                OnPropertyChanged("LearnerOutcomeItemsCV");
                ShowHidChildControls();
            }
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
                    case "ULN":
                        if (String.IsNullOrEmpty(ULN.ToString()))
                            sReturn = String.Format("{0} required.", columnName);

                        if (!string.IsNullOrEmpty(ULN))
                        {
                            sReturn += CheckPropertyLength(ULN, CLASSNAME, columnName);
                            sReturn += NumericValidations.CheckInt64ValidValue(ULN, columnName);
                        }
                        break;

                    default:
                        break;
                }
                return sReturn;
            }
        }

        #endregion

        private void ValidateToNumeric(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
