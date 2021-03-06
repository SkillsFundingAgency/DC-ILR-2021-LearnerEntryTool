﻿using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using ILR;

namespace ilrLearnerEntry.UserControls.LearnerEditorControls.LearnerControls
{
    /// <summary>
    /// Interaction logic for ucLearnerInformation.xaml
    /// </summary>
    public partial class ucLearnerInformation : BaseUserControl, INotifyPropertyChanged
    {
        #region Private Variables
        private Learner _learner;
        #endregion

        #region Constructor
        public ucLearnerInformation()
        {
            InitializeComponent();
        }
        #endregion

        #region Public Properties
        public Learner CurrentItem
        {
            get { return _learner; }
            set
            {
                if (value != null)
                {
                    _learner = value;
                    this.DataContext = this;
                    //OnPropertyChanged("AddLine1");
                    //OnPropertyChanged("AddLine2");
                    //OnPropertyChanged("AddLine3");
                    //OnPropertyChanged("AddLine4");
                    //OnPropertyChanged("PostCode");
                    //OnPropertyChanged("Telephone");
                    //OnPropertyChanged("Email");
                    OnPropertyChanged("RestrictedIndList");
                    OnPropertyChanged("EthnicityList");
                    OnPropertyChanged("PriorAttainmentList");
                    OnPropertyChanged("CurrentItem");
                }
                else
                {
                    this.DataContext = null;
                }
            }
        }

        public DataTable RestrictedIndList { set; get; }
        public DataTable EthnicityList { set; get; }
        public DataTable ECFList { set; get; }
        public DataTable MCFList { set; get; }
        public DataTable PriorAttainmentList { set; get; }
     

        #endregion

        #region Public Methods       
        #endregion

        #region PRIVATE Methods      
        #endregion

        private void UserControl_RequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
        {
            OnPropertyChanged("CurrentItem");
            CurrentItem.RefreshData();
        }

    }
}
