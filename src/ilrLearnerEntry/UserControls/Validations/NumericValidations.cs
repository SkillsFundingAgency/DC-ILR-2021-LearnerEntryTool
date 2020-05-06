using System;

namespace ilrLearnerEntry.UserControls.Validations
{
    public static class NumericValidations
    {
        public static string CheckInt32ValidValue(string value, string columnName)
        {
            return Int32.TryParse(value, out _) ? string.Empty : $"{columnName} has non numeric values. this will NOT be SAVED !!!";
        }

        public static string CheckInt64ValidValue(string value, string columnName)
        {
            return Int64.TryParse(value, out _) ? string.Empty : $"{columnName} has non numeric values. this will NOT be SAVED !!!";
        }
    }
}
