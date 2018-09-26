using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ILR.LearnerEntry.Tests
{
    [TestFixture]
    public class LearnerDeliveryHETests
    {
        private Message message =null;
        private string dataFolder = string.Empty;
        private string internalFileName = "internal1819.ilr";
        //private const string TYPEYR_VALIDATION_MESSAGE = "Type of instance year is mandatory.";

        [TestFixtureSetUp]
        public void Setup()
        {
            Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            string Path = assembly.Location;
            System.IO.FileInfo fi = new System.IO.FileInfo(Path);
            dataFolder = fi.DirectoryName;
            var fileName = System.IO.Path.Combine(dataFolder, internalFileName);
            var logFileName = System.IO.Path.Combine(dataFolder, "learnerdeliveryHETests.log");
            message = new Message(fileName, logFileName);
        }

        [Test]
        public void When_NUMHUS_IsBlank_ShouldNotHaveValidationError()
        {
            //AAA 
            var learner = message.CreateLearner();
            learner.FamilyName ="familyName";
            learner.GivenNames = "GivenName";
            var learningDelivery = learner.CreateLearningDelivery();
            var learningDeliveryHE = learningDelivery.CreateLearningDeliveryHE();
            var validationMessage = learningDeliveryHE["NUMHUS"];
            Assert.IsTrue(string.IsNullOrEmpty(validationMessage));
        }

        [Test]
        public void When_NUMHUS_IsNotBlank_ShouldNotHaveValidationError()
        {
            //AAA 
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "GivenName";
            var learningDelivery = learner.CreateLearningDelivery();
            var learningDeliveryHE = learningDelivery.CreateLearningDeliveryHE();
            learningDeliveryHE.NUMHUS = "abc";
            var validationMessage = learningDeliveryHE["NUMHUS"];
            Assert.IsTrue(string.IsNullOrEmpty(validationMessage));
        }


        [Test]
        public void When_TYPEYR_IsBlank_ShouldGiveTypeOfYearSpecificValidationError()
        {
            //AAA 
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "GivenName";
            var learningDelivery = learner.CreateLearningDelivery();
            var learningDeliveryHE = learningDelivery.CreateLearningDeliveryHE();
            var validationMessage = learningDeliveryHE["TYPEYR"];
            Assert.IsTrue(!string.IsNullOrEmpty(validationMessage));
            Assert.AreEqual(ilrLearnerEntry.Utils.MessagesConstants.TYPEYR_VALIDATION_MESSAGE, validationMessage);
        }

        [Test]
        public void When_TYPEYR_IsNotBlank_ShouldNotGiveValidationError()
        {
            //AAA 
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "GivenName";
            var learningDelivery = learner.CreateLearningDelivery();
            var learningDeliveryHE = learningDelivery.CreateLearningDeliveryHE();
            learningDeliveryHE.TYPEYR = 1; //any valid value
            var validationMessage = learningDeliveryHE["TYPEYR"];
            Assert.IsTrue(string.IsNullOrEmpty(validationMessage));
        }

        [Test]
        public void When_MODESTUD_IsBlank_ShouldGiveModeOfStudyValidationError()
        {
            //AAA 
            var learner = message.CreateLearner();
            learner.FamilyName = "familyName";
            learner.GivenNames = "GivenName";
            var learningDelivery = learner.CreateLearningDelivery();
            var learningDeliveryHE = learningDelivery.CreateLearningDeliveryHE();
            var validationMessage = learningDeliveryHE["MODESTUD"];
            Assert.IsTrue(!string.IsNullOrEmpty(validationMessage));
            Assert.AreEqual(ilrLearnerEntry.Utils.MessagesConstants.MODESTUD_VALIDATION_MESSAGE, validationMessage);
        }
    }
}
