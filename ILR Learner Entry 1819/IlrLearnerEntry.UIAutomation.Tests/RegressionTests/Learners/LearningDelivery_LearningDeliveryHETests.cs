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
        public void Learners_LearningDelivery_LearningDeliveryHE_StudentInstanceIdentifier_WhenCorrectValueEntered_ShouldAcceptAndSave()
        {
            // Arrange
            string studentInstanceIdentifier = RandomStrings.GetRandomAlphabets(10);

            // Act
            Learners_LearnerListObjects learnerTab = WindowObjects.Windows.Main.SelectLearnerTab; //select the learner tab
            learnerTab.ClickAddLearnerButton();
            LearningDeliveryObjects learningDeliveryTab = WindowObjects.Windows.Main.SelectLearningDeliveryTab;
            learningDeliveryTab.ClickAddLearnerButton();
            learningDeliveryTab.SelectLearningDeliveryHETab();
            learningDeliveryTab.SetStudentInstanceIdentifierTextBox(studentInstanceIdentifier);

            // Assert
            var error = learningDeliveryTab.GetStudentInstanceIdentifierValidationMessage;
            //TakeScreenShot();
            Assert.True(string.IsNullOrEmpty(error), $"Validation failed for an invalid Student Instance Identifier {studentInstanceIdentifier}");
        }

        [Fact]
        [Trait("Category", "Regression")]
        public void Learners_LearningDelivery_LearningDeliveryHE_UCASApplicationCode_WhenCorrectValueEntered_ShouldAcceptAndSave()
        {
            // Arrange
            string UCASApplicationCode = RandomStrings.GetRandomAlphabets(10);

            // Act
            Learners_LearnerListObjects learnerTab = WindowObjects.Windows.Main.SelectLearnerTab; //select the learner tab
            learnerTab.ClickAddLearnerButton();
            LearningDeliveryObjects learningDeliveryTab = WindowObjects.Windows.Main.SelectLearningDeliveryTab;
            learningDeliveryTab.ClickAddLearnerButton();
            learningDeliveryTab.SelectLearningDeliveryHETab();
            learningDeliveryTab.SetUCASApplicationCodeTextBox(UCASApplicationCode);

            // Assert
            var error = learningDeliveryTab.GetUCASApplicationCodeValidationMessage;         
            Assert.True(string.IsNullOrEmpty(error), $"Vaidation failed for invalid UCAS application code {UCASApplicationCode}");
        }

        [Fact]
        [Trait("Category", "Regression")]
        public void Learners_LearningDelivery_LearningDeliveryHE_StudentSupportNumber_WhenCorrectValueEntered_ShouldAcceptAndSave()
        {
            // Arrange
            string studentSupportNumber = RandomStrings.GetRandomAlphabets(10);

            // Act
            Learners_LearnerListObjects learnerTab = WindowObjects.Windows.Main.SelectLearnerTab; //select the learner tab
            learnerTab.ClickAddLearnerButton();
            LearningDeliveryObjects learningDeliveryTab = WindowObjects.Windows.Main.SelectLearningDeliveryTab;
            learningDeliveryTab.ClickAddLearnerButton();
            learningDeliveryTab.SelectLearningDeliveryHETab();
            learningDeliveryTab.SetStudentSupportNumberTextBox(studentSupportNumber);

            // Assert
            var error = learningDeliveryTab.GetStudentSupportNumberValidationMessage;
            Assert.True(string.IsNullOrEmpty(error), $"Validation failed for invalid Student support number {studentSupportNumber}");
        }

        [Fact]
        [Trait("Category", "Regression")]
        public void Learners_LearningDelivery_LearningDeliveryHE_OccupationCode_WhenCorrectValueEntered_ShouldAcceptAndSave()
        {
            // Arrange
            string occupationCode = "1234";

            // Act
            Learners_LearnerListObjects learnerTab = WindowObjects.Windows.Main.SelectLearnerTab; //select the learner tab
            learnerTab.ClickAddLearnerButton();
            LearningDeliveryObjects learningDeliveryTab = WindowObjects.Windows.Main.SelectLearningDeliveryTab;
            learningDeliveryTab.ClickAddLearnerButton();
            learningDeliveryTab.SelectLearningDeliveryHETab();
            learningDeliveryTab.SetOccupationCodeTextBox(occupationCode);

            // Assert 
            var error = learningDeliveryTab.GetOccupationCodeValidationMessage;
            Assert.True(string.IsNullOrEmpty(error), $"Validation failed for invalid Occupation code {occupationCode}");

        }

    }
}
