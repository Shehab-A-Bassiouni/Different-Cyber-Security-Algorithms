using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    /// <summary>
    /// The List<int> is row based. Which means that the key is given in row based manner.
    /// </summary>
    public class HillCipher : ICryptographicTechnique<List<int>, List<int>>
    {
        public List<int> Analyse(List<int> plainText, List<int> cipherText)
        {


            if (plainText.Count() < 6)
            {
                throw new InvalidAnlysisException();
            }


            List<int> keys = new List<int>();

            int count = 0;

            for (int i = 0; i < 26; i++)
            {
                for (int j = 0; j < 26; j++)
                {
                    for (int k = 0; k < 26; k++)
                    {

                        for (int l = 0; l < 26; l++)
                        {

                            if ((plainText[0] * i + plainText[1] * k) % 26 == cipherText[0] && (plainText[2] * i + plainText[3] * k) % 26 == cipherText[2] && (plainText[4] * i + plainText[5] * k) % 26 == cipherText[4])
                            {
                                if ((plainText[0] * j + plainText[1] * l) % 26 == cipherText[1] && (plainText[2] * j + plainText[3] * l) % 26 == cipherText[3] && (plainText[4] * j + plainText[5] * l) % 26 == cipherText[5])
                                {
                                    count++;

                                    keys.Add(i);
                                    keys.Add(k);
                                    keys.Add(j);
                                    keys.Add(l);
                                }




                            }






                        }


                    }
                }
            }



            return keys;
        }
        //---------------------------------------------------------------------------------------------------->
        int determ(int size, int[,] k)
        {
            int sum = 0;
            if (size == 2)
            {
                int value;

                value = k[0, 0] * k[1, 1] - k[1, 0] * k[0, 1];
                return value;
            }
            int size2 = size - 1;
            int[,] newK = new int[size2, size2];
            for (int i = 0; i < size; i++)
            {

                //-------------------------------------------------------------------------------------


                for (int ii = 1; ii < size; ii++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        if (j < i)
                        {
                            newK[ii - 1, j] = k[ii, j];
                        }
                        else if (j > i)
                        {
                            newK[ii - 1, j - 1] = k[ii, j];
                        }
                    }
                }

                for (int ii = 0; ii < size2; ii++)
                {
                    for (int j = 0; j < size2; j++)
                    {

                    }
                }
                //--------------------------------------------------------








                if (i % 2 == 0)
                { sum += determ(size2, newK) * k[0, i]; }
                else
                {
                    sum -= determ(size2, newK) * k[0, i];
                }




            }

            return sum;

        }
        int find_b(int determ)
        {
            while (determ < 0)
            {
                determ += 26;
            }

            for (int i = 0; i < 26; i++)
            {

                if ((i * determ) % 26 == 1)
                {
                    return i;
                }
            }




            return -100;
        }


        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public List<int> Decrypt(List<int> cipherText, List<int> key)
        {





            int size = (int)Math.Sqrt(key.Count());
            int[,] keyM = new int[size, size];
            int count = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    keyM[i, j] = key[count];
                    if (key[count] < 0)
                    {
                        throw new System.Exception();
                    }
                    count++;
                }
            }

            int determinent = determ(size, keyM) % 26;

            if (determinent == 0)
            {
                throw new System.Exception();
            }

            for (int i = 2; i < 26; i++)
            {
                if (determinent % i == 0 && 26 % i == 0)
                {
                    throw new System.Exception();
                }
            }

            int b = find_b(determinent);

            if (b == -100)
            {
                throw new System.Exception();
            }

            int[,] newkeyM = new int[size, size];
            int[,] subM = new int[size - 1, size - 1];
            int result = 0;
            for (int i = 0; i < size; i++)
            {



                for (int j = 0; j < size; j++)
                {


                    for (int ii = 0; ii < size; ii++)
                    {
                        if (ii < i)
                        {
                            for (int jj = 0; jj < size; jj++)
                            {
                                if (jj < j)
                                {
                                    subM[ii, jj] = keyM[ii, jj];
                                }
                                if (jj > j)
                                {
                                    subM[ii, jj - 1] = keyM[ii, jj];
                                }

                            }
                        }
                        if (ii > i)
                        {
                            for (int jj = 0; jj < size; jj++)
                            {
                                if (jj < j)
                                {
                                    subM[ii - 1, jj] = keyM[ii, jj];
                                }
                                if (jj > j)
                                {
                                    subM[ii - 1, jj - 1] = keyM[ii, jj];
                                }

                            }
                        }
                    }

                    if (size > 2)
                    {
                        result = b * (int)Math.Pow(-1, i + j) * determ(size - 1, subM) % 26;
                    }
                    else
                    {
                        result = b * (int)Math.Pow(-1, i + j) * subM[0, 0] % 26;
                    }



                    while (result < 0)
                    {
                        result += 26;
                    }

                    newkeyM[i, j] = result;


                }
            }
            int[,] transposednewkeyM = new int[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    transposednewkeyM[i, j] = newkeyM[j, i];
                }
            }
            int size2 = cipherText.Count();
            List<int> plaintext = new List<int>();
            count = 0;
            for (int i = 0; i < size2; i += size)
            {


                for (int j = 0; j < size; j++)
                {

                    for (int k = 0; k < size; k++)
                    {
                        count += cipherText[i + k] * transposednewkeyM[j, k];
                    }
                    plaintext.Add(count % 26);
                    count = 0;
                }

            }
            return plaintext;
        }

        //---------------------------------------------------------------------------------------------------->
        public List<int> Encrypt(List<int> plainText, List<int> key)
        {

            int size = (int)Math.Sqrt(key.Count());


            int[,] keyM = new int[size, size];
            int count = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    keyM[i, j] = key[count];
                    count++;
                }
            }




            int size2 = plainText.Count();

            List<int> cipher = new List<int>();
            count = 0;
            for (int i = 0; i < size2; i += size)
            {


                for (int j = 0; j < size; j++)
                {

                    for (int k = 0; k < size; k++)
                    {
                        count += plainText[i + k] * keyM[j, k];
                    }
                    cipher.Add(count % 26);
                    count = 0;
                }

            }


            return cipher;
        }

        //---------------------------------------------------------------------------------------------------->
        public List<int> Analyse3By3Key(List<int> plainText, List<int> cipherText)
        {
            throw new NotImplementedException();
        }

    }
}
