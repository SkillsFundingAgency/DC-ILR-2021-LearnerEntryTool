using IlrLearnerEntry.UIAutomation.Tests.TestHelper;
using IlrLearnerEntry.UIAutomation.Tests.WindowObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IlrLearnerEntry.UIAutomation.Tests.Tests
{
    [Collection("Application Collection")]
    public class HomePageTests: Tests
    {
        string expectedUKPRNMessage = "Please enter a valid integer value for the UKPRN!";
        [Fact]
        public void HomePage_UKPRN_When_NonNumeric_ShouldShowError()
        {
            
            HomeWindow homeWindow = WindowObjects.Windows.Main.SelectHomeTab; //select the learner tab
            string invalidUKPRN= RandomStrings.GetRandomAlphabets(8);
            homeWindow.SetUkPRN(invalidUKPRN);
            var error = homeWindow.GetUKPRNValidationMessage;
            Assert.Equal(error.TrimEnd(), expectedUKPRNMessage);
            

        }

        [Fact]
        public void HomePage_UKPRN_When_NumericNumberUpdated_ShouldNotShowError()
        {
            HomeWindow homeWindow = WindowObjects.Windows.Main.SelectHomeTab; //select the learner tab
            string invalidUKPRN = RandomStrings.GetRandomNumber(8);
            homeWindow.SetUkPRN(invalidUKPRN);
            var error = homeWindow.GetUKPRNValidationMessage;
            Assert.True(string.IsNullOrEmpty(error));
            

        }
    }
}
