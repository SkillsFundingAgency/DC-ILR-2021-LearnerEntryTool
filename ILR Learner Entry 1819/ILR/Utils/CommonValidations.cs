using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils
{
   public static class CommonValidations
    {

        public static bool IsValidNumber(string stringValue)
        {

            int num;
            return Int32.TryParse(stringValue, out num);
        }
    }
}
