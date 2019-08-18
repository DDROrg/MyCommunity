using System;
using System.Diagnostics;

namespace MyCommunity.Utility
{
    public class Base36
    {
        private readonly static char[] base36Chars = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

        [DebuggerStepThrough]
        public static string Encode(UInt64 value)
        {
            //InitilizeBase36();   // set the char[] array
            string returnValue = ""; // starting value
            while (value != 0)
            {
                returnValue = base36Chars[value % (uint)base36Chars.Length] + returnValue;
                value /= (uint)base36Chars.Length;
            }
            returnValue = returnValue.ToUpper();
            return returnValue;
        }

        [DebuggerStepThrough]
        public static UInt64 Decode(string input)
        {
            //InitilizeBase36();
            input = input.Trim();
            input = input.ToLower();
            UInt64 returnValue = 0;

            char[] characters = input.ToCharArray();
            Array.Reverse(characters);

            for (int i = 0; i < characters.Length; i++)
            {
                // find the index in the array that the char resides
                int valueindex = Array.IndexOf(base36Chars, characters[i]);
                // the actual value given by that is 
                // the index multiplied by the base number to the power of the index
                double temp = valueindex * Math.Pow(base36Chars.Length, i);
                // add this value to the counter until there are no more values
                returnValue += Convert.ToUInt64(temp);
            }
            // return the total result
            return returnValue;
        }
    }
}
