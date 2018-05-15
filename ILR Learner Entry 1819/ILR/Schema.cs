﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ILR
{
    public class Schema
    {
        public Dictionary<string, int> MaxLengths = new Dictionary<string, int> {
			{"CollectionDetails.Collection",3},
			{"CollectionDetails.FilePreparationDate",10},
			{"CollectionDetails.Year",4},
			{"ContactPreference.ContPrefCode",1},
			{"ContactPreference.ContPrefType",3},
			{"DPOutcome.OutCode",3},
			{"DPOutcome.OutCollDate",10},
			{"DPOutcome.OutEndDate",10},
			{"DPOutcome.OutStartDate",10},
			{"DPOutcome.OutType",3},
			{"EmploymentStatusMonitoring.ESMCode",2},
			{"EmploymentStatusMonitoring.ESMType",3},
			{"Learner.Accom",1},
			{"Learner.ALSCost",6},
			{"Learner.DateOfBirth",10},
			{"Learner.EngGrade",4},
			{"Learner.Ethnicity",2},
			{"Learner.FamilyName",100},
			{"Learner.GivenNames",100},
            {"Learner.CampId",8},

            {"Learner.AddLine1",50},
            {"Learner.AddLine2",50},
            {"Learner.AddLine3",50},
            {"Learner.AddLine4",50},
            {"Learner.PostCode",8},
            {"Learner.PostcodePrior",8},
            {"Learner.TelNo",18},
            {"Learner.Email",100},

            { "Learner.LearnRefNumber",12},
			{"Learner.LLDDHealthProb",1},
			{"Learner.MathGrade",4},
			{"Learner.NINumber",9},
			{"Learner.PlanEEPHours",4},
			{"Learner.PlanLearnHours",4},
			{"Learner.PrevLearnRefNumber",12},
			{"Learner.PrevUKPRN",8},
			{"Learner.PriorAttain",2},
            {"Learner.PMUKPRN",8},
            {"Learner.Sex",1},
			{"Learner.ULN",10},
			{"LearnerContact.ContType",1},
			{"LearnerContact.Email",100},
			{"LearnerContact.LocType",1},
			{"LearnerContact.PostCode",8},
			{"LearnerContact.TelNumber",18},
			{"LearnerDestinationandProgression.LearnRefNumber",12},
			{"LearnerDestinationandProgression.ULN",10},
			{"LearnerEmploymentStatus.DateEmpStatApp",10},
			{"LearnerEmploymentStatus.EmpId",9},
			{"LearnerEmploymentStatus.EmpStat",2},
			{"LearnerFAM.LearnFAMCode",3},
			{"LearnerFAM.LearnFAMType",3},
			{"LearnerHE.TTACCOM",1},
			{"LearnerHE.UCASPERID",10},
			{"LearnerHEFinancialSupport.FINAMOUNT",6},
			{"LearnerHEFinancialSupport.FINTYPE",1},
			{"LearningDelivery.AchDate",10},
			{"LearningDelivery.AddHours",4},
			{"LearningDelivery.AimSeqNumber",2},
			{"LearningDelivery.AimType",1},
			{"LearningDelivery.CompStatus",1},
			{"LearningDelivery.ConRefNumber",20},
			{"LearningDelivery.DelLocPostCode",8},
			{"LearningDelivery.EmpOutcome",1},
            {"LearningDelivery.EPAOrgID",7},
            {"LearningDelivery.FundModel",2},
			{"LearningDelivery.FworkCode",3},
			{"LearningDelivery.LearnActEndDate",10},
			{"LearningDelivery.LearnAimRef",8},
			{"LearningDelivery.LearnPlanEndDate",10},
			{"LearningDelivery.LearnStartDate",10},
			{"LearningDelivery.OrigLearnStartDate",10},
			{"LearningDelivery.OtherFundAdj",3},
			{"LearningDelivery.Outcome",1},
			{"LearningDelivery.OutGrade",6},
			{"LearningDelivery.PartnerUKPRN",8},
			{"LearningDelivery.PriorLearnFundAdj",2},
			{"LearningDelivery.ProgType",2},
			{"LearningDelivery.PwayCode",3},
			{"LearningDelivery.SWSupAimId",36},
			{"LearningDelivery.StdCode",5 },
			{"LearningDelivery.WithdrawReason",2},
			{"LearningDeliveryFAM.LearnDelFAMCode",5},
			{"LearningDeliveryFAM.LearnDelFAMDateFrom",10},
			{"LearningDeliveryFAM.LearnDelFAMDateTo",10},
			{"LearningDeliveryFAM.LearnDelFAMType",3},
			{"LearningDeliveryHE.DOMICILE",2},
			{"LearningDeliveryHE.ELQ",1},
			{"LearningDeliveryHE.FUNDCOMP",1},
			{"LearningDeliveryHE.FUNDLEV",2},
			{"LearningDeliveryHE.GROSSFEE",6},
			{"LearningDeliveryHE.HEPostCode",8},
			{"LearningDeliveryHE.MODESTUD",2},
			{"LearningDeliveryHE.MSTUFEE",2},
			{"LearningDeliveryHE.NETFEE",6},
			{"LearningDeliveryHE.NUMHUS",20},
			{"LearningDeliveryHE.PCFLDCS",4},
			{"LearningDeliveryHE.PCOLAB",4},
			{"LearningDeliveryHE.PCSLDCS",4},
			{"LearningDeliveryHE.PCTLDCS",4},
			{"LearningDeliveryHE.QUALENT3",3},
			{"LearningDeliveryHE.SEC",1},
			{"LearningDeliveryHE.SOC2000",4},
			{"LearningDeliveryHE.SPECFEE",1},
			{"LearningDeliveryHE.SSN",13},
			{"LearningDeliveryHE.STULOAD",4},
			{"LearningDeliveryHE.TYPEYR",1},
			{"LearningDeliveryHE.UCASAPPID",9},
			{"LearningDeliveryHE.YEARSTU",2},
			{"LearningDeliveryWorkPlacement.WorkPlaceEmpId",9},
			{"LearningDeliveryWorkPlacement.WorkPlaceEndDate",10},
			{"LearningDeliveryWorkPlacement.WorkPlaceMode",1},
			{"LearningDeliveryWorkPlacement.WorkPlaceStartDate",10},
            {"LearningDeliveryWorkPlacement.WorkPlaceHours",4},
            {"LearningProvider.UKPRN",8},
			{"LLDDandHealthProblem.LLDDCat",2},
			{"LLDDandHealthProblem.PrimaryLLDD",1},
			{"PostAdd.AddLine1",50},
			{"PostAdd.AddLine2",50},
			{"PostAdd.AddLine3",50},
			{"PostAdd.AddLine4",50},
			{"ProviderSpecDeliveryMonitoring.ProvSpecDelMon",20},
			{"ProviderSpecDeliveryMonitoring.ProvSpecDelMonOccur",1},
			{"ProviderSpecLearnerMonitoring.ProvSpecLearnMon",20},
			{"ProviderSpecLearnerMonitoring.ProvSpecLearnMonOccur",1},
			{"Source.ComponentSetVersion",20},
			{"Source.DateTime",10},
			{"Source.ProtectiveMarking",100},
			{"Source.ReferenceData",100},
			{"Source.Release",20},
			{"Source.SerialNo",2},
			{"Source.SoftwarePackage",30},
			{"Source.SoftwareSupplier",40},
			{"Source.UKPRN",8},
			{"SourceFile.DateTime",10},
			{"SourceFile.FilePreparationDate",10},
			{"SourceFile.Release",20},
			{"SourceFile.SerialNo",2},
			{"SourceFile.SoftwarePackage",30},
			{"SourceFile.SoftwareSupplier",40},
			{"SourceFile.SourceFileName",50},
			{"AppFinRecord.AFinAmount",6},
			{"AppFinRecord.AFinCode",2},
			{"AppFinRecord.AFinDate",10},
			{"AppFinRecord.AFinType",3},
            {"ApprenticeshipFinancialRecord.aFinAmount",6},
            {"ApprenticeshipFinancialRecord.aFinCode",2},
            {"ApprenticeshipFinancialRecord.aFinDate",10},
            {"ApprenticeshipFinancialRecord.aFinType",3}
        };
        public int GetMaxLength(string Attribute)
        {
            return this.MaxLengths[Attribute];
        }
        public Dictionary<string, int> GetMaxLengths()
        {
            return this.MaxLengths;
        }
    }
}
