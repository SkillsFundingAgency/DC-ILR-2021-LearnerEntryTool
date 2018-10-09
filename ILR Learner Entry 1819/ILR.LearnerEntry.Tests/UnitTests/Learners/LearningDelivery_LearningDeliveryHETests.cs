using NUnit.Framework;
using System.Reflection;

namespace ILR.LearnerEntry.Tests
{
    [TestFixture]
    public class LearningDelivery_LearningDeliveryHETests
    {
        private Message message =null;
        private string dataFolder = string.Empty;
        private string internalFileName = "internal1819.ilr";
        //private const string TYPEYR_VALIDATION_MESSAGE = "Type of instance year is mandatory.";

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
        public void Learners_LearningDelivery_LearningDeliveryHE_When_NUMHUS_IsBlank_ShouldNotHaveValidationError()
        {
            // Arrange  
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "GivenName";

            // Act
            var learningDelivery = learner.CreateLearningDelivery();
            var learningDeliveryHE = learningDelivery.CreateLearningDeliveryHE();
            var validationMessage = learningDeliveryHE["NUMHUS"];

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(validationMessage));
        }

        [Test]
        [Category("Category [Unit]")]
        public void Learners_LearningDelivery_LearnerDeliveryHE_When_NUMHUS_IsNotBlank_ShouldNotHaveValidationError()
        {
            // Arrange  
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "GivenName";

            // Act
            var learningDelivery = learner.CreateLearningDelivery();
            var learningDeliveryHE = learningDelivery.CreateLearningDeliveryHE();
            learningDeliveryHE.NUMHUS = "abc";
            var validationMessage = learningDeliveryHE["NUMHUS"];

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(validationMessage));
        }

        [Test]
        [Category("Category [Unit]")]
        public void Learners_LearningDelivery_LearningDeliveryHE_When_SSN_IsBlank_ShouldNotHaveValidationError()
        {
            // Arrange  
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "GivenName";

            // Act
            var learningDelivery = learner.CreateLearningDelivery();
            var learningDeliveryHE = learningDelivery.CreateLearningDeliveryHE();
            var validationMessage = learningDeliveryHE["SSN"];

            // Assert 
            Assert.IsTrue(string.IsNullOrEmpty(validationMessage));
        }

        [Test]
        [Category("Category [Unit]")]
        public void Learners_LearningDelivery_LearningDeliveryHE_When_SSN_IsNotBlank_ShouldNotHaveValidationError()
        {
            // Arrange 
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "GivenName";

            // Act
            var learningDelivery = learner.CreateLearningDelivery();
            var learningDeliveryHE = learningDelivery.CreateLearningDeliveryHE();
            learningDeliveryHE.SSN = "abc";
            var validationMessage = learningDeliveryHE["SSN"];

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(validationMessage));
        }

        [Test]
        [Category("Category [Unit]")]
        public void Learners_LearningDelivery_LearningDeliveryHE_When_QUALENT3_IsBlank_ShouldNotHaveValidationError()
        {
            // Arrange 
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "GivenName";

            // Act
            var learningDelivery = learner.CreateLearningDelivery();
            var learningDeliveryHE = learningDelivery.CreateLearningDeliveryHE();
            var validationMessage = learningDeliveryHE["QUALENT3"];

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(validationMessage));
        }

        [Test]
        [Category("Category [Unit]")]
        public void Learners_LearningDelivery_LearningDeliveryHE_When_QUALENT3_IsNotBlank_ShouldNotHaveValidationError()
        {
            // Arrange 
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "GivenName";

            // Act
            var learningDelivery = learner.CreateLearningDelivery();
            var learningDeliveryHE = learningDelivery.CreateLearningDeliveryHE();
            learningDeliveryHE.QUALENT3 = "abc";
            var validationMessage = learningDeliveryHE["QUALENT3"];
            
            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(validationMessage));
        }

        [Test]
        [Category("Category [Unit]")]
        public void Learners_LearningDelivery_LearningDeliveryHE_When_SOC2000_IsBlank_ShouldNotHaveValidationError()
        {
            // Arrange
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "GivenName";

            // Act
            var learningDelivery = learner.CreateLearningDelivery();
            var learningDeliveryHE = learningDelivery.CreateLearningDeliveryHE();
            var validationMessage = learningDeliveryHE["SOC2000"];

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(validationMessage));
        }

        [Test]
        [Category("Category [Unit]")]
        public void Learners_LearningDelivery_LearningDeliveryHE_When_SOC2000_IsNotBlank_ShouldNotHaveValidationError()
        {
            // Arrange
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "GivenName";

            // Act
            var learningDelivery = learner.CreateLearningDelivery();
            var learningDeliveryHE = learningDelivery.CreateLearningDeliveryHE();
            learningDeliveryHE.SOC2000 = 123;
            var validationMessage = learningDeliveryHE["SOC2000"];

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(validationMessage));
        }

        [Test]
        [Category("Category [Unit]")]
        public void Learners_LearningDelivery_LearningDeliveryHE_When_SEC_IsBlank_ShouldNotHaveValidationError()
        {
            // Arrange
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "GivenName";

            // Act
            var learningDelivery = learner.CreateLearningDelivery();
            var learningDeliveryHE = learningDelivery.CreateLearningDeliveryHE();
            var validationMessage = learningDeliveryHE["SEC"];

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(validationMessage));
        }

        [Test]
        [Category("Category [Unit]")]
        public void Learners_LearningDelivery_LearningDeliveryHE_When_SEC_IsNotBlank_ShouldNotHaveValidationError()
        {
            // Arrange
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "GivenName";

            // Act
            var learningDelivery = learner.CreateLearningDelivery();
            var learningDeliveryHE = learningDelivery.CreateLearningDeliveryHE();
            learningDeliveryHE.SEC = 123;
            var validationMessage = learningDeliveryHE["SEC"];

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(validationMessage));
        }

        [Test]
        [Category("Category [Unit]")]
        public void Learners_LearningDelivery_LearningDeliveryHE_When_UCASAPPID_IsBlank_ShouldNotHaveValidationError()
        {
            // Arrange
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "GivenName";

            // Act
            var learningDelivery = learner.CreateLearningDelivery();
            var learningDeliveryHE = learningDelivery.CreateLearningDeliveryHE();
            var validationMessage = learningDeliveryHE["UCASAPPID"];

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(validationMessage));
        }

        [Test]
        [Category("Category [Unit]")]
        public void Learners_LearningDelivery_LearningDeliveryHE_When_UCASAPPID_IsNotBlank_ShouldNotHaveValidationError()
        {
            // Arrange
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "GivenName";

            // Act
            var learningDelivery = learner.CreateLearningDelivery();
            var learningDeliveryHE = learningDelivery.CreateLearningDeliveryHE();
            learningDeliveryHE.UCASAPPID = "abc";
            var validationMessage = learningDeliveryHE["UCASAPPID"];

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(validationMessage));
        }

        [Test]
        [Category("Category [Unit]")]
        public void Learners_LearningDelivery_LearningDeliveryHE_When_TYPEYR_IsBlank_ShouldGiveTypeOfYearSpecificValidationError()
        {
            // Arrange
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "GivenName";

            // Act
            var learningDelivery = learner.CreateLearningDelivery();
            var learningDeliveryHE = learningDelivery.CreateLearningDeliveryHE();
            var validationMessage = learningDeliveryHE["TYPEYR"];

            // Assert
            Assert.IsTrue(!string.IsNullOrEmpty(validationMessage));
            Assert.AreEqual(ilrLearnerEntry.Utils.MessagesConstants.TYPEYR_ValidationMessage, validationMessage);
        }

        [Test]
        [Category("Category [Unit]")]
        public void Learners_LearningDelivery_LearningDeliveryHE_When_TYPEYR_IsNotBlank_ShouldNotGiveValidationError()
        {
            // Arrange
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "GivenName";

            // Act
            var learningDelivery = learner.CreateLearningDelivery();
            var learningDeliveryHE = learningDelivery.CreateLearningDeliveryHE();
            learningDeliveryHE.TYPEYR = 1; //any valid value
            var validationMessage = learningDeliveryHE["TYPEYR"];

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(validationMessage));
        }

        [Test]
        [Category("Category [Unit]")]
        public void Learners_LearningDelivery_LearningDeliveryHE_When_MODESTUD_IsBlank_ShouldGiveModeOfStudyValidationError()
        {
            // Arrange
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "GivenName";

            // Act
            var learningDelivery = learner.CreateLearningDelivery();
            var learningDeliveryHE = learningDelivery.CreateLearningDeliveryHE();
            var validationMessage = learningDeliveryHE["MODESTUD"];

            // Assert
            Assert.IsTrue(!string.IsNullOrEmpty(validationMessage));
            Assert.AreEqual(ilrLearnerEntry.Utils.MessagesConstants.MODESTUD_ValidationMessage, validationMessage);
        }

        [Test]
        [Category("Category [Unit]")]
        public void Learners_LearningDelivery_LearningDeliveryHE_When_MODESTUD_IsNotBlank_ShouldNotGiveValidationError()
        {
            // Arrange
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "GivenName";

            // Act
            var learningDelivery = learner.CreateLearningDelivery();
            var learningDeliveryHE = learningDelivery.CreateLearningDeliveryHE();
            learningDeliveryHE.MODESTUD = 1; //any valid value
            var validationMessage = learningDeliveryHE["MODESTUD"];

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(validationMessage));
        }

        [Test]
        [Category("Category [Unit]")]
        public void Learners_LearningDelivery_LearningDeliveryHE_When_FUNDLEV_IsBlank_ShouldGiveLevelApplicableToFundingCouncilHEIFESValidationError()
        {
            // Arrange
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "GivenName";

            // Act
            var learningDelivery = learner.CreateLearningDelivery();
            var learningDeliveryHE = learningDelivery.CreateLearningDeliveryHE();
            var validationMessage = learningDeliveryHE["FUNDLEV"];

            // Assert
            Assert.IsTrue(!string.IsNullOrEmpty(validationMessage));
            Assert.AreEqual(ilrLearnerEntry.Utils.MessagesConstants.FUNDLEV_ValidationMessage, validationMessage);
        }

        [Test]
        [Category("Category [Unit]")]
        public void Learners_LearningDelivery_LearningDeliveryHE_When_FUNDLEV_IsNotBlank_ShouldNotGiveValidationError()
        {
            // Arrange
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "GivenName";

            // Act
            var learningDelivery = learner.CreateLearningDelivery();
            var learningDeliveryHE = learningDelivery.CreateLearningDeliveryHE();
            learningDeliveryHE.FUNDLEV = 1; //any valid value
            var validationMessage = learningDeliveryHE["FUNDLEV"];

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(validationMessage));
        }

        [Test]
        [Category("Category [Unit]")]
        public void Learners_LearningDelivery_LearningDeliveryHE_When_FUNDCOMP_IsBlank_ShouldGiveCompletionOfYearOfInstanceValidationError()
        {
            // Arrange
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "GivenName";

            // Act
            var learningDelivery = learner.CreateLearningDelivery();
            var learningDeliveryHE = learningDelivery.CreateLearningDeliveryHE();
            var validationMessage = learningDeliveryHE["FUNDCOMP"];

            // Assert
            Assert.IsTrue(!string.IsNullOrEmpty(validationMessage));
            Assert.AreEqual(ilrLearnerEntry.Utils.MessagesConstants.FUNDCOMP_ValidationMessage, validationMessage);
        }

        [Test]
        [Category("Category [Unit]")]
        public void Learners_LearningDelivery_LearningDeliveryHE_When_FUNDCOMP_IsNotBlank_ShouldNotGiveValidationError()
        {
            // Arrange
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "GivenName";

            // Act
            var learningDelivery = learner.CreateLearningDelivery();
            var learningDeliveryHE = learningDelivery.CreateLearningDeliveryHE();
            learningDeliveryHE.FUNDCOMP = 1; //any valid value
            var validationMessage = learningDeliveryHE["FUNDCOMP"];

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(validationMessage));
        }

        [Test]
        [Category("Category [Unit]")]
        public void Learners_LearningDelivery_LearningDeliveryHE_When_STULOAD_IsBlank_ShouldNotHaveValidationError()
        {
            // Arrange
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "GivenName";

            // Act
            var learningDelivery = learner.CreateLearningDelivery();
            var learningDeliveryHE = learningDelivery.CreateLearningDeliveryHE();
            var validationMessage = learningDeliveryHE["STULOAD"];

            // Assert 
            Assert.IsTrue(string.IsNullOrEmpty(validationMessage));
        }

        [Test]
        [Category("Category [Unit]")]
        public void Learners_LearningDelivery_LearningDeliveryHE_When_STULOAD_IsNotBlank_ShouldNotHaveValidationError()
        {
            // Arrange  
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "GivenName";

            // Act
            var learningDelivery = learner.CreateLearningDelivery();
            var learningDeliveryHE = learningDelivery.CreateLearningDeliveryHE();
            learningDeliveryHE.STULOAD = 123;
            var validationMessage = learningDeliveryHE["STULOAD"];

            // Assert 
            Assert.IsTrue(string.IsNullOrEmpty(validationMessage));
        }

        [Test]
        [Category("Category [Unit]")]
        public void Learners_LearningDelivery_LearningDeliveryHE_When_YEARSTU_IsBlank_ShouldGiveYearOfStudentOnThisInstanceValidationError()
        {
            // Arrange
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "GivenName";

            // Act
            var learningDelivery = learner.CreateLearningDelivery();
            var learningDeliveryHE = learningDelivery.CreateLearningDeliveryHE();
            var validationMessage = learningDeliveryHE["YEARSTU"];

            // Assert
            Assert.IsTrue(!string.IsNullOrEmpty(validationMessage));
            Assert.AreEqual(ilrLearnerEntry.Utils.MessagesConstants.YEARSTU_ValidationMessage, validationMessage);
        }

        [Test]
        [Category("Category [Unit]")]
        public void Learners_LearningDelivery_LearningDeliveryHE_When_YEARSTU_IsNotBlank_ShouldNotGiveValidationError()
        {
            // Arrange
            Learner learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "GivenName";

            // Act
            var learningDelivery = learner.CreateLearningDelivery();
            var learningDeliveryHE = learningDelivery.CreateLearningDeliveryHE();
            learningDeliveryHE.YEARSTU = 1; //any valid value
            var validationMessage = learningDeliveryHE["YEARSTU"];

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(validationMessage));
        }

        [Test]
        [Category("Category [Unit]")]
        public void Learners_LearningDelivery_LearningDeliveryHE_When_MSTUFEE_IsBlank_ShouldGiveMajorSourceOfTuitonFeesValidationError()
        {
            // Arrange
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "GivenName";

            // Act
            var learningDelivery = learner.CreateLearningDelivery();
            var learningDeliveryHE = learningDelivery.CreateLearningDeliveryHE();
            var validationMessage = learningDeliveryHE["MSTUFEE"];

            // Assert
            Assert.IsTrue(!string.IsNullOrEmpty(validationMessage));
            Assert.AreEqual(ilrLearnerEntry.Utils.MessagesConstants.MSTUFEE_ValidationMessage, validationMessage);
        }

        [Test]
        [Category("Category [Unit]")]
        public void Learners_LearningDelivery_LearningDeliveryHE_When_MSTUFEE_IsNotBlank_ShouldNotGiveValidationError()
        {
            // Arrange
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "GivenName";

            // Act
            var learningDelivery = learner.CreateLearningDelivery();
            var learningDeliveryHE = learningDelivery.CreateLearningDeliveryHE();
            learningDeliveryHE.MSTUFEE = 1; //any valid value
            var validationMessage = learningDeliveryHE["MSTUFEE"];

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(validationMessage));
        }

        [Test]
        [Category("Category [Unit]")]
        public void Learners_LearningDelivery_LearningDeliveryHE_When_PCOLAB_IsBlank_ShouldNotHaveValidationError()
        {
            // Arrange 
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "GivenName";

            // Act
            var learningDelivery = learner.CreateLearningDelivery();
            var learningDeliveryHE = learningDelivery.CreateLearningDeliveryHE();
            var validationMessage = learningDeliveryHE["PCOLAB"];

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(validationMessage));
        }

        [Test]
        [Category("Category [Unit]")]
        public void Learners_LearningDelivery_LearningDeliveryHE_When_PCOLAB_IsNotBlank_ShouldNotHaveValidationError()
        {
            // Arrange
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "GivenName";

            // Act
            var learningDelivery = learner.CreateLearningDelivery();
            var learningDeliveryHE = learningDelivery.CreateLearningDeliveryHE();
            learningDeliveryHE.PCOLAB = 123;
            var validationMessage = learningDeliveryHE["PCOLAB"];

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(validationMessage));
        }

        [Test]
        [Category("Category [Unit]")]
        public void Learners_LearningDelivery_LearningDeliveryHE_When_PCFLDCS_IsBlank_ShouldNotHaveValidationError()
        {
            // Arrange 
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "GivenName";

            // Act
            var learningDelivery = learner.CreateLearningDelivery();
            var learningDeliveryHE = learningDelivery.CreateLearningDeliveryHE();
            var validationMessage = learningDeliveryHE["PCFLDCS"];

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(validationMessage));
        }

        [Test]
        [Category("Category [Unit]")]
        public void Learners_LearningDelivery_LearningDeliveryHE_When_PCFLDCS_IsNotBlank_ShouldNotHaveValidationError()
        {
            // Arrange 
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "GivenName";

            // Act
            var learningDelivery = learner.CreateLearningDelivery();
            var learningDeliveryHE = learningDelivery.CreateLearningDeliveryHE();
            learningDeliveryHE.PCFLDCS = 123;
            var validationMessage = learningDeliveryHE["PCFLDCS"];

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(validationMessage));
        }

        [Test]
        [Category("Category [Unit]")]
        public void Learners_LearningDelivery_LearningDeliveryHE_When_PCSLDCS_IsBlank_ShouldNotHaveValidationError()
        {
            // Arrange 
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "GivenName";

            // Act
            var learningDelivery = learner.CreateLearningDelivery();
            var learningDeliveryHE = learningDelivery.CreateLearningDeliveryHE();
            var validationMessage = learningDeliveryHE["PCSLDCS"];

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(validationMessage));
        }

        [Test]
        [Category("Category [Unit]")]
        public void Learners_LearningDelivery_LearningDeliveryHE_When_PCSLDCS_IsNotBlank_ShouldNotHaveValidationError()
        {
            // Arrange
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "GivenName";

            // Act
            var learningDelivery = learner.CreateLearningDelivery();
            var learningDeliveryHE = learningDelivery.CreateLearningDeliveryHE();
            learningDeliveryHE.PCSLDCS = 123;
            var validationMessage = learningDeliveryHE["PCSLDCS"];

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(validationMessage));
        }

        [Test]
        [Category("Category [Unit]")]
        public void Learners_LearningDelivery_LearningDeliveryHE_When_PCTLDCS_IsBlank_ShouldNotHaveValidationError()
        {
            // Arrange
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "GivenName";

            // Act
            var learningDelivery = learner.CreateLearningDelivery();
            var learningDeliveryHE = learningDelivery.CreateLearningDeliveryHE();
            var validationMessage = learningDeliveryHE["PCTLDCS"];

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(validationMessage));
        }

        [Test]
        [Category("Category [Unit]")]
        public void Learners_LearningDelivery_LearningDeliveryHE_When_PCTLDCS_IsNotBlank_ShouldNotHaveValidationError()
        {
            // Arrange
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "GivenName";

            // Act
            var learningDelivery = learner.CreateLearningDelivery();
            var learningDeliveryHE = learningDelivery.CreateLearningDeliveryHE();
            learningDeliveryHE.PCTLDCS =123;
            var validationMessage = learningDeliveryHE["PCTLDCS"];

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(validationMessage));
        }

        [Test]
        [Category("Category [Unit]")]
        public void Learners_LearningDelivery_LearningDeliveryHE_When_SPECFEE_IsBlank_ShouldGiveSpecialFeeIndicatorValidationError()
        {
            // Arrange
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "GivenName";

            // Act
            var learningDelivery = learner.CreateLearningDelivery();
            var learningDeliveryHE = learningDelivery.CreateLearningDeliveryHE();
            var validationMessage = learningDeliveryHE["SPECFEE"];

            // Assert
            Assert.IsTrue(!string.IsNullOrEmpty(validationMessage));
            Assert.AreEqual(ilrLearnerEntry.Utils.MessagesConstants.SPECFEE_ValidationMessage, validationMessage);
        }

        [Test]
        [Category("Category [Unit]")]
        public void Learners_LearningDelivery_LearningDeliveryHE_When_SPECFEE_IsNotBlank_ShouldNotGiveValidationError()
        {
            // Arrange
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "GivenName";

            // Act
            var learningDelivery = learner.CreateLearningDelivery();
            var learningDeliveryHE = learningDelivery.CreateLearningDeliveryHE();
            learningDeliveryHE.SPECFEE = 1; //any valid value
            var validationMessage = learningDeliveryHE["SPECFEE"];

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(validationMessage));
        }

        [Test]
        [Category("Category [Unit]")]
        public void Learners_LearningDelivery_LearningDeliveryHE_When_NETFEE_IsBlank_ShouldNotHaveValidationError()
        {
            // Arrange            
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "GivenName";

            // Act
            var learningDelivery = learner.CreateLearningDelivery();
            var learningDeliveryHE = learningDelivery.CreateLearningDeliveryHE();
            var validationMessage = learningDeliveryHE["NETFEE"];

            // Assert 
            Assert.IsTrue(string.IsNullOrEmpty(validationMessage));
        }

        [Test]
        [Category("Category [Unit]")]
        public void Learners_LearningDelivery_LearningDeliveryHE_When_NETFEE_IsNotBlank_ShouldNotHaveValidationError()
        {
            // Arrange   
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "GivenName";

            // Act
            var learningDelivery = learner.CreateLearningDelivery();
            var learningDeliveryHE = learningDelivery.CreateLearningDeliveryHE();
            learningDeliveryHE.NETFEE = 123;
            var validationMessage = learningDeliveryHE["NETFEE"];

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(validationMessage));
        }

        [Test]
        [Category("Category [Unit]")]
        public void Learners_LearningDelivery_LearningDeliveryHE_When_GROSSFEE_IsBlank_ShouldNotHaveValidationError()
        {
            // Arrange 
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "GivenName";

            // Act
            var learningDelivery = learner.CreateLearningDelivery();
            var learningDeliveryHE = learningDelivery.CreateLearningDeliveryHE();
            var validationMessage = learningDeliveryHE["GROSSFEE"];

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(validationMessage));
        }

        [Test]
        [Category("Category [Unit]")]
        public void Learners_LearningDelivery_LearningDeliveryHE_When_GROSSFEE_IsNotBlank_ShouldNotHaveValidationError()
        {
            // Arrange 
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "GivenName";

            // Act
            var learningDelivery = learner.CreateLearningDelivery();
            var learningDeliveryHE = learningDelivery.CreateLearningDeliveryHE();
            learningDeliveryHE.GROSSFEE = 123;
            var validationMessage = learningDeliveryHE["GROSSFEE"];

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(validationMessage));
        }

        [Test]
        [Category("Category [Unit]")]
        public void Learners_LearningDelivery_LearningDeliveryHE_When_DOMICILE_IsBlank_ShouldNotHaveValidationError()
        {
            //AAA 
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "GivenName";
            var learningDelivery = learner.CreateLearningDelivery();
            var learningDeliveryHE = learningDelivery.CreateLearningDeliveryHE();
            var validationMessage = learningDeliveryHE["DOMICILE"];
            Assert.IsTrue(string.IsNullOrEmpty(validationMessage));
        }

        [Test]
        [Category("Category [Unit]")]
        public void Learners_LearningDelivery_LearningDeliveryHE_When_DOMICILE_IsNotBlank_ShouldNotHaveValidationError()
        {
            // Arrange 
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "GivenName";

            // Act
            var learningDelivery = learner.CreateLearningDelivery();
            var learningDeliveryHE = learningDelivery.CreateLearningDeliveryHE();
            learningDeliveryHE.DOMICILE = "abc";
            var validationMessage = learningDeliveryHE["DOMICILE"];

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(validationMessage));
        }

        [Test]
        [Category("Category [Unit]")]
        public void Learners_LearningDelivery_LearningDeliveryHE_When_ELQ_IsBlank_ShouldNotHaveValidationError()
        {
            // Arrange 
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "GivenName";

            // Act
            var learningDelivery = learner.CreateLearningDelivery();
            var learningDeliveryHE = learningDelivery.CreateLearningDeliveryHE();
            var validationMessage = learningDeliveryHE["ELQ"];

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(validationMessage));
        }

        [Test]
        [Category("Category [Unit]")]
        public void Learners_LearningDelivery_LearningDeliveryHE_When_ELQ_IsNotBlank_ShouldNotHaveValidationError()
        {
            // Arrange 
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "GivenName";

            // Act
            var learningDelivery = learner.CreateLearningDelivery();
            var learningDeliveryHE = learningDelivery.CreateLearningDeliveryHE();
            learningDeliveryHE.ELQ = 123;
            var validationMessage = learningDeliveryHE["ELQ"];

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(validationMessage));
        }

        [Test]
        [Category("Category [Unit]")]
        public void Learners_LearningDelivery_LearningDeliveryHE_When_HEPostCode_IsBlank_ShouldNotHaveValidationError()
        {
            // Arrange 
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "GivenName";

            // Act
            var learningDelivery = learner.CreateLearningDelivery();
            var learningDeliveryHE = learningDelivery.CreateLearningDeliveryHE();
            var validationMessage = learningDeliveryHE["HEPostCode"];

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(validationMessage));
        }

        [Test]
        [Category("Category [Unit]")]
        public void Learners_LearningDelivery_LearningDeliveryHE_When_HEPostCode_IsNotBlank_ShouldNotHaveValidationError()
        {
            // Arrange 
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "GivenName";
            
            // Act
            var learningDelivery = learner.CreateLearningDelivery();
            var learningDeliveryHE = learningDelivery.CreateLearningDeliveryHE();
            learningDeliveryHE.HEPostCode = "abc";
            var validationMessage = learningDeliveryHE["HEPostCode"];

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(validationMessage));
        }
    }
}
