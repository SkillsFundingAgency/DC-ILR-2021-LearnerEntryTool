using TestStack.White.UIItems;
using TestStack.White.UIItems.WindowItems;

namespace IlrLearnerEntry.UIAutomation.Tests.WindowObjects
{
    public class HomeObjects: WindowObject
    {
        internal HomeObjects(Window window) : base(window)
        {

        }

        public string GetUKPRNValidationMessage { get { return UKPRNTextBox.HelpText; } }

        private TextBox UKPRNTextBox
        {
            get { return TextBox("txtUKPRM"); }
        }

        public HomeObjects SetUkPRN(string newUKPrn)
        {
            UKPRNTextBox.Text = newUKPrn;
            return this;
        }

    }
}
