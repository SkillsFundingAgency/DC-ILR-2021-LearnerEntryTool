﻿using System;
using System.ComponentModel;
using System.Xml;

namespace ILR
{
    public class LearningDeliveryHE : ChildEntity, IDataErrorInfo
    {
        //TODO: this property looks unused, needs to be removed after tests have been written.
        public bool IsBlank
        {
            get
            {
                if (
                      this.NUMHUS == null ||
                      this.SSN == null ||
                      this.QUALENT3 == null ||
                      this.SOC2000 == null ||
                      this.SEC == null ||
                      this.GROSSFEE == null ||
                      this.HEPostCode == null ||
                      this.UCASAPPID == null ||
                      this.TYPEYR == null ||
                      this.MODESTUD == null ||
                      this.FUNDLEV == null ||
                      this.FUNDCOMP == null ||
                      this.STULOAD == null ||
                      this.YEARSTU == null ||
                      this.MSTUFEE == null ||
                      this.PCOLAB == null ||
                      this.PCFLDCS == null ||
                      this.PCSLDCS == null ||
                      this.PCTLDCS == null ||
                      this.SPECFEE == null ||
                      this.NETFEE == null ||
                      this.DOMICILE == null ||
                      this.ELQ == null)
                    return false;

                return true;
            }
        }

        #region ILR Properties
        public string NUMHUS { get { return XMLHelper.GetChildValue("NUMHUS", Node, NSMgr); } set { XMLHelper.SetChildValue("NUMHUS", value, Node, NSMgr); OnPropertyChanged("NUMHUS"); } }
        public string SSN { get { return XMLHelper.GetChildValue("SSN", Node, NSMgr); } set { XMLHelper.SetChildValue("SSN", value, Node, NSMgr); OnPropertyChanged("SSN"); } }
        public string QUALENT3 { get { return XMLHelper.GetChildValue("QUALENT3", Node, NSMgr); } set { XMLHelper.SetChildValue("QUALENT3", value, Node, NSMgr); OnPropertyChanged("QUALENT3"); } }
        public string SOC2000 { get { return XMLHelper.GetChildValue("SOC2000", Node, NSMgr); } set { XMLHelper.SetChildValue("SOC2000", value, Node, NSMgr); OnPropertyChanged("SOC2000"); } }
        public int? SEC { get { string SEC = XMLHelper.GetChildValue("SEC", Node, NSMgr); return (SEC != null ? int.Parse(SEC) : (int?)null); } set { XMLHelper.SetChildValue("SEC", value, Node, NSMgr); OnPropertyChanged("SEC"); } }
        public string UCASAPPID { get { return XMLHelper.GetChildValue("UCASAPPID", Node, NSMgr); } set { XMLHelper.SetChildValue("UCASAPPID", value, Node, NSMgr); OnPropertyChanged("UCASAPPID"); } }
        public int? TYPEYR { get { string TYPEYR = XMLHelper.GetChildValue("TYPEYR", Node, NSMgr); return (TYPEYR != null ? int.Parse(TYPEYR) : (int?)null); } set { XMLHelper.SetChildValue("TYPEYR", value, Node, NSMgr); OnPropertyChanged("TYPEYR"); } }
        public int? MODESTUD { get { string MODESTUD = XMLHelper.GetChildValue("MODESTUD", Node, NSMgr); return (MODESTUD != null ? int.Parse(MODESTUD) : (int?)null); } set { XMLHelper.SetChildValue("MODESTUD", value, Node, NSMgr); OnPropertyChanged("MODESTUD"); } }
        public int? FUNDLEV { get { string FUNDLEV = XMLHelper.GetChildValue("FUNDLEV", Node, NSMgr); return (FUNDLEV != null ? int.Parse(FUNDLEV) : (int?)null); } set { XMLHelper.SetChildValue("FUNDLEV", value, Node, NSMgr); OnPropertyChanged("FUNDLEV"); } }
        public int? FUNDCOMP { get { string FUNDCOMP = XMLHelper.GetChildValue("FUNDCOMP", Node, NSMgr); return (FUNDCOMP != null ? int.Parse(FUNDCOMP) : (int?)null); } set { XMLHelper.SetChildValue("FUNDCOMP", value, Node, NSMgr); OnPropertyChanged("FUNDCOMP"); } }
        public string STULOAD { get { return XMLHelper.GetChildValue("STULOAD", Node, NSMgr); } set { XMLHelper.SetChildValue("STULOAD", value, Node, NSMgr); OnPropertyChanged("STULOAD"); } }
        public string YEARSTU { get { return XMLHelper.GetChildValue("YEARSTU", Node, NSMgr); } set { XMLHelper.SetChildValue("YEARSTU", value, Node, NSMgr); OnPropertyChanged("YEARSTU"); } }
        public int? MSTUFEE { get { string MSTUFEE = XMLHelper.GetChildValue("MSTUFEE", Node, NSMgr); return (MSTUFEE != null ? int.Parse(MSTUFEE) : (int?)null); } set { XMLHelper.SetChildValue("MSTUFEE", value, Node, NSMgr); OnPropertyChanged("MSTUFEE"); } }
        public string PCOLAB { get { return XMLHelper.GetChildValue("PCOLAB", Node, NSMgr); } set { XMLHelper.SetChildValue("PCOLAB", value, Node, NSMgr); OnPropertyChanged("PCOLAB"); } }
        public string PCFLDCS { get { return XMLHelper.GetChildValue("PCFLDCS", Node, NSMgr); } set { XMLHelper.SetChildValue("PCFLDCS", value, Node, NSMgr); OnPropertyChanged("PCFLDCS"); } }
        public string PCSLDCS { get { return XMLHelper.GetChildValue("PCSLDCS", Node, NSMgr); } set { XMLHelper.SetChildValue("PCSLDCS", value, Node, NSMgr); OnPropertyChanged("PCSLDCS"); } }
        public string PCTLDCS { get { return XMLHelper.GetChildValue("PCTLDCS", Node, NSMgr); } set { XMLHelper.SetChildValue("PCTLDCS", value, Node, NSMgr); OnPropertyChanged("PCTLDCS"); } }
        public int? SPECFEE { get { string SPECFEE = XMLHelper.GetChildValue("SPECFEE", Node, NSMgr); return (SPECFEE != null ? int.Parse(SPECFEE) : (int?)null); } set { XMLHelper.SetChildValue("SPECFEE", value, Node, NSMgr); OnPropertyChanged("SPECFEE"); } }
        public string NETFEE { get { return XMLHelper.GetChildValue("NETFEE", Node, NSMgr); } set { XMLHelper.SetChildValue("NETFEE", value, Node, NSMgr); OnPropertyChanged("NETFEE"); } }
        public string GROSSFEE { get { return XMLHelper.GetChildValue("GROSSFEE", Node, NSMgr); } set { XMLHelper.SetChildValue("GROSSFEE", value, Node, NSMgr); OnPropertyChanged("GROSSFEE"); } }
        public string DOMICILE { get { return XMLHelper.GetChildValue("DOMICILE", Node, NSMgr); } set { XMLHelper.SetChildValue("DOMICILE", value, Node, NSMgr); OnPropertyChanged("DOMICILE"); } }
        public int? ELQ { get { string ELQ = XMLHelper.GetChildValue("ELQ", Node, NSMgr); return (ELQ != null ? int.Parse(ELQ) : (int?)null); } set { XMLHelper.SetChildValue("ELQ", value, Node, NSMgr); OnPropertyChanged("ELQ"); } }
        public string HEPostCode { get { return XMLHelper.GetChildValue("HEPostCode", Node, NSMgr); } set { XMLHelper.SetChildValue("HEPostCode", value, Node, NSMgr); OnPropertyChanged("HEPostCode"); } }
        #endregion



        #region Constructors
        internal LearningDeliveryHE(XmlNode LearningDeliveryHENode, XmlNamespaceManager NSMgr)
        {
            this.Node = LearningDeliveryHENode;
            this.NSMgr = NSMgr;
        }
        internal LearningDeliveryHE(LearningDeliveryHE MigrationLearningDeliveryHE, XmlNode LearningDeliveryHENode, XmlNamespaceManager NSMgr)
        {
            this.Node = LearningDeliveryHENode;
            this.NSMgr = NSMgr;

            this.NUMHUS = MigrationLearningDeliveryHE.NUMHUS;
            this.SSN = MigrationLearningDeliveryHE.SSN;

            if (!string.Equals(MigrationLearningDeliveryHE.QUALENT3, "P69", StringComparison.OrdinalIgnoreCase)
                && !string.Equals(MigrationLearningDeliveryHE.QUALENT3, "X03", StringComparison.OrdinalIgnoreCase))
            {
                this.QUALENT3 = MigrationLearningDeliveryHE.QUALENT3;
            }

            this.HEPostCode = MigrationLearningDeliveryHE.HEPostCode;
            this.ELQ = MigrationLearningDeliveryHE.ELQ;
            this.SOC2000 = MigrationLearningDeliveryHE.SOC2000;
            this.SEC = MigrationLearningDeliveryHE.SEC;
            this.UCASAPPID = MigrationLearningDeliveryHE.UCASAPPID;
            this.TYPEYR = MigrationLearningDeliveryHE.TYPEYR.HasValue
                ? Math.Min(MigrationLearningDeliveryHE.TYPEYR.Value, 2) as int?
                : null;
            this.MODESTUD = MigrationLearningDeliveryHE.MODESTUD;
            this.FUNDLEV = MigrationLearningDeliveryHE.FUNDLEV;
            this.FUNDCOMP = MigrationLearningDeliveryHE.FUNDCOMP;
            this.STULOAD = MigrationLearningDeliveryHE.STULOAD;
            this.YEARSTU = MigrationLearningDeliveryHE.YEARSTU;
            if (MigrationLearningDeliveryHE.MSTUFEE == 16 || MigrationLearningDeliveryHE.MSTUFEE == 21)
                this.MSTUFEE = 97;
            else
                this.MSTUFEE = MigrationLearningDeliveryHE.MSTUFEE;
            this.PCOLAB = MigrationLearningDeliveryHE.PCOLAB;
            this.PCFLDCS = MigrationLearningDeliveryHE.PCFLDCS;
            this.PCSLDCS = MigrationLearningDeliveryHE.PCSLDCS;
            this.PCTLDCS = MigrationLearningDeliveryHE.PCTLDCS;
            this.SPECFEE = MigrationLearningDeliveryHE.SPECFEE;
            this.NETFEE = MigrationLearningDeliveryHE.NETFEE;
            this.GROSSFEE = MigrationLearningDeliveryHE.GROSSFEE;
            this.DOMICILE = MigrationLearningDeliveryHE.DOMICILE;
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

                //if (columnName == "SOC2000")
                //{
                //	if (SOC2000 != null && SOC2000.ToString().Length > 8)
                //	{
                //		result = "SOC2000 exceeds maximum length (8 digits).";
                //		//SOC2000 = (int?)int.Parse(SOC2000.ToString().Substring(0, 8));
                //	}
                //} 
                if (columnName == "SEC")
                {
                    if (SEC != null && SEC.ToString().Length > 8)
                    {
                        result = "SEC exceeds maximum length (8 digits).";
                        //SEC = (int?)int.Parse(SEC.ToString().Substring(0, 8));
                    }
                }
                if (columnName == "TYPEYR")
                {
                    if (TYPEYR != null && TYPEYR.ToString().Length > 8)
                    {
                        result = "TYPEYR exceeds maximum length (8 digits).";
                        //TYPEYR = (int?)int.Parse(TYPEYR.ToString().Substring(0, 8));
                    }
                    //else if (!TYPEYR.HasValue)
                    //{
                    //    result = MessagesConstants.TYPEYR_ValidationMessage;
                    //}
                }
                if (columnName == "MODESTUD")
                {
                    if (MODESTUD != null && MODESTUD.ToString().Length > 8)
                    {
                        result = "MODESTUD exceeds maximum length (8 digits).";
                        //MODESTUD = (int?)int.Parse(MODESTUD.ToString().Substring(0, 8));
                    }
                    //else if (!MODESTUD.HasValue)
                    //{
                    //    result = MessagesConstants.MODESTUD_ValidationMessage;
                    //}
                }
                if (columnName == "FUNDLEV")
                {
                    if (FUNDLEV != null && FUNDLEV.ToString().Length > 8)
                    {
                        result = "FUNDLEV exceeds maximum length (8 digits).";
                        //FUNDLEV = (int?)int.Parse(FUNDLEV.ToString().Substring(0, 8));
                    }
                    //else if (!FUNDLEV.HasValue)
                    //{
                    //    result = MessagesConstants.FUNDLEV_ValidationMessage;
                    //}
                }
                if (columnName == "FUNDCOMP")
                {
                    if (FUNDCOMP != null && FUNDCOMP.ToString().Length > 8)
                    {
                        result = "FUNDCOMP exceeds maximum length (8 digits).";
                        //FUNDCOMP = (int?)int.Parse(FUNDCOMP.ToString().Substring(0, 8));
                    }
                    //else if (!FUNDCOMP.HasValue)
                    //{
                    //    result = MessagesConstants.FUNDCOMP_ValidationMessage;
                    //}
                }
                //if (columnName == "STULOAD")
                //{
                //	if (STULOAD != null && STULOAD.ToString().Length > 10)
                //	{
                //		result = "STULOAD exceeds maximum length (10 digits).";
                //		//STULOAD = (decimal?)decimal.Parse(STULOAD.ToString().Substring(0, 10));
                //	}
                //} 
                //if (columnName == "YEARSTU")
                //{
                //	if (YEARSTU != null && YEARSTU.ToString().Length > 8)
                //	{
                //		result = "YEARSTU exceeds maximum length (8 digits).";
                //		//YEARSTU = (int?)int.Parse(YEARSTU.ToString().Substring(0, 8));
                //	}
                //	//else if (!YEARSTU.HasValue)
                //	//{
                //	//    result = MessagesConstants.YEARSTU_ValidationMessage;
                //	//}
                //            } 
                if (columnName == "MSTUFEE")
                {
                    if (MSTUFEE != null && MSTUFEE.ToString().Length > 8)
                    {
                        result = "MSTUFEE exceeds maximum length (8 digits).";
                        //MSTUFEE = (int?)int.Parse(MSTUFEE.ToString().Substring(0, 8));
                    }
                    //else if (!MSTUFEE.HasValue)
                    //{
                    //    result = MessagesConstants.MSTUFEE_ValidationMessage;
                    //}
                }
                //if (columnName == "PCOLAB")
                //{
                //	if (PCOLAB != null && PCOLAB.ToString().Length > 10)
                //	{
                //		result = "PCOLAB exceeds maximum length (10 digits).";
                //		//PCOLAB = (decimal?)decimal.Parse(PCOLAB.ToString().Substring(0, 10));
                //	}
                //} 
                if (columnName == "PCFLDCS")
                {
                    if (PCFLDCS != null && PCFLDCS.ToString().Length > 10)
                    {
                        result = "PCFLDCS exceeds maximum length (10 digits).";
                        //PCFLDCS = (decimal?)decimal.Parse(PCFLDCS.ToString().Substring(0, 10));
                    }
                }
                //if (columnName == "PCSLDCS")
                //{
                //	if (PCSLDCS != null && PCSLDCS.ToString().Length > 10)
                //	{
                //		result = "PCSLDCS exceeds maximum length (10 digits).";
                //		//PCSLDCS = (decimal?)decimal.Parse(PCSLDCS.ToString().Substring(0, 10));
                //	}
                //} 
                //if (columnName == "PCTLDCS")
                //{
                //	if (PCTLDCS != null && PCTLDCS.ToString().Length > 10)
                //	{
                //		result = "PCTLDCS exceeds maximum length (10 digits).";
                //		//PCTLDCS = (decimal?)decimal.Parse(PCTLDCS.ToString().Substring(0, 10));
                //	}
                //} 
                if (columnName == "SPECFEE")
                {
                    if (SPECFEE != null && SPECFEE.ToString().Length > 8)
                    {
                        result = "SPECFEE exceeds maximum length (8 digits).";
                        //SPECFEE = (int?)int.Parse(SPECFEE.ToString().Substring(0, 8));
                    }
                    //else if (!SPECFEE.HasValue)
                    //{
                    //    result = MessagesConstants.SPECFEE_ValidationMessage;
                    //}
                }
                //if (columnName == "NETFEE")
                //{
                //	if (NETFEE != null && NETFEE.ToString().Length > 8)
                //	{
                //		result = "NETFEE exceeds maximum length (8 digits).";
                //		//NETFEE = (int?)int.Parse(NETFEE.ToString().Substring(0, 8));
                //	}
                //}
                //if (columnName == "GROSSFEE")
                //{
                //    if (GROSSFEE != null && GROSSFEE.ToString().Length > 8)
                //    {
                //        result = "GROSSFEE exceeds maximum length (8 digits).";
                //        //GROSSFEE = (int?)int.Parse(GROSSFEE.ToString().Substring(0, 8));
                //    }
                //} 
                if (columnName == "ELQ")
                {
                    if (ELQ != null && ELQ.ToString().Length > 8)
                    {
                        result = "ELQ exceeds maximum length (8 digits).";
                        //ELQ = (int?)int.Parse(ELQ.ToString().Substring(0, 8));
                    }
                }
                return result;
            }
        }

        #endregion

    }
}
