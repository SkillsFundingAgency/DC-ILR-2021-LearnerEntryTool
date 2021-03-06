﻿using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ILR;
using ilrLearnerEntry.UserControls.Validations;

namespace ilrLearnerEntry.UserControls.LearnerEditorControls.LearnerControls
{
    /// <summary>
    /// Interaction logic for ucLLDDAndLearningSupport.xaml
    /// </summary>
    public partial class ucLLDDAndLearningSupport : BaseUserControl, INotifyPropertyChanged, IDataErrorInfo
    {
        #region Private Variables
        private const String CLASSNAME = "Learner";
        private Learner _learner;
        private const Int32 _maxLRSItem = 4;
        private DataTable _dt;
        private string _alscost = string.Empty;
        private string _priorlearnfundadj = string.Empty;
        #endregion

        #region Constructor
        public ucLLDDAndLearningSupport()
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
                _alscost = string.Empty;
                if (value != null)
                {
                    // LLDDItemListControl.CurrentItem = null;

                    _learner = value;
                    _alscost = value.ALSCost;

                    lv_LSR.SelectionChanged -= lv_LSR_SelectionChanged;
                    this.DataContext = this;
                    ClearAllLRSSelected();
                    LLDDItemListControl.CurrentItem = _learner;

                    // Need to finish of the Selection on reload ?
                    foreach (int? lrsCode in _learner.LSR)
                    {
                        SetLRSAsSelected(lrsCode.ToString());
                    }
                    lv_LSR.SelectionChanged += lv_LSR_SelectionChanged;
                    OnPropertyChanged("ALSCost");
                    OnPropertyChanged("LLDDTypeListDS");
                    OnPropertyChanged("LLDDTypeListLD");
                    OnPropertyChanged("LLDDHealthProblemList");
                    OnPropertyChanged("LSRList");
                    OnPropertyChanged("CurrentItem");
                    //OnPropertyChanged("LLDDHealthProb");					
                    //OnPropertyChanged("LLDDHealthProb");
                }
                else
                {
                    _learner = null;
                    lv_LSR.SelectionChanged -= lv_LSR_SelectionChanged;
                    ClearAllLRSSelected();
                    OnPropertyChanged("LSRList");
                    this.DataContext = null;
                }
            }
        }
        public string ALSCost
        {
            get { return _alscost; }
            set
            {
                _alscost = value;
                CurrentItem.ALSCost = value;
                //int number;
                //bool result = Int32.TryParse(System.Convert.ToString(value), out number);
                //if (result)
                //{

                //}
            }
        }
        public int? LLDDHealthProb
        {
            get { return CurrentItem.LLDDHealthProb; }
            set
            {
                if (value != 1)
                {
                    if (CurrentItem.LLDDandHealthProblemList != null)
                    { CurrentItem.LLDDandHealthProblemList.Clear(); }
                }
                CurrentItem.LLDDHealthProb = value;
                OnPropertyChanged("LLDDHealthProb");
                LLDDItemListControl.CurrentItem = this.CurrentItem;
                OnPropertyChanged("LLDDHealthProblemList");
            }
        }
        public DataTable LSRList
        {
            get { return _dt; }
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
                _dt = tmpDT;
                _dt.Columns.Add(new DataColumn("IsSelected", typeof(bool)));
            }
        }
        public DataTable LLDDTypeListDS { set; get; }
        public DataTable LLDDTypeListLD { set; get; }
        public DataTable LLDDHealthProblemList { set; get; }
        #endregion

        #region Public Methods
        #endregion

        #region PRIVATE Methods
        private void lv_LSR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (DataRowView x in e.AddedItems)
            {
                LearnerFAM tmp = _learner.LearnerFAMList.Where(f => f.LearnFAMType == "LSR" && f.LearnFAMCode == int.Parse(x[0].ToString())).FirstOrDefault();
                if (tmp == null)
                {
                    if (_learner.LSR.Count < _maxLRSItem)
                    {
                        _learner.AddFAM(LearnerFAM.MultiOccurrenceFAMs.LSR, int.Parse(x[0].ToString()));
                    }
                    else
                    {
                        MessageBox.Show(String.Format("   You may only select {0} items.", _maxLRSItem.ToString())
                                                               , "Max number of selectable items reached."
                                                               , MessageBoxButton.OK
                                                               , MessageBoxImage.Information
                                                               , MessageBoxResult.OK);

                        x["IsSelected"] = Convert.ToBoolean(false);
                        OnPropertyChanged("LSRList");
                    }
                }
            }
            foreach (DataRowView x in e.RemovedItems)
            {
                foreach (int fam in _learner.LSR)
                {
                    if (fam.ToString() == x[0].ToString())
                    {
                        _learner.RemoveFAM(LearnerFAM.MultiOccurrenceFAMs.LSR, int.Parse(x[0].ToString()));
                        break;
                    }
                }
            }
        }
        private void SetLRSAsSelected(string Filter)
        {
            if (_dt != null)
            {
                foreach (DataRow dr in _dt.Rows)
                {
                    if (dr["Code"].ToString() == Filter)
                    {
                        dr["IsSelected"] = Convert.ToBoolean(true);
                        break;
                    }

                }
            }
        }
        private void ClearAllLRSSelected()
        {
            if (_dt != null)
            {
                foreach (DataRow dr in _dt.Rows)
                {
                    dr["IsSelected"] = Convert.ToBoolean(false);
                }
                OnPropertyChanged("LSRList");
            }
        }
        #endregion

        #region IDataErrorInfo Members
        public string this[string columnName]
        {
            get
            {
                string sReturn = null;
                if (_learner != null)
                {
                    switch (columnName)
                    {
                        case "LLDDHealthProb":
                            if ((CurrentItem.LLDDHealthProb == null) || ((CurrentItem.LLDDHealthProb != null && CurrentItem.LLDDHealthProb.ToString().Length == 0)))
                            {
                                return "LLDDHealthProb - requiredss\r\n";
                            }
                            break;
                        //                 case "LLDDHealthProblemList":
                        //if (CurrentItem.LLDDHealthProb == null)
                        //{
                        //                         sReturn = "LLDDHealthProb nothing selected - required\r\n";
                        //}
                        //break;

                        //case "LLDDHealthProbTest1":
                        //	if ((CurrentItem.LLDDHealthProb == null) 
                        //		|| (
                        //			(CurrentItem.LLDDHealthProb != null) && (CurrentItem.LLDDHealthProb == 1)
                        //			&& ((CurrentItem.LLDDandHealthProblemList == null) || (CurrentItem.LLDDandHealthProblemList != null && CurrentItem.LLDDandHealthProblemList.Count < 1))
                        //		   )
                        //		)
                        //	{
                        //		return "LLDDandHealthProblem Not Selected- required\r\n";
                        //	}
                        //	break;
                        case "ALSCost":
                            if (!string.IsNullOrEmpty(ALSCost))
                            {
                                sReturn += CheckPropertyLength(ALSCost, CLASSNAME, columnName);
                                sReturn += NumericValidations.CheckInt32ValidValue(ALSCost, columnName);
                            }
                            break;
                        default:
                            break;
                    }
                }
                return sReturn;
            }
        }
        #endregion


        private void UserControl_RequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
        {
            if (CurrentItem != null)
            {
                //OnPropertyChanged("CurrentItem");
                CurrentItem.RefreshData();
            }
        }

    }
}