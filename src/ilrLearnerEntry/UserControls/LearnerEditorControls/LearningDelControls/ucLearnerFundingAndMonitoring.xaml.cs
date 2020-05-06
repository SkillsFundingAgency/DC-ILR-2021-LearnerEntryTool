using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Windows.Controls;

namespace ilrLearnerEntry.UserControls.LearnerEditorControls.LearningDelControls
{
    /// <summary>
    /// Interaction logic for ucLearnerFundingAndMonitoring.xaml
    /// </summary>
    public partial class ucLearnerFundingAndMonitoring : UserControl, INotifyPropertyChanged
    {
        #region Constructor
        public ucLearnerFundingAndMonitoring()
        {
            InitializeComponent();
            ALBControl.UserControlTitle = "Advanced Learner Loans Bursary funding";
            LSFControl.UserControlTitle = "Learning support funding";
            ACTControl.UserControlTitle = "Apprenticeship contract type";
        }
        #endregion

        #region Public Properties

        #region CurrentItem
        private ILR.LearningDelivery learningDelivery;
        public ILR.LearningDelivery CurrentItem
        {
            get { return learningDelivery; }
            set
            {
                if (value != null)
                {
                    learningDelivery = value;

                    PopulateLdmFamFormFields();
                    PopulateDamFamFormFields();

                    this.DataContext = this;

                    ALBControl.CurrentItem = learningDelivery;
                    LSFControl.CurrentItem = learningDelivery;
                    ACTControl.CurrentItem = learningDelivery;

                    OnPropertyChanged("PlusLoanBursaryFundList");
                    OnPropertyChanged("SourceOfFundingList");
                    OnPropertyChanged("FullOrCoFundedList");
                    OnPropertyChanged("NatioanSkillAgencyList");
                    OnPropertyChanged("SpecialProjectList");
                    OnPropertyChanged("EligibitiytAppFundingList");
                    OnPropertyChanged("ASLList");
                    OnPropertyChanged("PODList");
                    OnPropertyChanged("CurrentItem");
                }
                else
                {
                    this.DataContext = null;
                }
            }
        }

        private List<String> LdmFamFormFields = new List<String>(6);
        private void PopulateLdmFamFormFields()
        {
            LdmFamFormFields.Clear();
            foreach (String code in learningDelivery.LDM)
            {
                LdmFamFormFields.Add(code);
                if (LdmFamFormFields.Count >= 6)
                {
                    break;
                }
            }
            while (LdmFamFormFields.Count < 6)
            {
                LdmFamFormFields.Add(String.Empty);
            }
            OnPropertyChanged("LDM6");
            OnPropertyChanged("LDM5");
            OnPropertyChanged("LDM4");
            OnPropertyChanged("LDM3");
            OnPropertyChanged("LDM2");
            OnPropertyChanged("LDM1");
        }

        private List<String> DamFamFormFields = new List<String>(4);
        private void PopulateDamFamFormFields()
        {
            DamFamFormFields.Clear();
            foreach (String code in learningDelivery.DAM)
            {
                DamFamFormFields.Add(code);
                if (DamFamFormFields.Count >= 4)
                {
                    break;
                }
            }

            while (DamFamFormFields.Count < 4)
            {
                DamFamFormFields.Add(String.Empty);
            }
            OnPropertyChanged("DAM4");
            OnPropertyChanged("DAM3");
            OnPropertyChanged("DAM2");
            OnPropertyChanged("DAM1");
        }
        #endregion


        /// <summary>
        /// LDM1
        /// </summary>
        public String LDM1
        {
            get { return LdmFamFormFields.Count > 0 ? LdmFamFormFields[0] : String.Empty; }
            set { SetLdmFormFieldValue(0, value); }
        }

        /// <summary>
        /// LDM2
        /// </summary>
        public String LDM2
        {
            get { return LdmFamFormFields.Count > 1 ? LdmFamFormFields[1] : String.Empty; }
            set { SetLdmFormFieldValue(1, value); }
        }

        /// <summary>
        /// LDM3
        /// </summary>
        public String LDM3
        {
            get { return LdmFamFormFields.Count > 2 ? LdmFamFormFields[2] : String.Empty; }
            set { SetLdmFormFieldValue(2, value); }
        }

        /// <summary>
        /// LDM4
        /// </summary>
        public String LDM4
        {
            get { return LdmFamFormFields.Count > 3 ? LdmFamFormFields[3] : String.Empty; }
            set { SetLdmFormFieldValue(3, value); }
        }

        /// <summary>
        /// LDM5
        /// </summary>
        public String LDM5
        {
            get { return LdmFamFormFields.Count > 4 ? LdmFamFormFields[4] : String.Empty; }
            set { SetLdmFormFieldValue(4, value); }
        }

        /// <summary>
        /// LDM6
        /// </summary>
        public String LDM6
        {
            get { return LdmFamFormFields.Count > 5 ? LdmFamFormFields[5] : String.Empty; }
            set { SetLdmFormFieldValue(5, value); }
        }

        /// <summary>
        /// DAM1
        /// </summary>
        public String DAM1
        {
            get { return DamFamFormFields.Count > 0 ? DamFamFormFields[0] : String.Empty; }
            set { SetDamFormFieldValue(0, value); }
        }

        /// <summary>
        /// DAM2
        /// </summary>
        public String DAM2
        {
            get { return DamFamFormFields.Count > 1 ? DamFamFormFields[1] : String.Empty; }
            set { SetDamFormFieldValue(1, value); }
        }

        /// <summary>
        /// DAM3 
        /// </summary>
        public String DAM3
        {
            get { return DamFamFormFields.Count > 2 ? DamFamFormFields[2] : String.Empty; }
            set { SetDamFormFieldValue(2, value); }
        }

        /// <summary>
        /// DAM4
        /// </summary>
        public String DAM4
        {
            get { return DamFamFormFields.Count > 3 ? DamFamFormFields[3] : String.Empty; }
            set { SetDamFormFieldValue(3, value); }
        }


        private void SetLdmFormFieldValue(Int32 index, String value)
        {
            LdmFamFormFields[index] = value;

            List<String> TmpList = new List<String>(0);
            foreach (String code in LdmFamFormFields)
            {
                if (!string.IsNullOrEmpty(code.Trim()))
                {
                    TmpList.Add(code);
                }

                if (TmpList.Count >= 6)
                {
                    break;
                }
            }
            learningDelivery.LDM = TmpList;
            PopulateLdmFamFormFields();
        }


        private void SetDamFormFieldValue(Int32 index, String value)
        {
            DamFamFormFields[index] = value;

            List<String> TmpList = new List<String>(0);
            foreach (String code in DamFamFormFields)
            {
                if (!string.IsNullOrEmpty(code.Trim()))
                {
                    TmpList.Add(code);
                }

                if (TmpList.Count >= 4)
                {
                    break;
                }
            }
            learningDelivery.DAM = TmpList;
            PopulateDamFamFormFields();
        }


        public DataTable PlusLoanBursaryFundList { set; get; }
        public DataTable SourceOfFundingList { set; get; }
        public DataTable FullOrCoFundedList { set; get; }
        public DataTable NatioanSkillAgencyList { set; get; }
        public DataTable SpecialProjectList { set; get; }
        public DataTable ASLList { set; get; }

        public DataTable PODList{ set; get; }
        

        
        public DataTable EligibitiytAppFundingList { set; get; }
     
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
