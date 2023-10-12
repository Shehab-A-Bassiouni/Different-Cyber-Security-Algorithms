using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class AutokeyVigenere : ICryptographicTechnique<string, string>
    {
        public string Analyse(string plainText, string cipherText)
        {
            plainText = plainText.ToLower();
            cipherText = cipherText.ToLower();
            char[][] charArray = buildMat();
            Dictionary<char, int> maper = map();
            string key = "";
            for (int e = 0; e < cipherText.Length; e++)
            {
                int indx1 = maper[plainText[e]];
                int indx2 = Array.IndexOf(charArray[indx1], cipherText[e]);
                key += maper.FirstOrDefault(x => x.Value == indx2).Key;
            }



            string new_key = "";
            int i = 0;
            while (i < key.Length)
            {
                if (key[i] != plainText[0])
                {
                    new_key += key[i];
                    i++;
                }

                else
                {
                    int x = 0;
                    int temp = i;
                    bool flag = false;
                    while (i < key.Length)
                    {
                        if (x == plainText.Length) x = 0;
                        if (plainText[x] != key[i])
                        {
                            flag = true;
                            break;
                        }
                        else
                        {
                            x++;
                            i++;
                        }
                    }
                    if (flag == false) break;
                    else
                    {
                        for (int y = temp; y <= i; y++) new_key += key[y];
                        i++;
                    }

                }

            }



            return new_key;
        }

        public string Decrypt(string cipherText, string key)
        {
            cipherText = cipherText.ToLower();
            key = key.ToLower();
            char[][] charArray = buildMat();
            Dictionary<char, int> maper = map();
            string newKey = key;
            string plain = "";

            int i = 0;
            int w = 0;
            while (cipherText.Length - plain.Length != 0)
            {
                if (i < newKey.Length)
                {
                    int indx = Array.IndexOf(charArray[maper[newKey[i]]], cipherText[i]);
                    plain += maper.FirstOrDefault(x => x.Value == indx).Key;
                    i++;
                }
                else if (!(i < newKey.Length) && i < cipherText.Length && w < plain.Length)
                {
                    newKey += plain[w];
                    w++;
                }
                else if (!(w < plain.Length))
                {
                    w = 0;
                }
                else
                {
                    break;
                }

            }
            return plain;
        }

        public string Encrypt(string plainText, string key)
        {
            plainText = plainText.ToLower();
            key = key.ToLower();
            char[][] charArray = buildMat();
            Dictionary<char, int> maper = map();
            string newKey = new_key(plainText, key);
            string cipher = "";
            for (int i = 0; i < plainText.Length; i++)
            {
                int ind1 = maper[plainText[i]];
                int ind2 = maper[newKey[i]];
                cipher += charArray[ind1][ind2];

            }

            return cipher;
        }
        public string new_key(string plainText, string key)
        {
            string newKey = key;
            for (int i = 0; i < plainText.Length - key.Length; i++)
            {
                newKey += plainText[i];
            }
            return newKey;

        }

        public char[][] buildMat()
        {
            char[][] charArray = new char[26][];
            for (int i = 0; i < 26; i++)
            {
                charArray[i] = new char[26];
            }

            for (int i = 0; i < 26; i++)
            {
                char x = (char)('a' + i);

                for (int j = 0; j < 26; j++)
                {
                    charArray[i][j] = x;
                    x++;
                    if (x > 'z') x = 'a';
                }
            }

            return charArray;
        }



        public Dictionary<char, int> map()
        {
            Dictionary<char, int> maper = new Dictionary<char, int>();
            for (int i = 0; i < 26; i++)
                maper.Add((char)('a' + i), i);
            return maper;

        }
    }
}
