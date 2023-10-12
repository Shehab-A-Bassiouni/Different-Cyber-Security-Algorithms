using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class Ceaser : ICryptographicTechnique<string, int>
    {
        public string Encrypt(string plainText, int key)
        {
            StringBuilder tmp_plaintext = new StringBuilder(plainText);

            int i = 0;
            while (i < tmp_plaintext.Length)
            {
                if (tmp_plaintext[i] != ' ')

                {
                    tmp_plaintext[i] = (char)(tmp_plaintext[i] + key % tmp_plaintext.Length);

                }

                if ((tmp_plaintext[i] > 'z'))
                {
                    tmp_plaintext[i] = (char)((tmp_plaintext[i] % 'z') + ('a' - 1));
                }
                else if ((tmp_plaintext[i] > 'Z') && (tmp_plaintext[i] < 'a'))
                {
                    tmp_plaintext[i] = (char)((tmp_plaintext[i] % 'Z') + ('A' - 1));
                }



                i++;

            }
            plainText = tmp_plaintext.ToString();
            return plainText;
        }

        public string Decrypt(string cipherText, int key)
        {
            StringBuilder tmp_cipherText = new StringBuilder(cipherText);

            int i = 0;

            while (i < tmp_cipherText.Length)
            {
                if (tmp_cipherText[i] != ' ')

                {

                    tmp_cipherText[i] = (char)(tmp_cipherText[i] - key % cipherText.Length);

                }

                if (((tmp_cipherText[i] < 'a') && (tmp_cipherText[i] > 'Z')))
                {
                    tmp_cipherText[i] = (char)(('z' + 1) - ('a' - tmp_cipherText[i]));
                }
                else if ((tmp_cipherText[i] < 'A') && (tmp_cipherText[i] > ' '))
                {
                    tmp_cipherText[i] = (char)(('Z' + 1) - ('A' - tmp_cipherText[i]));
                }

                i++;



            }
            cipherText = tmp_cipherText.ToString();
            return cipherText;
        }

        public int Analyse(string plainText, string cipherText)
        {
            if (plainText.Length != cipherText.Length) return -1;
            int letterPN = letterNum(plainText[0]);
            int letterCN = letterNum(char.ToLower(cipherText[0]));

            return ((letterCN - letterPN) < 0) ? (letterCN - letterPN) + 26 : (letterCN - letterPN) % 26;
        }

        public int letterNum(char letter)
        {
            string alphabet = "abcdefghijklmnopqrstuvwxyz";
            for (int i = 0; i < 26; i++)
            {
                if (letter == alphabet[i]) return i;
            }

            return -1;
        }
    }
}
