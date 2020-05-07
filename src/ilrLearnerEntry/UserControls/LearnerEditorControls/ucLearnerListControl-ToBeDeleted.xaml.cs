using System.ComponentModel;
using ilrLearnerEntry.UserControls;

namespace ilrLearnerEntry.REMOVED.UserControls.LearnerEditorControls
{
    /// <summary>
    /// Interaction logic for ucLearnerListControl.xaml
    /// </summary>
    public partial class ucLearnerListControl : BaseUserControl, INotifyPropertyChanged
    {
        #region Constructor
        public ucLearnerListControl()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        #endregion
    }
}
