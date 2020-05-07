using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Controls;

namespace ilrLearnerEntry.UserControls.LearnerEditorControls.LearningDelControls
{
    /// <summary>
    /// Interaction logic for ucProviderSpecifiedDeliveryMonitorInformation.xaml
    /// </summary>
    public partial class ucProviderSpecifiedDeliveryMonitorInformation : BaseUserControl, INotifyPropertyChanged
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
    }
}
