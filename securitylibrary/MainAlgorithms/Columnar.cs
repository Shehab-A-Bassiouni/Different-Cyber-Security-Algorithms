using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class Columnar : ICryptographicTechnique<string, List<int>>
    {
        public List<int> Analyse(string plainText, string cipherText)
        {
            SortedDictionary<int, int> sortedDictionary = new SortedDictionary<int, int>();
            plainText = plainText.ToLower();
            cipherText = cipherText.ToLower();
            double PT_tSize = plainText.Length;

            for (int w = 1; w < Int32.MaxValue; w++)
            {
                int columnar_A = 0;
                double width = w;
                double height = Math.Ceiling(PT_tSize / w); ;
                string[,] matrix_A = new string[(int)height, (int)width];
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < w; j++)
                    {
                        if (columnar_A >= PT_tSize)
                        {
                            matrix_A[i, j] = "";
                        }
                        else
                        {
                            matrix_A[i, j] = plainText[columnar_A].ToString();

                            columnar_A++;
                        }
                    }
                }
                // taking col by col bntl3 l7rof nlz2ha n3ml string w ndkhlha f list
                //bashof hal l words mwgod f CT? yes?
                // right key and get order.. 
                //no? loop again for new key
                List<string> ARR = new List<string>();
                for (int i = 0; i < w; i++)
                {
                    string New_word = "";
                    for (int j = 0; j < height; j++)
                    {
                        New_word += matrix_A[j, i];
                    }
                    ARR.Add(New_word);
                }

                if (ARR.Count == 7)
                {
                    string s_1 = "";
                }

                bool New_Key_ = true;
                string Copy_CT = (string)cipherText.Clone();
                //map x makano fl cipher text m3 l col index 
                sortedDictionary = new SortedDictionary<int, int>();
                for (int i = 0; i < ARR.Count; i++)
                {
                    //get index of first substring occurance
                    int x1 = Copy_CT.IndexOf(ARR[i]);
                    if (x1 == -1)
                    {
                        New_Key_ = false;
                    }
                    else
                    {
                        sortedDictionary.Add(x1, i + 1);
                        Copy_CT.Replace(ARR[i], "$");
                    }

                }
                if (New_Key_)
                    break;

            }
            List<int> Final_key = new List<int>();
            Dictionary<int, int> newDictionary = new Dictionary<int, int>();

            //seprate string in col..
            //find in cipher if cipher contains all this string,,
            // then thats the key 

            for (int l = 0; l < sortedDictionary.Count; l++)
            {
                newDictionary.Add(sortedDictionary.ElementAt(l).Value, l + 1);
            }

            for (int k = 1; k < newDictionary.Count + 1; k++)
            {
                Final_key.Add(newDictionary[k]);
            }
            return Final_key;
        }

        public string Decrypt(string cipherText, List<int> key)
        {
            double width_d = key.Count;
            double C_TtSize = cipherText.Length;
            double height_d = Math.Ceiling(C_TtSize / width_d);
            int columnar_D = 0;
            char[,] matrix_d = new char[(int)height_d, (int)width_d];
            // TRIAL filling the matrix
            Dictionary<int, int> keymatrix = new Dictionary<int, int>();
            for (int i = 0; i < key.Count; i++)
            {
                keymatrix.Add(key[i] - 1, i);

            }
            int num_FullColumns = cipherText.Length % key.Count;
            for (int i = 0; i < key.Count; i++)
            {
                for (int k = 0; k < height_d; k++)
                {
                    if (num_FullColumns != 0 && keymatrix[i] >= num_FullColumns && k == height_d - 1)
                        continue;
                    matrix_d[k, keymatrix[i]] = cipherText[columnar_D];
                    columnar_D++;
                }

            }
            StringBuilder Build_ = new StringBuilder();

            for (int i = 0; i < height_d; i++)
            {
                for (int j = 0; j < width_d; j++)
                {
                    Build_.Append(matrix_d[i, j]);
                }
            }
            string P_T = Build_.ToString();
            return P_T.ToUpper();
        }

        public string Encrypt(string plainText, List<int> key)
        {
            double PT_Size = plainText.Length;
            double w_ = key.Count;
            double h_ = PT_Size / w_;
            double x = PT_Size / w_;
            //for rounding
            h_ = Math.Ceiling(x);

            char[,] matrix_e = new char[(int)h_, (int)w_];
            string C_T = "";
            int columnar = 0;
            for (int i = 0; i < h_; i++)
            {
                for (int j = 0; j < w_; j++)
                {
                    if (columnar < PT_Size)
                    {
                        matrix_e[i, j] = plainText[columnar];
                        columnar++;
                    }
                    else
                    {
                        matrix_e[i, j] = 'x';
                    }
                }
            }
            //filling the matrix
            Dictionary<int, int> keyMatrix = new Dictionary<int, int>();
            for (int i = 0; i < key.Count; i++)
            {
                keyMatrix.Add(key[i] - 1, i);
            }
            for (int i = 0; i < key.Count; i++)
            {
                for (int j = 0; j < h_; j++)
                {
                    C_T += matrix_e[j, keyMatrix[i]];
                }
            }
            return C_T.ToUpper();
        }
    }
}
