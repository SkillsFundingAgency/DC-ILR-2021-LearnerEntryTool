using IlrLearnerEntry.UIAutomation.Tests.WindowObjects;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.UIItems;
using TestStack.White.UIItems.TabItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.ListBoxItems;

namespace IlrLearnerEntry.UIAutomation.Tests.TabObjects.LearnersObjects
{
    public class LearningDeliveryObjects : WindowObject
    {
        internal LearningDeliveryObjects(Window window) : base(window)
        {
        }

        private Button AddLearningDeliveryButton
        {
            get
            {
                return Button("Add");
            }
        }

        private TabPage LearningDeliveryHETab
        {
            get
            {
                return _window.Get<TabPage>(SearchCriteria.ByText(" Learning Delivery HE "));
            }
        }

        public string GetStudentInstanceIdentifierValidationMessage
        {
            get
            {
                return StudentInstanceIdentifierTextBox.HelpText;
            }
        }

        public string GetUCASApplicationCodeValidationMessage
        {
            get
            {
                return UCASApplicationCodeTextBox.HelpText;
            }
        }

        public string GetQualificationOnEntryValidationMessage
        {
            get
            {
                return QualificationOnEntry.HelpText;
            }
        }

        public string GetEquivalentOrLowerQualificationValidationMessage
        {
            get
            {
                return EquivalentOrLowerQualification.HelpText;
            }
        }

        public string GetStudentSupportNumberValidationMessage
        {
            get
            {
                return StudentSupportNumberTextBox.HelpText;
            }
        }

        public string GetOccupationCodeValidationMessage
        {
            get
            {
                return OccupationCode.HelpText;
            }
        }

        public string GetSocioEconomicIndicatorValidationMessage
        {
            get
            {
                return SocioEconomicIndicator.HelpText;
            }
        }

        public string GetDomicileValidationMessage
        {
            get
            {
                return Domicile.HelpText;
            }
        }

        public string GetHECenterLocationPostCodeValidationMessage
        {
            get
            {
                return HECenterLocationPostCode.HelpText;
            }
        }

        public string GetTypeOfInstanceYearValidationMessage
        {
            get
            {
                return TypeOfInstanceYear.HelpText;
            }
        }

        public string GetModeOfStudyValidationMessage
        {
            get
            {
                return ModeOfStudy.HelpText;
            }
        }

        public string GetLevelApplicableToFundingCouncilHEIFESValidationMessage
        {
            get
            {
                return LevelApplicableToFundingCouncilHEIFES.HelpText;
            }
        }

        public string GetYearOfStudentOnThisInstanceValidationMessage
        {
            get
            {
                return YearOfStudentOnThisInstance.HelpText;
            }
        }

        public string GetCompletionOfYearOfInstanceValidationMessage
        {
            get
            {
                return CompletionOfYearOfInstance.HelpText;
            }
        }

        public string GetStudentInstanceFTEValidationMessage
        {
            get
            {
                return StudentInstanceFTE.HelpText;
            }
        }

        public string GetPercentNotThoughtByThisInstitutionValidationMessage
        {
            get
            {
                return PercentNotThoughtByThisInstitution.HelpText;
            }
        }

        public string GetPercentThoughtByFirstLDCSSubjectValidationMessage
        {
            get
            {
                return PercentTaughtByFirstLDCSSubject.HelpText;
            }
        }

        public string GetPercentTaughtBySecondLDCSSubjectValidationMessage
        {
            get
            {
                return PercentTaughtBySecondLDCSSubject.HelpText;
            }
        }

        public string GetPercentTaughtByThirdLDCSSubjectValidationMessage
        {
            get
            {
                return PercentTaughtByThirdLDCSSubject.HelpText;
            }
        }

        public string GetMajorSourceOfTuitonFeesValidationMessage
        {
            get
            {
                return MajorSourceOfTuitonFees.HelpText;
            }
        }

        public string GetNetTuitonFeeValidationMessage
        {
            get
            {
                return NetTuitonFee.HelpText;
            }
        }

        public string GetSpecialFeeIndicatorValidationMessage
        {
            get
            {
                return SpecialFeeIndicator.HelpText;
            }
        }

        public string GetGrossTuitonFeeValidationMessage
        {
            get
            {
                return GrossTuitonFee.HelpText;
            }
        }

        private TextBox StudentInstanceIdentifierTextBox
        {
            get { return TextBox("studentInstanceIdentifier"); }
        }

        private TextBox UCASApplicationCodeTextBox
        {
            get { return TextBox("ucasApplicationCode"); }
        }
        
        private ComboBox QualificationOnEntry
        {
            get { return ComboBox("qualificationOnEntry"); }
        }

        private ComboBox EquivalentOrLowerQualification
        {
            get { return ComboBox("equivalentOrLowerQualification"); }
        }

        private TextBox StudentSupportNumberTextBox
        {
            get { return TextBox("studentSupportNumber"); }
        }

        private TextBox OccupationCode
        {
            get { return TextBox("occupationCode"); }
        }

        private ComboBox SocioEconomicIndicator
        {
            get { return ComboBox("socioEconomicIndicator");}
        }

        private TextBox Domicile
        {
            get { return TextBox("domicile"); }
        }

        private TextBox HECenterLocationPostCode
        {
            get { return TextBox("heCenterLocationPostCode"); }
        }

        private ComboBox TypeOfInstanceYear
        {
            get { return ComboBox("typeOfInstanceYear"); }
        }

        private ComboBox ModeOfStudy
        {
            get { return ComboBox("modeOfStudy"); }
        }

        private ComboBox LevelApplicableToFundingCouncilHEIFES
        {
            get { return ComboBox("levelApplicableToFundingCouncilHEIFES"); }
        }

        private TextBox YearOfStudentOnThisInstance
        {
            get { return TextBox("yearOfStudentOnThisInstance"); }
        }

        private ComboBox CompletionOfYearOfInstance
        {
            get { return ComboBox("completionOfYearOfInstance"); }
        }

        private TextBox StudentInstanceFTE
        {
            get { return TextBox("studentInstanceFTE"); }
        }

        private TextBox PercentNotThoughtByThisInstitution
        {
            get { return TextBox("studentInstanceFTE"); }
        }

        private TextBox PercentTaughtByFirstLDCSSubject
        {
            get { return TextBox("percentTaughtByFirstLDCSSubject"); }
        }

        private TextBox PercentTaughtBySecondLDCSSubject
        {
            get { return TextBox("percentTaughtBySecondLDCSSubject"); }
        }

        private TextBox PercentTaughtByThirdLDCSSubject
        {
            get { return TextBox("percentTaughtByThirdLDCSSubject"); }
        }

        private ComboBox MajorSourceOfTuitonFees
        {
            get { return ComboBox("majorSourceOfTuitonFees"); }
        }

        private TextBox NetTuitonFee
        {
            get { return TextBox("netTuitonFee"); }
        }

        private ComboBox SpecialFeeIndicator
        {
            get { return ComboBox("specialFeeIndicator"); }
        }

        private TextBox GrossTuitonFee
        {
            get { return TextBox("grossTuitionFee"); }
        }

        public LearningDeliveryObjects ClickAddLearnerButton()
        {
            AddLearningDeliveryButton.Click();
            return this;
        }

        public LearningDeliveryObjects SelectLearningDeliveryHETab()
        {
            LearningDeliveryHETab.Select();
            return this;
        }

        public LearningDeliveryObjects SetStudentInstanceIdentifierTextBox(string studentInstanceIdentifier)
        {
            StudentInstanceIdentifierTextBox.Text = studentInstanceIdentifier;
            return this;
        }

        public LearningDeliveryObjects SetUCASApplicationCodeTextBox(string ucasApplicationCode)
        {
            UCASApplicationCodeTextBox.Text = ucasApplicationCode;
            return this;
        }

        public LearningDeliveryObjects SetQualificationOnEntryComboBox(string qualificationOnEntry)
        {
            QualificationOnEntry.SetValue(qualificationOnEntry);
            return this;
        }

        public LearningDeliveryObjects SetEquivalentOrLowerQualificationComboBox(string equivalentOrLowerQualification)
        {
            EquivalentOrLowerQualification.SetValue(equivalentOrLowerQualification);
            return this;
        }

        public LearningDeliveryObjects SetStudentSupportNumberTextBox(string StudentSupportNumber)
        {
            StudentSupportNumberTextBox.Text = StudentSupportNumber;
            return this;
        }

        public LearningDeliveryObjects SetOccupationCodeTextBox(string occupationCode)
        {
            OccupationCode.Text = occupationCode;
            return this;
        }

        public LearningDeliveryObjects SetSocioEconomicIndicatorComboBox(string socioEconomicIndicator)
        {
            SocioEconomicIndicator.SetValue(socioEconomicIndicator);
            return this;
        }

        public LearningDeliveryObjects SetDomicileTextBox(string domicile)
        {
            Domicile.Text = domicile;
            return this;
        }

        public LearningDeliveryObjects SetHECenterLocationPostCodeTextBox(string heCenterLocationPostCode)
        {
            HECenterLocationPostCode.Text = heCenterLocationPostCode;
            return this;
        }

        public LearningDeliveryObjects SetTypeOfInstanceYearComboBox(string typeOfInstanceYear)
        {
            TypeOfInstanceYear.SetValue(typeOfInstanceYear);
            return this;
        }

        public LearningDeliveryObjects SetModeOfStudyComboBox(string modeOfStudy)
        {
            ModeOfStudy.SetValue(modeOfStudy);
            return this;
        }

        public LearningDeliveryObjects SetLevelApplicableToFundingCouncilHEIFESComboBox(string levelApplicableToFundingCouncilHEIFES)
        {
            LevelApplicableToFundingCouncilHEIFES.SetValue(levelApplicableToFundingCouncilHEIFES);
            return this;
        }

        public LearningDeliveryObjects SetYearOfStudentOnThisInstanceTextBox(string yearOfStudentOnThisInstance)
        {
            YearOfStudentOnThisInstance.Text = yearOfStudentOnThisInstance;
            return this;
        }

        public LearningDeliveryObjects SetCompletionOfYearOfInstanceComboBox(string completionOfYearOfInstance)
        {
            CompletionOfYearOfInstance.SetValue(completionOfYearOfInstance);
            return this;
        }

        public LearningDeliveryObjects SetStudentInstanceFTETextBox(string studentInstanceFTE)
        {
            StudentInstanceFTE.Text = studentInstanceFTE;
            return this;
        }

        public LearningDeliveryObjects SetPercentNotThoughtByThisInstitutionTextBox(string percentNotThoughtByThisInstitution)
        {
            PercentNotThoughtByThisInstitution.Text = percentNotThoughtByThisInstitution;
            return this;
        }

        public LearningDeliveryObjects SetPercentTaughtByFirstLDCSSubjectTextBox(string percentTaughtByFirstLDCSSubject)
        {
            PercentTaughtByFirstLDCSSubject.Text = percentTaughtByFirstLDCSSubject;
            return this;
        }

        public LearningDeliveryObjects SetPercentTaughtBySecondLDCSSubjectTextBox(string percentTaughtBySecondLDCSSubject)
        {
            PercentTaughtBySecondLDCSSubject.Text = percentTaughtBySecondLDCSSubject;
            return this;
        }

        public LearningDeliveryObjects SetPercentTaughtByThirdLDCSSubjectTextBox(string percentTaughtByThirdLDCSSubject)
        {
            PercentTaughtByThirdLDCSSubject.Text = percentTaughtByThirdLDCSSubject;
            return this;
        }

        public LearningDeliveryObjects SetMajorSourceOfTuitonFeesComboBox(string majorSourceOfTuitonFees)
        {
            MajorSourceOfTuitonFees.SetValue(majorSourceOfTuitonFees);
            return this;
        }

        public LearningDeliveryObjects SetNetTuitonFeeTextBox(string netTuitonFee)
        {
            NetTuitonFee.Text = netTuitonFee;
            return this;
        }

        public LearningDeliveryObjects SetSpecialFeeIndicatorComboBox(string specialFeeIndicator)
        {
            SpecialFeeIndicator.SetValue(specialFeeIndicator);
            return this;
        }

        public LearningDeliveryObjects SetGrossTuitonFeeTextBox(string grossTuitonFee)
        {
            GrossTuitonFee.Text = grossTuitonFee;
            return this;
        }
    }
}
 