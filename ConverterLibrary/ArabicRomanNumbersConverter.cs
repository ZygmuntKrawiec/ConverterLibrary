using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConverterLibrary
{
    /// <summary>
    /// Represents a converter which changes an arabic number into roman numeral and vice versa.
    /// </summary>
    public class ArabicRomanNumbersConverter
    {   
        /// <summary>
        /// Converts an arabic number into a roman numeral.
        /// A number to convert must be in the range of 0 to 3999.
        /// </summary>
        /// <param name="num">An arabic number to convert.</param>
        /// <returns>A roman numeral.</returns>
        public string ConvertArabicToRoman(int num)
        {
            if (num > 3999 || num <= 0)
                return null;

            int number = num;
            StringBuilder value = new StringBuilder();
            var romanCharacters = Enum.GetValues(typeof(romanDigit));
            int i = romanCharacters.GetLength(0) - 1;
            romanDigit character = (romanDigit)romanCharacters.GetValue(i);

            do
            {
                if (number >= (int)character)
                {

                    value.Append(character);
                    number = number - (int)character;
                }
                else
                {
                    if (character != romanDigit.I)
                    {
                        int specialSymbol = findRomanSpecialNumber(character);
                        if (specialSymbol <= number)
                        {
                            romanSpecialNumber rsn = (Enum.GetValues(typeof(romanSpecialNumber))).OfType<romanSpecialNumber>().ToList().Find(x => (int)x == specialSymbol);
                            value.Append(rsn);
                            number -= (int)rsn;
                        }
                    }
                    character = (romanDigit)romanCharacters.GetValue(--i);
                }
            }
            while (number > 0);
            return value.ToString();
        }

        /// <summary>
        /// Converts a roman numeral into an arabic number.
        /// </summary>
        /// <param name="num">A roman numeral.</param>
        /// <returns>AN arabic number.</returns>
        public int ConvertRomanToArabic(string num)
        {
            if (num == null)
                return 0;
            int i = 0, value = 0;
            var numberString = num.ToUpper().Trim();
            int amountOfChars = numberString.Length;
            var romanCharacters = Enum.GetValues(typeof(romanDigit));
            int currentRomanDigit = romanCharacters.Length;
            romanDigit character = (romanDigit)romanCharacters.GetValue(--currentRomanDigit);

            // A Guardian instance checks if a romanDigit in the roman numeral appears maximum three times,
            // otherwise it throws an exception. 
            Guardian guardian = new Guardian(3, () => { throw new Exception("Your number format is wrong."); });

            do
            {
                if (character.ToString() == numberString[i].ToString())
                {
                    guardian.Call();
                    value += (int)character;
                    i++;
                }
                else
                {
                    if ((amountOfChars - i) > 1)
                    {
                        int specialSymbol = findRomanSpecialNumber(character);
                        romanSpecialNumber rsn = (Enum.GetValues(typeof(romanSpecialNumber))).OfType<romanSpecialNumber>().ToList().Find(x => (int)x == specialSymbol);
                        if (numberString.Substring(i, 2) == rsn.ToString())
                        {
                            value += (int)rsn;
                            i += 2;
                        }
                    }

                    if (currentRomanDigit == 0)
                    {
                        throw new Exception("You gave a wrong sign in your roman number or one of the " +
                                            "digits is on a wrong position");
                    }
                    character = (romanDigit)romanCharacters.GetValue(--currentRomanDigit);
                    guardian.ResetNumberOfCalls();
                }
            }
            while (amountOfChars > i);
            return value;
        }
        
        private int findRomanSpecialNumber(romanDigit character)
        {
            double conditionNumber = (int)character;
            while (conditionNumber > 10)
            {
                conditionNumber *= 0.1;
            }
            return (conditionNumber % 2 == 0) ? (int)((int)character * 0.9) : (int)((int)character - ((int)character * 0.2));
        }
    }

    /// <summary>
    /// Represents a set of roman digits.
    /// </summary>
    enum romanDigit
    {
        I = 1,
        V = 5,
        X = 10,
        L = 50,
        C = 100,
        D = 500,
        M = 1000,
    }

    /// <summary>
    /// Represents a set of roman special numbers, which appears only in a special sequence.
    /// </summary>
    enum romanSpecialNumber
    {
        IV = 4,
        IX = 9,
        XL = 40,
        XC = 90,
        CD = 400,
        CM = 900,
    }
}
