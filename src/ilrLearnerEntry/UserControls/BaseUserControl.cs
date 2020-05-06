using System;
using System.Windows.Controls;

namespace ilrLearnerEntry.UserControls
{
    public class BaseUserControl : UserControl
    {

        private readonly ILR.Schema XmlSchema = new ILR.Schema();

        private int GetItemSize(string ItemName)
        {
            return XmlSchema.GetMaxLength(ItemName);
        }

        protected string CheckPropertyLength(object itemValue, string ClassName, string ItemName)
        {
            String ItemFullName = String.Format("{0}.{1}", ClassName, ItemName);
            int ItemSize = GetItemSize(ItemFullName);
            if (itemValue != null && itemValue.ToString().Length > ItemSize)
            {
                return String.Format("exceeds maximum length ({0} characters). Current length : {1}\r\n", ItemSize, itemValue.ToString().Length);
            }
            return null;
        }

        public string Error => throw new NotImplementedException();
    }
}
