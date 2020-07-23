using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dollars_In_words
{
    class ConvertToWords
    {
        #region Properties
        public string WordsOfDollars { get; set; }
        public string WordsOfCents { get; set; }
        public bool HasCents { get; set; }
        public long Dollars { get; set; }
        public int Cents { get; set; }
        #endregion

        #region Constructor

        public ConvertToWords()
        {

        }

        #endregion

        #region Methods
        public string DollarsToWords(long dollars, int cents = 0)
        {
            Dollars = dollars;
            Cents = cents;
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

            #region Dollars
            if (dollars > 0)
            {
                var leastSignificantNumber = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                var mostSignificantNumber = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
                if (dollars < 20)
                    WordsOfDollars += leastSignificantNumber[dollars];
                else
                {
                    WordsOfDollars += mostSignificantNumber[dollars / 10];
                    if ((dollars % 10) > 0)
                        WordsOfDollars += "-" + leastSignificantNumber[dollars % 10];
                }
            }
            #endregion

            #region Cents
            if (cents > 0)
            {
                var leastSignificantNumber = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                var mostSignificantNumber = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                if (cents < 20)
                    WordsOfCents += leastSignificantNumber[cents];
                else
                {
                    WordsOfCents += mostSignificantNumber[cents / 10];
                    if ((cents % 10) > 0)
                        WordsOfCents += "-" + leastSignificantNumber[cents % 10];
                }
            }
            #endregion

            HasCents = string.IsNullOrEmpty(WordsOfCents);

            return WordsOfDollars + WordsOfCents;
        }
        public void ParsingTheString(string number, out long dollars, out int cents, out string Error)
        {
            Error = "";
            dollars = 0;
            cents = 0;
            Regex letters = new Regex(@"([\p{L}\s]+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            if (string.IsNullOrEmpty(number) || letters.IsMatch(number))
            {
                Error = "Insert a number in the correct format!";
                return;
            }
            #region WithDecimals
            if (number.Contains(","))
            {
                string[] dollarsAndCents = number.Split(',');
                if (dollarsAndCents.Length > 2)
                    Error = "You have too many commas there!";
                else
                {
                    string dollarsNumber = dollarsAndCents[0];
                    string centsNumber = dollarsAndCents[1].PadRight(2, '0');
                    if (centsNumber.Length >= 3)
                        Error = "Cents length is to big! Only 2 digits allowed";
                    else
                    {
                        try
                        {
                            dollars = Int64.Parse(dollarsNumber);
                            cents = Int32.Parse(centsNumber);
                        }
                        catch (Exception)
                        {
                            Error = "Unknow character! Try again";
                        }
                    }
                }
            }
            #endregion

            #region WithoutDecimals
            else
            {
                try
                {
                    dollars = Int64.Parse(number);
                }
                catch (Exception)
                {
                    Error = "Unknow character! Try again";
                }
            }
            #endregion
        }
        public string Result()
        {
            #region 0 dollars case and maximum/minimum amount reached
            if (Dollars > 999999999 || Cents > 99)
                return "Maximum of 999999999 dollars or 99 cents surpassed try with a smaller number";

            if (Dollars == 0 && Cents == 0)
                return "zero dollars";
            if (Dollars == 0)
                WordsOfDollars = "zero";

            if (Dollars < 0)
                return "Not possible to have more than 0 dollars!";
            #endregion

            return WordsOfDollars + " dollars " + (HasCents ? "" : "and " + WordsOfCents + " cents");
        }
        #endregion
    }
}
