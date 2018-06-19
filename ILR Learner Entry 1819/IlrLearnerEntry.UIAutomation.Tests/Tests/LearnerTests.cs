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
    public class LearnerTests: Tests
    {
        #region LearnerHeaderInformation

        [Fact]
        public void Learner_Create_Learner_NonNumericULN_ShouldRaiseError()
        {
            LearnerWindow learnerWindow = IlrLearnerEntry.UIAutomation.Tests.WindowObjects.Windows.Main.SelectLearnerTab; //select the learner tab
            learnerWindow.ClickAddLearnerButton();
            string invalidULN = RandomStrings.GetRandomAlphabets(8);
            learnerWindow.SetULN(invalidULN);
            var error=learnerWindow.GetULNValidationMessage;

            TakeScreenShot();
            Assert.False(string.IsNullOrEmpty(error), $"Validation failed for an invalid ULN {invalidULN}");

        }
        [Fact]
        public void Learner_Create_Learner_NumericULN_Shoul1dNotRaiseError()
        {
            LearnerWindow learnerWindow = IlrLearnerEntry.UIAutomation.Tests.WindowObjects.Windows.Main.SelectLearnerTab; //select the learner tab
            learnerWindow.ClickAddLearnerButton();
            string validULN = RandomStrings.GetRandomNumber(8);
            learnerWindow.SetULN(validULN);
            var error = learnerWindow.GetULNValidationMessage;
            TakeScreenShot();
            Assert.True(string.IsNullOrEmpty(error), $"Validation failed for an invalid ULN {validULN}");

        }

        [Fact]
        public void Learner_Create_Learner_MissingGivenName_ShoulldRaiseError()
        {
            LearnerWindow learnerWindow = IlrLearnerEntry.UIAutomation.Tests.WindowObjects.Windows.Main.SelectLearnerTab; //select the learner tab
            learnerWindow.ClickAddLearnerButton();
            var error = learnerWindow.GetGivenNameValidationMessage;
            TakeScreenShot();
            Assert.False(string.IsNullOrEmpty(error), $"Validation failed for an the missing Given Name");

        }
        [Fact]
        public void Learner_Create_Learner_GivenNameExist_ShoulldNotRaiseError()
        {
            LearnerWindow learnerWindow = IlrLearnerEntry.UIAutomation.Tests.WindowObjects.Windows.Main.SelectLearnerTab; //select the learner tab
            learnerWindow.ClickAddLearnerButton();
            learnerWindow.SetGivenName(RandomStrings.GetRandomAlphabets(8));
            var error = learnerWindow.GetGivenNameValidationMessage;
            TakeScreenShot();
            Assert.True(string.IsNullOrEmpty(error), $"Validation message generated for given name");

        }
        #endregion

        #region learner tab

        #endregion

    }
}
