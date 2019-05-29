using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml;
using System.Data;
using System.Text.RegularExpressions;
using Utils;

namespace ILR
{
    public class LearningDelivery : ChildEntity, IDataErrorInfo
    {
        private const String CLASSNAME = "LearningDelivery";
        private const String TABS = "\t\t";
        DateTime FIRST_AUG_2013 = new DateTime(2013, 8, 1);
        DateTime FIRST_AUG_2015 = new DateTime(2015, 8, 1);
        DateTime FIRST_AUG_2016 = new DateTime(2016, 8, 1);
        DateTime FIRST_AUG_2017 = new DateTime(2017, 8, 1);
        DateTime FIRST_AUG_2018 = new DateTime(2018, 8, 1);
        DateTime FIRST_AUG_2019 = new DateTime(2019, 8, 1);

        private const string PHOURS_PATTERN = "^[0-9]{1,4}$";

        #region LD updateLearnerEvent

        public event PropertyChangedEventHandler LearningDeliveryPropertyChanged;

        /// <summary>
        /// Fires the event for the property when it changes.
        /// </summary>
        public void OnLearningDeliveryPropertyChanged()
        {
            if (!this.IsImportRunning)
                LearningDeliveryPropertyChanged(this, new PropertyChangedEventArgs("From LD record"));
        }

        #endregion

        #region Accessors

        public Boolean IsImportRunning { get; set; }

        public override bool IsComplete
        {
            get
            {
                if (IncompleteMessage == null || IncompleteMessage == String.Empty)
                    return true;
                else
                    return false;
            }
        }

        public override string IncompleteMessage
        {
            get
            {
                string message = "";
                message += this["LearnAimRef"];
                message += this["AimType"];
                message += this["AimSeqNumber"];
                message += this["LearnStartDate"];
                message += this["LearnPlanEndDate"];
                message += this["FundModel"];
                message += this["CompStatus"];
                message += this["DelLocPostCode"];
                message += this["LSDPostCode"];

                message += this["StdCode"];
                message += this["FworkCode"];


                LearningDeliveryWorkPlacementList.ForEach(wp => message += wp.IncompleteMessage);
                return message;
            }
        }

        //private bool CanMigrateDueToCompletionStatusSix => CompStatus == 6 || CompStatus == 1;

        private bool CanMigrateDueToCompletionStatus
        {
            get
            {
                bool canMigrate = false;

                switch (CompStatus)
                {
                    case 1:
                        canMigrate = (LearnActEndDate == null && LearnPlanEndDate >= FIRST_AUG_2016);
                        break;
                    case 6:
                        canMigrate = true;
                        break;
                }

                return canMigrate;
            }
        }

        private bool CanMigrateDueToUnknownOutcome
        {
            get
            {
                var canMigrate = false;
                if (Outcome != null)
                {
                    canMigrate = Outcome == 8;
                }

                return canMigrate;
            }
        }


        private bool CanMigrateDuetoESFFunding
        {

            get { return this.FundModel == 70 && this.LearnStartDate >= FIRST_AUG_2015; }
        }

        public bool ShouldProbablyMigrate
        {
            get
            {
                //if completion status is "" that means learner has taken break and aim should be migrated
                if (CanMigrateDueToCompletionStatus) //migration rule 8
                {
                    return true;
                }
                else if (CanMigrateDueToUnknownOutcome) //migration rule 10
                {
                    return true;
                }
                else if (CanMigrateDuetoESFFunding) //migration rule 11
                {
                    return true;

                }
                else //all existing migration rules
                {

                    switch (this.AimType)
                    {
                        case 1:
                        case 4:
                            return ((this.FundModel != 70 && this.LearnPlanEndDate >= FIRST_AUG_2019 &&
                                     this.LearnActEndDate == null) || (this.FundModel == 70));
                        case 5:
                            if (this.FundModel == 70)
                                return true;
                            else
                            {
                                if ((this.LearnPlanEndDate >= FIRST_AUG_2019) &&
                                    ((this.LearnActEndDate == null) || (this.Outcome == 8 || this.Outcome == 6)))
                                    return this.LearnActEndDate == null ||
                                           (this.Outcome == 4 || this.Outcome == 5 || this.Outcome == 6 ||
                                            this.Outcome == 8);
                                else
                                    return false;
                            }

                        case 3:
                            return this.LearnPlanEndDate >= FIRST_AUG_2013 && this.LearnActEndDate == null;
                    }
                }

                return false;
            }
        }

        #endregion

        Nullable<int> _defaultStdCode;

        #region ILR Properties

        /// <summary>
        /// LearnAimRef
        /// </summary>
        public string LearnAimRef
        {
            get
            {
                return XMLHelper.GetChildValue("LearnAimRef", Node, NSMgr);
            }
            set
            {
                XMLHelper.SetChildValue("LearnAimRef", value, Node, NSMgr);
                OnLearningDeliveryPropertyChanged();
                OnPropertyChanged("LearnAimRef");
            }
        }

        /// <summary>
        /// AimType
        /// </summary>
        public int? AimType
        {
            get
            {
                string AimType = XMLHelper.GetChildValue("AimType", Node, NSMgr);
                return (AimType != null ? Int32.Parse(AimType) : (int?) null);
            }
            set
            {
                XMLHelper.SetChildValue("AimType", value, Node, NSMgr);
                OnLearningDeliveryPropertyChanged();
                OnPropertyChanged("AimType");
            }
        }

        /// <summary>
        /// AimSeqNumber
        /// </summary>
        public int? AimSeqNumber
        {
            get
            {
                string AimSeqNumber = XMLHelper.GetChildValue("AimSeqNumber", Node, NSMgr);
                return (AimSeqNumber != null ? Int32.Parse(AimSeqNumber) : (int?) null);
            }
            set
            {
                XMLHelper.SetChildValue("AimSeqNumber", value, Node, NSMgr);
                OnLearningDeliveryPropertyChanged();
                OnPropertyChanged("AimSeqNumber");
            }
        }

        /// <summary>
        /// LearnStartDate
        /// </summary>
        public DateTime? LearnStartDate
        {
            get
            {
                string LearnStartDate = XMLHelper.GetChildValue("LearnStartDate", Node, NSMgr);
                return (LearnStartDate != null ? DateTime.Parse(LearnStartDate) : (DateTime?) null);
            }
            set
            {
                XMLHelper.SetChildValue("LearnStartDate", value, Node, NSMgr);
                OnLearningDeliveryPropertyChanged();
                OnPropertyChanged("LearnStartDate");
            }
        }

        /// <summary>
        /// OrigLearnStartDate
        /// </summary>
        public DateTime? OrigLearnStartDate
        {
            get
            {
                string OrigLearnStartDate = XMLHelper.GetChildValue("OrigLearnStartDate", Node, NSMgr);
                return (OrigLearnStartDate != null ? DateTime.Parse(OrigLearnStartDate) : (DateTime?) null);
            }
            set
            {
                XMLHelper.SetChildValue("OrigLearnStartDate", value, Node, NSMgr);
                OnLearningDeliveryPropertyChanged();
                OnPropertyChanged("OrigLearnStartDate");
            }
        }

        /// <summary>
        /// LearnPlanEndDate
        /// </summary>
        public DateTime? LearnPlanEndDate
        {
            get
            {
                string LearnPlanEndDate = XMLHelper.GetChildValue("LearnPlanEndDate", Node, NSMgr);
                return (LearnPlanEndDate != null ? DateTime.Parse(LearnPlanEndDate) : (DateTime?) null);
            }
            set
            {
                XMLHelper.SetChildValue("LearnPlanEndDate", value, Node, NSMgr);
                OnLearningDeliveryPropertyChanged();
                OnPropertyChanged("LearnPlanEndDate");
            }
        }

        /// <summary>
        /// FundModel
        /// </summary>
        public int? FundModel
        {
            get
            {
                string FundModel = XMLHelper.GetChildValue("FundModel", Node, NSMgr);
                return (FundModel != null ? Int32.Parse(FundModel) : (int?) null);
            }
            set
            {
                XMLHelper.SetChildValue("FundModel", value, Node, NSMgr);
                OnLearningDeliveryPropertyChanged();
                OnPropertyChanged("FundModel");
            }
        }

        /// <summary>
        /// PHours - added for 1920
        /// Code template copied from the FundModel property
        /// however the int.Parse() function could throw an exception so at some point this may need to be looked at if the form is allowing non-numeric entries
        /// </summary>
        public int? PHours
        {
            get
            {
                string PHours = XMLHelper.GetChildValue("PHours", Node, NSMgr);
                return (PHours != null ? Int32.Parse(PHours) : (int?)null);
            }
            set
            {
                XMLHelper.SetChildValue("PHours", value, Node, NSMgr);
                OnLearningDeliveryPropertyChanged();
                OnPropertyChanged("PHours");
            }
        }

        /// <summary>
        /// ProgType
        /// </summary>
        public int? ProgType
        {
            get
            {
                string ProgType = XMLHelper.GetChildValue("ProgType", Node, NSMgr);
                return (ProgType != null ? Int32.Parse(ProgType) : (int?) null);
            }
            set
            {
                XMLHelper.SetChildValue("ProgType", value, Node, NSMgr);
                OnLearningDeliveryPropertyChanged();
                OnPropertyChanged("ProgType");
            }
        }

        /// <summary>
        /// FworkCode
        /// </summary>
        public string FworkCode
        {
            get { return XMLHelper.GetChildValue("FworkCode", Node, NSMgr); }
            set
            {
                XMLHelper.SetChildValue("FworkCode", value, Node, NSMgr);
                OnLearningDeliveryPropertyChanged();
                OnPropertyChanged("FworkCode");
            }
        }

        /// <summary>
        /// PwayCode
        /// </summary>
        public int? PwayCode
        {
            get
            {
                string PwayCode = XMLHelper.GetChildValue("PwayCode", Node, NSMgr);
                return (PwayCode != null ? Int32.Parse(PwayCode) : (int?) null);
            }
            set
            {
                XMLHelper.SetChildValue("PwayCode", value, Node, NSMgr);
                OnLearningDeliveryPropertyChanged();
                OnPropertyChanged("PwayCode");
            }
        }

        /// <summary>
        /// StdCode
        /// </summary>
        public string StdCode
        {
            get { return XMLHelper.GetChildValue("StdCode", Node, NSMgr); }
            set
            {
                XMLHelper.SetChildValue("StdCode", value, Node, NSMgr);
                OnLearningDeliveryPropertyChanged();
                OnPropertyChanged("StdCode");
            }
        }

        /// <summary>
        /// PartnerUKPRN
        /// </summary>
        public int? PartnerUKPRN
        {
            get
            {
                string PartnerUKPRN = XMLHelper.GetChildValue("PartnerUKPRN", Node, NSMgr);
                return (PartnerUKPRN != null ? Int32.Parse(PartnerUKPRN) : (int?) null);
            }
            set
            {
                XMLHelper.SetChildValue("PartnerUKPRN", value, Node, NSMgr);
                OnLearningDeliveryPropertyChanged();
                OnPropertyChanged("PartnerUKPRN");
            }
        }

        /// <summary>
        /// DellLocPostCode
        /// </summary>
        public string DelLocPostCode
        {
            get { return XMLHelper.GetChildValue("DelLocPostCode", Node, NSMgr); }
            set
            {
                XMLHelper.SetChildValue("DelLocPostCode", value, Node, NSMgr);
                OnLearningDeliveryPropertyChanged();
                OnPropertyChanged("DelLocPostCode");
            }
        }

        /// <summary>
        /// LSDPostCode - added for 1920
        /// </summary>
        public string LSDPostCode
        {
            get { return XMLHelper.GetChildValue("LSDPostCode", Node, NSMgr); }
            set
            {
                XMLHelper.SetChildValue("LSDPostCode", value, Node, NSMgr);
                OnLearningDeliveryPropertyChanged();
                OnPropertyChanged("LSDPostCode");
            }
        }

        /// <summary>
        /// AddHours
        /// </summary>
        public int? AddHours
        {
            get
            {
                string AddHours = XMLHelper.GetChildValue("AddHours", Node, NSMgr);
                return (AddHours != null ? Int32.Parse(AddHours) : (int?) null);
            }
            set
            {
                XMLHelper.SetChildValue("AddHours", value, Node, NSMgr);
                OnLearningDeliveryPropertyChanged();
                OnPropertyChanged("AddHours");
            }
        }

        /// <summary>
        /// PriorLearnFundAdj
        /// </summary>
        public int? PriorLearnFundAdj
        {
            get
            {
                string PriorLearnFundAdj = XMLHelper.GetChildValue("PriorLearnFundAdj", Node, NSMgr);
                return (PriorLearnFundAdj != null ? Int32.Parse(PriorLearnFundAdj) : (int?) null);
            }
            set
            {
                XMLHelper.SetChildValue("PriorLearnFundAdj", value, Node, NSMgr);
                OnLearningDeliveryPropertyChanged();
                OnPropertyChanged("PriorLearnFundAdj");
            }
        }

        public int? OtherFundAdj
        {
            get
            {
                string OtherFundAdj = XMLHelper.GetChildValue("OtherFundAdj", Node, NSMgr);
                return (OtherFundAdj != null ? Int32.Parse(OtherFundAdj) : (int?) null);
            }
            set
            {
                XMLHelper.SetChildValue("OtherFundAdj", value, Node, NSMgr);
                OnLearningDeliveryPropertyChanged();
                OnPropertyChanged("OtherFundAdj");
            }
        }

        public string ConRefNumber
        {
            get { return XMLHelper.GetChildValue("ConRefNumber", Node, NSMgr); }
            set
            {
                XMLHelper.SetChildValue("ConRefNumber", value, Node, NSMgr);
                OnLearningDeliveryPropertyChanged();
                OnPropertyChanged("ConRefNumber");
            }
        }

        public string EPAOrgID
        {
            get { return XMLHelper.GetChildValue("EPAOrgID", Node, NSMgr); }
            set
            {
                XMLHelper.SetChildValue("EPAOrgID", value, Node, NSMgr);
                OnLearningDeliveryPropertyChanged();
                OnPropertyChanged("EPAOrgID");
            }
        }

        public int? EmpOutcome
        {
            get
            {
                string EmpOutcome = XMLHelper.GetChildValue("EmpOutcome", Node, NSMgr);
                return (EmpOutcome != null ? Int32.Parse(EmpOutcome) : (int?) null);
            }
            set
            {
                XMLHelper.SetChildValue("EmpOutcome", value, Node, NSMgr);
                OnLearningDeliveryPropertyChanged();
                OnPropertyChanged("EmpOutcome");
            }
        }

        public int? CompStatus
        {
            get
            {
                string CompStatus = XMLHelper.GetChildValue("CompStatus", Node, NSMgr);
                return (CompStatus != null ? Int32.Parse(CompStatus) : (int?) null);
            }
            set
            {
                XMLHelper.SetChildValue("CompStatus", value, Node, NSMgr);
                OnLearningDeliveryPropertyChanged();
                OnPropertyChanged("CompStatus");
            }
        }

        public DateTime? LearnActEndDate
        {
            get
            {
                string LearnActEndDate = XMLHelper.GetChildValue("LearnActEndDate", Node, NSMgr);
                return (LearnActEndDate != null ? DateTime.Parse(LearnActEndDate) : (DateTime?) null);
            }
            set
            {
                XMLHelper.SetChildValue("LearnActEndDate", value, Node, NSMgr);
                OnLearningDeliveryPropertyChanged();
                OnPropertyChanged("LearnActEndDate");
            }
        }

        public int? WithdrawReason
        {
            get
            {
                string WithdrawReason = XMLHelper.GetChildValue("WithdrawReason", Node, NSMgr);
                return (WithdrawReason != null ? Int32.Parse(WithdrawReason) : (int?) null);
            }
            set
            {
                XMLHelper.SetChildValue("WithdrawReason", value, Node, NSMgr);
                OnLearningDeliveryPropertyChanged();
                OnPropertyChanged("WithdrawReason");
            }
        }

        public int? Outcome
        {
            get
            {
                string Outcome = XMLHelper.GetChildValue("Outcome", Node, NSMgr);
                return (Outcome != null ? Int32.Parse(Outcome) : (int?) null);
            }
            set
            {
                XMLHelper.SetChildValue("Outcome", value, Node, NSMgr);
                OnLearningDeliveryPropertyChanged();
                OnPropertyChanged("Outcome");
            }
        }

        public DateTime? AchDate
        {
            get
            {
                string AchDate = XMLHelper.GetChildValue("AchDate", Node, NSMgr);
                return (AchDate != null ? DateTime.Parse(AchDate) : (DateTime?) null);
            }
            set
            {
                XMLHelper.SetChildValue("AchDate", value, Node, NSMgr);
                OnLearningDeliveryPropertyChanged();
                OnPropertyChanged("AchDate");
            }
        }

        public string OutGrade
        {
            get
            {
                return XMLHelper.GetChildValue("OutGrade", Node, NSMgr);
            }
            set
            {
                XMLHelper.SetChildValue("OutGrade", value, Node, NSMgr);
                OnLearningDeliveryPropertyChanged();
                OnPropertyChanged("OutGrade");
            }
        }

        public string SWSupAimId
        {
            get
            {
                return getSwSuppAimId();
            }
            set
            {
                XMLHelper.SetChildValue("SWSupAimId", value, Node, NSMgr);
                OnLearningDeliveryPropertyChanged();
                OnPropertyChanged("SWSupAimId");
            }
        }


        private string getSwSuppAimId()
        {

            var suppId = XMLHelper.GetChildValue("SWSupAimId", Node, NSMgr);
            if (String.IsNullOrEmpty(suppId))
            {
                var swSuppAimId = Guid.NewGuid();
                SWSupAimId = swSuppAimId.ToString();
                suppId = swSuppAimId.ToString();
            }

            return suppId;
        }


        #endregion

        #region Lookup Properties

        public string AimTypeDescription
        {
            get
            {
                Lookup lookup = new Lookup();
                return lookup.GetDescription("AimType", this.AimType.ToString());
            }
        }

        #endregion

        #region Renormalised Child Entities

        #region LearningDeliveryFAM

        public bool RES
        {
            get { return GetFAMCode(LearningDeliveryFAM.SingleOccurrenceFAMs.RES) == "1"; }
            set
            {
                if (value)
                    SetFAM(LearningDeliveryFAM.SingleOccurrenceFAMs.RES, "1");
                else
                    RemoveFAM(LearningDeliveryFAM.SingleOccurrenceFAMs.RES);
                OnPropertyChanged("RES");
            }
        }

        public bool ADL
        {
            get { return GetFAMCode(LearningDeliveryFAM.SingleOccurrenceFAMs.ADL) == "1"; }
            set
            {
                if (value)
                    SetFAM(LearningDeliveryFAM.SingleOccurrenceFAMs.ADL, "1");
                else
                    RemoveFAM(LearningDeliveryFAM.SingleOccurrenceFAMs.ADL);
                OnPropertyChanged("ADL");
            }
        }

        public bool WPP
        {
            get { return GetFAMCode(LearningDeliveryFAM.SingleOccurrenceFAMs.WPP) == "1"; }
            set
            {
                if (value)
                    SetFAM(LearningDeliveryFAM.SingleOccurrenceFAMs.WPP, "1");
                else
                    RemoveFAM(LearningDeliveryFAM.SingleOccurrenceFAMs.WPP);
                OnPropertyChanged("WPP");
            }
        }

        public bool FLN
        {
            get { return GetFAMCode(LearningDeliveryFAM.SingleOccurrenceFAMs.FLN) == "1"; }
            set
            {
                if (value)
                    SetFAM(LearningDeliveryFAM.SingleOccurrenceFAMs.FLN, "1");
                else
                    RemoveFAM(LearningDeliveryFAM.SingleOccurrenceFAMs.FLN);
                OnPropertyChanged("FLN");
            }
        }

        public string SOF
        {
            get { return GetFAMCode(LearningDeliveryFAM.SingleOccurrenceFAMs.SOF); }
            set
            {
                if (value != null)
                    SetFAM(LearningDeliveryFAM.SingleOccurrenceFAMs.SOF, value);
                else
                    RemoveFAM(LearningDeliveryFAM.SingleOccurrenceFAMs.SOF);
                OnPropertyChanged("SOF");
            }
        }

        public string FFI
        {
            get { return GetFAMCode(LearningDeliveryFAM.SingleOccurrenceFAMs.FFI); }
            set
            {
                if (value != null)
                    SetFAM(LearningDeliveryFAM.SingleOccurrenceFAMs.FFI, value);
                else
                    RemoveFAM(LearningDeliveryFAM.SingleOccurrenceFAMs.FFI);
                OnPropertyChanged("FFI");
            }
        }

        public string EEF
        {
            get { return GetFAMCode(LearningDeliveryFAM.SingleOccurrenceFAMs.EEF); }
            set
            {
                if (value != null)
                    SetFAM(LearningDeliveryFAM.SingleOccurrenceFAMs.EEF, value);
                else
                    RemoveFAM(LearningDeliveryFAM.SingleOccurrenceFAMs.EEF);
                OnPropertyChanged("EEF");
            }
        }

        public string ASL
        {
            get { return GetFAMCode(LearningDeliveryFAM.SingleOccurrenceFAMs.ASL); }
            set
            {
                if (value != null)
                    SetFAM(LearningDeliveryFAM.SingleOccurrenceFAMs.ASL, value);
                else
                    RemoveFAM(LearningDeliveryFAM.SingleOccurrenceFAMs.ASL);
                OnPropertyChanged("ASL");
            }
        }

        //public string SPP
        //{
        //    get
        //    {
        //        return GetFAMCode(LearningDeliveryFAM.SingleOccurrenceFAMs.SPP);
        //    }
        //    set
        //    {
        //        if (value != null)
        //            SetFAM(LearningDeliveryFAM.SingleOccurrenceFAMs.SPP, value);
        //        else
        //            RemoveFAM(LearningDeliveryFAM.SingleOccurrenceFAMs.SPP);
        //        OnPropertyChanged("SPP");
        //    }
        //}
        public string NSA
        {
            get { return GetFAMCode(LearningDeliveryFAM.SingleOccurrenceFAMs.NSA); }
            set
            {
                if (value != null)
                    SetFAM(LearningDeliveryFAM.SingleOccurrenceFAMs.NSA, value);
                else
                    RemoveFAM(LearningDeliveryFAM.SingleOccurrenceFAMs.NSA);
                OnPropertyChanged("NSA");
            }
        }

        public string POD
        {
            get { return GetFAMCode(LearningDeliveryFAM.SingleOccurrenceFAMs.POD); }
            set
            {
                if (value != null)
                    SetFAM(LearningDeliveryFAM.SingleOccurrenceFAMs.POD, value);
                else
                    RemoveFAM(LearningDeliveryFAM.SingleOccurrenceFAMs.POD);
                OnPropertyChanged("POD");
            }
        }

        public List<string> DAM
        {
            get
            {
                List<string> DAMList = new List<string>(4);

                foreach(LearningDeliveryFAM fam in this.LearningDeliveryFAMList.FindAll(x => x.LearnDelFAMType == "DAM"))
                {
                    if (!String.IsNullOrEmpty(fam.LearnDelFAMCode))
                    {
                        DAMList.Add(fam.LearnDelFAMCode);
                    }
                }

                return DAMList;
            }
            set
            {
                ClearFAMList(LearningDeliveryFAM.MultiOccurrenceFAMs.DAM);
                int i = 1;
                foreach (String code in value)
                {
                    if (!String.IsNullOrEmpty(code))
                    {
                        AddFAM(LearningDeliveryFAM.MultiOccurrenceFAMs.DAM, code);
                        i++;
                    }

                    if (i > 4)
                    {
                        break;
                    }
                }

                OnPropertyChanged("DAM");
            }
        }

        public List<LearningDeliveryFAM> HEM
        {
            get { return this.LearningDeliveryFAMList.FindAll(x => x.LearnDelFAMType == "HEM"); }
            set
            {
                ClearFAMList(LearningDeliveryFAM.MultiOccurrenceFAMs.HEM);
                foreach (LearningDeliveryFAM fam in value)
                    AddFAM(LearningDeliveryFAM.MultiOccurrenceFAMs.HEM, fam.LearnDelFAMCode);
                OnPropertyChanged("HEM");
            }
        }

        public List<LearningDeliveryFAM> LDM2
        {
            get { return this.LearningDeliveryFAMList.FindAll(x => x.LearnDelFAMType == "LDM"); }
            set
            {
                ClearFAMList(LearningDeliveryFAM.MultiOccurrenceFAMs.LDM);
                foreach (LearningDeliveryFAM fam in value)
                    AddFAM(LearningDeliveryFAM.MultiOccurrenceFAMs.LDM, fam.LearnDelFAMCode);
                OnPropertyChanged("LDM");
            }
        }

        public List<string> HEM2
        {
            get
            {
                List<string> LDMList = new List<string>(0);

                foreach (LearningDeliveryFAM fam in this.LearningDeliveryFAMList.FindAll(
                    x => x.LearnDelFAMType == "HEM"))
                {
                    if (!String.IsNullOrEmpty(fam.LearnDelFAMCode))
                    {
                        LDMList.Add(fam.LearnDelFAMCode);
                    }
                }

                return LDMList;
            }
            set
            {
                ClearFAMList(LearningDeliveryFAM.MultiOccurrenceFAMs.HEM);
                Int16 i = 1;
                foreach (String code in value)
                {
                    if (!String.IsNullOrEmpty(code))
                    {
                        AddFAM(LearningDeliveryFAM.MultiOccurrenceFAMs.HEM, code);
                        i++;
                    }

                    if (i > 3)
                    {
                        break;
                    }
                }

                OnPropertyChanged("HEM");
            }
        }

        public List<string> LDM
        {
            get
            {
                List<string> LDMList = new List<string>(0);

                foreach (LearningDeliveryFAM fam in this.LearningDeliveryFAMList.FindAll(x => x.LearnDelFAMType == "LDM"))
                {
                    if (!String.IsNullOrEmpty(fam.LearnDelFAMCode))
                    {
                        LDMList.Add(fam.LearnDelFAMCode);
                    }
                }

                return LDMList;
            }
            set
            {
                ClearFAMList(LearningDeliveryFAM.MultiOccurrenceFAMs.LDM);
                int i = 1;
                foreach (String code in value)
                {
                    if (!String.IsNullOrEmpty(code))
                    {
                        AddFAM(LearningDeliveryFAM.MultiOccurrenceFAMs.LDM, code);
                        i++;
                    }

                    if (i > 6)
                    {
                        break;
                    }
                }

                OnPropertyChanged("LDM");
            }
        }

        public List<LearningDeliveryFAM> HHS
        {
            get { return this.LearningDeliveryFAMList.FindAll(x => x.LearnDelFAMType == "HHS"); }
            set
            {
                ClearFAMList(LearningDeliveryFAM.MultiOccurrenceFAMs.HHS);
                foreach (LearningDeliveryFAM fam in value)
                    AddFAM(LearningDeliveryFAM.MultiOccurrenceFAMs.HHS, fam.LearnDelFAMCode);
                OnPropertyChanged("HHS");
            }
        }

        public List<LearningDeliveryFAM> ALB
        {
            get { return this.LearningDeliveryFAMList.FindAll(x => x.LearnDelFAMType == "ALB"); }
            set
            {
                ClearFAMList(LearningDeliveryFAM.MultiOccurrenceFAMs.ALB);
                foreach (LearningDeliveryFAM fam in value)
                    AddFAM(LearningDeliveryFAM.MultiOccurrenceFAMs.ALB, fam.LearnDelFAMCode);
                OnPropertyChanged("ALB");
            }
        }

        public List<LearningDeliveryFAM> LSF
        {
            get { return this.LearningDeliveryFAMList.FindAll(x => x.LearnDelFAMType == "LSF"); }
            set
            {
                ClearFAMList(LearningDeliveryFAM.MultiOccurrenceFAMs.LSF);
                foreach (LearningDeliveryFAM fam in value)
                    AddFAM(LearningDeliveryFAM.MultiOccurrenceFAMs.LSF, fam.LearnDelFAMCode);
                OnPropertyChanged("LSF");
            }
        }

        public List<LearningDeliveryFAM> ACT
        {
            get { return this.LearningDeliveryFAMList.FindAll(x => x.LearnDelFAMType == "ACT"); }
            set
            {
                ClearFAMList(LearningDeliveryFAM.MultiOccurrenceFAMs.ACT);
                foreach (LearningDeliveryFAM fam in value)
                    AddFAM(LearningDeliveryFAM.MultiOccurrenceFAMs.ACT, fam.LearnDelFAMCode);
                OnPropertyChanged("ACT");
            }
        }
        //public string ALB1
        //{
        //    get
        //    {
        //        return GetFAMCode(LearningDeliveryFAM.DatedFAMs.ALB);
        //    }
        //    set
        //    {
        //        if (value != null)
        //            SetFAM(LearningDeliveryFAM.DatedFAMs.ALB, value);
        //        else
        //            RemoveFAM(LearningDeliveryFAM.DatedFAMs.ALB);
        //        OnPropertyChanged("ALB");
        //    }
        //}

        //public DateTime? ALBFrom
        //{
        //    get
        //    {
        //        return GetFAMFrom(LearningDeliveryFAM.DatedFAMs.ALB);
        //    }
        //    set
        //    {
        //        SetFAMFrom(LearningDeliveryFAM.DatedFAMs.ALB, value);
        //        OnPropertyChanged("ALBFrom");
        //    }
        //}
        //public DateTime? ALBTo
        //{
        //    get
        //    {
        //        return GetFAMTo(LearningDeliveryFAM.DatedFAMs.ALB);
        //    }
        //    set
        //    {
        //        SetFAMTo(LearningDeliveryFAM.DatedFAMs.ALB, value);
        //        OnPropertyChanged("ALBTo");
        //    }
        //}
        //public bool? LSF1
        //{
        //    get
        //    {
        //        return GetFAMCode(LearningDeliveryFAM.DatedFAMs.LSF) == "1";
        //    }
        //    set
        //    {
        //        if (value == true)
        //            SetFAM(LearningDeliveryFAM.DatedFAMs.LSF, "1");
        //        else
        //            RemoveFAM(LearningDeliveryFAM.DatedFAMs.LSF);
        //        OnPropertyChanged("LSF");
        //    }
        //}
        //public DateTime? LSFFrom
        //{
        //    get
        //    {
        //        return GetFAMFrom(LearningDeliveryFAM.DatedFAMs.LSF);
        //    }
        //    set
        //    {
        //        SetFAMFrom(LearningDeliveryFAM.DatedFAMs.LSF, value);
        //        OnPropertyChanged("LSFFrom");
        //    }
        //}
        //public DateTime? LSFTo
        //{
        //    get
        //    {
        //        return GetFAMTo(LearningDeliveryFAM.DatedFAMs.LSF);
        //    }
        //    set
        //    {
        //        SetFAMTo(LearningDeliveryFAM.DatedFAMs.LSF, value);
        //        OnPropertyChanged("LSFTo");
        //    }
        //}

        //public bool HHS_OneOrMoreApply
        //{
        //    get
        //    {
        //        return !HHS_NoneApply && !HHS_WontSay;
        //    }
        //    set
        //    {

        //    }
        //}
        //public bool HHS_OneOrMoreApply
        //{
        //    get
        //    {
        //        return !HHS_NoneApply && !HHS_WontSay;
        //    }
        //    set
        //    {
        //    }
        //}
        //public bool HHS_OneOrMoreApply_NoMemberIsEmployed
        //{
        //    get
        //    {
        //        List<string> hhs = GetFAMList(LearningDeliveryFAM.MultiOccurrenceFAMs.HHS);
        //        return hhs.Contains("2") || hhs.Contains("1");
        //    }
        //    set
        //    {
        //        if (value)
        //        {
        //            ClearFAMList(LearningDeliveryFAM.MultiOccurrenceFAMs.HHS);
        //            AddFAM(LearningDeliveryFAM.MultiOccurrenceFAMs.HHS, "2");
        //        }
        //        else
        //            RemoveFAM(LearningDeliveryFAM.MultiOccurrenceFAMs.HHS, "2");
        //    }
        //}
        //public bool HHS_OneOrMoreApply_OneAdult
        //{
        //    get
        //    {
        //        List<string> hhs = GetFAMList(LearningDeliveryFAM.MultiOccurrenceFAMs.HHS);
        //        return hhs.Contains("2")||hhs.Contains("1");
        //    }
        //    set
        //    {
        //        if (value)
        //        {
        //            ClearFAMList(LearningDeliveryFAM.MultiOccurrenceFAMs.HHS);
        //            AddFAM(LearningDeliveryFAM.MultiOccurrenceFAMs.HHS, "2");
        //        }
        //        else
        //            RemoveFAM(LearningDeliveryFAM.MultiOccurrenceFAMs.HHS, "2");
        //    }
        //}
        //public bool HHS_OneOrMoreApply_Kids
        //{
        //    get
        //    {
        //        List<string> hhs = GetFAMList(LearningDeliveryFAM.MultiOccurrenceFAMs.HHS);
        //        return hhs.Contains("3")||hhs.Contains("1");
        //    }
        //    set
        //    {
        //        if (value)
        //        {
        //            ClearFAMList(LearningDeliveryFAM.MultiOccurrenceFAMs.HHS);
        //            AddFAM(LearningDeliveryFAM.MultiOccurrenceFAMs.HHS, "3");
        //        }
        //        else
        //            RemoveFAM(LearningDeliveryFAM.MultiOccurrenceFAMs.HHS, "3");
        //    }
        //}


        public bool HHS_NoneApply
        {
            get { return GetFAMList(LearningDeliveryFAM.MultiOccurrenceFAMs.HHS).Contains("99"); }
            set
            {
                if (value)
                {
                    ClearFAMList(LearningDeliveryFAM.MultiOccurrenceFAMs.HHS);
                    AddFAM(LearningDeliveryFAM.MultiOccurrenceFAMs.HHS, "99");
                }
                else
                    RemoveFAM(LearningDeliveryFAM.MultiOccurrenceFAMs.HHS, "99");
            }
        }

        public bool HHS_WontSay
        {
            get { return GetFAMList(LearningDeliveryFAM.MultiOccurrenceFAMs.HHS).Contains("98"); }
            set
            {
                if (value)
                {
                    ClearFAMList(LearningDeliveryFAM.MultiOccurrenceFAMs.HHS);
                    AddFAM(LearningDeliveryFAM.MultiOccurrenceFAMs.HHS, "98");
                }
                else
                    RemoveFAM(LearningDeliveryFAM.MultiOccurrenceFAMs.HHS, "98");
            }
        }

        #endregion

        #region ProviderSpecDeliveryMonitoring

        public string ProvSpecMonA
        {
            get { return GetProvSpecMonValue(ProviderSpecDeliveryMonitoring.Occurrence.A); }
            set
            {
                SetProvSpecMon(ProviderSpecDeliveryMonitoring.Occurrence.A, value);
                OnPropertyChanged("ProvSpecMonA");
            }
        }

        public string ProvSpecMonB
        {
            get { return GetProvSpecMonValue(ProviderSpecDeliveryMonitoring.Occurrence.B); }
            set
            {
                SetProvSpecMon(ProviderSpecDeliveryMonitoring.Occurrence.B, value);
                OnPropertyChanged("ProvSpecMonB");
            }
        }

        public string ProvSpecMonC
        {
            get { return GetProvSpecMonValue(ProviderSpecDeliveryMonitoring.Occurrence.C); }
            set
            {
                SetProvSpecMon(ProviderSpecDeliveryMonitoring.Occurrence.C, value);
                OnPropertyChanged("ProvSpecMonC");
            }
        }

        public string ProvSpecMonD
        {
            get { return GetProvSpecMonValue(ProviderSpecDeliveryMonitoring.Occurrence.D); }
            set
            {
                SetProvSpecMon(ProviderSpecDeliveryMonitoring.Occurrence.D, value);
                OnPropertyChanged("ProvSpecMonD");
            }
        }

        #endregion

        #endregion

        #region FAM Lookups

        public bool HasFAM(string FAMType, string FAMCode)
        {
            var fams = from LearningDeliveryFAM ldFAM in this.LearningDeliveryFAMList
                where ldFAM.LearnDelFAMType == FAMType && ldFAM.LearnDelFAMCode == FAMCode
                select ldFAM;
            return fams.Count() > 0;
        }

        public bool HasFAMType(string FAMType)
        {
            return this.LearningDeliveryFAMList.Exists(f => f.LearnDelFAMType == FAMType);
        }

        #endregion

        #region ILR Child Entites

        public List<LearningDeliveryFAM> LearningDeliveryFAMList = new List<LearningDeliveryFAM>();

        public List<ApprenticeshipFinancialRecord> ApprenticeshipFinancialRecordList =
            new List<ApprenticeshipFinancialRecord>();

        public List<ProviderSpecDeliveryMonitoring> ProviderSpecDeliveryMonitoringList =
            new List<ProviderSpecDeliveryMonitoring>();

        public LearningDeliveryHE LearningDeliveryHE;

        public List<LearningDeliveryWorkPlacement> LearningDeliveryWorkPlacementList =
            new List<LearningDeliveryWorkPlacement>();

        #endregion

        #region Child Entity Creation

        public LearningDeliveryFAM CreateLearningDeliveryFAM()
        {
            XmlNode newNode = Node.OwnerDocument.CreateElement("LearningDeliveryFAM", NSMgr.LookupNamespace("ia"));
            LearningDeliveryFAM newInstance = new LearningDeliveryFAM(newNode, NSMgr);
            LearningDeliveryFAMList.Add(newInstance);
            AppendToLastOfNodeNamed(newNode, newNode.Name);
            return newInstance;
        }

        public ApprenticeshipFinancialRecord CreateApprenticeshipFinancialRecord()
        {
            XmlNode newNode = Node.OwnerDocument.CreateElement("AppFinRecord", NSMgr.LookupNamespace("ia"));
            ApprenticeshipFinancialRecord newInstance = new ApprenticeshipFinancialRecord(newNode, NSMgr);
            ApprenticeshipFinancialRecordList.Add(newInstance);
            AppendToLastOfNodeNamed(newNode, newNode.Name);
            return newInstance;
        }

        public ProviderSpecDeliveryMonitoring CreateProviderSpecDeliveryMonitoring()
        {
            XmlNode newNode =
                Node.OwnerDocument.CreateElement("ProviderSpecDeliveryMonitoring", NSMgr.LookupNamespace("ia"));
            ProviderSpecDeliveryMonitoring newInstance = new ProviderSpecDeliveryMonitoring(newNode, NSMgr);
            ProviderSpecDeliveryMonitoringList.Add(newInstance);
            AppendToLastOfNodeNamed(newNode, newNode.Name);
            return newInstance;
        }

        public LearningDeliveryHE CreateLearningDeliveryHE()
        {
            XmlNode newNode = Node.OwnerDocument.CreateElement("LearningDeliveryHE", NSMgr.LookupNamespace("ia"));
            LearningDeliveryHE = new LearningDeliveryHE(newNode, NSMgr);
            AppendToLastOfNodeNamed(newNode, newNode.Name);
            return LearningDeliveryHE;
        }

        public LearningDeliveryWorkPlacement CreateLearningDeliveryWorkPlacement()
        {
            XmlNode newNode =
                Node.OwnerDocument.CreateElement("LearningDeliveryWorkPlacement", NSMgr.LookupNamespace("ia"));
            LearningDeliveryWorkPlacement newInstance = new LearningDeliveryWorkPlacement(newNode, NSMgr);
            newInstance.LearningDeliveryWPPropertyChanged += NewInstance_LearningDeliveryWPPropertyChanged;
            LearningDeliveryWorkPlacementList.Add(newInstance);
            AppendToLastOfNodeNamed(newNode, newNode.Name);
            OnLearningDeliveryPropertyChanged();
            return newInstance;
        }

        private void NewInstance_LearningDeliveryWPPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnLearningDeliveryPropertyChanged();
        }

        private void AppendToLastOfNodeNamed(XmlNode NewNode, string NodeName)
        {
            switch (NodeName)
            {
                case "LearningDeliveryFAM":
                    if (LearningDeliveryWorkPlacementList.Count() == 0)
                        AppendToLastOfNodeNamed(NewNode, "LearningDeliveryWorkPlacement");
                    else
                        Node.InsertBefore(NewNode, LearningDeliveryWorkPlacementList.First().Node);
                    break;
                case "LearningDeliveryWorkPlacement":
                    if (ApprenticeshipFinancialRecordList.Count() == 0)
                        AppendToLastOfNodeNamed(NewNode, "AppFinRecord");
                    else
                        Node.InsertBefore(NewNode, ApprenticeshipFinancialRecordList.First().Node);
                    break;
                case "AppFinRecord":
                    if (ProviderSpecDeliveryMonitoringList.Count() == 0)
                        AppendToLastOfNodeNamed(NewNode, "ProviderSpecDeliveryMonitoring");
                    else
                        Node.InsertBefore(NewNode, ProviderSpecDeliveryMonitoringList.First().Node);
                    break;
                case "ProviderSpecDeliveryMonitoring":
                    if (LearningDeliveryHE == null)
                        AppendToLastOfNodeNamed(NewNode, "LearningDeliveryHE");
                    else
                        Node.InsertBefore(NewNode, LearningDeliveryHE.Node);
                    break;
                case "LearningDeliveryHE":
                    Node.AppendChild(NewNode);
                    break;
            }
        }

        #endregion

        #region Constructors

        internal LearningDelivery(XmlNode Node, XmlNamespaceManager NSMgr)
        {
            this.IsImportRunning = true;
            this.Node = Node;
            this.NSMgr = NSMgr;

            XmlNodeList nodes = Node.SelectNodes("./ia:LearningDeliveryFAM", NSMgr);
            foreach (XmlNode node in nodes)
                LearningDeliveryFAMList.Add(new LearningDeliveryFAM(node, NSMgr));

            nodes = Node.SelectNodes("./ia:AppFinRecord", NSMgr);
            foreach (XmlNode node in nodes)
                ApprenticeshipFinancialRecordList.Add(new ApprenticeshipFinancialRecord(node, NSMgr));

            nodes = Node.SelectNodes("./ia:ProviderSpecDeliveryMonitoring", NSMgr);
            foreach (XmlNode node in nodes)
                ProviderSpecDeliveryMonitoringList.Add(new ProviderSpecDeliveryMonitoring(node, NSMgr));

            XmlNode learningDeliveryHENode = Node.SelectSingleNode("./ia:LearningDeliveryHE", NSMgr);
            if (learningDeliveryHENode != null)
                LearningDeliveryHE = new LearningDeliveryHE(learningDeliveryHENode, NSMgr);
            //else
            //LearningDeliveryHE = this.CreateLearningDeliveryHE();

            nodes = Node.SelectNodes("./ia:LearningDeliveryWorkPlacement", NSMgr);
            foreach (XmlNode node in nodes)
            {
                var newInstance = new LearningDeliveryWorkPlacement(node, NSMgr);
                newInstance.LearningDeliveryWPPropertyChanged += NewInstance_LearningDeliveryWPPropertyChanged;
                LearningDeliveryWorkPlacementList.Add(newInstance);
                OnLearningDeliveryPropertyChanged();
            }

            OnPropertyChanged("CompStatus");
            this.SWSupAimId = getSwSuppAimId();
            this.IsImportRunning = false;

        }

        internal LearningDelivery(LearningDelivery MigrationLearningDelivery, XmlNode Node, XmlNamespaceManager NSMgr)
        {
            IsImportRunning = true;
            this.Node = Node;
            this.NSMgr = NSMgr;

            this.LearnAimRef = MigrationLearningDelivery.LearnAimRef;
            this.AimType = MigrationLearningDelivery.AimType;

            this.AimSeqNumber = MigrationLearningDelivery.AimSeqNumber;

            this.LearnStartDate = MigrationLearningDelivery.LearnStartDate;
            this.OrigLearnStartDate = MigrationLearningDelivery.OrigLearnStartDate;
            this.LearnPlanEndDate = MigrationLearningDelivery.LearnPlanEndDate;
            this.FundModel = MigrationLearningDelivery.FundModel;
            if (MigrationLearningDelivery.ProgType != 10)
                this.ProgType = MigrationLearningDelivery.ProgType;
            this.FworkCode = MigrationLearningDelivery.FworkCode;
            this.PwayCode = MigrationLearningDelivery.PwayCode;

            int stdCode;
            if (MigrationLearningDelivery.HasFAMType("TBS") &&
                Int32.TryParse(MigrationLearningDelivery.GetLegacyFAM("TBS").LearnDelFAMCode, out stdCode))
                this.StdCode = stdCode.ToString();
            else
                this.StdCode = MigrationLearningDelivery.StdCode;

            this.PartnerUKPRN = MigrationLearningDelivery.PartnerUKPRN;
            this.EPAOrgID = MigrationLearningDelivery.EPAOrgID;
            this.AddHours = MigrationLearningDelivery.AddHours;
            this.DelLocPostCode = MigrationLearningDelivery.DelLocPostCode;
            this.ConRefNumber = MigrationLearningDelivery.ConRefNumber;
            this.PriorLearnFundAdj = MigrationLearningDelivery.PriorLearnFundAdj;
            this.OtherFundAdj = MigrationLearningDelivery.OtherFundAdj;
            this.EmpOutcome = MigrationLearningDelivery.EmpOutcome;
            this.CompStatus = MigrationLearningDelivery.CompStatus;
            this.LearnActEndDate = MigrationLearningDelivery.LearnActEndDate;
            this.WithdrawReason = MigrationLearningDelivery.WithdrawReason;
            if (Convert.ToInt32(MigrationLearningDelivery.Outcome) == 6 ||
                Convert.ToInt32(MigrationLearningDelivery.Outcome) == 7)
                this.Outcome = null;
            else
                this.Outcome = MigrationLearningDelivery.Outcome;

            this.AchDate = MigrationLearningDelivery.AchDate;
            this.OutGrade = MigrationLearningDelivery.OutGrade;
            this.SWSupAimId = MigrationLearningDelivery.SWSupAimId;

            foreach (LearningDeliveryFAM migrationItem in MigrationLearningDelivery.LearningDeliveryFAMList.Where(f =>
                f.LearnDelFAMType != "TBS"))
            {
                if (migrationItem.LearnDelFAMType != "SPP" && migrationItem.LearnDelFAMType != "WPL" &&
                    !(migrationItem.LearnDelFAMType == "LDM" && migrationItem.LearnDelFAMType == "125") ||
                    (this.FundModel == 35 && this.LearnStartDate < FIRST_AUG_2013 &&
                     (this.ProgType == 2 || this.ProgType == 3 || this.ProgType == 10 || this.ProgType == 20 ||
                      this.ProgType == 21 || this.ProgType == 22 || this.ProgType == 23)))
                {
                    XmlNode newNode =
                        Node.OwnerDocument.CreateElement("LearningDeliveryFAM", NSMgr.LookupNamespace("ia"));
                    LearningDeliveryFAM newInstance = new LearningDeliveryFAM(migrationItem, newNode, NSMgr);
                    LearningDeliveryFAMList.Add(newInstance);
                    AppendToLastOfNodeNamed(newNode, newNode.Name);
                }
            }

            foreach (LearningDeliveryWorkPlacement migrationItem in MigrationLearningDelivery
                .LearningDeliveryWorkPlacementList)
            {
                XmlNode newNode =
                    Node.OwnerDocument.CreateElement("LearningDeliveryWorkPlacement", NSMgr.LookupNamespace("ia"));
                LearningDeliveryWorkPlacement newInstance =
                    new LearningDeliveryWorkPlacement(migrationItem, newNode, NSMgr);
                LearningDeliveryWorkPlacementList.Add(newInstance);
                newInstance.LearningDeliveryWPPropertyChanged += NewInstance_LearningDeliveryWPPropertyChanged;
                AppendToLastOfNodeNamed(newNode, newNode.Name);
                OnLearningDeliveryPropertyChanged();
            }
            //foreach (LearningDeliveryWorkPlacement migrationItem in MigrationLearningDelivery.LearningDeliveryWorkPlacementList)
            //{
            //    XmlNode newNode = Node.OwnerDocument.CreateElement("LearningDeliveryWorkPlacement", NSMgr.LookupNamespace("ia"));
            //    LearningDeliveryWorkPlacement newInstance = new LearningDeliveryWorkPlacement(migrationItem, newNode, NSMgr);
            //    LearningDeliveryWorkPlacementList.Add(newInstance);
            //    AppendToLastOfNodeNamed(newNode, newNode.Name);
            //}

            foreach (ApprenticeshipFinancialRecord migrationItem in MigrationLearningDelivery
                .ApprenticeshipFinancialRecordList)
            {
                XmlNode newNode = Node.OwnerDocument.CreateElement("AppFinRecord", NSMgr.LookupNamespace("ia"));
                ApprenticeshipFinancialRecord migrationItemAP = CreateApprenticeshipFinancialRecord();

                migrationItemAP.AFinCode = migrationItem.AFinCode;
                migrationItemAP.AFinType = migrationItem.AFinType;
                migrationItemAP.AFinDate = migrationItem.AFinDate;
                migrationItemAP.AFinAmount = migrationItem.AFinAmount;

                // ApprenticeshipFinancialRecord newInstance = new ApprenticeshipFinancialRecord(migrationItemAP, newNode, NSMgr);
                // ApprenticeshipFinancialRecordList.Add(migrationItemAP);
                AppendToLastOfNodeNamed(newNode, newNode.Name);
            }

            foreach (ProviderSpecDeliveryMonitoring migrationItem in MigrationLearningDelivery
                .ProviderSpecDeliveryMonitoringList)
            {
                XmlNode newNode =
                    Node.OwnerDocument.CreateElement("ProviderSpecDeliveryMonitoring", NSMgr.LookupNamespace("ia"));
                ProviderSpecDeliveryMonitoring migrationItemAP = CreateProviderSpecDeliveryMonitoring();

                migrationItemAP.ProvSpecDelMon = migrationItem.ProvSpecDelMon;
                migrationItemAP.ProvSpecDelMonOccur = migrationItem.ProvSpecDelMonOccur;
                ProviderSpecDeliveryMonitoringList.Add(migrationItemAP);
                AppendToLastOfNodeNamed(newNode, newNode.Name);
            }

            if (MigrationLearningDelivery.LearningDeliveryHE != null)
            {
                XmlNode newNode = Node.OwnerDocument.CreateElement("LearningDeliveryHE", NSMgr.LookupNamespace("ia"));
                LearningDeliveryHE =
                    new LearningDeliveryHE(MigrationLearningDelivery.LearningDeliveryHE, newNode, NSMgr);
                AppendToLastOfNodeNamed(newNode, newNode.Name);
            }

            IsImportRunning = false;
            //OnPropertyChanged("LearningDelivery");

        }

        #endregion

        #region Methods

        public void Delete(ChildEntity Child)
        {
            Node.RemoveChild(Child.Node);
            switch (Child.GetType().ToString())
            {
                case "ILR.LearningDeliveryFAM":
                    this.LearningDeliveryFAMList.Remove((LearningDeliveryFAM) Child);
                    break;
                case "ILR.ApprenticeshipFinancialRecord":
                    this.ApprenticeshipFinancialRecordList.Remove((ApprenticeshipFinancialRecord) Child);
                    break;
                case "ILR.ProviderSpecDeliveryMonitoring":
                    this.ProviderSpecDeliveryMonitoringList.Remove((ProviderSpecDeliveryMonitoring) Child);
                    break;
                case "ILR.LearningDeliveryHE":
                    this.LearningDeliveryHE = null;
                    break;
                case "ILR.LearningDeliveryWorkPlacement":
                    LearningDeliveryWorkPlacement childWp = Child as LearningDeliveryWorkPlacement;
                    if (childWp != null)
                    {
                        childWp.LearningDeliveryWPPropertyChanged -= NewInstance_LearningDeliveryWPPropertyChanged;
                        this.LearningDeliveryWorkPlacementList.Remove((LearningDeliveryWorkPlacement) Child);
                    }

                    break;
            }

            OnLearningDeliveryPropertyChanged();
        }

        #region FAM management

        private LearningDeliveryFAM GetLegacyFAM(string FAMType)
        {
            return this.LearningDeliveryFAMList.Where(x => x.LearnDelFAMType == FAMType).FirstOrDefault();
        }

        public LearningDeliveryFAM GetFAM(LearningDeliveryFAM.SingleOccurrenceFAMs FAMType)
        {
            return this.LearningDeliveryFAMList.Where(x => x.LearnDelFAMType == FAMType.ToString()).FirstOrDefault();
        }

        public string GetFAMCode(LearningDeliveryFAM.SingleOccurrenceFAMs FAMType)
        {
            LearningDeliveryFAM lFAM = GetFAM(FAMType);
            if (lFAM == null)
                return null;
            else
                return lFAM.LearnDelFAMCode;
        }

        public void SetFAM(LearningDeliveryFAM.SingleOccurrenceFAMs FAMType, string FAMCode)
        {
            LearningDeliveryFAM lFAM = GetFAM(FAMType);
            if (lFAM == null)
            {
                lFAM = this.CreateLearningDeliveryFAM();
                lFAM.LearnDelFAMType = FAMType.ToString();
            }

            lFAM.LearnDelFAMCode = FAMCode;
        }

        public void RemoveFAM(LearningDeliveryFAM.SingleOccurrenceFAMs FAMType)
        {
            LearningDeliveryFAM lFAM = GetFAM(FAMType);
            if (lFAM != null)
                Delete(lFAM);
        }

        public List<string> GetFAMList(LearningDeliveryFAM.MultiOccurrenceFAMs FAMType)
        {
            List<string> result = new List<string>();
            foreach (LearningDeliveryFAM lFAM in this.LearningDeliveryFAMList.Where(x =>
                x.LearnDelFAMType == FAMType.ToString()))
                result.Add(lFAM.LearnDelFAMCode);
            return result;
        }

        public void ClearFAMList(LearningDeliveryFAM.MultiOccurrenceFAMs FAMType)
        {
            List<LearningDeliveryFAM> TmpList = new List<LearningDeliveryFAM>(0);
            foreach (LearningDeliveryFAM lFAM in this.LearningDeliveryFAMList.Where(x =>
                x.LearnDelFAMType == FAMType.ToString()))
            {
                TmpList.Add(lFAM);
            }

            foreach (LearningDeliveryFAM lFAM in TmpList)
            {
                Delete(lFAM);
            }

            TmpList = null;

        }

        public void AddFAM(LearningDeliveryFAM.MultiOccurrenceFAMs FAMType, string FAMCode)
        {
            if (this.LearningDeliveryFAMList.Where(x => x.LearnDelFAMType == FAMType.ToString()
                                                        && x.LearnDelFAMCode == FAMCode.ToString()
                ).Count() == 0)
            {
                LearningDeliveryFAM lFAM = this.CreateLearningDeliveryFAM();
                lFAM.LearnDelFAMType = FAMType.ToString();
                lFAM.LearnDelFAMCode = FAMCode;
            }
        }

        public void AddFAM(LearningDeliveryFAM.MultiOccurrenceFAMs FAMType, string FAMCode, DateTime? FromDate,
            DateTime? ToDate)
        {
            if (this.LearningDeliveryFAMList.Where(x => x.LearnDelFAMType == FAMType.ToString()
                                                        && x.LearnDelFAMCode == FAMCode.ToString()
                ).Count() == 0)
            {
                LearningDeliveryFAM lFAM = this.CreateLearningDeliveryFAM();
                lFAM.LearnDelFAMType = FAMType.ToString();
                lFAM.LearnDelFAMCode = FAMCode;
                lFAM.LearnDelFAMDateFrom = FromDate;
                lFAM.LearnDelFAMDateTo = ToDate;
            }
        }

        public void RemoveFAM(LearningDeliveryFAM.MultiOccurrenceFAMs FAMType, string FAMCode)
        {
            LearningDeliveryFAM ldFAM = this.LearningDeliveryFAMList
                .Where(x => x.LearnDelFAMType == FAMType.ToString() && x.LearnDelFAMCode == FAMCode).FirstOrDefault();
            if (ldFAM != null)
                Delete(ldFAM);
        }

        public LearningDeliveryFAM GetFAM(LearningDeliveryFAM.DatedFAMs FAMType)
        {
            return this.LearningDeliveryFAMList.Where(x => x.LearnDelFAMType == FAMType.ToString()).FirstOrDefault();
        }

        public string GetFAMCode(LearningDeliveryFAM.DatedFAMs FAMType)
        {
            LearningDeliveryFAM lFAM = GetFAM(FAMType);
            if (lFAM == null)
                return null;
            else
                return lFAM.LearnDelFAMCode;
        }

        public DateTime? GetFAMFrom(LearningDeliveryFAM.DatedFAMs FAMType)
        {
            LearningDeliveryFAM lFAM = GetFAM(FAMType);
            if (lFAM == null)
                return null;
            else
                return lFAM.LearnDelFAMDateFrom;
        }

        public DateTime? GetFAMTo(LearningDeliveryFAM.DatedFAMs FAMType)
        {
            LearningDeliveryFAM lFAM = GetFAM(FAMType);
            if (lFAM == null)
                return null;
            else
                return lFAM.LearnDelFAMDateTo;
        }

        public void SetFAM(LearningDeliveryFAM.DatedFAMs FAMType, string FAMCode)
        {
            LearningDeliveryFAM lFAM = GetFAM(FAMType);
            if (lFAM == null)
            {
                lFAM = this.CreateLearningDeliveryFAM();
                lFAM.LearnDelFAMType = FAMType.ToString();
            }

            lFAM.LearnDelFAMCode = FAMCode;
        }

        public void SetFAMFrom(LearningDeliveryFAM.DatedFAMs FAMType, DateTime? FromDate)
        {
            LearningDeliveryFAM lFAM = GetFAM(FAMType);
            if (lFAM == null)
            {
                lFAM = this.CreateLearningDeliveryFAM();
                lFAM.LearnDelFAMType = FAMType.ToString();
            }

            lFAM.LearnDelFAMDateFrom = FromDate;
        }

        public void SetFAMTo(LearningDeliveryFAM.DatedFAMs FAMType, DateTime? ToDate)
        {
            LearningDeliveryFAM lFAM = GetFAM(FAMType);
            if (lFAM == null)
            {
                lFAM = this.CreateLearningDeliveryFAM();
                lFAM.LearnDelFAMType = FAMType.ToString();
            }

            lFAM.LearnDelFAMDateTo = ToDate;
        }

        public void RemoveFAM(LearningDeliveryFAM.DatedFAMs FAMType)
        {
            LearningDeliveryFAM lFAM = GetFAM(FAMType);
            if (lFAM != null)
                Delete(lFAM);
        }

        #endregion

        #region ProvSpecMon management

        public ProviderSpecDeliveryMonitoring GetProvSpecMon(ProviderSpecDeliveryMonitoring.Occurrence Occurrence)
        {
            return this.ProviderSpecDeliveryMonitoringList.Where(x => x.ProvSpecDelMonOccur == Occurrence.ToString())
                .FirstOrDefault();
        }

        public string GetProvSpecMonValue(ProviderSpecDeliveryMonitoring.Occurrence Occurrence)
        {
            ProviderSpecDeliveryMonitoring provSpecMon = GetProvSpecMon(Occurrence);
            if (provSpecMon == null)
                return null;
            else
                return provSpecMon.ProvSpecDelMon;
        }

        public void SetProvSpecMon(ProviderSpecDeliveryMonitoring.Occurrence Occurrence, string ProvSpecMonValue)
        {
            ProviderSpecDeliveryMonitoring provSpecMon = GetProvSpecMon(Occurrence);
            if (ProvSpecMonValue != null && ProvSpecMonValue.Length != 0)
            {
                if (provSpecMon == null)
                {
                    provSpecMon = this.CreateProviderSpecDeliveryMonitoring();
                    provSpecMon.ProvSpecDelMonOccur = Occurrence.ToString();
                }

                provSpecMon.ProvSpecDelMon = ProvSpecMonValue;
            }
            else if (provSpecMon != null)
                Delete(provSpecMon);
        }

        #endregion

        private void GiveFrountEndkickToRefresh()
        {
            OnPropertyChanged("LearnAimRef");
            OnPropertyChanged("AimType");
            OnPropertyChanged("AimSeqNumber");
            OnPropertyChanged("LearnStartDate");
            OnPropertyChanged("LearnPlanEndDate");
            OnPropertyChanged("FundModel");
            OnPropertyChanged("CompStatus");
            OnPropertyChanged("StdCode");
            OnPropertyChanged("FworkCode");

            OnPropertyChanged("IsComplete");
            OnPropertyChanged("IncompleteMessage");
        }

        public void RefreshData()
        {
            GiveFrountEndkickToRefresh();
        }

        #endregion

        #region IDataErrorInfo Members

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName]
        {
            get
            {
                string result = null;
                switch (columnName)
                {
                    case "LearnAimRef":
                        if ((LearnAimRef == null)
                            || ((LearnAimRef != null && LearnAimRef.ToString().Length == 0))
                        )
                            return "\t\tLearnAimRef missing\r\n";
                        if (LearnAimRef != null)
                            return CheckPropertyLength(LearnAimRef, CLASSNAME, columnName, TABS);
                        break;
                    case "AimType":
                        if ((AimType == null)
                            || ((AimType != null && AimType.ToString().Length == 0))
                        )
                            return "\t\tAimType missing\r\n";
                        if (AimType != null)
                            return CheckPropertyLength(AimType, CLASSNAME, columnName, TABS);
                        break;
                    case "AimSeqNumber":
                        if ((AimSeqNumber == null)
                            || ((AimSeqNumber != null && AimSeqNumber.ToString().Length == 0)
                            )
                        )
                            return "\t\tAimSeqNumber missing\r\n";
                        break;
                    case "LearnStartDate":
                        if ((LearnStartDate == null)
                            || ((LearnStartDate != null && LearnStartDate.ToString().Length == 0))
                        )
                            return "\t\tLearnStartDate - required\r\n";
                        break;
                    case "LearnPlanEndDate":
                        if ((LearnPlanEndDate == null)
                            || ((LearnPlanEndDate != null && LearnPlanEndDate.ToString().Length == 0))
                        )
                            return "\t\tLearnPlanEndDate - required\r\n";
                        break;
                    //case "LearnActEndDate":
                    //    if (LearnActEndDate != null)
                    //    {
                    //        //if (LearnActEndDate < (System.DateTime.Now.AddYears(-100)))
                    //        // 			return "Learn Act EndDate - required\r\n";
                    //        //if (LearnActEndDate < LearnStartDate)
                    //        //    return "Learn Act EndDate - before Start Date";
                    //    }
                    //    break;
                    case "FundModel":
                        if ((FundModel == null)
                            || ((FundModel != null && FundModel.ToString().Length == 0))
                        )
                            return "\t\tFundModel - required\r\n";
                        if (FundModel != null)
                            return CheckPropertyLength(FundModel, CLASSNAME, columnName, TABS);
                        break;
                    case "ProgType":
                        if (ProgType != null &&
                            ProgType.ToString().Length > GetItemSize("LearningDelivery." + columnName))
                            if (ProgType != null)
                                return CheckPropertyLength(ProgType, CLASSNAME, columnName, TABS);
                        break;
                    case "FworkCode":
                        if (FworkCode != null)
                        {
                            if (!CommonValidations.IsValidNumber(FworkCode))
                                return $"Framework code should be numeric";
                            else
                                return CheckPropertyLength(FworkCode, CLASSNAME, columnName, TABS);
                        }

                        break;
                    case "PwayCode":
                        if (PwayCode != null)
                            return CheckPropertyLength(PwayCode, CLASSNAME, columnName, TABS);
                        break;
                    case "StdCode":
                        if (StdCode != null)
                        {
                            if (!CommonValidations.IsValidNumber(StdCode))
                                return $"Apprenticeship standard code should be numeric";
                            else
                                return CheckPropertyLength(StdCode, CLASSNAME, columnName, TABS);
                        }

                        break;
                    case "PartnerUKPRN":
                        if (PartnerUKPRN != null)
                            return CheckPropertyLength(PartnerUKPRN, CLASSNAME, columnName, TABS);
                        break;
                    case "PriorLearnFundAdj":
                        if (PriorLearnFundAdj != null)
                            return CheckPropertyLength(PriorLearnFundAdj, CLASSNAME, columnName, TABS);
                        break;
                    case "OtherFundAdj":
                        if (OtherFundAdj != null)
                            return CheckPropertyLength(OtherFundAdj, CLASSNAME, columnName, TABS);
                        break;
                    case "EmpOutcome":
                        if (EmpOutcome != null)
                            return CheckPropertyLength(EmpOutcome, CLASSNAME, columnName, TABS);
                        break;
                    case "CompStatus":
                        if ((CompStatus == null)
                            || ((CompStatus != null && CompStatus.ToString().Length == 0))
                        )
                            return "\t\tCompStatus - required\r\n";
                        if (CompStatus != null)
                            return CheckPropertyLength(CompStatus, CLASSNAME, columnName, TABS);
                        break;
                    case "WithdrawReason":
                        if (WithdrawReason != null)
                            return CheckPropertyLength(WithdrawReason, CLASSNAME, columnName, TABS);
                        break;
                    case "Outcome":
                        if (Outcome != null)
                            return CheckPropertyLength(Outcome, CLASSNAME, columnName, TABS);
                        break;
                    case "AddHours":
                        if (AddHours != null)
                            return CheckPropertyLength(AddHours, CLASSNAME, columnName, TABS);
                        break;
                    case "ConRefNumber":
                        if (ConRefNumber != null)
                            return CheckPropertyLength(ConRefNumber, CLASSNAME, columnName, TABS);
                        break;
                    case "DelLocPostCode":
                        if ((DelLocPostCode == null)
                            || ((DelLocPostCode != null && DelLocPostCode.ToString().Length == 0))
                        )
                            return "\t\tDel Loc Post Code - required\r\n";
                        break;
                    case "EPAOrgID":
                        if (EPAOrgID != null)
                            return CheckPropertyLength(EPAOrgID, CLASSNAME, columnName, TABS);
                        break;
                    case "PHours":
                        if (PHours != null)
                            return CheckPropertyLength(PHours, CLASSNAME, columnName, TABS);
                        break;
                    case "LSDPostCode":
                        if ((LSDPostCode == null)
                            || ((LSDPostCode != null && LSDPostCode.ToString().Length == 0))
                        )
                            return "\t\tLocation Start Date Post Code - required\r\n";
                        break;



                    default:
                        break;
                }

                return result;
            }
        }


        private const string SWSUPPAIMID_PATTERN = "^[{(]?[0-9A-F]{8}[-]?([0-9A-F]{4}[-]?){3}[0-9A-F]{12}[)}]?$";

        private bool isValidSWSupAimId()
        {
            var regEx = new Regex(SWSUPPAIMID_PATTERN);
            return String.IsNullOrEmpty(SWSupAimId) ? true : regEx.Match(this.SWSupAimId).Success;

        }
        //private bool isOTJHoursValild()
        //{
        //    if (String.IsNullOrEmpty(PHours)) return true; //optional field hence a blank is alright.
        //    var regEx = new Regex(PHOURS_PATTERN);
        //    var regExMatch = regEx.Match(this.PHours).Success;
        //    if (regExMatch) //they are numbers
        //    {
        //        int numbers = 0;
        //        if (Int32.TryParse(PHours, out numbers)) //numbers
        //        {
        //            return ((numbers <= 9999) && (numbers >= 0));
        //        }

        //    }

        //    return false;
        //}



        #endregion
    }
}
