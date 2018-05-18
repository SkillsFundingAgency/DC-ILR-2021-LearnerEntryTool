using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace ILR.LearnerEntry.Tests
{
    [TestFixture]
    public class MessageImportTests
    {

        private const string ILRFileName = "internalIlr1819.ilr";
        private const string ILRFileToImport = "ILR-1234567-1718-01.xml";
        XNamespace ns = "ESFA/ILR/2018-19";
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
       
        [Test]
        public void Test03_Import_WhenFileContainsTrailBlazerFinRecord_OutputFile_ShouldHaveFinRecords()
        {
           
            

            string fileName = Path.Combine(Directory.GetCurrentDirectory(), ILRFileName);
            Message ilrMessage = new Message(fileName);
            string importFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), ILRFileToImport);
            ilrMessage.Import(importFile);
            Assert.True(ilrMessage.LearnerList.Count > 0, "Unable to populate learners from imported file");
            XDocument xDoc = XDocument.Load(fileName);
            var query = from t in xDoc.Descendants(ns + "AFinAmount")
                        select t.Value;
            var val = query.First();
            Assert.NotNull(val, "AppFinRecords are not created");
            Assert.AreEqual(val, "5000", "ApprenticeShip finance amount is wrong");
            // Assert.True(File.Exists(fileName), "Failed to create internal file");
        }

        [Test]
        public void Test04_Export_WhenFileValidatedWithSchema_ShouldHaveNoErrors()
        {
            XNamespace targateNs = "ESFA/ILR/2018-19";

            string fileName = Path.Combine(Directory.GetCurrentDirectory(), ILRFileName);
            Message ilrMessage = new Message(fileName);
            string importFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), ILRFileToImport);
            ilrMessage.Import(importFile);
            Assert.True(ilrMessage.LearnerList.Count > 0, "Unable to populate learners from imported file");

            string exportFileFolder = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Exported");
            if (!Directory.Exists(exportFileFolder))
                Directory.CreateDirectory(exportFileFolder);
            else
            {
                Directory.Delete(exportFileFolder, true);
                Directory.CreateDirectory(exportFileFolder);
            }
            string xsdFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "ILR-2018-19-v1.xsd");


            
            XDocument xDoc = XDocument.Load(fileName);
            ilrMessage.Export(exportFileFolder, "01");

            string exportFile = Directory.GetFiles(exportFileFolder, "*.xml").FirstOrDefault();
            bool validFile =IsValidXml(exportFile, xsdFilePath, targateNs.NamespaceName);

             Assert.True(validFile, "Exported file failed to validate against the ILR schema!");
        }


        [Test]
        public void Test05_Import_WhenFileContainsESFConRefNum_OutputFile_ShouldHaveConRefNum()
        {

            
            XNamespace nsa = "http://schemas.datacontract.org/2004/07/My.Namespace";

            string fileName = Path.Combine(Directory.GetCurrentDirectory(), ILRFileName);
            Message ilrMessage = new Message(fileName);
            string importFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), ILRFileToImport);
            ilrMessage.Import(importFile);
            Assert.True(ilrMessage.LearnerList.Count > 0, "Unable to populate learners from imported file");
            XDocument xDoc = XDocument.Load(fileName);
            var query = from t in xDoc.Descendants(ns + "ConRefNumber")
                        select t.Value;
            var val = query.First();
            Assert.NotNull(val, "ConRefNumber are not created");
            Assert.AreEqual(val, "ESF-123456789", "ConRefNumber value is wrong");
            // Assert.True(File.Exists(fileName), "Failed to create internal file");
        }


        [Test]
        public void Test06_Import_WhenFileDoesNotContainsSwAimId_OutputFile_ShouldHaveSwAimId()
        {
            string fileName = Path.Combine(Directory.GetCurrentDirectory(), ILRFileName);
            Message ilrMessage = new Message(fileName);
            string importFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), ILRFileToImport);
            ilrMessage.Import(importFile);
            Assert.True(ilrMessage.LearnerList.Count > 0, "Unable to populate learners from imported file");

            XDocument xDoc = XDocument.Load(importFile);
            

            var swquery = from t in xDoc.Descendants(ns + "SWSupAimId")
                          select t.Value;

            var swIdFound = swquery.Any();

            Assert.IsFalse(swIdFound, "swSuppAimId already exists in the test file, aborting");

            XDocument importedDoc = XDocument.Load(fileName);

            swquery = from t in importedDoc.Descendants(ns + "SWSupAimId")
                          select t.Value;

            swIdFound = swquery.Any();
            Assert.IsTrue(swIdFound, "swSuppAimId is missing from the internal ilr file after import, aborting");


        }




        #region helper functions
        public static bool IsValidXml(string xmlFilePath, string xsdFilePath, string namespaceName)
        {
            var xdoc = XDocument.Load(xmlFilePath);
            var schemas = new XmlSchemaSet();
            schemas.Add(namespaceName, xsdFilePath);

            try
            {
                xdoc.Validate(schemas, null);
            }
            catch (XmlSchemaValidationException)
            {
                return false;
            }

            return true;
        }
        #endregion
    }
}
