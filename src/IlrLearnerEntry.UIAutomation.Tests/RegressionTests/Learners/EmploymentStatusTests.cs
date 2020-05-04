using IlrLearnerEntry.UIAutomation.Tests.WindowObjects;
using Xunit;
using IlrLearnerEntry.UIAutomation.Tests.TabObjects.LearnersObjects;
using IlrLearnerEntry.UIAutomation.Tests.Tests;
using System;
using IlrLearnerEntry.UIAutomation.Tests.TestHelper;

namespace IlrLearnerEntry.UIAutomation.Tests.RegressionTests.Learners
{
    [Collection("Application Collection")]
    public class EmploymentStatusTests : TestsSetup
    {
        [Fact]
        [Trait("Category", "Regression")]
        public void EmploymentStatus_WhenCorrectSelectionIsMade_ShouldAcceptWithNoValidationErrors()
        {
            // Arrange
            string employmentStatus = "10 - In paid employment";

            // Act
            Learners_LearnerListObjects learnerTab = WindowObjects.Windows.Main.SelectLearnerTab;
            learnerTab.ClickAddLearnerButton();
            EmploymentStatusObjects employmentStatusTab = WindowObjects.Windows.Main.SelectEmploymentStatusTab;
            employmentStatusTab.ClickAddEmploymentStatusButton();
            employmentStatusTab.SetEmploymentStatusComboBox(employmentStatus);

            // Assertwe
            var error = employmentStatusTab.GetEmploymentStatusValidationMessage;
            TakeScreenShot();
            Assert.True(string.IsNullOrEmpty(error), $"Validation failed for invalid Employment status {employmentStatus}");
        }

        [Fact]
        [Trait("Category", "Regression")]
        public void DateStatusApplies_WhenDateSelected_ShouldAcceptWithNoValidationErrors()
        {
            // Arrange           
            var dateStatusApplies = new DateTime(2018, 01, 01);

            // Act
            Learners_LearnerListObjects learnerTab = WindowObjects.Windows.Main.SelectLearnerTab;
            learnerTab.ClickAddLearnerButton();
            EmploymentStatusObjects employmentStatusTab = WindowObjects.Windows.Main.SelectEmploymentStatusTab;
            employmentStatusTab.ClickAddEmploymentStatusButton();
            employmentStatusTab.SetDateStatusAppliesDateTime(dateStatusApplies);

            // Assert
            var error = employmentStatusTab.GetEmploymentStatusValidationMessage;
            TakeScreenShot();
            Assert.True(string.IsNullOrEmpty(error), $"Validation failed for Date status applies {dateStatusApplies}");
        }

        [Fact]
        [Trait("Category", "Regression")]
        public void EmployerIdentifier_WhenCorectValueEntered_ShouldAcceptWithNoValidationErrors()
        {
            // Arrange
            string employerIdentifier = RandomStrings.GetRandomNumber(9);

            // Act
            Learners_LearnerListObjects learnerTab = WindowObjects.Windows.Main.SelectLearnerTab;
            learnerTab.ClickAddLearnerButton();
            EmploymentStatusObjects employmentStatusTab = WindowObjects.Windows.Main.SelectEmploymentStatusTab;
            employmentStatusTab.ClickAddEmploymentStatusButton();
            employmentStatusTab.SetEmployerIdentifierTextBox(employerIdentifier);

            // Assert 
            var error = employmentStatusTab.GetEmployerIdentifierValidationMessage;
            TakeScreenShot();
            Assert.True(string.IsNullOrEmpty(error), $"Validation failed for Employer Identifier {employerIdentifier}");
        }

        [Fact]
        [Trait("Category", "Regression")]
        public void AgreementIdentifier_WhenCorectValueEntered_ShouldAcceptWithNoValidationErrors()
        {
            // Arrange
            string agreementIdentifier = RandomStrings.GetRandomNumber(6);

            // Act
            Learners_LearnerListObjects learnerTab = WindowObjects.Windows.Main.SelectLearnerTab;
            learnerTab.ClickAddLearnerButton();
            EmploymentStatusObjects employmentStatusTab = WindowObjects.Windows.Main.SelectEmploymentStatusTab;
            employmentStatusTab.ClickAddEmploymentStatusButton();
            employmentStatusTab.SetAgreementIdentifierTextBox(agreementIdentifier);

            // Assert 
            var error = employmentStatusTab.GetAgreementIdentifierValidationMessage;
            TakeScreenShot();
            Assert.True(string.IsNullOrEmpty(error), $"Validation failed for Agreement Identifier {agreementIdentifier}");
        }

        [Fact]
        [Trait("Category", "Regression")]
        public void LengthOfUnemployment_WhenCorrectSelectionIsMade_ShouldAcceptWithNoValidationErrors()
        {
            // Arrange
            string lengthOfUnemployment = "2 - Learner has been unemployed for 6-11 months ";

            // Act
            Learners_LearnerListObjects learnerTab = WindowObjects.Windows.Main.SelectLearnerTab;
            learnerTab.ClickAddLearnerButton();
            EmploymentStatusObjects employmentStatusTab = WindowObjects.Windows.Main.SelectEmploymentStatusTab;
            employmentStatusTab.ClickAddEmploymentStatusButton();
            employmentStatusTab.SetLengthOfUnemploymentComboBox(lengthOfUnemployment);

            // Assert
            var error = employmentStatusTab.GetLengthOfUnemploymentValidationMessage;
            TakeScreenShot();
            Assert.True(string.IsNullOrEmpty(error), $"Validation failed for Length of unemployment {lengthOfUnemployment}");
        }

        [Fact]
        [Trait("Category", "Regression")]
        public void BenefitStatusIndicator_WhenCorrectSelectionIsMade_ShouldAcceptWithNoValidationErrors()
        {
            // Arrange
            string benefitStatusIndicator = "1 - Learner is in receipt of Job Seekers Allowance (JSA) ";

            // Act
            Learners_LearnerListObjects learnerTab = WindowObjects.Windows.Main.SelectLearnerTab;
            learnerTab.ClickAddLearnerButton();
            EmploymentStatusObjects employmentStatusTab = WindowObjects.Windows.Main.SelectEmploymentStatusTab;
            employmentStatusTab.ClickAddEmploymentStatusButton();
            employmentStatusTab.SetBenefitStatusIndicatorComboBox(benefitStatusIndicator);

            // Assert
            var error = employmentStatusTab.GetBenefitStatusIndicatorValidationMessage;
            TakeScreenShot();
            Assert.True(string.IsNullOrEmpty(error), $"Validation failed for Benefit status indicator {benefitStatusIndicator}");
        }

        [Fact]
        [Trait("Category", "Regression")]
        public void EmploymentIntensityIndicator_WhenCorrectSelectionIsMade_ShouldAcceptWithNoValidationErrors()
        {
            // Arrange
            string employmentIntensityIndicator = "2 - Learner is employed for less than 16 hours per week";

            // Act
            Learners_LearnerListObjects learnerTab = WindowObjects.Windows.Main.SelectLearnerTab;
            learnerTab.ClickAddLearnerButton();
            EmploymentStatusObjects employmentStatusTab = WindowObjects.Windows.Main.SelectEmploymentStatusTab;
            employmentStatusTab.ClickAddEmploymentStatusButton();
            employmentStatusTab.SetEmploymentIntensityIndicatorComboBox(employmentIntensityIndicator);

            // Assert 
            var error = employmentStatusTab.GetEmploymentIntensityIndicatorValidationMessage;
            TakeScreenShot();
            Assert.True(string.IsNullOrEmpty(error), $"Validation failed for Employment intensity indicator {employmentIntensityIndicator}");
        }

        [Fact]
        [Trait("Category", "Regression")]
        public void LengthOfEmployment_WhenCorrectSelectionIsMade_ShouldAcceptWithNoValidationErrors()
        {
            // Arrange
            string lengthOfEmployment = "1 - Learner has been employed for up to 3 months ";

            // Act
            Learners_LearnerListObjects learnerTab = WindowObjects.Windows.Main.SelectLearnerTab;
            learnerTab.ClickAddLearnerButton();
            EmploymentStatusObjects employmentStatusTab = WindowObjects.Windows.Main.SelectEmploymentStatusTab;
            employmentStatusTab.ClickAddEmploymentStatusButton();
            employmentStatusTab.SetLengthOfEmploymentComboBox(lengthOfEmployment);

            // Assert
            var error = employmentStatusTab.GetLengthOfEmploymentValidationMessage;
            TakeScreenShot();
            Assert.True(string.IsNullOrEmpty(error), $"Validation failed for Length of employment {lengthOfEmployment}");
        }
    }
}
