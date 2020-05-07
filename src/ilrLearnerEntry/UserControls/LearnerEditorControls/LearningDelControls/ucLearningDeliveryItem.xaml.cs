using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Controls;

namespace ilrLearnerEntry.UserControls.LearnerEditorControls.LearningDelControls
{
    /// <summary>
    /// Interaction logic for ucLearningDeliveryItem.xaml
    /// </summary>
    public partial class ucLearningDeliveryItem : BaseUserControl, INotifyPropertyChanged
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
    }
}
