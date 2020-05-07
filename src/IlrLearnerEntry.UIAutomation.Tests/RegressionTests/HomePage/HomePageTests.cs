using IlrLearnerEntry.UIAutomation.Tests.TestHelper;
using IlrLearnerEntry.UIAutomation.Tests.WindowObjects;
using Xunit;

namespace IlrLearnerEntry.UIAutomation.Tests.Tests
{
    [Collection("Application Collection")]
    public class HomePageTests: TestsSetup
    {
        string expectedUKPRNMessage = "Please enter a valid integer value for the UKPRN!";
        [Fact]
        [Trait("Category", "Regression")]
        public void HomePage_UKPRN_When_NonNumeric_ShouldShowError()
        {            
            HomeObjects homeWindow = WindowObjects.Windows.Main.SelectHomeTab; //select the learner tab
            string invalidUKPRN= RandomStrings.GetRandomAlphabets(8);
            homeWindow.SetUkPRN(invalidUKPRN);
            var error = homeWindow.GetUKPRNValidationMessage;
            Assert.Equal(error.TrimEnd(), expectedUKPRNMessage);           
        }

        [Fact]
        [Trait("Category", "Regression")]
        public void HomePage_UKPRN_When_NumericNumberUpdated_ShouldNotShowError()
        {
            HomeObjects homeWindow = WindowObjects.Windows.Main.SelectHomeTab; //select the learner tab
            string invalidUKPRN = RandomStrings.GetRandomNumber(8);
            homeWindow.SetUkPRN(invalidUKPRN);
            var error = homeWindow.GetUKPRNValidationMessage;
            Assert.True(string.IsNullOrEmpty(error));           
        }
    }
}
