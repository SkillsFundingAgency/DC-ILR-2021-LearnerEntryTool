﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Data;
using System.Xml.Xsl;
using System.Reflection;

namespace ILR
{
    public class Message : INotifyPropertyChanged
    {
        #region Year-specific constants

        public static int CurrentYear = 2021;
        public static string CurrentNameSpace = @"ESFA/ILR/2020-21";
        public static string PreviousNameSpace = @"ESFA/ILR/2019-20";
        public static string FileNameTemplate = "ILR-$$UKPRN$$-$$YEAR$$-$$NOW$$-01.xml";
        public static DateTime CurrentYearStart = new DateTime(2020, 8, 1);
        public static string ProtectiveMarking = "OFFICIAL-SENSITIVE-Personal";
        private DataTable _statistics = new DataTable();
        private static bool _isImportRunning = false;
        private static string _logFileName = string.Empty;
        public static string ILR2020_21_XSLT = "ILR-2020-21.xslt";

        #endregion

        #region Counts

        public DataTable Statistics
        {
            get { return _statistics; }
        }

        public DataTable StatisticsOld
        {
            get
            {
                int learners = 0;
                int learningDeliveries = 0;
                int fm10 = 0;
                int fm25 = 0;
                int fm35 = 0;
                int fm70 = 0;
                int fm81 = 0;
                int fm82 = 0;
                int fm99 = 0;

                /*				foreach (Learner l in LearnerList)
                                {
                                    learners++;
                                    learningDeliveries += l.LearningDeliveryList.Count;

                                    if (l.HasLearningDeliveriesInFundingModel(10)) fm10++;
                                    if (l.HasLearningDeliveriesInFundingModel(25)) fm25++;
                                    if (l.HasLearningDeliveriesInFundingModel(35)) fm35++;
                                    if (l.HasLearningDeliveriesInFundingModel(70)) fm70++;
                                    if (l.HasLearningDeliveriesInFundingModel(81)) fm81++;
                                    if (l.HasLearningDeliveriesInFundingModel(82)) fm82++;
                                    if (l.HasLearningDeliveriesInFundingModel(99)) fm99++;
                                }
                */
                using (DataTable results = new DataTable())
                {
                    results.Columns.Add(new DataColumn("Description", typeof(string)));
                    results.Columns.Add(new DataColumn("Count", typeof(string)));
                    DataRow row = results.NewRow();
                    row["Description"] = "Learner count";
                    row["Count"] = learners.ToString();
                    results.Rows.Add(row);

                    if (learners > 0)
                    {
                        row = results.NewRow();
                        row["Description"] = "LearningDelivery count";
                        row["Count"] = learningDeliveries.ToString();
                        results.Rows.Add(row);
                        AddFundingModelRowStatistic(results, 10, fm10);
                        AddFundingModelRowStatistic(results, 25, fm25);
                        AddFundingModelRowStatistic(results, 35, fm35);
                        AddFundingModelRowStatistic(results, 70, fm70);
                        AddFundingModelRowStatistic(results, 81, fm81);
                        AddFundingModelRowStatistic(results, 82, fm82);
                        AddFundingModelRowStatistic(results, 99, fm99);

                        row = results.NewRow();
                        row["Description"] = "Learners excluded from export count";
                        row["Count"] = (LearnerCount - LearnerExportCount).ToString();
                        results.Rows.Add(row);
                    }

                    return results;
                }
            }
        }

        public void ReFreshStats()
        {
            ReBuildStatisticsDatatable();
        }

        public void ReBuildStatisticsDatatable()
        {
            _statistics = new DataTable();

            int learners = 0;
            int learningDeliveries = 0;
            int fm10 = 0;
            int fm25 = 0;
            int fm35 = 0;
            int fm70 = 0;
            int fm81 = 0;
            int fm82 = 0;
            int fm99 = 0;

            foreach (Learner l in LearnerList)
            {
                learners++;
                learningDeliveries += l.LearningDeliveryList.Count;

                if (l.HasLearningDeliveriesInFundingModel(10)) fm10++;
                if (l.HasLearningDeliveriesInFundingModel(25)) fm25++;
                if (l.HasLearningDeliveriesInFundingModel(35)) fm35++;
                if (l.HasLearningDeliveriesInFundingModel(70)) fm70++;
                if (l.HasLearningDeliveriesInFundingModel(81)) fm81++;
                if (l.HasLearningDeliveriesInFundingModel(82)) fm82++;
                if (l.HasLearningDeliveriesInFundingModel(99)) fm99++;
            }

            _statistics.Columns.Add(new DataColumn("Description", typeof(string)));
            _statistics.Columns.Add(new DataColumn("Count", typeof(string)));
            DataRow row = _statistics.NewRow();
            row["Description"] = "Learner count";
            row["Count"] = learners.ToString();
            _statistics.Rows.Add(row);

            if (learners > 0)
            {
                row = _statistics.NewRow();
                row["Description"] = "LearningDelivery count";
                row["Count"] = learningDeliveries.ToString();
                _statistics.Rows.Add(row);
                AddFundingModelRowStatistic(_statistics, 10, fm10);
                AddFundingModelRowStatistic(_statistics, 25, fm25);
                AddFundingModelRowStatistic(_statistics, 35, fm35);
                AddFundingModelRowStatistic(_statistics, 70, fm70);
                AddFundingModelRowStatistic(_statistics, 81, fm81);
                AddFundingModelRowStatistic(_statistics, 82, fm82);
                AddFundingModelRowStatistic(_statistics, 99, fm99);


                //if (learners < 500)
                //{
                row = _statistics.NewRow();
                row["Description"] = "Learners excluded from export count";
                row["Count"] = (learners - this.LearnerList.Count(x => !x.ExcludeFromExport)).ToString();

                _statistics.Rows.Add(row);
                //}
            }

            OnPropertyChanged("Statistics");
        }

        private void AddFundingModelRowStatistic(DataTable Table, int FundModel, int Learners)
        {
            if (Learners > 0)
            {
                DataRow row = Table.NewRow();
                row["Description"] = ILR.Lookup.GetDescription("FundModel", FundModel.ToString());
                row["Count"] = Learners;
                Table.Rows.Add(row);
            }
        }

        public int LearnerCount
        {
            get { return this.LearnerList.Count(); }
        }

        public int LearnerExportCount
        {
            get
            {
                int ExportlearnCounter = this.LearnerList.Count(x => x.IsComplete && !x.ExcludeFromExport);
                return ExportlearnCounter;
            }
        }

        public int LearningDeliveryCount
        {
            get
            {
                int learningDeliveries = 0;
                foreach (Learner learner in this.LearnerList)
                    learningDeliveries += learner.LearningDeliveryList.Count();
                return learningDeliveries;
            }
        }

        public int LearnerDestinationandProgressionCount
        {
            get { return this.LearnerDestinationandProgressionList.Count(); }
        }

        #endregion

        #region Private Properties

        XmlDocument ILRFile;
        XmlNamespaceManager NSMgr;
        string Filename;
        Dictionary<string, int> LearnerRefNumbers = new Dictionary<string, int>(1024);

        public static Boolean IsLoggingEnabled
        {
            get { return _logFileName == string.Empty ? false : true; }
        }

        #endregion

        #region public Properties

        public static String LogFileName
        {
            set { _logFileName = value; }
        }

        public Boolean IsFileImportLoadingRunning
        {
            get { return _isImportRunning; }
            set
            {
                foreach (Learner l in LearnerList)
                {
                    l.IsFileImportLoadingRunning = value;
                }

                _isImportRunning = value;
            }
        }

        #endregion

        #region ILR Child Entites

        private Header Header;
        public LearningProvider LearningProvider;
        public List<Learner> LearnerList = new List<Learner>();

        public List<LearnerDestinationandProgression> LearnerDestinationandProgressionList =
            new List<LearnerDestinationandProgression>(0);

        #endregion

        #region Public Methods

        public void Load(string Filename)
        {
            Log("Message", "Load", String.Format("Filename : {0}", Filename));

            //Instantiate a new document to hold the ILR data
            this.ILRFile = new XmlDocument();

            //Get a namespace manager from the XML document
            NSMgr = new XmlNamespaceManager(ILRFile.NameTable);
            NSMgr.AddNamespace("ia", CurrentNameSpace);

            //Load the data
            Load(Filename, ILRFile, NSMgr);

            //Extract the Learner Ref numbers for quicker exist / count checks
            var lrns =  ILRFile.SelectNodes("/ia:Message/ia:Learner/ia:LearnRefNumber", NSMgr);

            LearnerRefNumbers.Clear();
            foreach (XmlNode lrn in lrns)
            {
                var data = lrn.FirstChild.Value;

                if (LearnerRefNumbers.TryGetValue(data, out int existingCount))
                {
                    LearnerRefNumbers[data] = ++existingCount;
                }
                else
                {
                    LearnerRefNumbers.Add(data, 1);
                }
            }
        }

        protected void LoadPreviousYearILR(string Filename, string LogFilename)
        {
            _logFileName = LogFilename;
            Log("Message", "Load Previous Year ILR", String.Format("Filename : {0}", Filename));

            //Instantiate a new document to hold the ILR data
            this.ILRFile = new XmlDocument();

            //Get a namespace manager from the XML document
            NSMgr = new XmlNamespaceManager(ILRFile.NameTable);
            NSMgr.AddNamespace("ia", PreviousNameSpace);

            //Load the data
            Load(Filename, ILRFile, NSMgr);
        }

        private void Load(string Filename, XmlDocument Document, XmlNamespaceManager NSMgr)
        {
            Log("Message", "Load", String.Format("Filename : {0}", Filename));
            IsFileImportLoadingRunning = true;

            //Store the filename we're using
            this.Filename = Filename;

            //Create an XML document object from the file specified
            Document.Load(Filename);

            //Find the Header node 
            XmlNode headerNode = Document.SelectSingleNode("/ia:Message/ia:Header", NSMgr);
            if (headerNode == null)
            {
                headerNode = Document.CreateElement("Header", NSMgr.LookupNamespace("ia"));
                if (Document.DocumentElement.HasChildNodes)
                    Document.DocumentElement.InsertBefore(headerNode, Document.DocumentElement.FirstChild);
                else
                    Document.DocumentElement.AppendChild(headerNode);
            }

            this.Header = new ILR.Header(headerNode, NSMgr);

            //Find the LearningProvider node 
            XmlNode learningProviderNode = Document.SelectSingleNode("/ia:Message/ia:LearningProvider", NSMgr);

            //Find the Learner nodes
            XmlNodeList learnerNodes = Document.SelectNodes("/ia:Message/ia:Learner", NSMgr);

            //If we have no LearningProvider node create one in the correct place
            if (learningProviderNode == null)
            {
                learningProviderNode = Document.CreateElement("LearningProvider", NSMgr.LookupNamespace("ia"));
                if (learnerNodes.Count == 0)
                    Document.DocumentElement.AppendChild(learningProviderNode);
                else
                    Document.DocumentElement.InsertBefore(learningProviderNode, learnerNodes.Item(0));
            }

            //Create a LearningProvider instance
            this.LearningProvider = new ILR.LearningProvider(learningProviderNode, NSMgr);

            //Create Learner instances for all of the learners in the XML
            foreach (XmlNode node in learnerNodes)
            {
                Learner newInstance = new Learner(node, NSMgr, IsFileImportLoadingRunning);
                newInstance.Message = this;
                newInstance.ResequenceAimSeqNumber();
                newInstance.IsFileImportLoadingRunning = false;
                LearnerList.Add(newInstance);
                Log("Message", "Load",
                    String.Format("Add Leaerner {0} of {1} - {2}", LearnerList.Count, learnerNodes.Count,
                        newInstance.LearnRefNumber));
            }

            //Find the LearnerDestinationandProgression nodes
            XmlNodeList learnerDestinationandProgressionNodes =
                Document.SelectNodes("/ia:Message/ia:LearnerDestinationandProgression", NSMgr);

            //Create LearnerDestinationandProgression instances for all of the LearnerDestinationandProgressions in the XML
            foreach (XmlNode node in learnerDestinationandProgressionNodes)
            {
                LearnerDestinationandProgression newLDP = new LearnerDestinationandProgression(node, NSMgr);
                newLDP.Message = this;
                LearnerDestinationandProgressionList.Add(newLDP);
                Log("Message", "Load",
                    String.Format("Add LDP {0} of {1} - {2}", LearnerDestinationandProgressionList.Count,
                        learnerDestinationandProgressionNodes.Count, newLDP.LearnRefNumber));
            }

            //foreach (Learner l in LearnerList)
            //{
            //    foreach (LearningDelivery ld in l.LearningDeliveryList)
            //    {
            //        ld.IsImportRunning = false;
            //    }
            //    l.IsFileImportLoadingRunning = false;
            //}
            IsFileImportLoadingRunning = false;
        }

        public void Save(string fileName)
        {
            Log("Message", "Save", fileName);
            // remove empty blank nodes.
            var document = XDocument.Parse(this.ILRFile.OuterXml);

            document.Descendants()
                .Where(e => e.IsEmpty || String.IsNullOrWhiteSpace(e.Value))
                .Remove();
            document.Save(fileName);

            //this.ILRFile.Save(Filename);
        }

        public void Save()
        {
            Log("Message", "Save", "");
            this.Save(this.Filename);
        }

        public void Export(string ExportFolder, string Release)
        {
            Log("Message", "Export", String.Format("Release : {0} - ExportFolder : {1}", Release, ExportFolder));

            //Calculate filename
            string filename = ExportFolder;
            if (!filename.EndsWith("\\"))
                filename += "\\";
            string exportFileName = FileNameTemplate.Replace("$$UKPRN$$", LearningProvider.UKPRN.ToString())
                .Replace("$$YEAR$$", CurrentYear.ToString())
                .Replace("$$NOW$$", DateTime.Now.ToString("yyyyMMdd-HHmmss"));

            filename += exportFileName;
            Log("Message", "Export", String.Format("filename  : {0}", filename));

            //Update header information
            this.Header.CollectionDetails.FilePreparationDate =
                new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            this.Header.Source.UKPRN = LearningProvider.UKPRN;
            this.Header.Source.SerialNo = "01";
            this.Header.Source.DateTime = DateTime.Now;
            this.Header.Source.ProtectiveMarking = Message.ProtectiveMarking;
            this.Header.CollectionDetails.Year = Message.CurrentYear.ToString();

            if (Release != null)
                this.Header.Source.Release = Release;

            //Save where we are
            Save();

            TransformAndExport(filename, exportFileName);
        }

        public void TransformAndExport(string fullExportFileName, string exportFileName)
        {
            Message exportMessage = new Message(this.Filename, _logFileName);

            for (int i = exportMessage.LearnerList.Count - 1; i >= 0; i--)
            {
                var learner = exportMessage.LearnerList[i];
                if (!learner.IsComplete || learner.ExcludeFromExport)
                {
                    exportMessage.Delete(learner);
                }
            }

            for (int i = exportMessage.LearnerDestinationandProgressionList.Count - 1; i >= 0; i--)
            {
                var learnerDestinationandProgression = exportMessage.LearnerDestinationandProgressionList[i];
                if (!learnerDestinationandProgression.IsComplete || learnerDestinationandProgression.ExcludeFromExport)
                {
                    exportMessage.Delete(learnerDestinationandProgression);
                }
            }

            string tempInternalPath = Path.Combine(Path.GetTempPath(), exportFileName);
            exportMessage.Save(tempInternalPath);
            exportMessage = null;
            TransformExportedFile(tempInternalPath, fullExportFileName);

            GC.Collect();
        }

        XslCompiledTransform xslTransformer;

        private string getXsltFileName()
        {

            var assembly = Assembly.GetExecutingAssembly();
            var xsltResourceName = assembly
                .GetManifestResourceNames()
                .First(x => x.ToUpper().EndsWith(ILR2020_21_XSLT.ToUpper()));

            string path = Path.Combine(Path.GetTempPath(), ILR2020_21_XSLT);
            try
            {
                using (Stream stream = assembly.GetManifestResourceStream(xsltResourceName))
                using (StreamReader reader = new StreamReader(stream))
                {
                    string result = reader.ReadToEnd();

                    File.WriteAllText(path, result);
                }
            }
            catch (IOException iox)
            {
                Log("Message", "getXsltFileName", "Exception during reading the xslt resource");
            }

            return path;
        }

        public void TransformExportedFile(string sourceFile, string destinationFile)
        {
            if (xslTransformer == null)
            {
                string xslFile = getXsltFileName();
                xslTransformer = new XslCompiledTransform();
                xslTransformer.Load(xslFile);
            }

            xslTransformer.Transform(sourceFile, destinationFile);
        }
        public void Import(string FilenameToLoad)
        {
            Log("Message", "Import", String.Format("File : {0}", FilenameToLoad));
            String InterlStoreFilename = this.Filename;

            //Instantiate a new document to hold the ILR data
            this.ILRFile = new XmlDocument();

            //Get a namespace manager from the XML document
            NSMgr = new XmlNamespaceManager(ILRFile.NameTable);
            NSMgr.AddNamespace("ia", CurrentNameSpace);

            this.ILRFile.AppendChild(this.ILRFile.CreateElement("Message", NSMgr.LookupNamespace("ia")));

            // Clear Old Learner and Old LearnerDestinationandProgression Records
            this.LearnerList.Clear();
            this.LearnerDestinationandProgressionList.Clear();


            Message importMessage = new Message();

            System.IO.FileInfo fi = new System.IO.FileInfo(FilenameToLoad);
            if (fi.Name.Contains("-" + CurrentYear.ToString() + "-"))
            {
                Log("Message", "Import", String.Format("Current Year : {0}", CurrentYear));
                this.Load(FilenameToLoad);
            }
            else if (fi.Name.Contains("-" + (CurrentYear - 101).ToString() + "-"))
            {
                Log("Message", "Import", String.Format("Load Previous Year ILR : {0}", (CurrentYear - 101)));
                importMessage.LoadPreviousYearILR(FilenameToLoad, _logFileName);

                XmlNode headerNode = this.ILRFile.SelectSingleNode("/ia:Message/ia:Header", NSMgr);
                if (headerNode == null)
                {
                    headerNode = this.ILRFile.CreateElement("Header", NSMgr.LookupNamespace("ia"));
                }

                if (this.ILRFile.DocumentElement.HasChildNodes)
                    this.ILRFile.DocumentElement.InsertBefore(headerNode, this.ILRFile.DocumentElement.FirstChild);
                else
                    this.ILRFile.DocumentElement.AppendChild(headerNode);

                this.Header = new ILR.Header(headerNode, NSMgr);

                //Find the LearningProvider node 
                XmlNode learningProviderNode = this.ILRFile.SelectSingleNode("/ia:Message/ia:LearningProvider", NSMgr);

                //Find the Learner nodes
                XmlNodeList learnerNodes = this.ILRFile.SelectNodes("/ia:Message/ia:Learner", NSMgr);

                //If we have no LearningProvider node create one in the correct place
                if (learningProviderNode == null)
                {
                    learningProviderNode = this.ILRFile.CreateElement("LearningProvider", NSMgr.LookupNamespace("ia"));
                    if (learnerNodes.Count == 0)
                        this.ILRFile.DocumentElement.AppendChild(learningProviderNode);
                    else
                        this.ILRFile.DocumentElement.InsertBefore(learningProviderNode, learnerNodes.Item(0));
                }

                this.LearningProvider = new ILR.LearningProvider(learningProviderNode, NSMgr);
                this.LearningProvider.UKPRN = importMessage.LearningProvider.UKPRN;


                var x = importMessage.LearnerList.Where(l => l.HasContinuingAims);
                //var x = importMessage.LearnerList.ToList();
                var y = importMessage.LearnerDestinationandProgressionList.Where(ldp => ldp.HasCurrentDPOutcomes);

                importMessage = null;

                foreach (Learner learner in x)
                {
                    try
                    {
                        XmlNode newNode = ILRFile.CreateElement("Learner", NSMgr.LookupNamespace("ia"));
                        Learner newInstance = new Learner(learner, newNode, NSMgr);
                        newInstance.Message = this;
                        LearnerList.Add(newInstance);
                        AppendToLastOfNodeNamed(newNode, newNode.Name);
                        newInstance.ResequenceAimSeqNumber();
                        Log("Message", "Import",
                            String.Format("Learner {0} : {0}", LearnerList.Count, newInstance.LearnRefNumber));
                    }
                    catch (Exception el)
                    {
                        Log("Message", "Import", String.Format("Error Loading Learner : {0}", el.Message));
                        Console.WriteLine(String.Format("Learern Ref:{0}", learner.LearnRefNumber));
                    }
                }

                foreach (LearnerDestinationandProgression learnerDestinationandProgression in y)
                {
                    XmlNode newNode =
                        ILRFile.CreateElement("LearnerDestinationandProgression", NSMgr.LookupNamespace("ia"));
                    LearnerDestinationandProgression newInstance =
                        new LearnerDestinationandProgression(learnerDestinationandProgression, newNode, NSMgr);
                    newInstance.Message = this;
                    LearnerDestinationandProgressionList.Add(newInstance);
                    AppendToLastOfNodeNamed(newNode, newNode.Name);
                    Log("Message", "Import",
                        String.Format("LearnerDestinationandProgressionList {0} : {0}",
                            LearnerDestinationandProgressionList.Count, newInstance.LearnRefNumber));
                }

                importMessage = null;
            }
            else
            {
                throw (new Exception(
                    "Unable to identify Year from filename. Confirm the file name matchesd the ILR Specification."));
            }

            this.Filename = InterlStoreFilename;
            Save();
            GC.Collect();
        }

        public void Delete(ChildEntity Child)
        {
            ILRFile.DocumentElement.RemoveChild(Child.Node);
            switch (Child.GetType().ToString())
            {
                case "ILR.Learner":
                    this.LearnerList.Remove((Learner) Child);
                    break;
                case "ILR.LearnerDestinationandProgression":
                    this.LearnerDestinationandProgressionList.Remove((LearnerDestinationandProgression) Child);
                    break;
            }
        }

        public bool LearnRefNumberExists(string LearnRefNumber)
        {
            return LearnerRefNumbers.ContainsKey(LearnRefNumber);
        }

        public int CountLearnRefNumberInstances(string LearnRefNumber)
        {
            if (LearnerRefNumbers.TryGetValue(LearnRefNumber, out int existingCount))
            {
                return existingCount;
            }

            return 0;
        }

        public void UpdateLearnerRefCount(string oldLearnRefNumber, string newLearnRefNumber)
        {
            if (!string.IsNullOrEmpty(oldLearnRefNumber) &&  LearnerRefNumbers.TryGetValue(oldLearnRefNumber, out int oldCount))
            {
                if (oldCount == 1)
                {
                    LearnerRefNumbers.Remove(oldLearnRefNumber);
                }
                else
                {
                    LearnerRefNumbers[oldLearnRefNumber] = --oldCount;
                }
            }

            if (LearnerRefNumbers.TryGetValue(newLearnRefNumber, out int newCount))
            {
                LearnerRefNumbers[newLearnRefNumber] = ++newCount;
            }
            else
            {
                LearnerRefNumbers.Add(newLearnRefNumber, 1);
            }
        }

        public void FixLDAPULN()
        {
            XmlNodeList ldaps = ILRFile.SelectNodes("ia:Message/ia:LearnerDestinationandProgression", NSMgr);
            foreach (XmlNode ldap in ldaps)
            {
                XmlNode uln = ldap.SelectSingleNode("./ia:ULN", NSMgr);
                XmlNode lrn = ldap.SelectSingleNode("./ia:LearnRefNumber", NSMgr);
                if (uln != null && ldap.ChildNodes.Count > 1)
                {
                    ldap.RemoveChild(uln);
                    if (lrn != null)
                        ldap.InsertAfter(uln, lrn);
                    else
                        ldap.InsertBefore(uln, ldap.FirstChild);
                }
            }
        }

        public static void Log(String className, String caller, String message)
        {
#if DEBUG
            if (IsLoggingEnabled)
            {
                using (System.IO.StreamWriter writer = new System.IO.StreamWriter(_logFileName, true))
                {
                    writer.WriteLine("{0} | {1} | {2} | {3}", DateTime.Now.ToString("yyyy.MM.dd.hhss"), className,
                        caller, message);
                }
            }
#endif
        }

        #endregion

        #region Child Entity Creation

        public Learner CreateLearner()
        {
            XmlNode newNode = ILRFile.CreateElement("Learner", NSMgr.LookupNamespace("ia"));
            Learner newInstance = new Learner(newNode, NSMgr, false);
            newInstance.LearnRefNumber = GetNextLearnRefNumber();
            newInstance.Message = this;
            LearnerList.Add(newInstance);
            AppendToLastOfNodeNamed(newNode, newNode.Name);
            return newInstance;
        }

        public LearnerDestinationandProgression CreateLearnerDestinationandProgression()
        {
            XmlNode newNode = ILRFile.CreateElement("LearnerDestinationandProgression", NSMgr.LookupNamespace("ia"));
            LearnerDestinationandProgression newInstance = new LearnerDestinationandProgression(newNode, NSMgr);
            newInstance.Message = this;
            LearnerDestinationandProgressionList.Add(newInstance);
            AppendToLastOfNodeNamed(newNode, newNode.Name);
            return newInstance;
        }

        private void AppendToLastOfNodeNamed(XmlNode NewNode, string NodeName)
        {
            switch (NodeName)
            {
                case "Learner":
                    if (LearnerDestinationandProgressionList.Count() == 0)
                        AppendToLastOfNodeNamed(NewNode, "LearnerDestinationandProgression");
                    else
                        ILRFile.DocumentElement.InsertBefore(NewNode,
                            LearnerDestinationandProgressionList.First().Node);
                    break;
                case "LearnerDestinationandProgression":
                    ILRFile.DocumentElement.AppendChild(NewNode);
                    break;
            }
        }

        private string GetNextLearnRefNumber()
        {
            XmlNodeList learnerNodes = ILRFile.SelectNodes("/ia:Message/ia:Learner", NSMgr);

            int learnRefNumber = learnerNodes.Count + 1;

            while (this.LearnRefNumberExists(learnRefNumber.ToString("0000")))
                learnRefNumber++;

            return learnRefNumber.ToString("0000");

        }

        #endregion

        #region Constructor

        protected Message()
        {
        }

        public Message(string Filename)
        {
            CreateMessage(Filename);
        }

        public Message(string Filename, string LogFilename)
        {
            _logFileName = LogFilename;
            CreateMessage(Filename);
        }

        private void CreateMessage(string Filename)
        {
            Log("Message", "Constructor", String.Format("Filename : {0}", Filename));
            if (!File.Exists(Filename))
            {
                Log("Message", "Constructor", "Filename not found create new file");
                ILRFile = new XmlDocument();
                ILRFile.LoadXml("<Message xmlns=\"" + CurrentNameSpace +
                                "\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" />");
                ILRFile.Save(Filename);
            }

            IsFileImportLoadingRunning = true;
            Load(Filename);
            FixLDAPULN();
            IsFileImportLoadingRunning = false;
        }

        #endregion

        #region INotifyPropertyChanged Members

        /// <summary>
        /// INotifyPropertyChanged requires a property called PropertyChanged.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Fires the event for the property when it changes.
        /// </summary>
        protected virtual void OnPropertyChanged(string propertyName)
        {
#if DEBUG
            VerifyPropertyName(propertyName);
#endif
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public void VerifyPropertyName(string propertyName)
        {
            // Verify that the property name matches a real,  
            // public, instance property on this object.
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                var msg = "Invalid property name: " + propertyName;

                if (this.ThrowOnInvalidPropertyName)
                {
                    throw new Exception(msg);
                }
                else
                {
                    Debug.Fail(msg);
                }
            }
        }

        protected bool ThrowOnInvalidPropertyName { get; set; }

        #endregion
    }
}
