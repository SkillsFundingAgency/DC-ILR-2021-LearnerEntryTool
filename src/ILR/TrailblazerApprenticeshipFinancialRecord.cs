using System;
using System.ComponentModel;
using System.Xml;

namespace ILR
{
    public class AppFinRecord : ChildEntity, IDataErrorInfo
    {

        #region ILR Properties
        public string AFinType { get { return XMLHelper.GetChildValue("AFinType", Node, NSMgr); } set { XMLHelper.SetChildValue("AFinType", value, Node, NSMgr); OnPropertyChanged("AFinType"); } }
        public int? AFinCode { get { string AFinCode = XMLHelper.GetChildValue("AFinCode", Node, NSMgr); return (AFinCode != null ? int.Parse(AFinCode) : (int?)null); } set { XMLHelper.SetChildValue("AFinCode", value, Node, NSMgr); OnPropertyChanged("AFinCode"); } }
        public DateTime? AFinDate { get { string AFinDate = XMLHelper.GetChildValue("AFinDate", Node, NSMgr); return (AFinDate != null ? DateTime.Parse(AFinDate) : (DateTime?)null); } set { XMLHelper.SetChildValue("AFinDate", value, Node, NSMgr); OnPropertyChanged("AFinDate"); } }
        public int? AFinAmount { get { string AFinAmount = XMLHelper.GetChildValue("AFinAmount", Node, NSMgr); return (AFinAmount != null ? int.Parse(AFinAmount) : (int?)null); } set { XMLHelper.SetChildValue("AFinAmount", value, Node, NSMgr); OnPropertyChanged("AFinAmount"); } }
        #endregion

        #region Constructors
        internal AppFinRecord(XmlNode ApprenticeshipTrailblazerFinancialDetailsNode, XmlNamespaceManager NSMgr)
        {
            this.Node = ApprenticeshipTrailblazerFinancialDetailsNode;
            this.NSMgr = NSMgr;
        }
        internal AppFinRecord(AppFinRecord MigrationLearnerEmploymentStatus, XmlNode Node, XmlNamespaceManager NSMgr)
        {
            this.Node = Node;
            this.NSMgr = NSMgr;

            this.AFinType = MigrationLearnerEmploymentStatus.AFinType;
            this.AFinCode = MigrationLearnerEmploymentStatus.AFinCode;
            this.AFinDate = MigrationLearnerEmploymentStatus.AFinDate;
            this.AFinAmount = MigrationLearnerEmploymentStatus.AFinAmount;

        }
        #endregion

        #region IDataErrorInfo Members
        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName]
        {
            get
            {
                string result = null;
                if (columnName == "AFinCode")
                {
                    if (AFinCode != null && AFinCode.ToString().Length > 8)
                    {
                        result = "AFinCode exceeds maximum length (8).";
                        AFinCode = (int?)int.Parse(AFinCode.ToString().Substring(0, 8));
                    }
                }
                if (columnName == "AFinAmount")
                {
                    if (AFinAmount != null && AFinAmount.ToString().Length > 8)
                    {
                        result = "AFinAmount exceeds maximum length (8).";
                        AFinAmount = (int?)int.Parse(AFinAmount.ToString().Substring(0, 8));
                    }
                }
                return result;
            }
        }
        #endregion
    }
}
