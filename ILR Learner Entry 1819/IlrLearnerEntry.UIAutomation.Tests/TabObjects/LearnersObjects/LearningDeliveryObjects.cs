using IlrLearnerEntry.UIAutomation.Tests.WindowObjects;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.UIItems;
using TestStack.White.UIItems.TabItems;
using TestStack.White.UIItems.Finders;

namespace IlrLearnerEntry.UIAutomation.Tests.TabObjects.LearnersObjects
{
    public class LearningDeliveryObjects : WindowObject
    {
        internal LearningDeliveryObjects(Window window) : base(window)
        {
        }

        private Button AddLearningDeliveryButton
        {
            get
            {
                return Button("Add");
            }
        }

        private TabPage LearningDeliveryHETab
        {
            get
            {
                return _window.Get<TabPage>(SearchCriteria.ByText(" Learning Delivery HE "));
            }
        }

        public string GetStudentInstanceIdentifierValidationMessage
        {
            get
            {
                return StudentInstanceIdentifierTextBox.HelpText;
            }
        }

        public string GetUCASApplicationCodeValidationMessage
        {
            get
            {
                return UCASApplicationCodeTextBox.HelpText;
            }
        }

        public string GetStudentSupportNumberValidationMessage
        {
            get
            {
                return StudentSupportNumberTextBox.HelpText;
            }
        }

        public string GetOccupationCodeValidationMessage
        {
            get
            {
                return OccupationCode.HelpText;
            }
        }

        private TextBox StudentInstanceIdentifierTextBox
        {
            get { return TextBox("studentInstanceIdentifier"); }
        }

        private TextBox UCASApplicationCodeTextBox
        {
            get { return TextBox("ucasApplicationCode"); }
        }

        private TextBox StudentSupportNumberTextBox
        {
            get { return TextBox("studentSupportNumber"); }
        }

        private TextBox OccupationCode
        {
            get { return TextBox("occupationCode"); }
        }


        public LearningDeliveryObjects ClickAddLearnerButton()
        {
            AddLearningDeliveryButton.Click();
            return this;
        }

        public LearningDeliveryObjects SelectLearningDeliveryHETab()
        {
            LearningDeliveryHETab.Select();
            return this;
        }

        public LearningDeliveryObjects SetStudentInstanceIdentifierTextBox(string studentInstanceIdentifier)
        {
            StudentInstanceIdentifierTextBox.Text = studentInstanceIdentifier;
            return this;
        }

        public LearningDeliveryObjects SetUCASApplicationCodeTextBox(string ucasApplicationCode)
        {
            UCASApplicationCodeTextBox.Text = ucasApplicationCode;
            return this;
        }

        public LearningDeliveryObjects SetStudentSupportNumberTextBox(string StudentSupportNumber)
        {
            StudentSupportNumberTextBox.Text = StudentSupportNumber;
            return this;
        }

        public LearningDeliveryObjects SetOccupationCodeTextBox(string occupationCode)
        {
            OccupationCode.Text = occupationCode;
            return this;
        }
    }
}
 