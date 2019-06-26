using IlrLearnerEntry.UIAutomation.Tests.TestHelper;
using IlrLearnerEntry.UIAutomation.Tests.WindowObjects;
using Xunit;
using IlrLearnerEntry.UIAutomation.Tests.TabObjects.LearnersObjects;

namespace IlrLearnerEntry.UIAutomation.Tests.Tests
{
    [Collection("Application Collection")]
    public class LearningDelivery_LearningDeliveryHETests : TestsSetup
    {
        [Fact]
        [Trait("Category", "Regression")]
        public void StudentInstanceIdentifier_WhenCorrectValueEntered_ShouldAcceptWithNoValidationErrors()
        {
            // Arrange
            string studentInstanceIdentifier = RandomStrings.GetRandomAlphabets(10);

            // Act
            Learners_LearnerListObjects learnerTab = WindowObjects.Windows.Main.SelectLearnerTab; 
            learnerTab.ClickAddLearnerButton();
            LearningDeliveryObjects learningDeliveryTab = WindowObjects.Windows.Main.SelectLearningDeliveryTab;
            learningDeliveryTab.ClickAddLearnerButton();
            learningDeliveryTab.SelectLearningDeliveryHETab();
            learningDeliveryTab.SetStudentInstanceIdentifierTextBox(studentInstanceIdentifier);

            // Assert
            var error = learningDeliveryTab.GetStudentInstanceIdentifierValidationMessage;
            TakeScreenShot();  
            Assert.True(string.IsNullOrEmpty(error), $"Validation failed for an invalid Student Instance Identifier {studentInstanceIdentifier}");
        }

        [Fact]
        [Trait("Category", "Regression")]
        public void UCASApplicationCode_WhenCorrectValueEntered_ShouldAcceptWithNoValidationErrors()
        {
            // Arrange
            string UCASApplicationCode = RandomStrings.GetRandomAlphabets(10);

            // Act
            Learners_LearnerListObjects learnerTab = WindowObjects.Windows.Main.SelectLearnerTab; 
            learnerTab.ClickAddLearnerButton();
            LearningDeliveryObjects learningDeliveryTab = WindowObjects.Windows.Main.SelectLearningDeliveryTab;
            learningDeliveryTab.ClickAddLearnerButton();
            learningDeliveryTab.SelectLearningDeliveryHETab();
            learningDeliveryTab.SetUCASApplicationCodeTextBox(UCASApplicationCode);

            // Assert
            var error = learningDeliveryTab.GetUCASApplicationCodeValidationMessage;
            TakeScreenShot();  
            Assert.True(string.IsNullOrEmpty(error), $"Vaidation failed for invalid UCAS application code {UCASApplicationCode}");
        }

        [Fact]
        [Trait("Category", "Regression")]
        public void QualificationsOnEntry_WhenCorrectSelectionIsMade_ShouldAcceptWithNoValidationErrors()
        {
            // Arrange
            string qualificationOnEntry = "X06 - Not known";

            // Act
            Learners_LearnerListObjects learnerTab = WindowObjects.Windows.Main.SelectLearnerTab;
            learnerTab.ClickAddLearnerButton();
            LearningDeliveryObjects learningDeliveryTab = WindowObjects.Windows.Main.SelectLearningDeliveryTab;
            learningDeliveryTab.ClickAddLearnerButton();
            learningDeliveryTab.SelectLearningDeliveryHETab();
            learningDeliveryTab.SetQualificationOnEntryComboBox(qualificationOnEntry);

            // Assert 
            var error = learningDeliveryTab.GetQualificationOnEntryValidationMessage;
            TakeScreenShot();  
            Assert.True(string.IsNullOrEmpty(error), $"Validation failed for invalid Qualification on entry {qualificationOnEntry}");
        }

        [Fact]
        [Trait("Category", "Regression")]
        public void EquivalentOrLowerQualification_WhenCorrectSelectionIsMade_ShouldAcceptWithNoValidationErrors()
        {
            // Arrange
            string equivalentOrLowerQualification = "9 - Not required";

            // Act
            Learners_LearnerListObjects learnerTab = WindowObjects.Windows.Main.SelectLearnerTab; 
            learnerTab.ClickAddLearnerButton();
            LearningDeliveryObjects learningDeliveryTab = WindowObjects.Windows.Main.SelectLearningDeliveryTab;
            learningDeliveryTab.ClickAddLearnerButton();
            learningDeliveryTab.SelectLearningDeliveryHETab();
            learningDeliveryTab.SetEquivalentOrLowerQualificationComboBox(equivalentOrLowerQualification);

            // Assert 
            var error = learningDeliveryTab.GetEquivalentOrLowerQualificationValidationMessage;
            TakeScreenShot();  
            Assert.True(string.IsNullOrEmpty(error), $"Validation failed for invalid Equivalent or lower qualification {equivalentOrLowerQualification}");
        }

        [Fact]
        [Trait("Category", "Regression")]
        public void StudentSupportNumber_WhenCorrectValueEntered_ShouldAcceptWithNoValidationErrors()
        {
            // Arrange
            string studentSupportNumber = RandomStrings.GetRandomAlphabets(10);

            // Act
            Learners_LearnerListObjects learnerTab = WindowObjects.Windows.Main.SelectLearnerTab; 
            learnerTab.ClickAddLearnerButton();
            LearningDeliveryObjects learningDeliveryTab = WindowObjects.Windows.Main.SelectLearningDeliveryTab;
            learningDeliveryTab.ClickAddLearnerButton();
            learningDeliveryTab.SelectLearningDeliveryHETab();
            learningDeliveryTab.SetStudentSupportNumberTextBox(studentSupportNumber);

            // Assert
            var error = learningDeliveryTab.GetStudentSupportNumberValidationMessage;
            TakeScreenShot();  
            Assert.True(string.IsNullOrEmpty(error), $"Validation failed for invalid Student support number {studentSupportNumber}");
        }

        [Fact]
        [Trait("Category", "Regression")]
        public void OccupationCode_WhenCorrectValueEntered_ShouldAcceptWithNoValidationErrors()
        {
            // Arrange
            string occupationCode = "1234";

            // Act
            Learners_LearnerListObjects learnerTab = WindowObjects.Windows.Main.SelectLearnerTab; 
            learnerTab.ClickAddLearnerButton();
            LearningDeliveryObjects learningDeliveryTab = WindowObjects.Windows.Main.SelectLearningDeliveryTab;
            learningDeliveryTab.ClickAddLearnerButton();
            learningDeliveryTab.SelectLearningDeliveryHETab();
            learningDeliveryTab.SetOccupationCodeTextBox(occupationCode);

            // Assert 
            var error = learningDeliveryTab.GetOccupationCodeValidationMessage;
            TakeScreenShot();  
            Assert.True(string.IsNullOrEmpty(error), $"Validation failed for invalid Occupation code {occupationCode}");
        }

        [Fact]
        [Trait("Category", "Regression")]
        public void SocioEconomicIndicator_WhenCorrectSelectionIsMade_ShouldAcceptWithNoValidationErrors()
        {
            // Arrange
            string socioEconomicIndicator = "9 - Not classified";

            // Act
            Learners_LearnerListObjects learnerTab = WindowObjects.Windows.Main.SelectLearnerTab; 
            learnerTab.ClickAddLearnerButton();
            LearningDeliveryObjects learningDeliveryTab = WindowObjects.Windows.Main.SelectLearningDeliveryTab;
            learningDeliveryTab.ClickAddLearnerButton();
            learningDeliveryTab.SelectLearningDeliveryHETab();
            learningDeliveryTab.SetSocioEconomicIndicatorComboBox(socioEconomicIndicator);

            // Assert 
            var error = learningDeliveryTab.GetSocioEconomicIndicatorValidationMessage;
            TakeScreenShot();  
            Assert.True(string.IsNullOrEmpty(error), $"Validation faild for invalid Socio-economic indicator {socioEconomicIndicator}");
        }


        [Fact]
        [Trait("Category", "Regression")]
        public void Domicile_WhenCorrectValueEntered_ShouldAcceptWithNoValidationErrors()
        {
            // Arrange
            string domicile = "AB";

            // Act
            Learners_LearnerListObjects learnerTab = WindowObjects.Windows.Main.SelectLearnerTab; 
            learnerTab.ClickAddLearnerButton();
            LearningDeliveryObjects learningDeliveryTab = WindowObjects.Windows.Main.SelectLearningDeliveryTab;
            learningDeliveryTab.ClickAddLearnerButton();
            learningDeliveryTab.SelectLearningDeliveryHETab();
            learningDeliveryTab.SetDomicileTextBox(domicile);

            // Assert 
            var error = learningDeliveryTab.GetDomicileValidationMessage;
            TakeScreenShot();  
            Assert.True(string.IsNullOrEmpty(error), $"Validation failed for invalid Domicile {domicile}");
        }

        [Fact]
        [Trait("Category", "Regression")]
        public void HECenterLocationPostCode_WhenCorrectValueEntered_ShouldAcceptWithNoValidationErrors()
        {
            // Arrange
            string heCenterLocationPostCode = "12345678";

            // Act
            Learners_LearnerListObjects learnerTab = WindowObjects.Windows.Main.SelectLearnerTab; 
            learnerTab.ClickAddLearnerButton();
            LearningDeliveryObjects learningDeliveryTab = WindowObjects.Windows.Main.SelectLearningDeliveryTab;
            learningDeliveryTab.ClickAddLearnerButton();
            learningDeliveryTab.SelectLearningDeliveryHETab();
            learningDeliveryTab.SetHECenterLocationPostCodeTextBox(heCenterLocationPostCode);

            // Assert 
            var error = learningDeliveryTab.GetHECenterLocationPostCodeValidationMessage;
            TakeScreenShot();  
            Assert.True(string.IsNullOrEmpty(error), $"Validation failed for invalid HE centre location postcode {heCenterLocationPostCode}");
        }

        [Fact]
        [Trait("Category", "Regression")]
        public void TypeOfInstanceYear_WhenCorrectSelectionIsMade_ShouldAcceptWithNoValidationErrors()
        {
            // Arrange
            string typeOfInstanceYear = "1 - Year of instance contained within the reporting period 01 August to 31 July";

            // Act
            Learners_LearnerListObjects learnerTab = WindowObjects.Windows.Main.SelectLearnerTab; 
            learnerTab.ClickAddLearnerButton();
            LearningDeliveryObjects learningDeliveryTab = WindowObjects.Windows.Main.SelectLearningDeliveryTab;
            learningDeliveryTab.ClickAddLearnerButton();
            learningDeliveryTab.SelectLearningDeliveryHETab();
            learningDeliveryTab.SetTypeOfInstanceYearComboBox(typeOfInstanceYear);

            // Assert 
            var error = learningDeliveryTab.GetTypeOfInstanceYearValidationMessage;
            TakeScreenShot();  
            Assert.True(string.IsNullOrEmpty(error), $"Validation failed for invalid Type of instance year {typeOfInstanceYear}");
        }

        [Fact]
        [Trait("Category", "Regression")]
        public void ModeOfStudy_WhenCorrectSelectionIsMade_ShouldAcceptWithNoValidationErrors()
        {
            // Arrange
            string modeOfStudy = "3 - Part-time";

            // Act
            Learners_LearnerListObjects learnerTab = WindowObjects.Windows.Main.SelectLearnerTab; 
            learnerTab.ClickAddLearnerButton();
            LearningDeliveryObjects learningDeliveryTab = WindowObjects.Windows.Main.SelectLearningDeliveryTab;
            learningDeliveryTab.ClickAddLearnerButton();
            learningDeliveryTab.SelectLearningDeliveryHETab();
            learningDeliveryTab.SetModeOfStudyComboBox(modeOfStudy);

            // Assert 
            var error = learningDeliveryTab.GetModeOfStudyValidationMessage;
            TakeScreenShot();  
            Assert.True(string.IsNullOrEmpty(error), $"Validation failed for invalid Mode of study {modeOfStudy}");
        }

        [Fact]
        [Trait("Category", "Regression")]
        public void LevelApplicableToFundingCouncilHEIFES_WhenCorrectSelectionIsMade_ShouldAcceptWithNoValidationErrors()
        {
            // Arrange
            string levelApplicableToFundingCouncilHEIFES = "10 - Undergraduate";

            // Act
            Learners_LearnerListObjects learnerTab = WindowObjects.Windows.Main.SelectLearnerTab; 
            learnerTab.ClickAddLearnerButton();
            LearningDeliveryObjects learningDeliveryTab = WindowObjects.Windows.Main.SelectLearningDeliveryTab;
            learningDeliveryTab.ClickAddLearnerButton();
            learningDeliveryTab.SelectLearningDeliveryHETab();
            learningDeliveryTab.SetLevelApplicableToFundingCouncilHEIFESComboBox(levelApplicableToFundingCouncilHEIFES);

            // Assert 
            var error = learningDeliveryTab.GetLevelApplicableToFundingCouncilHEIFESValidationMessage;
            TakeScreenShot();  
            Assert.True(string.IsNullOrEmpty(error), $"Validation failed for invalid Level applicable to funding council HEIFES {levelApplicableToFundingCouncilHEIFES}");
        }

        [Fact]
        [Trait("Category", "Regression")]
        public void YearOfStudentOnThisInstance_WhenCorrectValueEntered_ShouldAcceptWithNoValidationErrors()
        {
            // Arrange
            string yearOfStudentOnThisInstance = "10";

            // Act
            Learners_LearnerListObjects learnerTab = WindowObjects.Windows.Main.SelectLearnerTab; 
            learnerTab.ClickAddLearnerButton();
            LearningDeliveryObjects learningDeliveryTab = WindowObjects.Windows.Main.SelectLearningDeliveryTab;
            learningDeliveryTab.ClickAddLearnerButton();
            learningDeliveryTab.SelectLearningDeliveryHETab();
            learningDeliveryTab.SetYearOfStudentOnThisInstanceTextBox(yearOfStudentOnThisInstance);

            // Assert 
            var error = learningDeliveryTab.GetYearOfStudentOnThisInstanceValidationMessage;
            TakeScreenShot();  
            Assert.True(string.IsNullOrEmpty(error), $"Validation failed for invalid Year of student on this instance {yearOfStudentOnThisInstance}");
        }

        [Fact]
        [Trait("Category", "Regression")]
        public void CompletionOfYearOfInstance_WhenCorrectSelectionIsMade_ShouldAcceptWithNoValidationErrors()
        {
            // Arrange
            string completionOfYearOfInstance = "1 - Completed the current year of programme of study";

            // Act
            Learners_LearnerListObjects learnerTab = WindowObjects.Windows.Main.SelectLearnerTab; 
            learnerTab.ClickAddLearnerButton();
            LearningDeliveryObjects learningDeliveryTab = WindowObjects.Windows.Main.SelectLearningDeliveryTab;
            learningDeliveryTab.ClickAddLearnerButton();
            learningDeliveryTab.SelectLearningDeliveryHETab();
            learningDeliveryTab.SetCompletionOfYearOfInstanceComboBox(completionOfYearOfInstance);

            // Assert 
            var error = learningDeliveryTab.GetCompletionOfYearOfInstanceValidationMessage;
            TakeScreenShot();  
            Assert.True(string.IsNullOrEmpty(error), $"Validation failed for invalid Completion of year of instance {completionOfYearOfInstance}");
        }

        [Fact]
        [Trait("Category", "Regression")]
        public void StudentInstanceFTE_WhenCorrectValueEntered_ShouldAcceptWithNoValidationErrors()
        {
            // Arrange
            string studentInstanceFTE = "";

            // Act
            Learners_LearnerListObjects learnerTab = WindowObjects.Windows.Main.SelectLearnerTab; 
            learnerTab.ClickAddLearnerButton();
            LearningDeliveryObjects learningDeliveryTab = WindowObjects.Windows.Main.SelectLearningDeliveryTab;
            learningDeliveryTab.ClickAddLearnerButton();
            learningDeliveryTab.SelectLearningDeliveryHETab();
            learningDeliveryTab.SetStudentInstanceFTETextBox(studentInstanceFTE);

            // Assert
            var error = learningDeliveryTab.GetStudentInstanceFTEValidationMessage;
            TakeScreenShot();  
            Assert.True(string.IsNullOrEmpty(error), $"Validation failed for invalid Student instance FTE {studentInstanceFTE}");
        }

        [Fact]
        [Trait("Category", "Regression")]
        public void PercentNotThoughtByThisInstitution_WhenCorrectValueEntered_ShouldAcceptWithNoValidationErrors()
        {
            // Arrange
            string percentNotThoughtByThisInstitution = "50";

            // Act
            Learners_LearnerListObjects learnerTab = WindowObjects.Windows.Main.SelectLearnerTab; 
            learnerTab.ClickAddLearnerButton();
            LearningDeliveryObjects learningDeliveryTab = WindowObjects.Windows.Main.SelectLearningDeliveryTab;
            learningDeliveryTab.ClickAddLearnerButton();
            learningDeliveryTab.SelectLearningDeliveryHETab();
            learningDeliveryTab.SetPercentNotThoughtByThisInstitutionTextBox(percentNotThoughtByThisInstitution);

            // Assert 
            var error = learningDeliveryTab.GetPercentNotThoughtByThisInstitutionValidationMessage;
            TakeScreenShot();  
            Assert.True(string.IsNullOrEmpty(error), $"Validation failed for invalid Percent not thought by this institution {percentNotThoughtByThisInstitution}");
        }

        [Fact]
        [Trait("Category", "Regression")]
        public void PercentTaughtByFirstLDCSSubject_WhenCorrectValueEntered_ShouldAcceptWithNoValidationErrors()
        {
            // Arrange
            string percentTaughtByFirstLDCSSubject = "50";

            // Act
            Learners_LearnerListObjects learnerTab = WindowObjects.Windows.Main.SelectLearnerTab; 
            learnerTab.ClickAddLearnerButton();
            LearningDeliveryObjects learningDeliveryTab = WindowObjects.Windows.Main.SelectLearningDeliveryTab;
            learningDeliveryTab.ClickAddLearnerButton();
            learningDeliveryTab.SelectLearningDeliveryHETab();
            learningDeliveryTab.SetPercentTaughtByFirstLDCSSubjectTextBox(percentTaughtByFirstLDCSSubject);

            // Assert 
            var error = learningDeliveryTab.GetPercentThoughtByFirstLDCSSubjectValidationMessage;
            TakeScreenShot();  
            Assert.True(string.IsNullOrEmpty(error), $"Validation failed for invalid Percent taught by first LDCS subject {percentTaughtByFirstLDCSSubject}");
        }

        [Fact]
        [Trait("Category", "Regression")]
        public void PercentTaughtBySecondLDCSSubject_WhenCorrectValueEntered_ShouldAcceptWithNoValidationErrors()
        {
            // Arrange
            string percentTaughtBySecondLDCSSubject = "50";

            // Act
            Learners_LearnerListObjects learnerTab = WindowObjects.Windows.Main.SelectLearnerTab; 
            learnerTab.ClickAddLearnerButton();
            LearningDeliveryObjects learningDeliveryTab = WindowObjects.Windows.Main.SelectLearningDeliveryTab;
            learningDeliveryTab.ClickAddLearnerButton();
            learningDeliveryTab.SelectLearningDeliveryHETab();
            learningDeliveryTab.SetPercentTaughtBySecondLDCSSubjectTextBox(percentTaughtBySecondLDCSSubject);

            // Assert 
            var error = learningDeliveryTab.GetPercentTaughtBySecondLDCSSubjectValidationMessage;
            TakeScreenShot();  
            Assert.True(string.IsNullOrEmpty(error), $"Validation failed for invalid Percent taught by second LDCS subject {percentTaughtBySecondLDCSSubject}");
        }

        [Fact]
        [Trait("Category", "Regression")]
        public void PercentTaughtByThirdLDCSSubject_WhenCorrectValueEntered_ShouldAcceptWithNoValidationErrors()
        {
            // Arrange
            string percentTaughtByThirdLDCSSubject = "50";

            // Act
            Learners_LearnerListObjects learnerTab = WindowObjects.Windows.Main.SelectLearnerTab; 
            learnerTab.ClickAddLearnerButton();
            LearningDeliveryObjects learningDeliveryTab = WindowObjects.Windows.Main.SelectLearningDeliveryTab;
            learningDeliveryTab.ClickAddLearnerButton();
            learningDeliveryTab.SelectLearningDeliveryHETab();
            learningDeliveryTab.SetPercentTaughtByThirdLDCSSubjectTextBox(percentTaughtByThirdLDCSSubject);

            // Assert 
            var error = learningDeliveryTab.GetPercentTaughtByThirdLDCSSubjectValidationMessage;
            TakeScreenShot();  
            Assert.True(string.IsNullOrEmpty(error), $"Validation failed for invalid Percent taught by second LDCS subject {percentTaughtByThirdLDCSSubject}");
        }

        [Fact]
        [Trait("Category", "Regression")]
        public void MajorSourceOfTuitonFees_WhenCorrectSelectionIsMade_ShouldAcceptWithNoValidationErrors()
        {
            // Arrange
            string majorSourceOfTuitonFees = "33 - BIS";

            // Act
            Learners_LearnerListObjects learnerTab = WindowObjects.Windows.Main.SelectLearnerTab; 
            learnerTab.ClickAddLearnerButton();
            LearningDeliveryObjects learningDeliveryTab = WindowObjects.Windows.Main.SelectLearningDeliveryTab;
            learningDeliveryTab.ClickAddLearnerButton();
            learningDeliveryTab.SelectLearningDeliveryHETab();
            learningDeliveryTab.SetMajorSourceOfTuitonFeesComboBox(majorSourceOfTuitonFees);

            // Assert 
            var error = learningDeliveryTab.GetMajorSourceOfTuitonFeesValidationMessage;
            TakeScreenShot();  
            Assert.True(string.IsNullOrEmpty(error), $"Validation failed for invalid Major source of tuiton fees {majorSourceOfTuitonFees}");
        }

        [Fact]
        [Trait("Category", "Regression")]
        public void NetTuitonFee_WhenCorrectValueEntered_ShouldAcceptWithNoValidationErrors()
        {
            // Arrange
            string netTuitonFee = "123456";

            // Act
            Learners_LearnerListObjects learnerTab = WindowObjects.Windows.Main.SelectLearnerTab; 
            learnerTab.ClickAddLearnerButton();
            LearningDeliveryObjects learningDeliveryTab = WindowObjects.Windows.Main.SelectLearningDeliveryTab;
            learningDeliveryTab.ClickAddLearnerButton();
            learningDeliveryTab.SelectLearningDeliveryHETab();
            learningDeliveryTab.SetNetTuitonFeeTextBox(netTuitonFee);

            // Assert 
            var error = learningDeliveryTab.GetNetTuitonFeeValidationMessage;
            TakeScreenShot();  
            Assert.True(string.IsNullOrEmpty(error), $"Validation failed for invalid Net tuiton fee {netTuitonFee}");
        }

        [Fact]
        [Trait("Category", "Regression")]
        public void SpecialFeeIndicator_WhenCorrectSelectionIsMade_ShouldAcceptWithNoValidationErrors()
        {
            // Arrange
            string specialFeeIndicator = "9 - Other fee";

            // Act
            Learners_LearnerListObjects learnerTab = WindowObjects.Windows.Main.SelectLearnerTab; 
            learnerTab.ClickAddLearnerButton();
            LearningDeliveryObjects learningDeliveryTab = WindowObjects.Windows.Main.SelectLearningDeliveryTab;
            learningDeliveryTab.ClickAddLearnerButton();
            learningDeliveryTab.SelectLearningDeliveryHETab();
            learningDeliveryTab.SetSpecialFeeIndicatorComboBox(specialFeeIndicator);

            // Assert 
            var error = learningDeliveryTab.GetSpecialFeeIndicatorValidationMessage;
            TakeScreenShot();  
            Assert.True(string.IsNullOrEmpty(error), $"Validation failed for invalid Special fee indicator {specialFeeIndicator}");
        }

        [Fact]
        [Trait("Category", "Regression")]
        public void GrossTuitonFee_WhenCorrectValueEntered_ShouldAcceptWithNoValidationErrors()
        {
            // Arrange
            string grossTuitonFee = "123456";

            // Act
            Learners_LearnerListObjects learnerTab = WindowObjects.Windows.Main.SelectLearnerTab; 
            learnerTab.ClickAddLearnerButton();
            LearningDeliveryObjects learningDeliveryTab = WindowObjects.Windows.Main.SelectLearningDeliveryTab;
            learningDeliveryTab.ClickAddLearnerButton();
            learningDeliveryTab.SelectLearningDeliveryHETab();
            learningDeliveryTab.SetGrossTuitonFeeTextBox(grossTuitonFee);

            // Assert 
            var error = learningDeliveryTab.GetGrossTuitonFeeValidationMessage;
            TakeScreenShot();  
            Assert.True(string.IsNullOrEmpty(error), $"Validation failed for invalid Gross tuition fee {grossTuitonFee}");
        }

    }
}
