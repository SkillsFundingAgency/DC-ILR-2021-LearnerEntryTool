using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.White.UIItems;
using TestStack.White.UIItems.ListBoxItems;
using TestStack.White.UIItems.WindowItems;

namespace IlrLearnerEntry.UIAutomation.Tests.WindowObjects
{
    public class LearnerWindow: WindowObject
    {
        internal LearnerWindow(Window window) : base(window)
        {
        }

        private Button AddLearnerButton
        {
            get {
                return Button("  Add Learner  ");
            }
        }

        private TextBox LearnerRefTextBox
        {
            get { return TextBox("txtLearnerRef"); }
        }

        private TextBox LearnerULNTextBox
        {
            get { return TextBox("txtULN"); }
        }

        private TextBox LearnerPrevRefTextBox
        {
            get { return TextBox("txtPrvRefNumber"); }
        }

        private TextBox LearnerPrevUKPFRNTextBox
        {
            get { return TextBox("txtPrvUKPRN"); }
        }

        private TextBox LearnerCampusIdTextBox
        {
            get { return TextBox("txtCampusId"); }
        }
        private TextBox LearnerOTJHoursTextBox
        {
            get { return TextBox("txtOTJHours"); }
        }

        private TextBox LearnerGivenNameTextBox
        {
            get { return TextBox("txtGivenName"); }
        }

        private TextBox LearnerFamilyNameTextBox
        {
            get { return TextBox("txtFamilyName"); }
        }
        private DateTimePicker LearnerDoBDateTimePicker
        { 
                get { return DateTimePicker("dtDOB"); }
        }

        private ComboBox LearnerSexComboBox
        {
            get { return ComboBox("cmbSex"); }
        }

        public LearnerWindow SetReferenceNumber(string refNumber)
        {
            LearnerRefTextBox.Text = refNumber;
            return this;
        }

        public LearnerWindow SetULN(string uln)
        {
            LearnerULNTextBox.Text = uln;
            return this;
        }

        public LearnerWindow SetPreviousRefNumber(string refNumber)
        {
            LearnerPrevRefTextBox.Text = refNumber;
            return this;
        }
        public LearnerWindow SetPreviousUKPRN(string ukprn)
        {
            LearnerPrevUKPFRNTextBox.Text = ukprn;
            return this;
        }

        public LearnerWindow SetCampusId(string campusId)
        {
            LearnerCampusIdTextBox.Text = campusId;
            return this;
        }

        public LearnerWindow SetOffTheJobHours(string otHours)
        {
            LearnerOTJHoursTextBox.Text = otHours;
            return this;
        }

        public LearnerWindow SetGivenName(string givenName)
        {
            LearnerGivenNameTextBox.Text = givenName;
            return this;
        }

        public LearnerWindow SetFamilyName(string familyName)
        {
            LearnerGivenNameTextBox.Text = familyName;
            return this;
        }

        public LearnerWindow SetDoB(DateTime dateOfBirth)
        {
            LearnerDoBDateTimePicker.SetValue(dateOfBirth);
            return this;
        }

        public LearnerWindow SetSex(string sex)
        {
            LearnerSexComboBox.SetValue("Male");
            return this;
        }

        public LearnerWindow ClickAddLearnerButton()
        {
            AddLearnerButton.Click();
            return this;
        }


        #region ValidationErrorMessage
        public string GetULNValidationMessage
        {
            get
            {
                return LearnerULNTextBox.HelpText;
            }
        }

        public string GetRefNumberValidationMessage
        {
            get
            {
                return LearnerRefTextBox.HelpText;
            }
        }

        public string GetGivenNameValidationMessage { get {
                return LearnerGivenNameTextBox.HelpText;
            } }
        #endregion

    }

}
