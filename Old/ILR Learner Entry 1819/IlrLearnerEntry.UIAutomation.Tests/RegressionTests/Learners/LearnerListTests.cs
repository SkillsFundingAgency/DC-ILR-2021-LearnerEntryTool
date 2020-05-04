using IlrLearnerEntry.UIAutomation.Tests.TestHelper;
using IlrLearnerEntry.UIAutomation.Tests.WindowObjects;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TestStack.White;
using Xunit;

namespace IlrLearnerEntry.UIAutomation.Tests.Tests
{
    [Collection("Application Collection")]
    public class LearnerListTests: TestsSetup
    {
        #region LearnerHeaderInformation

        [Fact]
        [Trait("Category", "Regression")]
        public void Learner_Create_Learner_NonNumericULN_ShouldRaiseError()
        {
            Learners_LearnerListObjects learnerTab = IlrLearnerEntry.UIAutomation.Tests.WindowObjects.Windows.Main.SelectLearnerTab; //select the learner tab
            learnerTab.ClickAddLearnerButton();
            string invalidULN = RandomStrings.GetRandomAlphabets(8);
            learnerTab.SetULN(invalidULN);
            var error=learnerTab.GetULNValidationMessage;

            TakeScreenShot();
            Assert.False(string.IsNullOrEmpty(error), $"Validation failed for an invalid ULN {invalidULN}");

        }
        [Fact]
        [Trait("Category", "Regression")]
        public void Learner_Create_Learner_NumericULN_Shoul1dNotRaiseError()
        {
            Learners_LearnerListObjects learnerTab = IlrLearnerEntry.UIAutomation.Tests.WindowObjects.Windows.Main.SelectLearnerTab; //select the learner tab
            learnerTab.ClickAddLearnerButton();
            string validULN = RandomStrings.GetRandomNumber(8);
            learnerTab.SetULN(validULN);
            var error = learnerTab.GetULNValidationMessage;
            TakeScreenShot();
            Assert.True(string.IsNullOrEmpty(error), $"Validation failed for an invalid ULN {validULN}");

        }

        [Fact]
        [Trait("Category", "Regression")]
        public void Learner_Create_Learner_MissingGivenName_ShoulldRaiseError()
        {
            Learners_LearnerListObjects learnerTab = IlrLearnerEntry.UIAutomation.Tests.WindowObjects.Windows.Main.SelectLearnerTab; //select the learner tab
            learnerTab.ClickAddLearnerButton();
            var error = learnerTab.GetGivenNameValidationMessage;
            TakeScreenShot();
            Assert.False(string.IsNullOrEmpty(error), $"Validation failed for an the missing Given Name");

        }
        [Fact]
        [Trait("Category", "Regression")]
        public void Learner_Create_Learner_GivenNameExist_ShoulldNotRaiseError()
        {
            Learners_LearnerListObjects learnerTab = IlrLearnerEntry.UIAutomation.Tests.WindowObjects.Windows.Main.SelectLearnerTab; //select the learner tab
            learnerTab.ClickAddLearnerButton();
            learnerTab.SetGivenName(RandomStrings.GetRandomAlphabets(8));
            var error = learnerTab.GetGivenNameValidationMessage;
            TakeScreenShot();
            Assert.True(string.IsNullOrEmpty(error), $"Validation message generated for given name");

        }
        #endregion

        #region learner tab

        #endregion

    }
}
