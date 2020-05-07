using System;
using System.ComponentModel;
using System.Data;

namespace ilrLearnerEntry.UserControls.LearnerEditorControls.DPOutcomeControls
{
    /// <summary>
    /// Interaction logic for ucDPO_OutcomeDetail.xaml
    /// </summary>
    public partial class ucDPO_OutcomeDetail : BaseUserControl, INotifyPropertyChanged
    {
        #region Private Variables
        private ILR.DPOutcome _outcome;
        private DataTable _dt;
        private string _outCode;

        #endregion

        #region Constructor
        public ucDPO_OutcomeDetail()
        {
            InitializeComponent();
        }
        #endregion

        #region Public Properties

        public ILR.DPOutcome CurrentItem
        {
            get { return _outcome; }
            set
            {
                if (value != null)
                {
                    this.DataContext = this;
                    if (_outcome != value)
                    {
                        _outcome = value;
                        OutCode = value.OutCode.HasValue ? value.OutCode.ToString() : "";
                    }
                }
                else
                {
                    this.DataContext = null;
                }
                OnPropertyChanged("OutcomeTypeList");
                OnPropertyChanged("CurrentItem");
            }
        }

        public string OutCode
        {
            get { return _outCode; }
            set
            {
                _outCode = value;
                if (String.IsNullOrEmpty(value))
                {
                    CurrentItem.OutCode = null;
                }
                else
                {
                    bool result = Int32.TryParse(System.Convert.ToString(value), out var number);
                    if (result)
                    {
                        CurrentItem.OutCode = number;
                    }
                }
                OnPropertyChanged("OutCode");
            }
        }

        public DataTable OutcomeTypeList
        {
            set
            {
                _dt = value;
                OnPropertyChanged("OutcomeTypeList");
            }
            get { return _dt; }
        }
        #endregion

        #region Public Methods
        #endregion

        #region PRIVATE Methods
        #endregion
    }
}

