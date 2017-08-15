using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace ILR.LearnerEntry.Tests
{
    [TestFixture]
    public class MessageImportTests
    {

        private const string ILRFileName = "internalIlr1718.ilr";
        private const string ILRFileToImport = "ILR-1234567-1617-01.xml";
        [OneTimeSetUp]
        public void Setup()
        {

        }

        [Test]
        public void Test01_Import_WhenFileContainsStdCode_OutputFile_ShouldHaveCode()
        {
            string fileName = Path.Combine(Directory.GetCurrentDirectory(), ILRFileName);
            Message ilrMessage = new Message(fileName);
            string importFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), ILRFileToImport);
            ilrMessage.Import(importFile);
            Assert.True(ilrMessage.LearnerList.Count > 0, "Unable to populate learners from imported file");
            var learner = ilrMessage.LearnerList[0];
            Assert.NotNull(learner.LearningDeliveryList[0].StdCode, "Apprenticeship standard code is failed to import, import failed");

           // Assert.True(File.Exists(fileName), "Failed to create internal file");


        }

    }
}
