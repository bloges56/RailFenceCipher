using System;

namespace RailFenceCipher
{
    class Program
    {
        static void Main(string[] args)
        {
            string plainText = System.IO.File.ReadAllText(args[0]).ToLower();

            int depth = Int32.Parse(args[1]);

            string cipherText = Encrypt(plainText, plainText, depth, 1, 1, true);

            Console.WriteLine(cipherText);

            plainText = Decrypt(cipherText, depth - 1);

            Console.WriteLine(plainText);

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

        public static string Decrypt(string cipherText, int depth)
        {
            bool[,] boolMatrix = new bool[depth+1,cipherText.Length];

            FormBoolMatrix(boolMatrix, 0, 0, true, depth, cipherText.Length);

            char[,] charMatrix = new char[depth+1, cipherText.Length];

            FormCharMatrix(cipherText, cipherText, boolMatrix, charMatrix, 0, 0, depth, cipherText.Length);

            return CreatePlainText(charMatrix, 0, 0, true, depth, cipherText.Length);
        }

        public static string CreatePlainText(char[,] matrix, int row, int col, bool rising, int depth, int length)
        {
            if(col == length)
            {
                return "";
            }

            if(row == depth)
            {
                return matrix[row,col] + CreatePlainText(matrix, --row, ++col, false, depth, length);
            }

            if(row == 0)
            {
                return matrix[row,col] + CreatePlainText(matrix, ++row, ++col, true, depth, length);
            }

            if(rising)
            {
                return matrix[row,col] + CreatePlainText(matrix, ++row, ++col, true, depth, length);
            }

            return matrix[row,col] + CreatePlainText(matrix, --row, ++col, false, depth, length);
        }

        public static char[,] FormCharMatrix(string cipherText, string modifiedText, bool[,] blueMatrix, char[,] matrix, int row, int col, int depth, int length)
        {
            if (col == length  && row == depth)
            {
                return matrix;
            }
    
            if (col == length)
            {
                return FormCharMatrix(cipherText, modifiedText, blueMatrix, matrix, ++row, 0, depth, length);
            }

            if (blueMatrix[row, col] == true)
            {
                matrix[row, col] = modifiedText[0];
                return FormCharMatrix(cipherText, modifiedText.Substring(1), blueMatrix, matrix, row, ++col, depth, length);
            }

            return FormCharMatrix(cipherText, modifiedText, blueMatrix, matrix, row, ++col, depth, length);

        }

        public static bool[,] FormBoolMatrix(bool[,] matrix, int row, int col, bool rising, int depth, int length)
        {
            if(col == length)
            {
                return matrix;
            }

            matrix[row,col] = true;

            if (row == depth)
            {
                return FormBoolMatrix(matrix, --row, ++col, false, depth, length);
            }

            if(row == 0)
            {
                return FormBoolMatrix(matrix, ++row, ++col, true, depth, length);
            }

            if(rising)
            {
                return FormBoolMatrix(matrix, ++row, ++col, true, depth, length);
            }

            return FormBoolMatrix(matrix, --row, ++col, false, depth, length);
        }

    }
}
