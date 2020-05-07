using IlrLearnerEntry.UIAutomation.Tests.WindowObjects;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.UIItems;
using TestStack.White.UIItems.ListBoxItems;
using System;

namespace IlrLearnerEntry.UIAutomation.Tests.TabObjects.LearnersObjects
{
    public class EmploymentStatusObjects : WindowObject
    {
        internal EmploymentStatusObjects(Window window) : base(window)
        {
        }

        private Button AddEmploymentStatusButton
        {
            get
            {
                return Button("Add");
            }
        }

        public EmploymentStatusObjects ClickAddEmploymentStatusButton()
        {
            AddEmploymentStatusButton.Click();
            return this;
        }

        private ComboBox EmploymentStatus
        {
            get { return ComboBox("employmentStatus"); }
        }

        private DateTimePicker DateStatusApplies
        {
            get { return DateTimePicker("dtEmpStatDate");}
        }

        private TextBox EmployerIdentifierTextBox
        {
            get { return TextBox("txtEmpId"); }
        }

        private TextBox AgreementIdentifierTextBox
        {
            get { return TextBox("txtAgreeId"); }
        }

        private ComboBox LengthOfUnemploymentComboBox
        {
            get { return ComboBox("lengthOfUnemployment"); }
        }

        private ComboBox BenefitStatusIndicatorComboBox
        {
            get { return ComboBox("benefitStatusIndicator"); }
        }

        private ComboBox EmploymentIntensityIndicatorComboBox
        {
            get { return ComboBox("employmentIntensityIndicator"); }
        }

        private ComboBox LengthOfEmploymentComboBox
        {
            get { return ComboBox("lengthOfEmployment"); }
        }

        public EmploymentStatusObjects SetEmploymentStatusComboBox(string employmentStatus)
        {
            EmploymentStatus.SetValue(employmentStatus);
            return this;              
        }

        public EmploymentStatusObjects SetDateStatusAppliesDateTime(DateTime dateTime)
        {
            DateStatusApplies.SetDate(dateTime, DateFormat.DayMonthYear);
            return this;
        }

        public EmploymentStatusObjects SetEmployerIdentifierTextBox(string employerIdentifier)
        {
            EmployerIdentifierTextBox.SetValue(employerIdentifier);
            return this;
        }
        
        public EmploymentStatusObjects SetAgreementIdentifierTextBox(string agreementIdentifier)
        {
            AgreementIdentifierTextBox.SetValue(agreementIdentifier);
            return this;
        }

        public EmploymentStatusObjects SetLengthOfUnemploymentComboBox(string lengthOfUnemployment)
        {
            LengthOfUnemploymentComboBox.SetValue(lengthOfUnemployment);
            return this;
        }

        public EmploymentStatusObjects SetBenefitStatusIndicatorComboBox(string benefitStatusIndicator)
        {
            BenefitStatusIndicatorComboBox.SetValue(benefitStatusIndicator);
            return this;
        }

        public EmploymentStatusObjects SetEmploymentIntensityIndicatorComboBox(string employmentIntensityIndicator)
        {
            EmploymentIntensityIndicatorComboBox.SetValue(employmentIntensityIndicator);
            return this;
        }

        public EmploymentStatusObjects SetLengthOfEmploymentComboBox(string lengthOfEmployment)
        {
            LengthOfEmploymentComboBox.SetValue(lengthOfEmployment);
            return this;
        }

        public string GetEmploymentStatusValidationMessage
        {
            get
            {
                return EmploymentStatus.HelpText;
            }
        }

        public string GetEmployerIdentifierValidationMessage
        {
            get
            {
                return EmployerIdentifierTextBox.HelpText;
            }
        }

        public string GetAgreementIdentifierValidationMessage
        {
            get
            {
                return AgreementIdentifierTextBox.HelpText;
            }
        }

        public string GetLengthOfUnemploymentValidationMessage
        {
            get
            {
                return LengthOfUnemploymentComboBox.HelpText;
            }
        }

        public string GetBenefitStatusIndicatorValidationMessage
        {
            get
            {
                return BenefitStatusIndicatorComboBox.HelpText;
            }
        }

        public string GetEmploymentIntensityIndicatorValidationMessage
        {
            get
            {
                return EmploymentIntensityIndicatorComboBox.HelpText;
            }
        }

        public string GetLengthOfEmploymentValidationMessage
        {
            get
            {
                return LengthOfEmploymentComboBox.HelpText;
            }
        }
    }
}
