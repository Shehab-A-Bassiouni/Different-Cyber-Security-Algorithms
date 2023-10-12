using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class RailFence : ICryptographicTechnique<string, int>
    {
        public int Analyse(string plainText, string cipherText)
        {
            int Result = 0;
            cipherText = cipherText.ToUpper();
            plainText = plainText.ToUpper();
            char c = cipherText[1];
            for (int i = 0; i < plainText.Length; i++)
            {
                if (c == plainText[i])
                {
                    Result = i;
                    if (plainText[i] == plainText[i + 1])
                        Result++;
                    break;
                }
            }
            return Result;
        }

        public string Decrypt(string cipherText, int key)
        {
            string Result = string.Empty;
            double V = Math.Ceiling(Convert.ToDouble(cipherText.Length) / key);
            char[,] Matrix = Build_Clean_Matrix(key, ((int)V));
            int x = 0;
            //FILL_MATRIX
            for (int i = 0; i < key; i++)
            {
                for (int j = 0; j < ((int)Math.Ceiling(V)); j++)
                {
                    Matrix[i, j] = cipherText[x];
                    x++;
                    if (x == cipherText.Length)
                        break;
                }
                if (x == cipherText.Length)
                    break;
            }
            for (int i = 0; i < ((int)Math.Ceiling(V)); i++)
            {
                for (int j = 0; j < key; j++)
                {
                    if (Matrix[j, i] == '*')
                    {
                        break;
                    }
                    Result += Matrix[j, i];
                }
            }
            return Result;
        }

        public string Encrypt(string plainText, int key)
        {
            string Result = string.Empty;
            char[,] Matrix = Build_Clean_Matrix(key, plainText.Length);
            int c = 0;

            //FILL_MATRIX
            for (int i = 0; i < plainText.Length; i++)
            {
                for (int j = 0; j < key; j++)
                {
                    Matrix[j, c] = plainText[i];
                    i++;
                    if (i == plainText.Length)
                        break;
                }
                i--;
                c++;
            }

            for (int i = 0; i < key; i++)
            {
                for (int j = 0; j < plainText.Length; j++)
                {
                    if (Matrix[i, j] == '*')
                        continue;
                    Result += Matrix[i, j];
                }
            }
            return Result;
        }

        public static char[,] Build_Clean_Matrix(int rows, int cols)
        {
            char[,] result = new char[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[i, j] = '*';
                }

            }
            return result;
        }
    }
}
