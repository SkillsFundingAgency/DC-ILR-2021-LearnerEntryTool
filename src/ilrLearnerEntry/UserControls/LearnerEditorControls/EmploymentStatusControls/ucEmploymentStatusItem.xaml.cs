using System.ComponentModel;
using System.Data;
using ILR;

namespace ilrLearnerEntry.UserControls.EmploymentStatus
{
    /// <summary>
    /// Interaction logic for ucEmploymentStatusItem.xaml
    /// </summary>
    public partial class ucEmploymentStatusItem : BaseUserControl, INotifyPropertyChanged
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
    }
}
