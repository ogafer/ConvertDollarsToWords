using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;

namespace Server
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class ServiceConvertToDollars : IServiceConvertToDollars
    {
        #region Properties
        public static string WordsOfDollars { get; set; }
        public static string WordsOfCents { get; set; }
        public static bool HasCents { get; set; }
        public static long Dollars { get; set; }
        public static int Cents { get; set; }
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
        public void ParsingTheString(string numberFromUser, ref long dollarsConverted, ref int centsConverted, ref string error)
        {
            error = "";
            dollarsConverted = 0;
            centsConverted = 0;
            Regex letters = new Regex(@"([\p{L}\s]+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            if (string.IsNullOrEmpty(numberFromUser) || letters.IsMatch(numberFromUser))
            {
                error = "Insert a number in the correct format!";
                return;
            }
            #region WithDecimals
            if (numberFromUser.Contains(","))
            {
                string[] dollarsAndCents = numberFromUser.Split(',');
                if (dollarsAndCents.Length > 2)
                    error = "You have too many commas there!";
                else
                {
                    string dollarsNumber = dollarsAndCents[0];
                    string centsNumber = dollarsAndCents[1].PadRight(2, '0');
                    if (centsNumber.Length >= 3)
                        error = "Cents length is to big! Only 2 digits allowed";
                    else
                    {
                        try
                        {
                            dollarsConverted = Int64.Parse(dollarsNumber);
                            centsConverted = Int32.Parse(centsNumber);
                        }
                        catch (Exception)
                        {
                            error = "Unknow character! Try again";
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
                    dollarsConverted = Int64.Parse(numberFromUser);
                }
                catch (Exception)
                {
                    error = "Unknow character! Try again";
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
                return "Not possible to have less than 0 dollars!";
            #endregion

            return WordsOfDollars + " dollars " + (HasCents ? "" : "and " + WordsOfCents + " cents");
        }
        #endregion
    }
}
