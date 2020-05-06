using System;
using NUnit.Framework;
using System.Reflection;

namespace ILR.LearnerEntry.Tests.UnitTests.Learners
{
    [TestFixture]
    public class EmploymentStatusTests
    {
        private Message message = null;
        private string dataFolder = string.Empty;
        private string internalFileName = "internal1920.ilr";

        [SetUp]
        public void Setup()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string Path = assembly.Location;
            System.IO.FileInfo fi = new System.IO.FileInfo(Path);
            dataFolder = fi.DirectoryName;
            var fileName = System.IO.Path.Combine(dataFolder, internalFileName);
            var logFileName = System.IO.Path.Combine(dataFolder, "learnerdeliveryHETests.log");
            message = new Message(fileName, logFileName);
        }

        [Test]
        [Category("Category [Unit]")]
        public void EmploymentStatus_EmploymentStatus_When_Empstat_IsBlank_ShouldNotHaveValidationError()
        {
            // Arrange
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "givenName";

            // Act 
            var employmentStatus = learner.CreateLearnerEmploymentStatus();
            var validationMessage = employmentStatus.EmpStat.ToString();

            // Assert 
            Assert.AreEqual(string.Empty, validationMessage);
        }

        [Test]
        [Category("Category [Unit]")]
        public void EmploymentStatus_EmploymentStatus_When_Empstat_IsNotBlank_ShouldNotHaveValidationError()
        {
            // Arrange
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "givenName";

            // Act 
            var employmentStatus = learner.CreateLearnerEmploymentStatus();
            employmentStatus.EmpStat = 1;
            var validationMessage = employmentStatus.EmpStat;

            // Assert 
            Assert.IsFalse(string.IsNullOrEmpty(validationMessage.ToString()));
        }

        [Test]
        [Category("Category [Unit]")]
        public void EmploymentStatus_DateStatusApplies_When_DateEmpStatApp_IsBlank_ShouldNotHaveValidationError()
        {
            // Arrange
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "givenName";

            // Act 
            var employmentStatus = learner.CreateLearnerEmploymentStatus();
            var validationMessage = employmentStatus.DateEmpStatApp.ToString();

            // Assert 
            Assert.AreEqual(string.Empty, validationMessage);
        }

        [Test]
        [Category("Category [Unit]")]
        public void EmploymentStatus_DateStatusApplies_When_DateEmpStatApp_IsNotBlank_ShouldNotHaveValidationError()
        {
            // Arrange
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "givenName";

            // Act 
            var employmentStatus = learner.CreateLearnerEmploymentStatus();
            employmentStatus.DateEmpStatApp = DateTime.Now;
            var validationMessage = employmentStatus.DateEmpStatApp.ToString();

            // Assert 
            Assert.IsFalse(string.IsNullOrEmpty(validationMessage.ToString()));
        }

        [Test]
        [Category("Category [Unit]")]
        public void EmploymentStatus_EmployerIdentifier_When_EmpId_IsBlank_ShouldNotHaveValidationError()
        {
            // Arrange
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "givenName";

            // Act 
            var employmentStatus = learner.CreateLearnerEmploymentStatus();
            var validationMessage = employmentStatus.EmpId;

            // Assert 
            Assert.AreEqual(null, validationMessage);
        }

        [Test]
        [Category("Category [Unit]")]
        public void EmploymentStatus_EmployerIdentifier_When_EmpId_IsNotBlank_ShouldNotHaveValidationError()
        {
            // Arrange
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "givenName";

            // Act 
            var employmentStatus = learner.CreateLearnerEmploymentStatus();
            employmentStatus.EmpId = "123456";
            var validationMessage = employmentStatus.EmpId;

            // Assert 
            Assert.IsFalse(string.IsNullOrEmpty(validationMessage.ToString()));
        }

        [Test]
        [Category("Category [Unit]")]
        public void EmploymentStatus_AgreementIdentifier_When_AgreeId_IsBlank_ShouldNotHaveValidationError()
        {
            // Arrange
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "givenName";

            // Act 
            var employmentStatus = learner.CreateLearnerEmploymentStatus();
            var validationMessage = employmentStatus.AgreeId;

            // Assert 
            Assert.AreEqual(null, validationMessage);
        }

        [Test]
        [Category("Category [Unit]")]
        public void EmploymentStatus_AgreementIdentifier_When_AgreeId_IsNotBlank_ShouldNotHaveValidationError()
        {
            // Arrange
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "givenName";

            // Act 
            var employmentStatus = learner.CreateLearnerEmploymentStatus();
            employmentStatus.AgreeId = "123456";
            var validationMessage = employmentStatus.AgreeId;

            // Assert 
            Assert.IsFalse(string.IsNullOrEmpty(validationMessage.ToString()));
        }

        [Test]
        [Category("Category [Unit]")]
        public void EmploymentStatus_LengthOfUnemployment_When_LOU_IsBlank_ShouldNotHaveValidationError()
        {
            // Arrange
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "givenName";

            // Act 
            var employmentStatus = learner.CreateLearnerEmploymentStatus();
            var validationMessage = employmentStatus.LOU;

            // Assert 
            Assert.AreEqual(null, validationMessage);
        }

        [Test]
        [Category("Category [Unit]")]
        public void EmploymentStatus_LengthOfUnemployment_When_LOU_IsNotBlank_ShouldNotHaveValidationError()
        {
            // Arrange
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "givenName";

            // Act 
            var employmentStatus = learner.CreateLearnerEmploymentStatus();
            employmentStatus.LOU = 1;
            var validationMessage = employmentStatus.LOU;

            // Assert 
            Assert.IsFalse(string.IsNullOrEmpty(validationMessage.ToString()));
        }

        [Test]
        [Category("Category [Unit]")]
        public void EmploymentStatus_BenifitStatusIndicator_When_BSI_IsBlank_ShouldNotHaveValidationError()
        {
            // Arrange
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "givenName";

            // Act 
            var employmentStatus = learner.CreateLearnerEmploymentStatus();
            var validationMessage = employmentStatus.BSI;

            // Assert 
            Assert.AreEqual(null, validationMessage);
        }

        [Test]
        [Category("Category [Unit]")]
        public void EmploymentStatus_BenifitStatusIndicator_When_BSI_IsNotBlank_ShouldNotHaveValidationError()
        {
            // Arrange
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "givenName";

            // Act 
            var employmentStatus = learner.CreateLearnerEmploymentStatus();
            employmentStatus.BSI = 1;
            var validationMessage = employmentStatus.BSI;

            // Assert 
            Assert.IsFalse(string.IsNullOrEmpty(validationMessage.ToString()));
        }

        [Test]
        [Category("Category [Unit]")]
        public void EmploymentStatus_EmploymentIntensityIndicator_When_EII_IsBlank_ShouldNotHaveValidationError()
        {
            // Arrange
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "givenName";

            // Act 
            var employmentStatus = learner.CreateLearnerEmploymentStatus();
            var validationMessage = employmentStatus.EII;

            // Assert 
            Assert.AreEqual(null, validationMessage);
        }

        [Test]
        [Category("Category [Unit]")]
        public void EmploymentStatus_EmploymentIntensityIndicator_When_EII_IsNotBlank_ShouldNotHaveValidationError()
        {
            // Arrange
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "givenName";

            // Act 
            var employmentStatus = learner.CreateLearnerEmploymentStatus();
            employmentStatus.EII = 1;
            var validationMessage = employmentStatus.EII;

            // Assert 
            Assert.IsFalse(string.IsNullOrEmpty(validationMessage.ToString()));
        }

        [Test]
        [Category("Category [Unit]")]
        public void EmploymentStatus_LengthOfEmployment_When_LOE_IsBlank_ShouldNotHaveValidationError()
        {
            // Arrange
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "givenName";

            // Act 
            var employmentStatus = learner.CreateLearnerEmploymentStatus();
            var validationMessage = employmentStatus.LOE;

            // Assert 
            Assert.AreEqual(null, validationMessage);
        }

        [Test]
        [Category("Category [Unit]")]
        public void EmploymentStatus_LengthOfEmployment_When_LOE_IsNotBlank_ShouldNotHaveValidationError()
        {
            // Arrange
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "givenName";

            // Act 
            var employmentStatus = learner.CreateLearnerEmploymentStatus();
            employmentStatus.LOE = 1;
            var validationMessage = employmentStatus.LOE;

            // Assert 
            Assert.IsFalse(string.IsNullOrEmpty(validationMessage.ToString()));
        }
    }
}
