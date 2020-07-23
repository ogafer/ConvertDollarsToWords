using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dollars_In_words
{
    class ConvertToWords
    {
        #region Properties
        public string WordsOfDollars { get; set; }
        public string WordsOfCents { get; set; }
        #endregion

        #region Constructor

        public ConvertToWords()
        {

        }

        #endregion

        #region Methods
        public string DollarsToWords(long dollars, int cents = 0)
        {
            if (dollars > 999999999 || cents > 99)
                return "Maximum of 999999999 dollars or 99 cents surpassed try with a smaller number";

            if (dollars == 0)
                return "zero";

            if (dollars < 0)
                return "Not possible to have more than 0 dollars!";

            WordsOfDollars = "";
            WordsOfCents = "";

            if ((dollars / 1000000) > 0)
            {
                WordsOfDollars += DollarsToWords(dollars / 1000000) + " million ";
                dollars %= 1000000;
            }

            if ((dollars / 1000) > 0)
            {
                WordsOfDollars += DollarsToWords(dollars / 1000) + " thousand ";
                dollars %= 1000;
            }

            if ((dollars / 100) > 0)
            {
                WordsOfDollars += DollarsToWords(dollars / 100) + " hundred ";
                dollars %= 100;
            }

            if (dollars > 0)
            {
                if (WordsOfDollars != "")
                    WordsOfDollars += "and ";

                var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                if (dollars < 20)
                {
                    WordsOfDollars += unitsMap[dollars];
                }
                else
                {
                    WordsOfDollars += tensMap[dollars / 10];
                    if ((dollars % 10) > 0)
                    {
                        WordsOfDollars += "-" + unitsMap[dollars % 10];
                    }
                }
            }
            #region Cents
            if (cents > 0)
            {
                if (WordsOfCents != "")
                    WordsOfCents += "and ";

                var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                if (cents < 20)
                {
                    WordsOfCents += unitsMap[cents];
                }
                else
                {
                    WordsOfCents += tensMap[cents / 10];
                    if ((cents % 10) > 0)
                    {
                        WordsOfCents += "-" + unitsMap[cents % 10];
                    }
                }
            }
            #endregion
            bool noCents = string.IsNullOrEmpty(WordsOfCents);
            return WordsOfDollars + " dollars " + (noCents? "" : "and " + WordsOfCents + " cents");
        }
        #endregion
    }
}
