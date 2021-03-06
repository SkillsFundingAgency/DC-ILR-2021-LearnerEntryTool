﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Input;
using System.Text.RegularExpressions;

using ILR;

namespace ilrLearnerEntry.UserControls.LearnerEditorControls.LearningDelControls
{
    /// <summary>
    /// Interaction logic for ucLearningEDInformation.xaml
    /// </summary>
    public partial class ucLearningEDInformation : BaseUserControl, INotifyPropertyChanged//, IDataErrorInfo
    {

        #region Private Variables
        private const String CLASSNAME = "LearningDeliveryHE";
        private ILR.LearningDelivery _learningDelivery;
        //private String _pcolab = string.Empty;
        //private String _pcfldcs = string.Empty;
        //private String _pcsldcs = string.Empty;
        //private String _pctldcs = string.Empty;
        //private String _soc2000 = string.Empty;
        //private String _yearstu = string.Empty;
        //private String _netfee = string.Empty;
        //private String _grossfee = string.Empty;
        private String _studload = string.Empty;
        #endregion

        #region Constructor
        public ucLearningEDInformation()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        #endregion
        private void ValidateToNumeric(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[0 - 9]{ 1,2} ([.][0 - 9]{ 1,2})?$");           
            e.Handled = regex.IsMatch(e.Text);
        }

        #region Public Properties
        public ILR.LearningDeliveryHE HeItem
        {
            get { return ((_learningDelivery == null) || (_learningDelivery.LearningDeliveryHE == null)) ? null : _learningDelivery.LearningDeliveryHE; }
            set
            {
                _learningDelivery.LearningDeliveryHE = value;
                _studload = (((_learningDelivery == null) || (_learningDelivery.LearningDeliveryHE == null) || (_learningDelivery.LearningDeliveryHE.GROSSFEE == null)) ? null : _learningDelivery.LearningDeliveryHE.GROSSFEE.ToString());
                OnPropertyChanged("HeItem");
            }
        }
        public ILR.LearningDelivery CurrentItem
        {
            get { return _learningDelivery; }
            set
            {
                _learningDelivery = value;
                OnPropertyChanged("HeItem");
                //_pcolab = string.Empty;
                //_pcfldcs = string.Empty;
                //_pcsldcs = string.Empty;
                //_pctldcs = string.Empty;
                //_soc2000 = string.Empty;
                //_yearstu = string.Empty;
                //_netfee = string.Empty;
                //_grossfee = string.Empty;
                //_studload = string.Empty;
                if (_learningDelivery != null)
                {
                    if (_learningDelivery.LearningDeliveryHE == null)
                    {
                        _learningDelivery.CreateLearningDeliveryHE();
                    }
                }
                //_pcolab = (((_learningDelivery == null) || (_learningDelivery.LearningDeliveryHE == null) || (_learningDelivery.LearningDeliveryHE.PCOLAB == null)) ? null : _learningDelivery.LearningDeliveryHE.PCOLAB.ToString());
                //_pcfldcs = (((_learningDelivery == null) || (_learningDelivery.LearningDeliveryHE == null) || (_learningDelivery.LearningDeliveryHE.PCFLDCS == null)) ? null : _learningDelivery.LearningDeliveryHE.PCFLDCS.ToString());
                //_pcsldcs = (((_learningDelivery == null) || (_learningDelivery.LearningDeliveryHE == null) || (_learningDelivery.LearningDeliveryHE.PCSLDCS == null)) ? null : _learningDelivery.LearningDeliveryHE.PCSLDCS.ToString());
                //_pctldcs = (((_learningDelivery == null) || (_learningDelivery.LearningDeliveryHE == null) || (_learningDelivery.LearningDeliveryHE.PCTLDCS == null)) ? null : _learningDelivery.LearningDeliveryHE.PCTLDCS.ToString());
                //_soc2000 = (((_learningDelivery == null) || (_learningDelivery.LearningDeliveryHE == null) || (_learningDelivery.LearningDeliveryHE.SOC2000 == null)) ? null : _learningDelivery.LearningDeliveryHE.SOC2000.ToString());
                //_yearstu = (((_learningDelivery == null) || (_learningDelivery.LearningDeliveryHE == null) || (_learningDelivery.LearningDeliveryHE.YEARSTU == null)) ? null : _learningDelivery.LearningDeliveryHE.YEARSTU.ToString());
                //_netfee = (((_learningDelivery == null) || (_learningDelivery.LearningDeliveryHE == null) || (_learningDelivery.LearningDeliveryHE.NETFEE == null)) ? null : _learningDelivery.LearningDeliveryHE.NETFEE.ToString());
                //_grossfee = (((_learningDelivery == null) || (_learningDelivery.LearningDeliveryHE == null) || (_learningDelivery.LearningDeliveryHE.GROSSFEE == null)) ? null : _learningDelivery.LearningDeliveryHE.GROSSFEE.ToString());
                //_studload = (((_learningDelivery == null) || (_learningDelivery.LearningDeliveryHE == null) || (_learningDelivery.LearningDeliveryHE.STULOAD == null)) ? null : _learningDelivery.LearningDeliveryHE.STULOAD.ToString());
                //}
                //OnPropertyChanged("SOC2000List");
                //OnPropertyChanged("EconomicList");
                OnPropertyChanged("ELQList");
                OnPropertyChanged("TYPEYRList");
                OnPropertyChanged("MSTUFEEList");
                //OnPropertyChanged("SPECFEEList");
                OnPropertyChanged("MODESTUDList");
                OnPropertyChanged("FUNDLEVList");
                OnPropertyChanged("FUNDCOMPList");
                OnPropertyChanged("QUALENT3List");
                OnPropertyChanged("CurrentItem");
                //OnPropertyChanged("PCOLAB"); --
                //OnPropertyChanged("PCFLDCS"); --
                //OnPropertyChanged("PCSLDCS");--
                //OnPropertyChanged("PCTLDCS"); --
                //OnPropertyChanged("SOC2000"); --
                //OnPropertyChanged("YEARSTU"); --
                //OnPropertyChanged("NETFEE"); --
                //OnPropertyChanged("GROSSFEE"); --
                OnPropertyChanged("HeItem");
                //OnPropertyChanged("STULOAD"); --
            }
        }
        //public String PCOLAB
        //{
        //    get { return _pcolab; }
        //    set
        //    {
        //        _pcolab = value;
        //        if (value == null || value == string.Empty)
        //        {
        //            _learningDelivery.LearningDeliveryHE.PCOLAB = null;
        //        }
        //        else
        //        {
        //            int number;
        //            bool result = Int32.TryParse(System.Convert.ToString(value), out number);
        //            if (result)
        //            {
        //                _learningDelivery.LearningDeliveryHE.PCOLAB = number;
        //            }
        //        }
        //        OnPropertyChanged("PCOLAB");
        //    }
        //}
        //public String PCFLDCS
        //{
        //    get { return _pcfldcs; }
        //    set
        //    {
        //        _pcfldcs = value;
        //        if (value == null || value == string.Empty)
        //        {
        //            HeItem.PCFLDCS = null;
        //        }
        //        else
        //        {
        //            int number;
        //            bool result = Int32.TryParse(System.Convert.ToString(value), out number);
        //            if (result)
        //            {
        //                HeItem.PCFLDCS = number;
        //            }
        //        }
        //        OnPropertyChanged("PCFLDCS");
        //    }
        //}
        //public String PCSLDCS
        //{
        //    get { return _pcsldcs; }
        //    set
        //    {
        //        _pcsldcs = value;
        //        if (value == null || value == string.Empty)
        //        {
        //            HeItem.PCSLDCS = null;
        //        }
        //        else
        //        {
        //            int number;
        //            bool result = Int32.TryParse(System.Convert.ToString(value), out number);
        //            if (result)
        //            {
        //                HeItem.PCSLDCS = number;
        //            }
        //        }

        //        OnPropertyChanged("PCSLDCS");
        //    }
        //}
        //public String PCTLDCS
        //{
        //    get { return _pctldcs; }
        //    set
        //    {
        //        _pctldcs = value;
        //        if (value == null || value == string.Empty)
        //        {
        //            HeItem.PCTLDCS = null;
        //        }
        //        else
        //        {
        //            int number;
        //            bool result = Int32.TryParse(System.Convert.ToString(value), out number);
        //            if (result)
        //            {
        //                HeItem.PCTLDCS = number;
        //            }
        //        }
        //        OnPropertyChanged("GROSSFEE");
        //    }
        //}
        //public String SOC2000
        //{
        //    get { return _soc2000; }
        //    set
        //    {
        //        _soc2000 = value;
        //        if (value == null || value == string.Empty)
        //        {
        //            HeItem.SOC2000 = null;
        //        }
        //        else
        //        {
        //            int number;
        //            bool result = Int32.TryParse(System.Convert.ToString(value), out number);
        //            if (result)
        //            {
        //                HeItem.SOC2000 = number;
        //            }
        //        }
        //        OnPropertyChanged("SOC2000");
        //    }
        //}
        //public String YEARSTU
        //{
        //    get { return _yearstu; }
        //    set
        //    {
        //        _yearstu = value;
        //        if (value == null || value == string.Empty)
        //        {
        //            HeItem.YEARSTU = null;
        //        }
        //        else
        //        {
        //            int number;
        //            bool result = Int32.TryParse(System.Convert.ToString(value), out number);
        //            if (result)
        //            {
        //                HeItem.YEARSTU = number;
        //            }
        //        }
        //        OnPropertyChanged("YEARSTU");
        //    }
        //}
        //public String NETFEE
        //{
        //    get { return _netfee; }
        //    set
        //    {
        //        _netfee = value;
        //        if (value == null || value == string.Empty)
        //        {
        //            HeItem.NETFEE = null;
        //        }
        //        else
        //        {
        //            int number;
        //            bool result = Int32.TryParse(System.Convert.ToString(value), out number);
        //            if (result)
        //            {
        //                HeItem.NETFEE = number;
        //            }
        //        }
        //        OnPropertyChanged("NETFEE");
        //    }
        //}
        //public String GROSSFEE
        //{
        //    get { return _grossfee; }
        //    set
        //    {
        //        _grossfee = value;
        //        if (value == null || value == string.Empty)
        //        {
        //            HeItem.GROSSFEE = null;
        //        }
        //        else
        //        {
        //            int number;
        //            bool result = Int32.TryParse(System.Convert.ToString(value), out number);
        //            if (result)
        //            {
        //                HeItem.GROSSFEE = number;
        //            }
        //        }
        //        OnPropertyChanged("GROSSFEE");
        //    }
        //}

        //public String STULOAD
        //{
        //    get { return _studload; }
        //    set
        //    {
        //        _studload = value;
        //        if (value == null || value == string.Empty)
        //        {
        //            HeItem.STULOAD = null;
        //        }
        //        else
        //        {
        //            decimal number;
        //            bool result = Decimal.TryParse(System.Convert.ToString(value), out number);
        //            if (result)
        //            {
        //                HeItem.STULOAD = number;
        //            }
        //        }
        //        OnPropertyChanged("STULOAD");
        //    }
        //}

        public DataTable SOC2000List { set; get; }
        public DataTable EconomicList { set; get; }
        public DataTable ELQList { set; get; }
        public DataTable TYPEYRList { set; get; }
        public DataTable MSTUFEEList { set; get; }
        public DataTable SPECFEEList { set; get; }
        public DataTable MODESTUDList { set; get; }
        public DataTable FUNDLEVList { set; get; }
        public DataTable FUNDCOMPList { set; get; }
        public DataTable QUALENT3List { set; get; }
        #endregion

        #region Public Methods
        #endregion

        #region PRIVATE Methods

        #endregion

        #region IDataErrorInfo Members
        //public string Error
        //{
        //    get { throw new NotImplementedException(); }
        //}
        //public string this[string columnName]
        //{
        //    get
        //    {
        //        string sReturn = null;
        //        switch (columnName)
        //        {
        //            //            case "PCOLAB":
        //            //                if (PCOLAB != null && PCOLAB.Length > 0)
        //            //                {
        //            //                    sReturn += CheckPropertyLength(PCOLAB, CLASSNAME, columnName);
        //            //                    int number;
        //            //                    bool result = Int32.TryParse(PCOLAB, out number);
        //            //                    if (!result)
        //            //                    {
        //            //                        sReturn += String.Format("{0} has non numeric values. this will NOT be SAVED !!!", columnName);
        //            //                    }
        //            //                }
        //            //                break;
        //            //            case "PCFLDCS":
        //            //                if (PCFLDCS != null && PCFLDCS.Length > 0)
        //            //                {
        //            //                    sReturn += CheckPropertyLength(PCFLDCS, CLASSNAME, columnName);
        //            //                    int number;
        //            //                    bool result = Int32.TryParse(PCFLDCS, out number);
        //            //                    if (!result)
        //            //                    {
        //            //                        sReturn += String.Format("{0} has non numeric values. this will NOT be SAVED !!!", columnName);
        //            //                    }
        //            //                }
        //            //                break;
        //            //            case "PCSLDCS":
        //            //                if (PCSLDCS != null && PCSLDCS.Length > 0)
        //            //                {
        //            //                    sReturn += CheckPropertyLength(PCSLDCS, CLASSNAME, columnName);
        //            //                    int number;
        //            //                    bool result = Int32.TryParse(PCSLDCS, out number);
        //            //                    if (!result)
        //            //                    {
        //            //                        sReturn += String.Format("{0} has non numeric values. this will NOT be SAVED !!!", columnName);
        //            //                    }
        //            //                }
        //            //                break;
        //            //            case "PCTLDCS":
        //            //                if (PCTLDCS != null && PCTLDCS.Length > 0)
        //            //                {
        //            //                    sReturn += CheckPropertyLength(PCTLDCS, CLASSNAME, columnName);
        //            //                    int number;
        //            //                    bool result = Int32.TryParse(PCTLDCS, out number);
        //            //                    if (!result)
        //            //                    {
        //            //                        sReturn += String.Format("{0} has non numeric values. this will NOT be SAVED !!!", columnName);
        //            //                    }
        //            //                }
        //            //                break;
        //            case "SOC2000":
        //                if (SOC2000 != null && SOC2000.Length > 0)
        //                {
        //                    sReturn += CheckPropertyLength(SOC2000, CLASSNAME, columnName);
        //                    int number;
        //                    bool result = Int32.TryParse(SOC2000, out number);
        //                    if (!result)
        //                    {
        //                        sReturn += String.Format("{0} has non numeric values. this will NOT be SAVED !!!", "Occupation code");
        //                    }
        //                }
        //                break;
        //            //            case "YEARSTU":
        //            //                if (YEARSTU != null && YEARSTU.Length > 0)
        //            //                {
        //            //                    sReturn += CheckPropertyLength(YEARSTU, CLASSNAME, columnName);
        //            //                    int number;
        //            //                    bool result = Int32.TryParse(YEARSTU, out number);
        //            //                    if (!result)
        //            //                    {
        //            //                        sReturn += String.Format("{0} has non numeric values. this will NOT be SAVED !!!", columnName);
        //            //                    }
        //            //                }
        //            //                break;
        //            //            case "NETFEE":
        //            //                if (NETFEE != null && NETFEE.Length > 0)
        //            //                {
        //            //                    sReturn += CheckPropertyLength(NETFEE, CLASSNAME, columnName);
        //            //                    int number;
        //            //                    bool result = Int32.TryParse(NETFEE, out number);
        //            //                    if (!result)
        //            //                    {
        //            //                        sReturn += String.Format("{0} has non numeric values. this will NOT be SAVED !!!", columnName);
        //            //                    }
        //            //                }
        //            //                break;
        //            //            case "GROSSFEE":
        //            //                if (GROSSFEE != null && GROSSFEE.Length > 0)
        //            //                {
        //            //                    sReturn += CheckPropertyLength(GROSSFEE, CLASSNAME, columnName);
        //            //                    int number;
        //            //                    bool result = Int32.TryParse(GROSSFEE, out number);
        //            //                    if (!result)
        //            //                    {
        //            //                        sReturn += String.Format("{0} has non numeric values. this will NOT be SAVED !!!", columnName);
        //            //                    }
        //            //                }
        //            //                break;

        //            //            case "STULOAD":
        //            //                if (STULOAD != null && STULOAD.Length > 0)
        //            //                {
        //            //                    sReturn += CheckPropertyLength(STULOAD, CLASSNAME, columnName);
        //            //                    decimal number;
        //            //                    bool result = decimal.TryParse(STULOAD, out number);
        //            //                    if (!result)
        //            //                    {
        //            //                        sReturn += String.Format("{0} cannot be converted to decimal. this will NOT be SAVED !!!", columnName);
        //            //                    }
        //            //                }
        //            //                break;

        //            default:
        //                break;
        //        }
        //        return sReturn;
        //    }
        //}

        //public int GetItemSize(string ItemName)
        //{
        //    return XmlSchema.GetMaxLength(ItemName);
        //}
        //public string CheckPropertyLength(object itemValue, string ClassName, string ItemName)
        //{
        //    String ItemFullName = String.Format("{0}.{1}", ClassName, ItemName);
        //    int ItemSize = GetItemSize(ItemFullName);
        //    if (itemValue != null && itemValue.ToString().Length > ItemSize)
        //    {
        //        return String.Format("exceeds maximum length ({0} characters). Current length : {1}\r\n", ItemSize, itemValue.ToString().Length);
        //    }
        //    return null;
        //}
        #endregion
    }
}
