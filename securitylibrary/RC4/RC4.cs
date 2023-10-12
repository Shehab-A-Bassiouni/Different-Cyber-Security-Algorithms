using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.RC4
{

    public class RC4 : CryptographicTechnique
    {
        static string Hex_String_To_String(string hex_String)
        {
            var String_Builder = new StringBuilder();
            for (var i = 2; i < hex_String.Length; i += 2)
            {
                var hexChar = hex_String.Substring(i, 2);
                String_Builder.Append((char)Convert.ToByte(hexChar, 16));
            }
            return String_Builder.ToString();
        }

        static string String_To_Hex_String(string s)
        {
            byte[] ba = Encoding.Default.GetBytes(s);
            var Hex_String = BitConverter.ToString(ba);
            Hex_String = Hex_String.Replace("-", "");
            return "0x" + Hex_String;
        }

        public override string Decrypt(string cipherText, string key)
        {
            return Encrypt(cipherText, key);

        }

        public override string Encrypt(string plainText, string key)
        {
            bool hex = false;
            if (plainText.Substring(0, 2) == "0x")
            {
                hex = true;
                plainText = Hex_String_To_String(plainText);
            }
            if (key.Substring(0, 2) == "0x")
                key = Hex_String_To_String(key);

            int[] s = new int[256], t = new int[256];
            for (int i = 0; i < 256; i++)
            {
                s[i] = i;

                int j = (i % key.Length);
                t[i] = key[j];
            }


            for (int i = 0, j = 0; i < 256; i++)
            {
                j = (j + s[i] + t[i]) % 256;


                s[i] += s[j];
                s[j] = s[i] - s[j];
                s[i] -= s[j];
            }

            string cipherText = "";
            for (int ind = 0, i = 0, j = 0; ind < plainText.Length; ind++)
            {
                i = (i + 1) % 256;
                j = (j + s[i]) % 256;


                s[i] += s[j];
                s[j] = s[i] - s[j];
                s[i] -= s[j];

                int x = (s[i] + s[j]) % 256;
                cipherText += (char)(plainText[ind] ^ s[x]);

            }

            if (hex)
            {
                cipherText = String_To_Hex_String(cipherText);
            }

            return cipherText;
        }
    }
}

