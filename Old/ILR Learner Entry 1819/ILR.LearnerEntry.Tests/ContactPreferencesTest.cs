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
using System.Security.Policy;

namespace ILR.LearnerEntry.Tests
{
    [TestFixture]
    public class ContactPreferencesTest
    {
        Message ilrMessage = null;
        string fileName = null;
        string importFile = null;
        private const string ILRFileName = "internalIlr1819.ilr";
        private const string ILRFileToImport = "ILR-1234567-1718-01.xml";
        XNamespace ns = "ESFA/ILR/2018-19";

        Learner learner = null;

        [SetUp]
        public void Setup()
        {

            UriBuilder uri = new UriBuilder(Assembly.GetExecutingAssembly().CodeBase);
            string path = Uri.UnescapeDataString(uri.Path);


            fileName = Path.Combine(Path.GetDirectoryName(path), ILRFileName);

            ilrMessage = new Message(fileName);
            importFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), ILRFileToImport);
            ilrMessage.Import(importFile);
            learner = ilrMessage.LearnerList[0];
            Assert.NotNull(learner.LearningDeliveryList[0].StdCode, "Setup failed, unable to load learner from test file");
        }

        //[Test]
        //public void When_Select_LearnerIsDead_RU1_RUI2_RUI4_RUI5_and_RUI6_ShouldBeFalse()
        //{

        //    //setup
        //    learner.RUI1 = true;

        //    learner.RUI5 = true;
        //    Assert.IsFalse(learner.RUI1, "Unable to remove contact preference about course or learning opportunity when learner is set to have died");
        //    Assert.IsFalse(learner.RUI2, "Unable to remove contact preference For surveys and research when learner is set to have died");

        //    Assert.IsFalse(learner.RUI4, "Unable to remove contact preference for 'Suffered sever illness or other circumstance' when learner is set to have died");

        //    Assert.IsFalse(learner.RUI6, "Unable to remove contact preference for leaners started on or after 25th May 2018 about course or learning opportunity when learner is set to have died");
        //    Assert.IsFalse(learner.RUI7, "Unable to remove contact preference for leaners started on or after 25th May 2018 For surveys and research when learner is set to have died");

        //}


        //[Test]
        //public void When_Select_LearnerIsSufferedIllness_RU1_RUI2_RUI5_RUI5_and_RUI6_ShouldBeFalse()
        //{

        //    //setup
        //    learner.RUI1 = true;

        //    learner.RUI4 = true;
        //    Assert.IsFalse(learner.RUI1, "Unable to remove contact preference about course or learning opportunity when learner is set to have died");
        //    Assert.IsFalse(learner.RUI2, "Unable to remove contact preference For surveys and research when learner is set to have died");

        //    Assert.IsFalse(learner.RUI5, "Unable to remove contact preference 'Learner has died' when learner is set to have suffered severe illness");

        //    Assert.IsFalse(learner.RUI6, "Unable to remove contact preference for leaners started on or after 25th May 2018 about course or learning opportunity when learner is set to have died");
        //    Assert.IsFalse(learner.RUI7, "Unable to remove contact preference for leaners started on or after 25th May 2018 For surveys and research when learner is set to have died");

        //}

        //[Test]
        //public void When_LearnerIsDeadSelected_And_RU1Selected_LearnerIsDeadShouldBeFalse()
        //{

        //    //setup
        //    learner.RUI5 = true;

        //    learner.RUI1 = true;
        //    Assert.IsFalse(learner.RUI5, "Unable to remove contact preference 'Learner has died' when learner preference 'About course or learning opportunities' is set for learners starts before May 2018");
        //}

        //[Test]
        //public void When_LearnerIsDeadSelected_And_RU2Selected_LearnerIsDeadShouldBeFalse()
        //{

        //    //setup
        //    learner.RUI5 = true;

        //    learner.RUI2 = true;
        //    Assert.IsFalse(learner.RUI5, "Unable to remove contact preference 'Learner has died' when learner preference 'For surveys and research' is set for learners starts before May 2018");
        //}

        //[Test]
        //public void When_LearnerIsDeadSelected_And_RU6Selected_LearnerIsDeadShouldBeFalse()
        //{

        //    //setup
        //    learner.RUI5 = true;

        //    learner.RUI6 = true;
        //    Assert.IsFalse(learner.RUI5, "Unable to remove contact preference 'Learner has died' when learner preference 'About course or learning opportunities' is set for learners starts after May 2018");
        //}

        //[Test]
        //public void When_LearnerIsDeadSelected_And_RU7Selected_LearnerIsDeadShouldBeFalse()
        //{

        //    //setup
        //    learner.RUI5 = true;

        //    learner.RUI7 = true;
        //    Assert.IsFalse(learner.RUI5, "Unable to remove contact preference 'Learner has died' when learner preference 'For surveys and research' is set for learners starts after May 2018");
        //}
    }
}
