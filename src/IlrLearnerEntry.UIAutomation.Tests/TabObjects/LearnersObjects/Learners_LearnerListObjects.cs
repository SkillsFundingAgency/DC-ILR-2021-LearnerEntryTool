using System;
using TestStack.White.UIItems;
using TestStack.White.UIItems.ListBoxItems;
using TestStack.White.UIItems.WindowItems;

namespace IlrLearnerEntry.UIAutomation.Tests.WindowObjects
{
    public class Learners_LearnerListObjects: WindowObject
    {
        internal Learners_LearnerListObjects(Window window) : base(window)
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

        public Learners_LearnerListObjects SetReferenceNumber(string refNumber)
        {
            LearnerRefTextBox.Text = refNumber;
            return this;
        }

        public Learners_LearnerListObjects SetULN(string uln)
        {
            LearnerULNTextBox.Text = uln;
            return this;
        }

        public Learners_LearnerListObjects SetPreviousRefNumber(string refNumber)
        {
            LearnerPrevRefTextBox.Text = refNumber;
            return this;
        }
        public Learners_LearnerListObjects SetPreviousUKPRN(string ukprn)
        {
            LearnerPrevUKPFRNTextBox.Text = ukprn;
            return this;
        }

        public Learners_LearnerListObjects SetCampusId(string campusId)
        {
            LearnerCampusIdTextBox.Text = campusId;
            return this;
        }

        //public Learners_LearnerListObjects SetOffTheJobHours(string otHours)
        //{
        //    LearnerOTJHoursTextBox.Text = otHours;
        //    return this;
        //}

        public Learners_LearnerListObjects SetGivenName(string givenName)
        {
            LearnerGivenNameTextBox.Text = givenName;
            return this;
        }

        public Learners_LearnerListObjects SetFamilyName(string familyName)
        {
            LearnerGivenNameTextBox.Text = familyName;
            return this;
        }

        public Learners_LearnerListObjects SetDoB(DateTime dateOfBirth)
        {
            LearnerDoBDateTimePicker.SetValue(dateOfBirth);
            return this;
        }

        public Learners_LearnerListObjects SetSex(string sex)
        {
            LearnerSexComboBox.SetValue("Male");
            return this;
        }

        public Learners_LearnerListObjects ClickAddLearnerButton()
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
