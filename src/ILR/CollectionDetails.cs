﻿using System;
using System.ComponentModel;
using System.Xml;

namespace ILR
{
    public class CollectionDetails : ChildEntity, IDataErrorInfo
    {
        #region ILR Properties

        public string Collection
        {
            get { return XMLHelper.GetChildValue("Collection", Node, NSMgr); }
            set { XMLHelper.SetChildValue("Collection", value, Node, NSMgr); }
        }

        public string Year
        {
            get { return XMLHelper.GetChildValue("Year", Node, NSMgr); }
            set { XMLHelper.SetChildValue("Year", value, Node, NSMgr); }
        }

        public DateTime? FilePreparationDate
        {
            get
            {
                string filePreparationDate = XMLHelper.GetChildValue("FilePreparationDate", Node, NSMgr);
                return (filePreparationDate != null ? DateTime.Parse(filePreparationDate) : (DateTime?) null);
            }
            set { XMLHelper.SetChildValue("FilePreparationDate", value, Node, NSMgr); }
        }

        #endregion

        #region Constructors

        internal CollectionDetails(XmlNode Node, XmlNamespaceManager NSMgr)
        {
            this.Node = Node;
            this.NSMgr = NSMgr;

            if (this.Collection == null)
                this.Collection = "ILR";
            if (this.Year == null)
                this.Year = Message.CurrentYear.ToString();
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
                if (columnName == "NINumber")
                {
                    result = "Error message here";
                }

                return result;
            }
        }

        #endregion


    }
}
