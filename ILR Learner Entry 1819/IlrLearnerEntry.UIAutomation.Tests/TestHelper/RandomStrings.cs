using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IlrLearnerEntry.UIAutomation.Tests.TestHelper
{
    public static class RandomStrings
    {
        const string allChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        const string allNumbs = "0123456789";
        const string allnumChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz0123456789";

        public static string GetRandomAlphabets(int length)
        {
            Random random = new Random();
            var stringChars = new char[length];
            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = allChars[random.Next(allChars.Length)];
            }
            return new String(stringChars);
        }

        public static string GetRandomAlphaNumeric(int length)
        {
            Random random = new Random();
            var stringChars = new char[length];
            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = allnumChars[random.Next(allnumChars.Length)];
            }
            return new String(stringChars);
        }

        public static string GetRandomNumber(int length)
        {
            Random random = new Random();
            var stringChars = new char[length];
            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = allNumbs[random.Next(allNumbs.Length)];
            }
            return new String(stringChars);
        }
    }
}
