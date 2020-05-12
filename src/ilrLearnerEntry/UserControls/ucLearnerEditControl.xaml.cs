using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using ILR;

namespace ilrLearnerEntry.UserControls
{
    /// <summary>
    /// Interaction logic for ucLearnerEditControl.xaml
    /// </summary>
    public partial class ucLearnerEditControl : BaseUserControl, INotifyPropertyChanged
    {


        #region Private Variables
        private String _learnerfilterString = string.Empty;
        private const string FILTER_TEXT_PROMPT = "Enter value to search for";
        private String _filterText = String.Empty;

        #endregion

        #region Constructor
        public ucLearnerEditControl()
        {
            InitializeComponent();
            LearnerDetailGrid.Visibility = System.Windows.Visibility.Hidden;
            this.DataContext = this;
            SetupLookups();
        }
        #endregion

        #region Public Properties
        public ICollectionView LearnerItemsCV
        {
            get;
            private set;
        }
        private bool LearnerFilter(object item)
        {
            Learner learner = item as Learner;
            if (!string.IsNullOrEmpty(_learnerfilterString))
            {
                bool bReturn = false;
                if ((learner.FamilyName != null) && (learner.FamilyName.ToString().ToLower().Contains(_learnerfilterString.ToLower()))) { bReturn = true; }
                if ((learner.GivenNames != null) && (learner.GivenNames.ToString().ToLower().Contains(_learnerfilterString.ToLower()))) { bReturn = true; }
                if ((learner.ULN != null) && (learner.ULN.ToString().ToLower().Contains(_learnerfilterString.ToLower()))) { bReturn = true; }
                if ((learner.LearnRefNumber != null) && (learner.LearnRefNumber.ToString().ToLower().Contains(_learnerfilterString.ToLower()))) { bReturn = true; }
                if ((learner.PostCode != null) && (learner.PostCode.ToString().ToLower().Contains(_learnerfilterString.ToLower()))) { bReturn = true; }
                return bReturn;
            }
            else
            {
                return true;
            }
        }
        public string LearnerFilterString
        {
            get { return _learnerfilterString; }
            set
            {
                _learnerfilterString = value;
                OnPropertyChanged("LearnerFilterString");
                if (LearnerItemsCV != null)
                {
                    LearnerItemsCV.Refresh();
                    OnPropertyChanged("LearnerItemsCV");
                }
            }
        }

        #endregion

        #region Private Properties
        #endregion

        #region Public Methods
        public void UpdateChildControlAsNewDataLoaded()
        {
            SetupListData();

            if (App.ILRMessage.LearnerList.Count > 0)
            {
                LearnerItemsCV.MoveCurrentToFirst();
                OnPropertyChanged("LearnerItemsCV");
            }

            SelectFirstLearnerWhenNewDocumentUploaded();
        }

        public string FilterValue
        {
            get { return _filterText; }
            set
            {

                if (_filterText != value)
                {
                    _filterText = value;
                    OnPropertyChanged("FilterValue");
                }
            }
        }
        #endregion

        #region Private Methods
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtFilter.Text == "Enter value to search for")
            {
                txtFilter.Text = "";
            }
        }
        private void SetupListData()
        {
            LearnerItemsCV = null;
            if (App.ILRMessage.LearnerList.Count > 0)
            {

                LearnerItemsCV = CollectionViewSource.GetDefaultView(App.ILRMessage.LearnerList);
                using (LearnerItemsCV.DeferRefresh())
                {
                    LearnerItemsCV.SortDescriptions.Clear();
                    LearnerItemsCV.SortDescriptions.Add(new SortDescription("IsComplete", ListSortDirection.Ascending));
                    LearnerItemsCV.SortDescriptions.Add(new SortDescription("LearnRefNumber", ListSortDirection.Ascending));
                    LearnerItemsCV.Filter = LearnerFilter;
                }
            }
            else
            {
                LearnerItemsCV = CollectionViewSource.GetDefaultView(App.ILRMessage.LearnerList);
                if (LearnerItemsCV != null)
                {
                    LearnerItemsCV.Refresh();
                }
                LearnerDetailGrid.Visibility = System.Windows.Visibility.Hidden;
                LearnerItemsCV = null;
            }
        }
        private void SetSubControl(Learner learner)
        {
            if (LearnerItemsCV.CurrentItem != null)
            {
                LearnerDetailGrid.Visibility = System.Windows.Visibility.Visible;

                LearnerHeaderControl.CurrentItem = learner;
                LearnerDetailControl.CurrentItem = learner;
                LearnerContacePreferencesControl.CurrentItem = learner;
                LLDDAndLearningSupportControl.CurrentItem = learner;
                ProviderSpecifiedLearningMonitorInformationControl.CurrentItem = learner;
                EmploymentStatusListControl.CurrentItem = learner;
                LearningDeliveryListControl.CurrentItem = learner;
                LearnerHEInformationControl.CurrentItem = learner;
                LearnerFAMsControl.CurrentItem = learner;
            }
            else
            {
                LearnerHeaderControl.CurrentItem = null;
                LearnerDetailControl.CurrentItem = null;
                LearnerDetailControl.CurrentItem = null;
                LLDDAndLearningSupportControl.CurrentItem = null;
                ProviderSpecifiedLearningMonitorInformationControl.CurrentItem = null;
                EmploymentStatusListControl.CurrentItem = null;
                LearningDeliveryListControl.CurrentItem = null;
                LearnerFAMsControl.CurrentItem = null;

                LearnerDetailGrid.Visibility = System.Windows.Visibility.Hidden;
            }
        }
        private void DataItemListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (App.ILRMessage.LearnerList.Count > 0)
            {
                if (e.AddedItems.Count > 0)
                {
                    Learner lr = e.AddedItems[0] as Learner;
                    LearnerItemsCV.MoveCurrentTo(lr);
                    lr.IsSelected = true;
                    SetSubControl(lr);
                }
                LearnerDetailGrid.Visibility = System.Windows.Visibility.Visible;
            }
        }
        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((DataItemListBox != null) && (txtFilter.Text != null) && (this.LearnerFilterString != txtFilter.Text))
            {
                this.LearnerFilterString = txtFilter.Text;
            }
        }
        private void AddLearner_Click(object sender, RoutedEventArgs e)
        {
            //if (LearnerItemsCV != null && LearnerItemsCV.CurrentItem != null)
            //{                
            //    foreach (Learner learner in LearnerItemsCV)
            //    {
            //        if (!string.IsNullOrWhiteSpace(learner.IncompleteMessage))
            //        {
            //            MessageBox.Show($"Please complete all mandatory fields for learner {learner.LearnRefNumber} before proceeding"
            //                                                   , "Cannot proceed"
            //                                                   , MessageBoxButton.OK
            //                                                   , MessageBoxImage.Error
            //                                                   , MessageBoxResult.OK);
            //            return;
            //        }
            //    }                
            //}
            Learner newLr = App.ILRMessage.CreateLearner();
            App.ILRMessage.Save();

            if (App.ILRMessage.LearnerList.Count == 1)
            {
                SetupListData();
            }

            foreach (Learner l in LearnerItemsCV)
            {
                //if (l != newLr)
                l.IsSelected = false;
            }

            newLr.IsSelected = true;
            DataItemListBox.SelectedItem = newLr;
            //LearnerItemsCV.MoveCurrentTo(newLr);
            LearnerItemsCV.Refresh();
            OnPropertyChanged("LearnerItemsCV");
            newLr.RefreshData();

        }
        private void RemoveLearner_Click(object sender, RoutedEventArgs e)
        {
            if (LearnerItemsCV.CurrentItem != null)
            {
                Learner lr2Remove = LearnerItemsCV.CurrentItem as Learner;
                if (lr2Remove != null)
                {
                    MessageBoxResult result = MessageBox.Show(String.Format("Are you sure you want to delete Learner {0} {0} Learner Ref - {3} {0} Name : {1} {2}", Environment.NewLine, lr2Remove.GivenNames, lr2Remove.FamilyName, lr2Remove.LearnRefNumber)
                                                            , "Confirmation"
                                                            , MessageBoxButton.YesNo
                                                            , MessageBoxImage.Stop
                                                            , MessageBoxResult.No);
                    if (result == MessageBoxResult.Yes)
                    {
                        App.ILRMessage.Delete(lr2Remove);
                        App.ILRMessage.Save();
                        LearnerItemsCV.Refresh();
                        if (App.ILRMessage.LearnerList.Count > 0)
                        {
                            if (!LearnerItemsCV.MoveCurrentToPrevious())
                            {
                                LearnerItemsCV.MoveCurrentToFirst();
                                LearnerItemsCV.Refresh();
                                OnPropertyChanged("LearnerItemsCV");
                            }
                            if ((LearnerItemsCV.CurrentItem != null) && (LearnerItemsCV.CurrentItem != lr2Remove))
                            {
                                Learner lr = LearnerItemsCV.CurrentItem as Learner;
                                lr.IsSelected = true;

                            }
                            else
                            {
                                LearnerItemsCV.MoveCurrentToNext();
                                if (LearnerItemsCV.CurrentItem != null)
                                {
                                    Learner lr = LearnerItemsCV.CurrentItem as Learner;
                                    lr.IsSelected = true;

                                }
                            }
                            LearnerItemsCV.Refresh();
                            OnPropertyChanged("LearnerItemsCV");
                        }
                        else
                        {
                            SetupListData();
                        }
                    }
                }
            }
        }
        private void SetupLookups()
        {
            LearnerHeaderControl.GenderList =ILR.Lookup.GetLookup("Sex");
            LearnerDetailControl.EthnicityList =ILR.Lookup.GetLookup("Ethnicity");
            LearnerDetailControl.ECFList =ILR.Lookup.GetLookup("LearnerFAM", "ECF");
            LearnerDetailControl.MCFList =ILR.Lookup.GetLookup("LearnerFAM", "MCF");
            LearnerDetailControl.RestrictedIndList =ILR.Lookup.GetLookup("ContactPreference", "RUI");
            LearnerFAMsControl.PriorAttainmentList =ILR.Lookup.GetLookup("PriorAttain");
            LearnerFAMsControl.FMEList =ILR.Lookup.GetLookup("LearnerFAM", "FME");
            LearnerFAMsControl.NLMList =ILR.Lookup.GetLookup("LearnerFAM", "NLM");
            LLDDAndLearningSupportControl.LLDDHealthProblemList =ILR.Lookup.GetLookup("LLDDHealthProb");
            LLDDAndLearningSupportControl.LSRList =ILR.Lookup.GetLookup("LearnerFAM", "LSR");
            LLDDAndLearningSupportControl.LLDDItemListControl.LLDDCatlList =ILR.Lookup.GetLookup("LLDDCat");
            LearningDeliveryListControl.LearningDeliveryItemControl.LearningStartInformationControl.AimTypeList =ILR.Lookup.GetLookup("AimType");
            LearningDeliveryListControl.LearningDeliveryItemControl.LearningStartInformationControl.FundModelList =ILR.Lookup.GetLookup("FundModel");
            LearningDeliveryListControl.LearningDeliveryItemControl.LearningStartInformationControl.ProgTypeList =ILR.Lookup.GetLookup("ProgType");
            LearningDeliveryListControl.LearningDeliveryItemControl.LearningStartInformationControl.HHSList =ILR.Lookup.GetLookup("LearningDeliveryFAM", "HHS");
            LearningDeliveryListControl.LearningDeliveryItemControl.LearningEndInformationControl.CompStatusList =ILR.Lookup.GetLookup("CompStatus");
            LearningDeliveryListControl.LearningDeliveryItemControl.LearningEndInformationControl.OutcomeList =ILR.Lookup.GetLookup("Outcome");
            LearningDeliveryListControl.LearningDeliveryItemControl.LearningEndInformationControl.WithdrawReasonList =ILR.Lookup.GetLookup("WithdrawReason");
            LearningDeliveryListControl.LearningDeliveryItemControl.LearningEndInformationControl.EmpOutcomeList =ILR.Lookup.GetLookup("EmpOutcome");
            LearningDeliveryListControl.LearningDeliveryItemControl.HEControl.SOC2000List =ILR.Lookup.GetLookup("SOC2000");
            LearningDeliveryListControl.LearningDeliveryItemControl.HEControl.EconomicList =ILR.Lookup.GetLookup("SEC");
            LearningDeliveryListControl.LearningDeliveryItemControl.HEControl.ELQList =ILR.Lookup.GetLookup("ELQ");
            LearningDeliveryListControl.LearningDeliveryItemControl.HEControl.MSTUFEEList =ILR.Lookup.GetLookup("MSTuFee");
            LearningDeliveryListControl.LearningDeliveryItemControl.HEControl.SPECFEEList =ILR.Lookup.GetLookup("SpecFee");
            LearningDeliveryListControl.LearningDeliveryItemControl.HEControl.MODESTUDList =ILR.Lookup.GetLookup("ModeStud");
            LearningDeliveryListControl.LearningDeliveryItemControl.HEControl.FUNDLEVList =ILR.Lookup.GetLookup("FundLev");
            LearningDeliveryListControl.LearningDeliveryItemControl.HEControl.FUNDCOMPList =ILR.Lookup.GetLookup("FundComp");
            LearningDeliveryListControl.LearningDeliveryItemControl.HEControl.QUALENT3List =ILR.Lookup.GetLookup("QualEnt3");
            LearningDeliveryListControl.LearningDeliveryItemControl.HEControl.TYPEYRList =ILR.Lookup.GetLookup("TypeYr");
            LearningDeliveryListControl.LearningDeliveryItemControl.LearnerFundingAndMonitoringControl.SourceOfFundingList =ILR.Lookup.GetLookup("LearningDeliveryFAM", "SOF");
            LearningDeliveryListControl.LearningDeliveryItemControl.LearnerFundingAndMonitoringControl.FullOrCoFundedList =ILR.Lookup.GetLookup("LearningDeliveryFAM", "FFI");
            LearningDeliveryListControl.LearningDeliveryItemControl.LearnerFundingAndMonitoringControl.NatioanSkillAgencyList =ILR.Lookup.GetLookup("LearningDeliveryFAM", "NSA");
            LearningDeliveryListControl.LearningDeliveryItemControl.LearnerFundingAndMonitoringControl.EligibitiytAppFundingList =ILR.Lookup.GetLookup("LearningDeliveryFAM", "EEF");
            LearningDeliveryListControl.LearningDeliveryItemControl.LearnerFundingAndMonitoringControl.SpecialProjectList =ILR.Lookup.GetLookup("LearningDeliveryFAM", "SPP");
            LearningDeliveryListControl.LearningDeliveryItemControl.LearnerFundingAndMonitoringControl.ASLList =ILR.Lookup.GetLookup("LearningDeliveryFAM", "ASL");
            LearningDeliveryListControl.LearningDeliveryItemControl.LearnerFundingAndMonitoringControl.PODList =ILR.Lookup.GetLookup("LearningDeliveryFAM", "POD");
            //LearningDeliveryListControl.LearningDeliveryItemControl.TrailblazersListControl.TrlblazItemControl.TBFinTypeList =ILR.Lookup.GetLookup("TBFinType");
            //LearningDeliveryListControl.LearningDeliveryItemControl.TrailblazersListControl.TrlblazItemControl.TBFinCodetList =ILR.Lookup.GetLookup("TrailblazerApprenticeshipFinancialRecord", "PMR");

            LearningDeliveryListControl.LearningDeliveryItemControl.FinancialDetailsListControl.FinTypeList =ILR.Lookup.GetLookup("AppFinRecord");
            //LearningDeliveryListControl.LearningDeliveryItemControl.FinancialDetailsListControl.FinCodetList_TNP =ILR.Lookup.GetLookup("ApprenticeshipFinancialRecord", "TNP");
            //LearningDeliveryListControl.LearningDeliveryItemControl.FinancialDetailsListControl.FinCodetList_PMR =ILR.Lookup.GetLookup("ApprenticeshipFinancialRecord", "PMR");

            LearningDeliveryListControl.LearningDeliveryItemControl.WorkplaceListControls.WorkPlacementItemControl.WorkplacementCodeList =ILR.Lookup.GetLookup("WorkPlaceMode");
            EmploymentStatusListControl.EmpStausItemControl.EmpStatList =ILR.Lookup.GetLookup("EmpStat");
            EmploymentStatusListControl.EmpStausItemControl.LengthOfUnemploymentList =ILR.Lookup.GetLookup("EmploymentStatusMonitoring", "LOU");
            EmploymentStatusListControl.EmpStausItemControl.BenifitStatusIndicatorList =ILR.Lookup.GetLookup("EmploymentStatusMonitoring", "BSI");
            EmploymentStatusListControl.EmpStausItemControl.EmploymentIntensityIndicatorList =ILR.Lookup.GetLookup("EmploymentStatusMonitoring", "EII");
            EmploymentStatusListControl.EmpStausItemControl.LengthOfEmploymentList =ILR.Lookup.GetLookup("EmploymentStatusMonitoring", "LOE");
            LearnerHEInformationControl.TermTimeAccList =ILR.Lookup.GetLookup("TTAccom");
        }
        private void SaveLearner_Click(object sender, RoutedEventArgs e)
        {
            App.ILRMessage.Save();
        }
        private void txtFilter_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtFilter.Text == FILTER_TEXT_PROMPT)
            {
                FilterValue = string.Empty;
            }
        }

        private void SelectFirstLearnerWhenNewDocumentUploaded()
        {
            if (App.ILRMessage.LearnerList.Count > 0)
            {
                DataItemListBox.SelectedIndex = 0;
            }
            else
            {
                SetupListData();
            }

        }

        #endregion
    }
}
