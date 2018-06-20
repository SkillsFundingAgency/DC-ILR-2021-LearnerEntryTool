using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.White.UIItems;
using TestStack.White.UIItems.WindowItems;

namespace IlrLearnerEntry.UIAutomation.Tests.WindowObjects
{
    public class HomeWindow: WindowObject
    {
        internal HomeWindow(Window window) : base(window)
        {

        }

        public string GetUKPRNValidationMessage { get { return UKPRNTextBox.HelpText; } }

        private TextBox UKPRNTextBox
        {
            get { return TextBox("txtUKPRM"); }
        }

        public HomeWindow SetUkPRN(string newUKPrn)
        {
            UKPRNTextBox.Text = newUKPrn;
            return this;
        }

    }
}
