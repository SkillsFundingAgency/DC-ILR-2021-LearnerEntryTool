using System.Windows.Controls;

namespace ilrLearnerEntry.Utils
{
    public class StringToIntValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (int.TryParse(value.ToString(), out _))
                return new ValidationResult(true, null);

            return new ValidationResult(false, "Please enter a valid integer value.");
        }
    }

    public class UKPRNValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (int.TryParse(value.ToString(), out _))
                return new ValidationResult(true, null);

            return new ValidationResult(false, "Please enter a valid integer value for the UKPRN!");
        }
    }
}
