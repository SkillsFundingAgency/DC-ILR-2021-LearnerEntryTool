using System.Collections.Generic;

namespace ILR
{
    public static  class Schema
    {
        private static readonly Dictionary<string, int> MaxLengths = new Dictionary<string, int> {
            {"CollectionDetails.Collection",3},
            {"CollectionDetails.Year",4},
            {"CollectionDetails.FilePreparationDate",10},

            {"Source.ProtectiveMarking",30},
            {"Source.UKPRN",8},
            {"Source.SoftwareSupplier",40},
            {"Source.SoftwarePackage",30},
            {"Source.Release",20},
            {"Source.SerialNo",2},
            {"Source.DateTime",10},
            {"Source.ReferenceData",100},
            {"Source.ComponentSetVersion",20},

            {"SourceFile.DateTime",10},
            {"SourceFile.FilePreparationDate",10},
            {"SourceFile.Release",20},
            {"SourceFile.SerialNo",2},
            {"SourceFile.SoftwarePackage",30},
            {"SourceFile.SoftwareSupplier",40},
            {"SourceFile.SourceFileName",50},

            {"LearningProvider.UKPRN",8},

            {"Learner.LearnRefNumber",12},
            {"Learner.PrevLearnRefNumber",12},
            {"Learner.PrevUKPRN",8},
            {"Learner.PMUKPRN",8},
            {"Learner.CampId",8},
            {"Learner.ULN",10},
            {"Learner.FamilyName",100},
            {"Learner.GivenNames",100},
            {"Learner.DateOfBirth",10},
            {"Learner.Ethnicity",2},
            {"Learner.Sex",1},
            {"Learner.LLDDHealthProb",1},
            {"Learner.NINumber",9},
            {"Learner.PriorAttain",2},
            {"Learner.Accom",1},
            {"Learner.ALSCost",6},
            {"Learner.PlanLearnHours",4},
            {"Learner.PlanEEPHours",4},
            {"Learner.MathGrade",4},
            {"Learner.EngGrade",4},
            {"Learner.PostCode",8},
            {"Learner.PostcodePrior",8},
            {"Learner.AddLine1",50},
            {"Learner.AddLine2",50},
            {"Learner.AddLine3",50},
            {"Learner.AddLine4",50},
            {"Learner.TelNo",18},
            {"Learner.Email",100},

            {"LearnerContact.ContType",1},
            {"LearnerContact.Email",100},
            {"LearnerContact.LocType",1},
            {"LearnerContact.PostCode",8},
            {"LearnerContact.TelNumber",18},

            {"PostAdd.AddLine1",50},
            {"PostAdd.AddLine2",50},
            {"PostAdd.AddLine3",50},
            {"PostAdd.AddLine4",50},

             {"ContactPreference.ContPrefType",3},
            {"ContactPreference.ContPrefCode",1},

            {"LLDDandHealthProblem.LLDDCat",2},
            {"LLDDandHealthProblem.PrimaryLLDD",1},

            {"LearnerFAM.LearnFAMType",3},
            {"LearnerFAM.LearnFAMCode",3},

            {"ProviderSpecLearnerMonitoring.ProvSpecLearnMonOccur",1},
            {"ProviderSpecLearnerMonitoring.ProvSpecLearnMon",20},

            {"LearnerEmploymentStatus.EmpStat",2},
            {"LearnerEmploymentStatus.DateEmpStatApp",10},
            {"LearnerEmploymentStatus.EmpId",9},

            {"EmploymentStatusMonitoring.ESMType",3},
            {"EmploymentStatusMonitoring.ESMCode",2},

            {"LearnerHE.UCASPERID",10},
            {"LearnerHE.TTACCOM",1},

            {"LearnerHEFinancialSupport.FINTYPE",1},
            {"LearnerHEFinancialSupport.FINAMOUNT",6},

            {"LearningDelivery.LearnAimRef",8},
            {"LearningDelivery.AimType",1},
            {"LearningDelivery.AimSeqNumber",2},
            {"LearningDelivery.LearnStartDate",10},
            {"LearningDelivery.OrigLearnStartDate",10},
            {"LearningDelivery.LearnPlanEndDate",10},
            {"LearningDelivery.FundModel",2},
            {"LearningDelivery.PHours",4},
            {"LearningDelivery.OTJActhours",4},
            {"LearningDelivery.ProgType",2},
            {"LearningDelivery.FworkCode",3},
            {"LearningDelivery.PwayCode",3},
            {"LearningDelivery.StdCode",5 },
            {"LearningDelivery.PartnerUKPRN",8},
            {"LearningDelivery.DelLocPostCode",8},
            {"LearningDelivery.LSDPostcode",8},
             {"LearningDelivery.AddHours",4},
            {"LearningDelivery.PriorLearnFundAdj",2},
            {"LearningDelivery.OtherFundAdj",3},
            {"LearningDelivery.ConRefNumber",20},
            {"LearningDelivery.EPAOrgID",7},
            {"LearningDelivery.EmpOutcome",1},
             {"LearningDelivery.CompStatus",1},
            {"LearningDelivery.LearnActEndDate",10},
            {"LearningDelivery.WithdrawReason",2},
            {"LearningDelivery.Outcome",1},
            {"LearningDelivery.AchDate",10},
            {"LearningDelivery.OutGrade",6},
            {"LearningDelivery.SWSupAimId",36},

            {"LearningDeliveryFAM.LearnDelFAMType",3},
            {"LearningDeliveryFAM.LearnDelFAMCode",5},
            {"LearningDeliveryFAM.LearnDelFAMDateFrom",10},
            {"LearningDeliveryFAM.LearnDelFAMDateTo",10},

            {"LearningDeliveryWorkPlacement.WorkPlaceStartDate",10},
             {"LearningDeliveryWorkPlacement.WorkPlaceEndDate",10},
            {"LearningDeliveryWorkPlacement.WorkPlaceHours",4},
            {"LearningDeliveryWorkPlacement.WorkPlaceMode",1},
            {"LearningDeliveryWorkPlacement.WorkPlaceEmpId",9},

            {"AppFinRecord.AFinType",3},
            {"AppFinRecord.AFinCode",2},
            {"AppFinRecord.AFinDate",10},
            {"AppFinRecord.AFinAmount",6},

            {"ApprenticeshipFinancialRecord.aFinAmount",6},
            {"ApprenticeshipFinancialRecord.aFinCode",2},
            {"ApprenticeshipFinancialRecord.aFinDate",10},
            {"ApprenticeshipFinancialRecord.aFinType",3},

            {"ProviderSpecDeliveryMonitoring.ProvSpecDelMonOccur",1},
            {"ProviderSpecDeliveryMonitoring.ProvSpecDelMon",20},

            {"LearningDeliveryHE.NUMHUS",20},
            {"LearningDeliveryHE.SSN",13},
            {"LearningDeliveryHE.QUALENT3",3},
            {"LearningDeliveryHE.SOC2000",4},
            {"LearningDeliveryHE.SEC",1},
            {"LearningDeliveryHE.UCASAPPID",9},
            {"LearningDeliveryHE.TYPEYR",1},
            {"LearningDeliveryHE.MODESTUD",2},
            {"LearningDeliveryHE.FUNDLEV",2},
            {"LearningDeliveryHE.FUNDCOMP",1},
            {"LearningDeliveryHE.STULOAD",4},
            {"LearningDeliveryHE.YEARSTU",2},
            {"LearningDeliveryHE.MSTUFEE",2},
            {"LearningDeliveryHE.PCOLAB",4},
            {"LearningDeliveryHE.PCFLDCS",4},
            {"LearningDeliveryHE.PCSLDCS",4},
            {"LearningDeliveryHE.PCTLDCS",4},
            {"LearningDeliveryHE.SPECFEE",1},
            {"LearningDeliveryHE.NETFEE",6},
            {"LearningDeliveryHE.GROSSFEE",6},
            {"LearningDeliveryHE.DOMICILE",2},
            {"LearningDeliveryHE.ELQ",1},
            {"LearningDeliveryHE.HEPostCode",8},

            {"LearnerDestinationandProgression.LearnRefNumber",12},
            {"LearnerDestinationandProgression.ULN",10},

            {"DPOutcome.OutType",3},
            {"DPOutcome.OutCode",3},
            {"DPOutcome.OutStartDate",10},
            {"DPOutcome.OutEndDate",10},
            {"DPOutcome.OutCollDate",10}
        };
        public static int GetMaxLength(string Attribute)
        {
            return MaxLengths[Attribute];
        }
        public static Dictionary<string, int> GetMaxLengths()
        {
            return MaxLengths;
        }
    }
}
