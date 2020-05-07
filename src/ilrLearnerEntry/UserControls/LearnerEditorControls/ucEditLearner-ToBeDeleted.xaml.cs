using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Controls;
using ilrLearnerEntry.UserControls;

//using ILR;

namespace ilrLearnerEntry.REMOVED.UserControls.LearnerEditorControls
{
    /// <summary>
    /// Interaction logic for ucEditor.xaml
    /// </summary>
    public partial class ucEditLearner : BaseUserControl, INotifyPropertyChanged
    {
        #region Constructor
        public ucEditLearner()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        #endregion
    }
}
