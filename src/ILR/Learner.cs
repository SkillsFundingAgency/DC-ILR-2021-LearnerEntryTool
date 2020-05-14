using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;

namespace ILR
{
    public class Learner : ChildEntity, IDataErrorInfo
    {
        private const String CLASSNAME = "Learner";
        private const String TABS = "";
        private bool _isImportRunning = false;
        private const string CAMPID_PATTERN = "^[A-Za-z0-9]{1,8}$";

        private readonly IList<string> _learnFamTypeListExcludedForMigration = new List<string>()
            {"LDA", "HNS", "LSR", "SEN", "FME", "PPE", "EDF", "MCF", "ECF"};

        #region Accessors

        public Boolean IsFileImportLoadingRunning
        {
            get { return _isImportRunning; }
            set
            {
                foreach (LearningDelivery ld in LearningDeliveryList)
                {
                    ld.IsImportRunning = value;
                }

                _isImportRunning = value;
            }
        }

        public override bool IsComplete
        {
            get
            {
                if (IncompleteMessage == string.Empty)
                    return true;
                else
                    return false;
            }
        }

        public override string IncompleteMessage
        {
            get
            {
                string message = string.Empty;

                message += this["GivenNames"];
                message += this["FamilyName"];
                message += this["Sex"];
                message += this["LearnRefNumber"];
                message += this["ULN"];
                message += this["Ethnicity"];
                message += this["TelNumber"];
                message += this["PostCode"];
                message += this["LLDDHealthProb"];

                if (this.LearningDeliveryList.Count == 0)
                    message += "No LearningDelivery records\r\n";

                if (this.LearningDeliveryList.FindAll(x => x.IsComplete == false).Count > 0)
                {
                    message += "LearningDelivery Issues\r\n";

                    foreach (ILR.LearningDelivery ld in LearningDeliveryList.FindAll(x => x.IsComplete == false))
                    {
                        message += "\tAim Sequence Numbers (" + ld.AimSeqNumber.ToString() + ") missing :" + "\r\n" +
                                   ld.IncompleteMessage;
                    }
                }

                if (this.LearnerEmploymentStatusList.Count > 0)
                {
                    foreach (var lEmpStatus in this.LearnerEmploymentStatusList.FindAll(x => x.IsComplete == false))
                    {
                        message += "\tLearner Employment status is not valid: " + lEmpStatus.IncompleteMessage;
                    }

                }

                if (string.IsNullOrEmpty(this.PostcodePrior))
                    message += "Postcode Prior to Enrolment required\r\n";
                return message;
            }
        }

        public bool HasContinuingAims => LearningDeliveriesToMigrate.Count > 0;

        public List<LearningDelivery> LearningDeliveriesToMigrate
        {
            get
            {
                List<LearningDelivery> learningDeliveriesToMigrate = new List<LearningDelivery>();
                foreach (LearningDelivery learningDelivery in this.LearningDeliveryList)
                {
                    switch (learningDelivery.AimType)
                    {
                        case 1:
                            if (learningDelivery.ShouldProbablyMigrate)
                                learningDeliveriesToMigrate.Add(learningDelivery);
                            else if (this.LearningDeliveryList.Exists(ld =>
                                ld.AimType == 3 && ld.ShouldProbablyMigrate &&
                                ld.FworkCode == learningDelivery.FworkCode &&
                                ld.ProgType == learningDelivery.ProgType && ld.PwayCode == learningDelivery.PwayCode))
                                learningDeliveriesToMigrate.Add(learningDelivery);
                            break;

                        case 4:
                            if (learningDelivery.ShouldProbablyMigrate)
                                learningDeliveriesToMigrate.Add(learningDelivery);
                            break;

                        case 5:
                            if (learningDelivery.ShouldProbablyMigrate)
                                learningDeliveriesToMigrate.Add(learningDelivery);
                            break;

                        case 3:
                            if (learningDelivery.ShouldProbablyMigrate)
                                learningDeliveriesToMigrate.Add(learningDelivery);
                            else if (learningDelivery.ProgType != 25 && this.LearningDeliveryList.Exists(ld =>
                                         ld.AimType == 1 && ld.ShouldProbablyMigrate &&
                                         ld.FworkCode == learningDelivery.FworkCode &&
                                         ld.ProgType == learningDelivery.ProgType &&
                                         ld.PwayCode == learningDelivery.PwayCode))
                                learningDeliveriesToMigrate.Add(learningDelivery);
                            else if (learningDelivery.ProgType == 25 && learningDelivery.StdCode != null &&
                                     this.LearningDeliveryList.Exists(ld =>
                                         ld.AimType == 1 && ld.ShouldProbablyMigrate &&
                                         ld.ProgType == learningDelivery.ProgType))
                                learningDeliveriesToMigrate.Add(learningDelivery);
                            break;
                    }
                }

                return learningDeliveriesToMigrate;
            }
        }

        public List<LearnerEmploymentStatus> LearnerEmploymentStatusToMigrate
        {
            get
            {
                var statusList = new List<LearnerEmploymentStatus>();
                if (this.LearningDeliveriesToMigrate.Count > 0)
                {
                    //get the earliest learning delivery
                    this.LearningDeliveriesToMigrate.Sort((ld1, ld2) =>
                        ld1.LearnStartDate.Value.CompareTo(ld2.LearnStartDate.Value));

                    var earliestLearningDate = this.LearningDeliveriesToMigrate[0].LearnStartDate.Value;

                    this.LearnerEmploymentStatusList.Sort((les1, les2) =>
                        les1.DateEmpStatApp.Value.CompareTo(les2.DateEmpStatApp.Value));
                    int lastIndex = 0;
                    for (int i = 0; i < LearnerEmploymentStatusList.Count; i++)
                    {
                        if (LearnerEmploymentStatusList[i].DateEmpStatApp.Value >= earliestLearningDate)
                            break;
                        lastIndex = i;
                    }

                    for (int t = lastIndex; t < LearnerEmploymentStatusList.Count; t++)
                    {
                        statusList.Add(LearnerEmploymentStatusList[t]);
                    }

                }

                return statusList;
            }
        }

        #endregion

        #region ILR Properties

        public string LearnRefNumber
        {
            get { return XMLHelper.GetChildValue("LearnRefNumber", Node, NSMgr); }
            set
            {
                Message.UpdateLearnerRefCount(LearnRefNumber, value);
                XMLHelper.SetChildValue("LearnRefNumber", value, Node, NSMgr);
                OnPropertyChanged("LearnRefNumber");
                RefreshFormProperties();
            }
        }

        public string PrevLearnRefNumber
        {
            get { return XMLHelper.GetChildValue("PrevLearnRefNumber", Node, NSMgr); }
            set
            {
                XMLHelper.SetChildValue("PrevLearnRefNumber", value, Node, NSMgr);
                OnPropertyChanged("PrevLearnRefNumber");
                RefreshFormProperties();
            }
        }

        public long? ULN
        {
            get
            {
                string ULN = XMLHelper.GetChildValue("ULN", Node, NSMgr);
                return (ULN != null ? long.Parse(ULN) : (long?)null);
            }
            set
            {
                XMLHelper.SetChildValue("ULN", value, Node, NSMgr);
                OnPropertyChanged("ULN");
                RefreshFormProperties();
            }
        }

        public int? PrevUKPRN
        {
            get
            {
                string PrevUKPRN = XMLHelper.GetChildValue("PrevUKPRN", Node, NSMgr);
                return (PrevUKPRN != null ? int.Parse(PrevUKPRN) : (int?) null);
            }
            set
            {
                XMLHelper.SetChildValue("PrevUKPRN", value, Node, NSMgr);
                OnPropertyChanged("PrevUKPRN");
                RefreshFormProperties();
            }
        }

        public int? PMUKPRN
        {
            get
            {
                string PMUKPRN = XMLHelper.GetChildValue("PMUKPRN", Node, NSMgr);
                return (PMUKPRN != null ? int.Parse(PMUKPRN) : (int?) null);
            }
            set
            {
                XMLHelper.SetChildValue("PMUKPRN", value, Node, NSMgr);
                OnPropertyChanged("PMUKPRN");
                RefreshFormProperties();
            }
        }

        public string CampId
        {
            get { return XMLHelper.GetChildValue("CampId", Node, NSMgr); }
            set
            {
                XMLHelper.SetChildValue("CampId", value, Node, NSMgr);
                OnPropertyChanged("CampId");
                RefreshFormProperties();
            }
        }

        public string GivenNames
        {
            get { return XMLHelper.GetChildValue("GivenNames", Node, NSMgr); }
            set
            {
                XMLHelper.SetChildValue("GivenNames", value, Node, NSMgr);
                OnPropertyChanged("GivenNames");
                RefreshFormProperties();
            }
        }

        public string FamilyName
        {
            get { return XMLHelper.GetChildValue("FamilyName", Node, NSMgr); }
            set
            {
                XMLHelper.SetChildValue("FamilyName", value, Node, NSMgr);
                OnPropertyChanged("FamilyName");
                RefreshFormProperties();
            }
        }

        public DateTime? DateOfBirth
        {
            get
            {
                string DateOfBirth = XMLHelper.GetChildValue("DateOfBirth", Node, NSMgr);
                return (DateOfBirth != null ? DateTime.Parse(DateOfBirth) : (DateTime?) null);
            }
            set
            {
                XMLHelper.SetChildValue("DateOfBirth", value, Node, NSMgr);
                OnPropertyChanged("DateOfBirth");
                RefreshFormProperties();
            }
        }

        public string Sex
        {
            get { return XMLHelper.GetChildValue("Sex", Node, NSMgr); }
            set
            {
                XMLHelper.SetChildValue("Sex", value, Node, NSMgr);
                OnPropertyChanged("Sex");
                RefreshFormProperties();
            }
        }
  
        public string NINumber
        {
            get { return XMLHelper.GetChildValue("NINumber", Node, NSMgr); }
            set
            {
                XMLHelper.SetChildValue("NINumber", value, Node, NSMgr);
                OnPropertyChanged("NINumber");
                RefreshFormProperties();
            }
        }

        public int? Ethnicity
        {
            get
            {
                string Ethnicity = XMLHelper.GetChildValue("Ethnicity", Node, NSMgr);
                return (Ethnicity != null ? int.Parse(Ethnicity) : (int?)null);
            }
            set
            {
                XMLHelper.SetChildValue("Ethnicity", value, Node, NSMgr);
                OnPropertyChanged("Ethnicity");
                RefreshFormProperties();
            }
        }

        public string PostcodePrior
        {
            get { return XMLHelper.GetChildValue("PostcodePrior", Node, NSMgr); }
            set
            {
                XMLHelper.SetChildValue("PostcodePrior", value, Node, NSMgr);
                OnPropertyChanged("PostcodePrior");
                RefreshFormProperties();
            }
        }

        public string AddLine1
        {
            get { return XMLHelper.GetChildValue("AddLine1", Node, NSMgr); }
            set
            {
                XMLHelper.SetChildValue("AddLine1", value, Node, NSMgr);
                OnPropertyChanged("AddLine1");
                RefreshFormProperties();
            }
        }

        public string AddLine2
        {
            get { return XMLHelper.GetChildValue("AddLine2", Node, NSMgr); }
            set
            {
                XMLHelper.SetChildValue("AddLine2", value, Node, NSMgr);
                OnPropertyChanged("AddLine2");
                RefreshFormProperties();
            }
        }

        public string AddLine3
        {
            get { return XMLHelper.GetChildValue("AddLine3", Node, NSMgr); }
            set
            {
                XMLHelper.SetChildValue("AddLine3", value, Node, NSMgr);
                OnPropertyChanged("AddLine3");
                RefreshFormProperties();
            }
        }

        public string AddLine4
        {
            get { return XMLHelper.GetChildValue("AddLine4", Node, NSMgr); }
            set
            {
                XMLHelper.SetChildValue("AddLine4", value, Node, NSMgr);
                OnPropertyChanged("AddLine4");
                RefreshFormProperties();
            }
        }

        public string PostCode
        {
            get { return XMLHelper.GetChildValue("Postcode", Node, NSMgr); }
            set
            {
                XMLHelper.SetChildValue("Postcode", value, Node, NSMgr);
                OnPropertyChanged("PostCode");
                RefreshFormProperties();
            }
        }

        public string TelNo
        {
            get { return XMLHelper.GetChildValue("TelNo", Node, NSMgr); }
            set
            {
                XMLHelper.SetChildValue("TelNo", value, Node, NSMgr);
                OnPropertyChanged("TelNo");
                RefreshFormProperties();
            }
        }

        public string Email
        {
            get { return XMLHelper.GetChildValue("Email", Node, NSMgr); }
            set
            {
                XMLHelper.SetChildValue("Email", value, Node, NSMgr);
                OnPropertyChanged("Email");
                RefreshFormProperties();
            }
        }

        public bool Accom
        {
            get
            {
                string Accom = XMLHelper.GetChildValue("Accom", Node, NSMgr);
                return (Accom != null ? true : false);
            }
            set
            {
                XMLHelper.SetChildValue("Accom", value ? (int?)5 : (int?)null, Node, NSMgr);
                OnPropertyChanged("Accom");
                RefreshFormProperties();
            }
        }

        public string EngGrade
        {
            get { return XMLHelper.GetChildValue("EngGrade", Node, NSMgr); }
            set
            {
                XMLHelper.SetChildValue("EngGrade", value, Node, NSMgr);
                OnPropertyChanged("EngGrade");
                RefreshFormProperties();
            }
        }

        public string MathGrade
        {
            get { return XMLHelper.GetChildValue("MathGrade", Node, NSMgr); }
            set
            {
                XMLHelper.SetChildValue("MathGrade", value, Node, NSMgr);
                OnPropertyChanged("MathGrade");
                RefreshFormProperties();
            }
        }

        public int? LLDDHealthProb
        {
            get
            {
                string LLDDHealthProb = XMLHelper.GetChildValue("LLDDHealthProb", Node, NSMgr);
                return (LLDDHealthProb != null ? int.Parse(LLDDHealthProb) : (int?) null);
            }
            set
            {
                XMLHelper.SetChildValue("LLDDHealthProb", value, Node, NSMgr);
                OnPropertyChanged("LLDDHealthProb");
                RefreshFormProperties();
            }
        }

        public int? PriorAttain
        {
            get
            {
                string PriorAttain = XMLHelper.GetChildValue("PriorAttain", Node, NSMgr);
                return (PriorAttain != null ? int.Parse(PriorAttain) : (int?) null);
            }
            set
            {
                XMLHelper.SetChildValue("PriorAttain", value, Node, NSMgr);
                OnPropertyChanged("PriorAttain");
                RefreshFormProperties();
            }
        }

 
        public string ALSCost
        {
            get { return XMLHelper.GetChildValue("ALSCost", Node, NSMgr); }
            set
            {
                XMLHelper.SetChildValue("ALSCost", value, Node, NSMgr);
                OnPropertyChanged("ALSCost");
                RefreshFormProperties();
            }
        }

        public int? PlanLearnHours
        {
            get
            {
                string PlanLearnHours = XMLHelper.GetChildValue("PlanLearnHours", Node, NSMgr);
                return (PlanLearnHours != null ? int.Parse(PlanLearnHours) : (int?) null);
            }
            set
            {
                XMLHelper.SetChildValue("PlanLearnHours", value, Node, NSMgr);
                OnPropertyChanged("PlanLearnHours");
                RefreshFormProperties();
            }
        }

        public int? PlanEEPHours
        {
            get
            {
                string PlanEEPHours = XMLHelper.GetChildValue("PlanEEPHours", Node, NSMgr);
                return (PlanEEPHours != null ? int.Parse(PlanEEPHours) : (int?) null);
            }
            set
            {
                XMLHelper.SetChildValue("PlanEEPHours", value, Node, NSMgr);
                OnPropertyChanged("PlanEEPHours");
                RefreshFormProperties();
            }
        } 

        #endregion

        #region Application Properties

        public bool ExcludeFromExport
        {
            get
            {
                XmlAttribute attr = Node.Attributes["ExcludeFromExport"];
                return ((attr != null && attr.Value == "true") || !IsComplete);
            }
            set
            {
                if (value)
                    ((XmlElement) Node).SetAttribute("ExcludeFromExport", "true");
                else
                    ((XmlElement) Node).RemoveAttribute("ExcludeFromExport");
                OnPropertyChanged("ExcludeFromExport");
            }
        }

        #endregion

        #region Renormalised Child Entities


        #region LearnerFAM

        //public bool LDA
        //{
        //    get
        //    {
        //        return GetFAMCode(LearnerFAM.SingleOccurrenceFAMs.LDA) == 1;
        //    }
        //    set
        //    {
        //        if (value)
        //            SetFAM(LearnerFAM.SingleOccurrenceFAMs.LDA, 1);
        //        else
        //            RemoveFAM(LearnerFAM.SingleOccurrenceFAMs.LDA);
        //        OnPropertyChanged("LDA");
        //    }
        //}

        public bool HNS
        {
            get { return GetFAMCode(LearnerFAM.SingleOccurrenceFAMs.HNS) == 1; }
            set
            {
                if (value)
                    SetFAM(LearnerFAM.SingleOccurrenceFAMs.HNS, 1);
                else
                    RemoveFAM(LearnerFAM.SingleOccurrenceFAMs.HNS);
                OnPropertyChanged("HNS");
            }
        }

        public bool EHC
        {
            get { return GetFAMCode(LearnerFAM.SingleOccurrenceFAMs.EHC) == 1; }
            set
            {
                if (value)
                    SetFAM(LearnerFAM.SingleOccurrenceFAMs.EHC, 1);
                else
                    RemoveFAM(LearnerFAM.SingleOccurrenceFAMs.EHC);
                OnPropertyChanged("EHC");
            }
        }

        public bool DLA
        {
            get { return GetFAMCode(LearnerFAM.SingleOccurrenceFAMs.DLA) == 1; }
            set
            {
                if (value)
                    SetFAM(LearnerFAM.SingleOccurrenceFAMs.DLA, 1);
                else
                    RemoveFAM(LearnerFAM.SingleOccurrenceFAMs.DLA);
                OnPropertyChanged("DLA");
            }
        }

        public List<int> LSR
        {
            get { return GetFAMList(LearnerFAM.MultiOccurrenceFAMs.LSR); }
            set
            {
                ClearFAMList(LearnerFAM.MultiOccurrenceFAMs.LSR);
                foreach (int lFAMCode in value)
                    AddFAM(LearnerFAM.MultiOccurrenceFAMs.LSR, lFAMCode);
                OnPropertyChanged("LSR");
            }
        }

        public List<int> NLM
        {
            get { return GetFAMList(LearnerFAM.MultiOccurrenceFAMs.NLM); }
            set
            {
                ClearFAMList(LearnerFAM.MultiOccurrenceFAMs.NLM);
                foreach (int lFAMCode in value)
                    AddFAM(LearnerFAM.MultiOccurrenceFAMs.NLM, lFAMCode);
                OnPropertyChanged("NLM");
            }
        }

        public int? FME
        {
            get { return GetFAMCode(LearnerFAM.SingleOccurrenceFAMs.FME); }
            set
            {
                if (value != null)
                    SetFAM(LearnerFAM.SingleOccurrenceFAMs.FME, (int) value);
                else
                    RemoveFAM(LearnerFAM.SingleOccurrenceFAMs.FME);
                OnPropertyChanged("FME");
            }
        }

        public bool SEN
        {
            get { return GetFAMCode(LearnerFAM.SingleOccurrenceFAMs.SEN) == 1; }
            set
            {
                if (value)
                    SetFAM(LearnerFAM.SingleOccurrenceFAMs.SEN, 1);
                else
                    RemoveFAM(LearnerFAM.SingleOccurrenceFAMs.SEN);
                OnPropertyChanged("SEN");
            }
        }

        public int? ECF
        {
            get { return GetFAMCode(LearnerFAM.SingleOccurrenceFAMs.ECF); }
            set
            {
                if (value != null)
                    SetFAM(LearnerFAM.SingleOccurrenceFAMs.ECF, (int) value);
                else
                    RemoveFAM(LearnerFAM.SingleOccurrenceFAMs.ECF);
                OnPropertyChanged("ECF");
            }
        }

        public int? MCF
        {
            get { return GetFAMCode(LearnerFAM.SingleOccurrenceFAMs.MCF); }
            set
            {
                if (value != null)
                    SetFAM(LearnerFAM.SingleOccurrenceFAMs.MCF, (int) value);
                else
                    RemoveFAM(LearnerFAM.SingleOccurrenceFAMs.MCF);
                OnPropertyChanged("MCF");
            }
        }

        public List<int> EDF
        {
            get { return GetFAMList(LearnerFAM.MultiOccurrenceFAMs.EDF); }
            set
            {
                ClearFAMList(LearnerFAM.MultiOccurrenceFAMs.EDF);
                foreach (int lFAMCode in value)
                    AddFAM(LearnerFAM.MultiOccurrenceFAMs.EDF, lFAMCode);
                OnPropertyChanged("EDF");
            }
        }

        public bool EDF1
        {
            get
            {
                LearnerFAM tmp = this.LearnerFAMList.Where(x =>
                        x.LearnFAMType.ToUpper() == LearnerFAM.MultiOccurrenceFAMs.EDF.ToString() &&
                        x.LearnFAMCode == 1)
                    .FirstOrDefault();
                return (tmp != null ? true : false);
            }
            set
            {
                LearnerFAM tmp = this.LearnerFAMList.Where(x =>
                        x.LearnFAMType.ToUpper() == LearnerFAM.MultiOccurrenceFAMs.EDF.ToString() &&
                        x.LearnFAMCode == 1)
                    .FirstOrDefault();
                if (value)
                {
                    if (tmp == null)
                    {
                        this.AddFAM(LearnerFAM.MultiOccurrenceFAMs.EDF, 1);
                    }
                }
                else
                {
                    if (tmp != null)
                    {
                        this.Delete(tmp);
                    }
                }

                OnPropertyChanged("EDF1");
            }
        }

        public bool EDF2
        {
            get
            {
                LearnerFAM tmp = this.LearnerFAMList.Where(x =>
                        x.LearnFAMType.ToUpper() == LearnerFAM.MultiOccurrenceFAMs.EDF.ToString() &&
                        x.LearnFAMCode == 2)
                    .FirstOrDefault();
                return (tmp != null ? true : false);
            }
            set
            {
                LearnerFAM tmp = this.LearnerFAMList.Where(x =>
                        x.LearnFAMType.ToUpper() == LearnerFAM.MultiOccurrenceFAMs.EDF.ToString() &&
                        x.LearnFAMCode == 2)
                    .FirstOrDefault();
                if (value)
                {
                    if (tmp == null)
                    {
                        this.AddFAM(LearnerFAM.MultiOccurrenceFAMs.EDF, 2);
                    }
                }
                else
                {
                    if (tmp != null)
                    {
                        this.Delete(tmp);
                    }
                }

                OnPropertyChanged("EDF2");
            }
        }

        public Boolean PPE1
        {
            get
            {
                LearnerFAM tmp = this.LearnerFAMList.Where(x =>
                    x.LearnFAMType.ToUpper() == LearnerFAM.MultiOccurrenceFAMs.PPE.ToString()
                    && x.LearnFAMCode == 1
                ).FirstOrDefault();
                return (tmp != null ? true : false);
            }
            set
            {
                LearnerFAM tmp = this.LearnerFAMList.Where(x =>
                    x.LearnFAMType.ToUpper() == LearnerFAM.MultiOccurrenceFAMs.PPE.ToString()
                    && x.LearnFAMCode == 1
                ).FirstOrDefault();
                if (value)
                {
                    if (tmp == null)
                    {
                        this.AddFAM(LearnerFAM.MultiOccurrenceFAMs.PPE, 1);
                    }
                }
                else
                {
                    if (tmp != null)
                    {
                        this.Delete(tmp);
                    }
                }

                OnPropertyChanged("PPE1");
            }
        }

        public Boolean PPE2
        {
            get
            {
                LearnerFAM tmp = this.LearnerFAMList.Where(x =>
                    x.LearnFAMType.ToUpper() == LearnerFAM.MultiOccurrenceFAMs.PPE.ToString()
                    && x.LearnFAMCode == 2
                ).FirstOrDefault();
                return (tmp != null ? true : false);
            }
            set
            {
                LearnerFAM tmp = this.LearnerFAMList.Where(x =>
                    x.LearnFAMType.ToUpper() == LearnerFAM.MultiOccurrenceFAMs.PPE.ToString()
                    && x.LearnFAMCode == 2
                ).FirstOrDefault();
                if (value)
                {
                    if (tmp == null)
                    {
                        this.AddFAM(LearnerFAM.MultiOccurrenceFAMs.PPE, 2);
                    }
                }
                else
                {
                    if (tmp != null)
                    {
                        this.Delete(tmp);
                    }
                }

                OnPropertyChanged("PPE2");
            }
        }

        #endregion

        #region ProviderSpecLearnerMonitoring

        public string ProvSpecMonA
        {
            get { return GetProvSpecMonValue(ProviderSpecLearnerMonitoring.Occurrence.A); }
            set
            {
                SetProvSpecMon(ProviderSpecLearnerMonitoring.Occurrence.A, value);
                OnPropertyChanged("ProvSpecMonA");
                RefreshFormProperties();
            }
        }

        public string ProvSpecMonB
        {
            get { return GetProvSpecMonValue(ProviderSpecLearnerMonitoring.Occurrence.B); }
            set
            {
                SetProvSpecMon(ProviderSpecLearnerMonitoring.Occurrence.B, value);
                OnPropertyChanged("ProvSpecMonB");
                RefreshFormProperties();
            }
        }

        #endregion

        #region ContactPreference

        public bool PMC1
        {
            get
            {
                return this.ContactPreferenceList.Where(x => x.ContPrefType == "PMC" && x.ContPrefCode == 1).Count() >
                       0;
            }
            set
            {
                if (value && !PMC1)
                {
                    ContactPreference newInstance = CreateContactPreference();
                    newInstance.ContPrefType = "PMC";
                    newInstance.ContPrefCode = 1;
                }
                else if (!value && PMC1)
                {
                    ContactPreference deleteInstance = this.ContactPreferenceList
                        .Where(x => x.ContPrefType == "PMC" && x.ContPrefCode == 1).FirstOrDefault();
                    this.DeleteContactInstance(deleteInstance);
                }

                OnPropertyChanged("PMC1");
            }
        }

        public bool PMC2
        {
            get
            {
                return this.ContactPreferenceList.Where(x => x.ContPrefType == "PMC" && x.ContPrefCode == 2).Count() >
                       0;
            }
            set
            {
                if (value && !PMC2)
                {
                    ContactPreference newInstance = CreateContactPreference();
                    newInstance.ContPrefType = "PMC";
                    newInstance.ContPrefCode = 2;
                }
                else if (!value && PMC2)
                {
                    ContactPreference deleteInstance = this.ContactPreferenceList
                        .Where(x => x.ContPrefType == "PMC" && x.ContPrefCode == 2).FirstOrDefault();
                    this.DeleteContactInstance(deleteInstance);
                }

                OnPropertyChanged("PMC2");
            }
        }

        public bool PMC3
        {
            get
            {
                return this.ContactPreferenceList.Where(x => x.ContPrefType == "PMC" && x.ContPrefCode == 3).Count() >
                       0;
            }
            set
            {
                if (value && !PMC3)
                {
                    ContactPreference newInstance = CreateContactPreference();
                    newInstance.ContPrefType = "PMC";
                    newInstance.ContPrefCode = 3;
                }
                else if (!value && PMC3)
                {
                    ContactPreference deleteInstance = this.ContactPreferenceList
                        .Where(x => x.ContPrefType == "PMC" && x.ContPrefCode == 3).FirstOrDefault();
                    this.DeleteContactInstance(deleteInstance);
                }

                OnPropertyChanged("PMC3");
            }
        }

        public bool PMC4
        {
            get
            {
                return this.ContactPreferenceList.Where(x => x.ContPrefType == "PMC" && x.ContPrefCode == 4).Count() >
                       0;
            }
            set
            {
                if (value && !PMC4)
                {
                    ContactPreference newInstance = CreateContactPreference();
                    newInstance.ContPrefType = "PMC";
                    newInstance.ContPrefCode = 4;
                }
                else if (!value && PMC4)
                {
                    ContactPreference deleteInstance = this.ContactPreferenceList
                        .Where(x => x.ContPrefType == "PMC" && x.ContPrefCode == 4).FirstOrDefault();
                    this.DeleteContactInstance(deleteInstance);
                }

                OnPropertyChanged("PMC4");
            }
        }

        public bool PMC5
        {
            get
            {
                return this.ContactPreferenceList.Where(x => x.ContPrefType == "PMC" && x.ContPrefCode == 5).Count() >
                       0;
            }
            set
            {
                if (value && !PMC5)
                {
                    ContactPreference newInstance = CreateContactPreference();
                    newInstance.ContPrefType = "PMC";
                    newInstance.ContPrefCode = 5;
                }
                else if (!value && PMC5)
                {
                    ContactPreference deleteInstance = this.ContactPreferenceList
                        .Where(x => x.ContPrefType == "PMC" && x.ContPrefCode == 5).FirstOrDefault();
                    this.DeleteContactInstance(deleteInstance);
                }

                OnPropertyChanged("PMC5");
            }
        }

        public bool PMC6
        {
            get
            {
                return this.ContactPreferenceList.Where(x => x.ContPrefType == "PMC" && x.ContPrefCode == 6).Count() >
                       0;
            }
            set
            {
                if (value && !PMC6)
                {
                    ContactPreference newInstance = CreateContactPreference();
                    newInstance.ContPrefType = "PMC";
                    newInstance.ContPrefCode = 6;
                }
                else if (!value && PMC6)
                {
                    ContactPreference deleteInstance = this.ContactPreferenceList
                        .Where(x => x.ContPrefType == "PMC" && x.ContPrefCode == 6).FirstOrDefault();
                    this.DeleteContactInstance(deleteInstance);
                }

                OnPropertyChanged("PMC6");
            }
        }

        public bool RUI1
        {
            get
            {
                return this.ContactPreferenceList.Where(x => x.ContPrefType == "RUI" && x.ContPrefCode == 1).Count() >
                       0;
            }
            set
            {
                if (value && !RUI1)
                {
                    //RUI4 = false;
                    //RUI5 = false;
                    ContactPreference newInstance = CreateContactPreference();
                    newInstance.ContPrefType = "RUI";
                    newInstance.ContPrefCode = 1;
                }
                else if (!value && RUI1)
                {
                    ContactPreference deleteInstance = this.ContactPreferenceList
                        .Where(x => x.ContPrefType == "RUI" && x.ContPrefCode == 1).FirstOrDefault();
                    this.DeleteContactInstance(deleteInstance);
                }

                OnPropertyChanged("RUI1");
            }
        }

        public bool RUI2
        {
            get
            {
                return this.ContactPreferenceList.Where(x => x.ContPrefType == "RUI" && x.ContPrefCode == 2).Count() >
                       0;
            }
            set
            {
                if (value && !RUI2)
                {
                    //RUI4 = false;
                    //RUI5 = false;
                    ContactPreference newInstance = CreateContactPreference();
                    newInstance.ContPrefType = "RUI";
                    newInstance.ContPrefCode = 2;
                }
                else if (!value && RUI2)
                {
                    ContactPreference deleteInstance = this.ContactPreferenceList
                        .Where(x => x.ContPrefType == "RUI" && x.ContPrefCode == 2).FirstOrDefault();
                    this.DeleteContactInstance(deleteInstance);
                }

                OnPropertyChanged("RUI2");
            }
        }

        public bool RUI4
        {
            get
            {
                return this.ContactPreferenceList.Where(x => x.ContPrefType == "RUI" && x.ContPrefCode == 4).Count() >
                       0;
            }
            set
            {
                if (value && !RUI4)
                {
                    //RUI1 = false;
                    //RUI2 = false;
                    //RUI5 = false;
                    //RUI6 = false;
                    //RUI7 = false;
                    //OnPropertyChanged("RUI1");
                    //OnPropertyChanged("RUI2");
                    //OnPropertyChanged("RUI5");
                    //OnPropertyChanged("RUI6");
                    //OnPropertyChanged("RUI7");

                    ContactPreference newInstance = CreateContactPreference();
                    newInstance.ContPrefType = "RUI";
                    newInstance.ContPrefCode = 4;
                }
                else if (!value && RUI4)
                {
                    ContactPreference deleteInstance = this.ContactPreferenceList
                        .Where(x => x.ContPrefType == "RUI" && x.ContPrefCode == 4).FirstOrDefault();
                    this.DeleteContactInstance(deleteInstance);
                }

                OnPropertyChanged("RUI4");
            }
        }

        public bool RUI5
        {
            get
            {
                return this.ContactPreferenceList.Where(x => x.ContPrefType == "RUI" && x.ContPrefCode == 5).Count() >
                       0;
            }
            set
            {
                if (value && !RUI5)
                {
                    //RUI1 = false;
                    //RUI2 = false;
                    //RUI4 = false;
                    //RUI6 = false;
                    //RUI7 = false;
                    //OnPropertyChanged("RUI1");
                    //OnPropertyChanged("RUI2");
                    //OnPropertyChanged("RUI4");
                    //OnPropertyChanged("RUI6");
                    //OnPropertyChanged("RUI7");
                    ContactPreference newInstance = CreateContactPreference();
                    newInstance.ContPrefType = "RUI";
                    newInstance.ContPrefCode = 5;
                }
                else if (!value && RUI5)
                {
                    ContactPreference deleteInstance = this.ContactPreferenceList
                        .Where(x => x.ContPrefType == "RUI" && x.ContPrefCode == 5).FirstOrDefault();

                    this.DeleteContactInstance(deleteInstance);
                }

                OnPropertyChanged("RUI5");
            }
        }

        public bool RUI6
        {
            get
            {
                return this.ContactPreferenceList.Where(x => x.ContPrefType == "RUI" && x.ContPrefCode == 6).Count() >
                       0;
            }
            set
            {
                if (value && !RUI6)
                {
                    //RUI4 = false;
                    //RUI5 = false;
                    ContactPreference newInstance = CreateContactPreference();
                    newInstance.ContPrefType = "RUI";
                    newInstance.ContPrefCode = 6;
                }
                else if (!value && RUI6)
                {
                    ContactPreference deleteInstance = this.ContactPreferenceList
                        .Where(x => x.ContPrefType == "RUI" && x.ContPrefCode == 6).FirstOrDefault();
                    this.DeleteContactInstance(deleteInstance);
                }

                OnPropertyChanged("RUI6");
            }
        }

        public bool RUI7
        {
            get
            {
                return this.ContactPreferenceList.Where(x => x.ContPrefType == "RUI" && x.ContPrefCode == 7).Count() >
                       0;
            }
            set
            {
                if (value && !RUI7)
                {
                    //RUI4 = false;
                    //RUI5 = false;
                    ContactPreference newInstance = CreateContactPreference();
                    newInstance.ContPrefType = "RUI";
                    newInstance.ContPrefCode = 7;
                }
                else if (!value && RUI7)
                {
                    ContactPreference deleteInstance = this.ContactPreferenceList
                        .Where(x => x.ContPrefType == "RUI" && x.ContPrefCode == 7).FirstOrDefault();
                    this.DeleteContactInstance(deleteInstance);
                }

                OnPropertyChanged("RUI7");
            }
        }

        private void DeleteContactInstance(ContactPreference contactPreference)
        {
            if (contactPreference != null)
                this.Delete(contactPreference);
        }


        #endregion

        #region LearnerHE

        public string UCASPERID
        {
            get
            {
                if (this.LearnerHE == null)
                    return null;
                else
                    return this.LearnerHE.UCASPERID;
            }
            set
            {
                if (this.LearnerHE == null)
                {
                    if (value != null)
                    {
                        this.LearnerHE = this.CreateLearnerHE();
                        this.LearnerHE.UCASPERID = value;
                    }
                }
                else
                {
                    if (value == null && this.LearnerHE.TTACCOM == null)
                        this.LearnerHE = null;
                    else
                        this.LearnerHE.UCASPERID = value;
                }

                OnPropertyChanged("UCASPERID");
                RefreshFormProperties();
            }
        }

        public int? TTACCOM
        {
            get
            {
                if (this.LearnerHE == null)
                    return null;
                else
                    return this.LearnerHE.TTACCOM;
            }
            set
            {
                if (this.LearnerHE == null)
                {
                    if (value != null)
                    {
                        this.LearnerHE = this.CreateLearnerHE();
                        this.LearnerHE.TTACCOM = value;
                    }
                }
                else
                {
                    if (value == null && this.LearnerHE.TTACCOM == null)
                        this.LearnerHE = null;
                    else
                        this.LearnerHE.TTACCOM = value;
                }

                OnPropertyChanged("TTACCOM");
                RefreshFormProperties();
            }
        }

        #endregion

        #region LearnerHEFinancialSupport

        public int? HEFinCash
        {
            get { return this.GetHEFinAmount(LearnerHEFinancialSupport.LearnerHEFinancialSupportTypes.Cash); }
            set
            {
                if (value != null)
                    SetHEFin(LearnerHEFinancialSupport.LearnerHEFinancialSupportTypes.Cash, (int) value);
                else
                    RemoveHEFin(LearnerHEFinancialSupport.LearnerHEFinancialSupportTypes.Cash);
                OnPropertyChanged("HEFinCash");
            }
        }

        public int? HEFinNearCash
        {
            get { return this.GetHEFinAmount(LearnerHEFinancialSupport.LearnerHEFinancialSupportTypes.NearCash); }
            set
            {
                if (value != null)
                    SetHEFin(LearnerHEFinancialSupport.LearnerHEFinancialSupportTypes.NearCash, (int) value);
                else
                    RemoveHEFin(LearnerHEFinancialSupport.LearnerHEFinancialSupportTypes.NearCash);
                OnPropertyChanged("HEFinNearCash");
            }
        }

        public int? HEFinAccommodationDiscounts
        {
            get
            {
                return this.GetHEFinAmount(LearnerHEFinancialSupport.LearnerHEFinancialSupportTypes
                    .AccommodationDiscounts);
            }
            set
            {
                if (value != null)
                    SetHEFin(LearnerHEFinancialSupport.LearnerHEFinancialSupportTypes.AccommodationDiscounts,
                        (int) value);
                else
                    RemoveHEFin(LearnerHEFinancialSupport.LearnerHEFinancialSupportTypes.AccommodationDiscounts);
                OnPropertyChanged("HEFinAccommodationDiscounts");
            }
        }

        public int? HEFinOther
        {
            get { return this.GetHEFinAmount(LearnerHEFinancialSupport.LearnerHEFinancialSupportTypes.Other); }
            set
            {
                if (value != null)
                    SetHEFin(LearnerHEFinancialSupport.LearnerHEFinancialSupportTypes.Other, (int) value);
                else
                    RemoveHEFin(LearnerHEFinancialSupport.LearnerHEFinancialSupportTypes.Other);
                OnPropertyChanged("HEFinOther");
            }
        }

        #endregion

        #endregion

        #region ILR Child Entites

        public List<ContactPreference> ContactPreferenceList = new List<ContactPreference>();
        public List<LLDDandHealthProblem> LLDDandHealthProblemList = new List<LLDDandHealthProblem>();
        public List<LearnerFAM> LearnerFAMList = new List<LearnerFAM>();

        public List<ProviderSpecLearnerMonitoring> ProviderSpecLearnerMonitoringList =
            new List<ProviderSpecLearnerMonitoring>();

        public List<LearnerEmploymentStatus> LearnerEmploymentStatusList = new List<LearnerEmploymentStatus>();
        public LearnerHE LearnerHE;
        public List<LearningDelivery> LearningDeliveryList = new List<LearningDelivery>();

        #endregion

        #region Child Entity Creation

        //internal LearnerContact CreateLearnerContact()
        //{
        //    XmlNode newNode = Node.OwnerDocument.CreateElement("LearnerContact", NSMgr.LookupNamespace("ia"));
        //    LearnerContact newInstance = new LearnerContact(newNode, NSMgr);
        //    LearnerContactList.Add(newInstance);
        //    AppendToLastOfNodeNamed(newNode, newNode.Name);
        //    return newInstance;
        //}
        public ContactPreference CreateContactPreference()
        {
            XmlNode newNode = Node.OwnerDocument.CreateElement("ContactPreference", NSMgr.LookupNamespace("ia"));
            ContactPreference newInstance = new ContactPreference(newNode, NSMgr);
            ContactPreferenceList.Add(newInstance);
            AppendToLastOfNodeNamed(newNode, newNode.Name);
            return newInstance;
        }

        public LLDDandHealthProblem CreateLLDDandHealthProblem()
        {
            XmlNode newNode = Node.OwnerDocument.CreateElement("LLDDandHealthProblem", NSMgr.LookupNamespace("ia"));
            LLDDandHealthProblem newInstance = new LLDDandHealthProblem(newNode, NSMgr);
            LLDDandHealthProblemList.Add(newInstance);
            AppendToLastOfNodeNamed(newNode, newNode.Name);
            return newInstance;
        }

        public LearnerFAM CreateLearnerFAM()
        {
            XmlNode newNode = Node.OwnerDocument.CreateElement("LearnerFAM", NSMgr.LookupNamespace("ia"));
            LearnerFAM newInstance = new LearnerFAM(newNode, NSMgr);
            LearnerFAMList.Add(newInstance);
            AppendToLastOfNodeNamed(newNode, newNode.Name);
            return newInstance;
        }

        public ProviderSpecLearnerMonitoring CreateProviderSpecLearnerMonitoring()
        {
            XmlNode newNode =
                Node.OwnerDocument.CreateElement("ProviderSpecLearnerMonitoring", NSMgr.LookupNamespace("ia"));
            ProviderSpecLearnerMonitoring newInstance = new ProviderSpecLearnerMonitoring(newNode, NSMgr);
            ProviderSpecLearnerMonitoringList.Add(newInstance);
            AppendToLastOfNodeNamed(newNode, newNode.Name);
            return newInstance;
        }

        public LearnerEmploymentStatus CreateLearnerEmploymentStatus()
        {
            XmlNode newNode = Node.OwnerDocument.CreateElement("LearnerEmploymentStatus", NSMgr.LookupNamespace("ia"));
            LearnerEmploymentStatus newInstance = new LearnerEmploymentStatus(newNode, NSMgr);
            LearnerEmploymentStatusList.Add(newInstance);
            AppendToLastOfNodeNamed(newNode, newNode.Name);
            return newInstance;
        }

        public LearnerHE CreateLearnerHE()
        {
            XmlNode newNode = Node.OwnerDocument.CreateElement("LearnerHE", NSMgr.LookupNamespace("ia"));
            LearnerHE = new LearnerHE(newNode, NSMgr);
            AppendToLastOfNodeNamed(newNode, newNode.Name);
            return LearnerHE;
        }

        public LearningDelivery CreateLearningDelivery()
        {
            XmlNode newNode = Node.OwnerDocument.CreateElement("LearningDelivery", NSMgr.LookupNamespace("ia"));
            LearningDelivery newInstance = new LearningDelivery(newNode, NSMgr);
            newInstance.LearningDeliveryPropertyChanged += LearningDeliveryPropertyChanged;
            LearningDeliveryList.Add(newInstance);
            AppendToLastOfNodeNamed(newNode, newNode.Name);
            newInstance.AimSeqNumber = LearningDeliveryList.Count();
            //Added Fault Comp Status as requested by Trvor and Lyne.
            newInstance.CompStatus = 1;
            return newInstance;
        }

        private void AppendToLastOfNodeNamed(XmlNode NewNode, string NodeName)
        {
            switch (NodeName)
            {
                case "LearnerContact":
                    if (ContactPreferenceList.Count() == 0)
                        AppendToLastOfNodeNamed(NewNode, "ContactPreference");
                    else
                        Node.InsertBefore(NewNode, ContactPreferenceList.First().Node);
                    break;
                case "ContactPreference":
                    if (LLDDandHealthProblemList.Count() == 0)
                        AppendToLastOfNodeNamed(NewNode, "LLDDandHealthProblem");
                    else
                        Node.InsertBefore(NewNode, LLDDandHealthProblemList.First().Node);
                    break;
                case "LLDDandHealthProblem":
                    if (LearnerFAMList.Count() == 0)
                        AppendToLastOfNodeNamed(NewNode, "LearnerFAM");
                    else
                        Node.InsertBefore(NewNode, LearnerFAMList.First().Node);
                    break;
                case "LearnerFAM":
                    if (ProviderSpecLearnerMonitoringList.Count() == 0)
                        AppendToLastOfNodeNamed(NewNode, "ProviderSpecLearnerMonitoring");
                    else
                        Node.InsertBefore(NewNode, ProviderSpecLearnerMonitoringList.First().Node);
                    break;
                case "ProviderSpecLearnerMonitoring":
                    if (LearnerEmploymentStatusList.Count() == 0)
                        AppendToLastOfNodeNamed(NewNode, "LearnerEmploymentStatus");
                    else
                        Node.InsertBefore(NewNode, LearnerEmploymentStatusList.First().Node);
                    break;
                case "LearnerEmploymentStatus":
                    if (LearnerHE == null)
                        AppendToLastOfNodeNamed(NewNode, "LearnerHE");
                    else
                        Node.InsertBefore(NewNode, LearnerHE.Node);
                    break;
                case "LearnerHE":
                    if (LearningDeliveryList.Count() == 0)
                        AppendToLastOfNodeNamed(NewNode, "LearningDelivery");
                    else
                        Node.InsertBefore(NewNode, LearningDeliveryList.First().Node);
                    break;
                case "LearningDelivery":
                    Node.AppendChild(NewNode);
                    break;
            }
        }

        #endregion

        #region Constructors

        internal Learner(XmlNode LearnerNode, XmlNamespaceManager NSMgr, Boolean IsFileImport)
        {
            IsFileImportLoadingRunning = IsFileImport;
            this.Node = LearnerNode;
            this.NSMgr = NSMgr;


            XmlNodeList contactPreferenceNodes = LearnerNode.SelectNodes("./ia:ContactPreference", NSMgr);
            foreach (XmlNode node in contactPreferenceNodes)
                ContactPreferenceList.Add(new ContactPreference(node, NSMgr));

            XmlNodeList llddAndHealthProblemeNodes = LearnerNode.SelectNodes("./ia:LLDDandHealthProblem", NSMgr);
            foreach (XmlNode node in llddAndHealthProblemeNodes)
                LLDDandHealthProblemList.Add(new LLDDandHealthProblem(node, NSMgr));

            XmlNodeList learnerFAMNodes = LearnerNode.SelectNodes("./ia:LearnerFAM", NSMgr);
            foreach (XmlNode node in learnerFAMNodes)
                LearnerFAMList.Add(new LearnerFAM(node, NSMgr));
            XmlNodeList providerSpecLearnerMonitoringNodes =
                LearnerNode.SelectNodes("./ia:ProviderSpecLearnerMonitoring", NSMgr);
            foreach (XmlNode node in providerSpecLearnerMonitoringNodes)
                ProviderSpecLearnerMonitoringList.Add(new ProviderSpecLearnerMonitoring(node, NSMgr));
            XmlNodeList learnerEmploymentStatusNodes = LearnerNode.SelectNodes("./ia:LearnerEmploymentStatus", NSMgr);
            foreach (XmlNode node in learnerEmploymentStatusNodes)
            {
                LearnerEmploymentStatus newInstance = new LearnerEmploymentStatus(node, NSMgr);
                newInstance.EmploymentStatusChanged += NewInstance_EmploymentStatusChanged;
                LearnerEmploymentStatusList.Add(newInstance);
            }

            XmlNode learnerHENode = LearnerNode.SelectSingleNode("./ia:LearnerHE", NSMgr);
            if (learnerHENode != null)
                LearnerHE = new LearnerHE(learnerHENode, NSMgr);

            XmlNodeList learningDeliveryNodes = LearnerNode.SelectNodes("./ia:LearningDelivery", NSMgr);
            foreach (XmlNode node in learningDeliveryNodes)
            {
                LearningDelivery ld = new LearningDelivery(node, NSMgr);
                LearningDeliveryList.Add(ld);
            }

            foreach (LearningDelivery ld in LearningDeliveryList)
            {
                ld.LearningDeliveryPropertyChanged += LearningDeliveryPropertyChanged;
            }

            IsFileImportLoadingRunning = false;
        }

        private void LearningDeliveryPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!IsFileImportLoadingRunning)
                RefreshFormProperties();
            //throw new NotImplementedException();
        }

        internal Learner(Learner MigrationLearner, XmlNode LearnerNode, XmlNamespaceManager NSMgr)
        {
            IsFileImportLoadingRunning = true;
            this.Node = LearnerNode;
            this.NSMgr = NSMgr;

            this.LearnRefNumber             = MigrationLearner.LearnRefNumber;
            this.PMUKPRN                    = MigrationLearner.PMUKPRN;
            this.CampId                     = MigrationLearner.CampId;
            this.ULN                        = MigrationLearner.ULN;
            this.FamilyName                 = MigrationLearner.FamilyName;
            this.GivenNames                 = MigrationLearner.GivenNames;
            this.DateOfBirth                = MigrationLearner.DateOfBirth;
            this.Ethnicity                  = MigrationLearner.Ethnicity;
            this.Sex                        = MigrationLearner.Sex;
            this.LLDDHealthProb             = MigrationLearner.LLDDHealthProb;
            this.NINumber                   = MigrationLearner.NINumber;
            this.PriorAttain                = MigrationLearner.PriorAttain;

            this.PostcodePrior              = MigrationLearner.PostcodePrior;
            this.PostCode                   = MigrationLearner.PostCode;
            this.AddLine1                   = MigrationLearner.AddLine1;
            this.AddLine2                   = MigrationLearner.AddLine2;
            this.AddLine3                   = MigrationLearner.AddLine3;
            this.AddLine4                   = MigrationLearner.AddLine4;

            this.Email                      = MigrationLearner.Email;
            this.TelNo                      = MigrationLearner.TelNo;

            foreach (ContactPreference migrationItem in MigrationLearner.ContactPreferenceList)
            {
                XmlNode newNode = Node.OwnerDocument.CreateElement("ContactPreference", NSMgr.LookupNamespace("ia"));
                ContactPreference newInstance = new ContactPreference(migrationItem, newNode, NSMgr);
                ContactPreferenceList.Add(newInstance);
                AppendToLastOfNodeNamed(newNode, newNode.Name);
            }

            foreach (LLDDandHealthProblem migrationItem in MigrationLearner.LLDDandHealthProblemList)
            {
                XmlNode newNode = Node.OwnerDocument.CreateElement("LLDDandHealthProblem", NSMgr.LookupNamespace("ia"));
                LLDDandHealthProblem newInstance = new LLDDandHealthProblem(migrationItem, newNode, NSMgr);
                LLDDandHealthProblemList.Add(newInstance);
                AppendToLastOfNodeNamed(newNode, newNode.Name);
            }

            foreach (LearnerFAM migrationItem in MigrationLearner.LearnerFAMList.Where(fmType =>
                !_learnFamTypeListExcludedForMigration.Contains(fmType.LearnFAMType)))
            {
                XmlNode newNode = Node.OwnerDocument.CreateElement("LearnerFAM", NSMgr.LookupNamespace("ia"));
                LearnerFAM newInstance = new LearnerFAM(migrationItem, newNode, NSMgr);
                LearnerFAMList.Add(newInstance);
                AppendToLastOfNodeNamed(newNode, newNode.Name);
            }

            foreach (ProviderSpecLearnerMonitoring migrationItem in MigrationLearner.ProviderSpecLearnerMonitoringList)
            {
                XmlNode newNode =
                    Node.OwnerDocument.CreateElement("ProviderSpecLearnerMonitoring", NSMgr.LookupNamespace("ia"));
                ProviderSpecLearnerMonitoring newInstance =
                    new ProviderSpecLearnerMonitoring(migrationItem, newNode, NSMgr);
                ProviderSpecLearnerMonitoringList.Add(newInstance);
                AppendToLastOfNodeNamed(newNode, newNode.Name);
            }

            foreach (LearnerEmploymentStatus migrationItem in MigrationLearner.LearnerEmploymentStatusToMigrate)
            {
                XmlNode newNode =
                    Node.OwnerDocument.CreateElement("LearnerEmploymentStatus", NSMgr.LookupNamespace("ia"));
                LearnerEmploymentStatus newInstance = new LearnerEmploymentStatus(migrationItem, newNode, NSMgr);
                newInstance.EmploymentStatusChanged += NewInstance_EmploymentStatusChanged;
                LearnerEmploymentStatusList.Add(newInstance);
                AppendToLastOfNodeNamed(newNode, newNode.Name);
            }

            if (MigrationLearner.LearnerHE != null)
            {
                XmlNode newNode = Node.OwnerDocument.CreateElement("LearnerHE", NSMgr.LookupNamespace("ia"));
                LearnerHE = new LearnerHE(MigrationLearner.LearnerHE, newNode, NSMgr);
                AppendToLastOfNodeNamed(newNode, newNode.Name);
            }

            foreach (LearningDelivery migrationItem in MigrationLearner.LearningDeliveriesToMigrate)
            {
                XmlNode newNode = Node.OwnerDocument.CreateElement("LearningDelivery", NSMgr.LookupNamespace("ia"));
                LearningDelivery newInstance = new LearningDelivery(migrationItem, newNode, NSMgr);
                newInstance.LearningDeliveryPropertyChanged += LearningDeliveryPropertyChanged;
                LearningDeliveryList.Add(newInstance);
                AppendToLastOfNodeNamed(newNode, newNode.Name);
            }
        }

        private void NewInstance_EmploymentStatusChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!IsFileImportLoadingRunning)
                RefreshFormProperties();
        }

        //internal void AddNewLearningDeliveryForTraineeshipIfNeeded(Learner MigrationLearner, XmlNode LearnerNode, XmlNamespaceManager NSMgr)
        //{
        //    // Migrate Traineeships -- Add new Learning Delivery Record and its FAMs as needed
        //    List<LearningDelivery> AnyLdWithLdm323 = new List<LearningDelivery>(0);


        //    foreach (LearningDelivery ld in this.LearningDeliveryList)
        //    {
        //        var LookforFAM = (from ILR.LearningDeliveryFAM ldFam in ld.LearningDeliveryFAMList
        //                          where (ldFam.LearnDelFAMType == "LDM")
        //                             && (ldFam.LearnDelFAMCode == "323")
        //                          select ldFam).FirstOrDefault();
        //        if (LookforFAM != null)
        //            AnyLdWithLdm323.Add(ld);
        //    }

        //    if (AnyLdWithLdm323.Count() > 0)
        //    {
        //        var earliestAim = (from ILR.LearningDelivery ldInner in AnyLdWithLdm323
        //                           orderby ldInner.LearnStartDate
        //                           select ldInner).FirstOrDefault();

        //        var EmpOutcomeAimList = new[] { "Z0007836", "Z0007837", "Z0007838" };
        //        var OutcomeAimList = new[] { "Z0007834", "Z0007835", "Z0007836", "Z0007837", "Z0007838" };

        //        DateTime? _learnStartDate = this.LearningDeliveryList.OrderBy(l => l.LearnStartDate).FirstOrDefault().LearnStartDate;
        //        int? _fundModel = this.LearningDeliveryList.OrderBy(l => _learnStartDate).FirstOrDefault().FundModel;

        //        LearningDelivery _empOutcome = (from l in this.LearningDeliveryList where EmpOutcomeAimList.Contains(l.LearnAimRef) select l) as LearningDelivery;
        //        LearningDelivery _outcome = (from l in this.LearningDeliveryList where OutcomeAimList.Contains(l.LearnAimRef) select l) as LearningDelivery;


        //        XmlNode newNode = Node.OwnerDocument.CreateElement("LearningDelivery", NSMgr.LookupNamespace("ia"));
        //        LearningDelivery newInstance = new LearningDelivery(newNode, NSMgr);
        //        newInstance.LearningDeliveryPropertyChanged += LearningDeliveryPropertyChanged;
        //        newInstance.LearnAimRef = "ZPROG001";
        //        newInstance.AimType = 1;
        //        newInstance.AimSeqNumber = LearningDeliveryList.Count() + 1;
        //        newInstance.LearnStartDate = _learnStartDate;
        //        newInstance.FundModel = _fundModel;
        //        newInstance.ProgType = 24;
        //        if (_empOutcome != null) newInstance.EmpOutcome = _empOutcome.EmpOutcome;
        //        if (_outcome != null) newInstance.Outcome = _outcome.Outcome;

        //        // Add Fam SOF
        //        // Where Set to the Source of funding of the earliest learning aim where LearnDelFAMType=LDM and LearnDelFAMCode=323
        //        var sof = earliestAim.GetFAM(LearningDeliveryFAM.SingleOccurrenceFAMs.SOF);


        //        // Add Fam FFI
        //        // If present set to the Fully funded indicator of the earliest learning aim where LearnDelFAMType=LDM and LearnDelFAMCode=323
        //        var ffi = earliestAim.GetFAM(LearningDeliveryFAM.SingleOccurrenceFAMs.FFI);


        //        // Add Fam LSF
        //        // Set to LSF1 if Learning Support Funding is present and continuing (where Date applies to >01/08/2014) on any aims where LearnDelFAMType=LDM and LearnDelFAMCode=323.*

        //        foreach (LearningDelivery ld in AnyLdWithLdm323)
        //        {
        //            if (ld.LearningDeliveryFAMList.Count(x => x.LearnDelFAMType == "LSF" && ld.LearnPlanEndDate > Convert.ToDateTime("01 Aug 2014")) > 0)
        //            {
        //                //XmlNode LSFNode = Node.OwnerDocument.CreateElement("LearningDeliveryFAM", NSMgr.LookupNamespace("ia"));
        //                //LearningDeliveryFAM newLsfFamInstance = new LearningDeliveryFAM( newNode, NSMgr);
        //                //newLsfFamInstance.LearnDelFAMDateFrom = Convert.ToDateTime("01 Aug 2014");
        //                //newLsfFamInstance.LearnDelFAMDateTo = ld.LearnPlanEndDate;

        //                newInstance.AddFAM(LearningDeliveryFAM.MultiOccurrenceFAMs.LSF, "1", Convert.ToDateTime("01 Aug 2014"), ld.LearnPlanEndDate);
        //                break;
        //            }
        //        }

        //        //DateFrom	Set to 01/08/2014 if Learning support funding (LSF) is recorded*
        //        // DateTo	Set to the planned end date of the programme aim or date that learning suppport is required until if earlier than the planned end date


        //        //*Note about Learning support funding:	Learning support funding for 2014/15 must be recorded on the Traineeship programme aim. 
        //        // Any learning suppport funding previously recorded on any of the Traineeship component aims should be stopped by recording a date of 31/07/2014 
        //        //      in the The Date applies to field for that aim	

        //        newInstance.AddFAM(LearningDeliveryFAM.MultiOccurrenceFAMs.LDM, "323");

        //        LearningDeliveryList.Add(newInstance);
        //        AppendToLastOfNodeNamed(newNode, newNode.Name);
        //    }
        //}

        #endregion

        #region Methods

        public void Delete(ChildEntity Child)
        {
            Node.RemoveChild(Child.Node);
            switch (Child.GetType().ToString())
            {
                //case "ILR.LearnerContact":
                //    this.LearnerContactList.Remove((LearnerContact)Child);
                //    break;
                case "ILR.ContactPreference":
                    this.ContactPreferenceList.Remove((ContactPreference) Child);
                    break;
                case "ILR.LLDDandHealthProblem":
                    this.LLDDandHealthProblemList.Remove((LLDDandHealthProblem) Child);
                    break;
                case "ILR.LearnerFAM":
                    this.LearnerFAMList.Remove((LearnerFAM) Child);
                    break;
                case "ILR.ProviderSpecLearnerMonitoring":
                    this.ProviderSpecLearnerMonitoringList.Remove((ProviderSpecLearnerMonitoring) Child);
                    break;
                case "ILR.LearnerEmploymentStatus":
                    LearnerEmploymentStatus employementStatus = (LearnerEmploymentStatus) Child;
                    employementStatus.EmploymentStatusChanged -= NewInstance_EmploymentStatusChanged;
                    this.LearnerEmploymentStatusList.Remove(employementStatus);
                    break;
                case "ILR.LearnerHE":
                    this.LearnerHE = null;
                    break;
                case "ILR.LearningDelivery":
                    LearningDelivery toDelete = (LearningDelivery) Child;
                    toDelete.LearningDeliveryPropertyChanged -= LearningDeliveryPropertyChanged;
                    foreach (LearningDelivery ld in this.LearningDeliveryList.Where(x =>
                        x.AimSeqNumber > toDelete.AimSeqNumber))
                        ld.AimSeqNumber -= 1;
                    this.LearningDeliveryList.Remove(toDelete);
                    break;
            }

            RefreshFormProperties();
        }



        #region FAM management

        public LearnerFAM GetFAM(LearnerFAM.SingleOccurrenceFAMs FAMType)
        {
            return this.LearnerFAMList.Where(x => x.LearnFAMType == FAMType.ToString()).FirstOrDefault();
        }

        public int? GetFAMCode(LearnerFAM.SingleOccurrenceFAMs FAMType)
        {
            LearnerFAM lFAM = GetFAM(FAMType);
            if (lFAM == null)
                return null;
            else
                return lFAM.LearnFAMCode;
        }

        public void SetFAM(LearnerFAM.SingleOccurrenceFAMs FAMType, int FAMCode)
        {
            LearnerFAM lFAM = GetFAM(FAMType);
            if (lFAM == null)
            {
                lFAM = this.CreateLearnerFAM();
                lFAM.LearnFAMType = FAMType.ToString();
            }

            lFAM.LearnFAMCode = FAMCode;
        }

        public void RemoveFAM(LearnerFAM.SingleOccurrenceFAMs FAMType)
        {
            LearnerFAM lFAM = GetFAM(FAMType);
            if (lFAM != null)
                Delete(lFAM);
        }

        public void RemoveFAM(LearnerFAM.MultiOccurrenceFAMs FAMType, int Code)
        {
            LearnerFAM lFAM = this.LearnerFAMList
                .Where(x => x.LearnFAMType == FAMType.ToString() && x.LearnFAMCode == Code).FirstOrDefault();
            if (lFAM != null)
                Delete(lFAM);
        }

        public List<int> GetFAMList(LearnerFAM.MultiOccurrenceFAMs FAMType)
        {
            List<int> result = new List<int>();
            foreach (LearnerFAM lFAM in this.LearnerFAMList.Where(x => x.LearnFAMType == FAMType.ToString()))
                result.Add(lFAM.LearnFAMCode);
            return result;
        }

        public void ClearFAMList(LearnerFAM.MultiOccurrenceFAMs FAMType)
        {
            List<LearnerFAM> fmList = this.LearnerFAMList
                .Where(x => x.LearnFAMType.ToUpper() == FAMType.ToString().ToUpper()).ToList();
            foreach (LearnerFAM lFAM in fmList)
                Delete(lFAM);
        }

        public void AddFAM(LearnerFAM.MultiOccurrenceFAMs FAMType, int FAMCode)
        {
            if ((FAMType == LearnerFAM.MultiOccurrenceFAMs.LSR) || (FAMType == LearnerFAM.MultiOccurrenceFAMs.NLM))
            {
                LearnerFAM tmp = this.LearnerFAMList
                    .Where(x => x.LearnFAMType == FAMType.ToString() && x.LearnFAMCode == FAMCode).FirstOrDefault();
                if (tmp == null)
                {
                    LearnerFAM lFAM = this.CreateLearnerFAM();
                    lFAM.LearnFAMType = FAMType.ToString();
                    lFAM.LearnFAMCode = FAMCode;
                }

                tmp = null;
            }
            else
            {
                LearnerFAM lFAM = this.CreateLearnerFAM();
                lFAM.LearnFAMType = FAMType.ToString();
                lFAM.LearnFAMCode = FAMCode;

            }
        }

        #endregion

        #region ProvSpecMon management

        public ProviderSpecLearnerMonitoring GetProvSpecMon(ProviderSpecLearnerMonitoring.Occurrence Occurrence)
        {
            return this.ProviderSpecLearnerMonitoringList.Where(x => x.ProvSpecLearnMonOccur == Occurrence.ToString())
                .FirstOrDefault();
        }

        public string GetProvSpecMonValue(ProviderSpecLearnerMonitoring.Occurrence Occurrence)
        {
            ProviderSpecLearnerMonitoring provSpecMon = GetProvSpecMon(Occurrence);
            if (provSpecMon == null)
                return null;
            else
                return provSpecMon.ProvSpecLearnMon;
        }

        public void SetProvSpecMon(ProviderSpecLearnerMonitoring.Occurrence Occurrence, string ProvSpecMonValue)
        {
            ProviderSpecLearnerMonitoring provSpecMon = GetProvSpecMon(Occurrence);
            if (ProvSpecMonValue != null && ProvSpecMonValue.Length != 0)
            {
                if (provSpecMon == null)
                {
                    provSpecMon = this.CreateProviderSpecLearnerMonitoring();
                    provSpecMon.ProvSpecLearnMonOccur = Occurrence.ToString();
                }

                provSpecMon.ProvSpecLearnMon = ProvSpecMonValue;
            }
            else if (provSpecMon != null)
                Delete(provSpecMon);
        }

        #endregion

        #region HE Fin management

        public LearnerHEFinancialSupport GetHEFin(LearnerHEFinancialSupport.LearnerHEFinancialSupportTypes HEFinType)
        {
            if (this.LearnerHE == null)
                return null;
            return this.LearnerHE.LearnerHEFinancialSupportList.Where(x => x.FINTYPE == (int) HEFinType)
                .FirstOrDefault();
        }

        public int? GetHEFinAmount(LearnerHEFinancialSupport.LearnerHEFinancialSupportTypes HEFinType)
        {
            LearnerHEFinancialSupport heFinType = GetHEFin(HEFinType);
            if (heFinType == null)
                return null;
            else
                return heFinType.FINAMOUNT;
        }

        public void SetHEFin(LearnerHEFinancialSupport.LearnerHEFinancialSupportTypes HEFinType, int HEFinAmount)
        {
            LearnerHEFinancialSupport heFin = GetHEFin(HEFinType);
            if (heFin == null)
            {
                if (this.LearnerHE == null)
                    this.LearnerHE = this.CreateLearnerHE();
                heFin = this.LearnerHE.CreateLearnerHEFinancialSupport();
                heFin.FINTYPE = (int) HEFinType;
            }

            heFin.FINAMOUNT = HEFinAmount;
        }

        public void RemoveHEFin(LearnerHEFinancialSupport.LearnerHEFinancialSupportTypes HEFinType)
        {
            LearnerHEFinancialSupport heFin = GetHEFin(HEFinType);
            if (heFin != null)
                this.LearnerHE.Delete(heFin);
        }

        public List<int> GetHEFinList(LearnerFAM.MultiOccurrenceFAMs FAMType)
        {
            List<int> result = new List<int>();
            foreach (LearnerFAM lFAM in this.LearnerFAMList.Where(x => x.LearnFAMType == FAMType.ToString()))
                result.Add(lFAM.LearnFAMCode);
            return result;
        }

        public void ClearHEFinList(LearnerFAM.MultiOccurrenceFAMs FAMType)
        {
            foreach (LearnerFAM lFAM in this.LearnerFAMList.Where(x =>
                x.LearnFAMType.ToString().ToUpper() == FAMType.ToString()))
                Delete(lFAM);
        }

        public void AddHEFin(LearnerHEFinancialSupport.LearnerHEFinancialSupportTypes HEFinType, int HEFinAmount)
        {
            LearnerHEFinancialSupport lFAM = this.LearnerHE.CreateLearnerHEFinancialSupport();
            lFAM.FINTYPE = (int) HEFinType;
            lFAM.FINAMOUNT = HEFinAmount;
        }

        #endregion

        public void ResequenceAimSeqNumber()
        {
            int i = 1;
            foreach (LearningDelivery ld in this.LearningDeliveryList)
            {
                ld.IsImportRunning = true;
                ld.AimSeqNumber = i;
                ld.IsImportRunning = false;
                i++;
            }
        }

        public bool HasLearningDeliveriesInFundingModel(int FundModel)
        {
            return LearningDeliveryList.Exists(ld => ld.FundModel == FundModel);

            //foreach (LearningDelivery ld in LearningDeliveryList)
            //{
            //    if (ld.FundModel == FundModel)
            //        return true;
            //}
            //return false;
        }

        public bool HasLearningDeliveriesInFundingModelX(int FundModel)
        {
            foreach (LearningDelivery ld in LearningDeliveryList)
            {
                if (ld.FundModel == FundModel)
                    return true;
            }

            return false;
        }

        private void RefreshFormProperties()
        {
            if (!IsFileImportLoadingRunning)
            {
                OnPropertyChanged("ULN");
                OnPropertyChanged("PrevUKPRN");
                OnPropertyChanged("PMUKPRN");
                OnPropertyChanged("CampId");
                OnPropertyChanged("GivenNames");
                OnPropertyChanged("FamilyName");
                OnPropertyChanged("DateOfBirth");
                OnPropertyChanged("Sex");
                OnPropertyChanged("NINumber");
                OnPropertyChanged("Ethnicity");
                OnPropertyChanged("PostcodePrior");
                OnPropertyChanged("ExcludeFromExport");
            }

            foreach (ILR.LearningDelivery ld in LearningDeliveryList.FindAll(x => x.IsComplete == false))
            {
                ld.RefreshData();
            }

            OnPropertyChanged("IsComplete");
            OnPropertyChanged("IncompleteMessage");
        }

        public void RefreshData()
        {
            RefreshFormProperties();
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
                    case "LearnRefNumber":
                        if (LearnRefNumber == null || LearnRefNumber.ToString().Length == 0)
                            return "Learner Reference Number - required\r\n";
                        if (LearnRefNumber != null)
                        {
                            String sReturn = string.Empty;
                            if (LearnRefNumber != null)
                                sReturn += CheckPropertyLength(LearnRefNumber, CLASSNAME, columnName, TABS);
                            if (Message.CountLearnRefNumberInstances(LearnRefNumber) > 1)
                                sReturn += "LearnRefNumber is not unique.\r\n";
                            return sReturn;
                        }
                        break;

                    case "PrevLearnRefNumber":
                        if (PrevLearnRefNumber != null)
                        {
                            return CheckPropertyLength(PrevLearnRefNumber, CLASSNAME, columnName, TABS);
                        }
                        break;

                    case "ULN":
                        return IsUlnValid(columnName);

                    case "PrevUKPRN":
                        if (PrevUKPRN != null)
                            return CheckPropertyLength(PrevUKPRN, CLASSNAME, columnName, TABS);
                        break;

                    case "PMUKPRN":
                        if (PMUKPRN != null)
                            return CheckPropertyLength(PMUKPRN, CLASSNAME, columnName, TABS);
                        break;

                    case "CampId":
                        if (!isCampIdValild())
                            return $"Campus Identifier: {CampId} is not valid";
                        break;

                    case "GivenNames":
                        if (GivenNames == null || GivenNames.ToString().Length == 0)
                            return "Given Names - required\r\n";
                        if (GivenNames != null)
                            return CheckPropertyLength(GivenNames, CLASSNAME, columnName, TABS);
                        break;

                    case "FamilyName":
                        if (FamilyName == null || FamilyName.ToString().Length == 0)
                            return "Family Name - required\r\n";
                        if (FamilyName != null)
                            return CheckPropertyLength(FamilyName, CLASSNAME, columnName, TABS);
                        break;

                    case "DateOfBirth":
                        if (DateOfBirth == null || DateOfBirth.ToString().Length == 0)
                            return "DateOfBirth - required\r\n";
                        if (DateOfBirth != null)
                            return CheckPropertyLength(DateOfBirth, CLASSNAME, columnName, TABS);
                        break;

                    case "Sex":

                        if (Sex == null || Sex.ToString().Length == 0)
                            return "Sex - required\r\n";
                        break;

                    case "NINumber":
                        if (NINumber != null)
                        {
                            return CheckPropertyLength(NINumber, CLASSNAME, columnName, TABS);
                        }
                        break;

                    case "Ethnicity":
                        if ((Ethnicity == null) || ((Ethnicity != null && Ethnicity.ToString().Length == 0)))
                        {
                            return "Ethnicity - required\r\n";
                        }

                        if (Ethnicity != null)
                            return CheckPropertyLength(Ethnicity, CLASSNAME, columnName, TABS);
                        break;

                    case "LLDDHealthProb":
                        if ((LLDDHealthProb == null) ||
                            ((LLDDHealthProb != null && LLDDHealthProb.ToString().Length == 0)))
                        {
                            return "LLDDHealthProb - required\r\n";
                        }

                        if (LLDDHealthProb != null)
                            return CheckPropertyLength(LLDDHealthProb, CLASSNAME, columnName, TABS);
                        break;

                    case "LLDDandHealthProblemList":
                        if ((this.LLDDHealthProb != null) && (this.LLDDHealthProb == 1)
                                                          && ((LLDDandHealthProblemList == null) ||
                                                              (LLDDandHealthProblemList != null &&
                                                               LLDDandHealthProblemList.Count < 1))
                        )
                        {
                            return "LLDDandHealthProblem Not Selected- required\r\n";
                        }

                        break;

                    case "PriorAttain":
                        if (PriorAttain != null)
                            return CheckPropertyLength(PriorAttain, CLASSNAME, columnName, TABS);
                        break;

                    case "ALSCost":
                        if (ALSCost != null)
                            return CheckPropertyLength(ALSCost, CLASSNAME, columnName, TABS);
                        break;

                    case "PlanLearnHours":
                        if (PlanLearnHours != null)
                            return CheckPropertyLength(PlanLearnHours, CLASSNAME, columnName, TABS);
                        break;

                    case "PlanEEPHours":
                        if (PlanEEPHours != null)
                            return CheckPropertyLength(PlanEEPHours, CLASSNAME, columnName, TABS);
                        break;

                    case "MathGrade":
                        if (MathGrade != null)
                            return CheckPropertyLength(MathGrade, CLASSNAME, columnName, TABS);
                        break;

                    case "EngGrade":
                        if (EngGrade != null)
                            return CheckPropertyLength(EngGrade, CLASSNAME, columnName, TABS);
                        break;

                    case "PostCode":
                        if (PostCode == null)
                            return "Postcode required\r\n";
                        //else if (PostCode != null)
                        //	return CheckPropertyLength(PostCode, CLASSNAME, columnName, TABS);
                        break;

                    case "PostcodePrior":
                        if (string.IsNullOrEmpty(PostcodePrior))
                            return "Postcode Prior to Enrolment required\r\n";
                        break;

                    case "TelNumber":
                        if (this.TelNo != null)
                        {
                            String sReturn = string.Empty;
                            if (this.TelNo.Contains(" "))
                                sReturn += "TelNumber should not have spaces\r\n";
                            if (!Int64.TryParse(this.TelNo, out _))
                                sReturn += "TelNumber Should contain only numbers\r\n";
                            return sReturn;
                        }
                        break;


                    default:
                        break;
                }

                return result;
            }
        }

        private bool isCampIdValild()
        {

            var regEx = new Regex(CAMPID_PATTERN);
            return string.IsNullOrEmpty(CampId) ? true : regEx.Match(this.CampId).Success;
        }


        private string IsUlnValid(string columnName)
        {
            string sReturn = string.Empty;
            string strULN = ULN.ToString();
            if (strULN == null || strULN.Length == 0)
                sReturn += "ULN - required\r\n";
            if (strULN != null && strULN.Length > 0)
            {
                bool result = Int64.TryParse(strULN, out _);
                if (!result)
                {
                    sReturn += String.Format("{0} has non numeric values. this will NOT be SAVED !!!", columnName);
                }
                else
                    sReturn += CheckPropertyLength(ULN, CLASSNAME, columnName, TABS);
            }

            return sReturn;

        }



        #endregion
    }
}