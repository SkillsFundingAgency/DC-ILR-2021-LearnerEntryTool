using System;
using System.Text.RegularExpressions;

namespace Utils
{
   public static class CommonValidations
    {
        private static readonly Regex _regex = new Regex("[^0-9.-]+"); 

        public static bool IsValidNumber(string stringValue)
        {

            int num;
            return Int32.TryParse(stringValue, out num);
        }
 
        public static bool IsNumber(string text)
        {
            return _regex.IsMatch(text);
        }
    }
}
