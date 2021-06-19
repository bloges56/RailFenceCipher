using System;

namespace RailFenceCipher
{
    class Program
    {
        static void Main(string[] args)
        {
            string plainText = System.IO.File.ReadAllText(args[0]).ToLower();

            string cipherText = Encrypt(plainText, plainText, Int32.Parse(args[1]), 1, 1, true);

            Console.WriteLine(cipherText);
        }

        public static string Encrypt(string plainText, string modifiedText, int depth, int curLevel, int lowestLevel, bool rising)
        {
            if(lowestLevel > depth)
            {
                return "";
            }
            if(modifiedText == "")
            {
                return "" + Encrypt(plainText, plainText, depth, 1, ++lowestLevel, true);
            }
            if (curLevel == lowestLevel)
            {
                if(curLevel == depth)
                {
                    return modifiedText[0] + Encrypt(plainText, modifiedText.Substring(1), depth, --curLevel, lowestLevel, false);
                }
                if(curLevel == 1)
                {
                    return modifiedText[0] + Encrypt(plainText, modifiedText.Substring(1), depth, ++curLevel, lowestLevel, true);
                }

                if(rising)
                {
                    return modifiedText[0] + Encrypt(plainText, modifiedText.Substring(1), depth, ++curLevel, lowestLevel, true);
                }

                return modifiedText[0] + Encrypt(plainText, modifiedText.Substring(1), depth, --curLevel, lowestLevel, false);

            }

            if(curLevel == depth)
            {
                return "" + Encrypt(plainText, modifiedText.Substring(1), depth, --curLevel, lowestLevel, false);
            }

            if(curLevel == 1)
            {
                return "" + Encrypt(plainText, modifiedText.Substring(1), depth, ++curLevel, lowestLevel, true);
            }

            if(rising)
            {
                return "" + Encrypt(plainText, modifiedText.Substring(1), depth, ++curLevel, lowestLevel, true);
            }

            return "" + Encrypt(plainText, modifiedText.Substring(1), depth, --curLevel, lowestLevel, false);

        }
    }
}
