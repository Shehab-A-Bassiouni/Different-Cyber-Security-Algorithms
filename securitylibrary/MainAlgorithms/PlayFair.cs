using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class PlayFair : ICryptographic_Technique<string, string>
    {
        public string Decrypt(string cipherText, string key)
        {
            string mainPlain = cipherText.ToLower();

            mainPlain = mainPlain.Replace('j', 'i');
            string mainKey = key.ToLower();
            mainKey = mainKey.Replace('j', 'i');

            Stack<char> myStack = new Stack<char>();
            foreach (char x in mainKey.Reverse())
            {
                myStack.Push(x);
            }

            char[,] array = new char[5, 5];
            IDictionary<char, bool> used = new Dictionary<char, bool>();
            for (char i = 'a'; i <= 'z'; i++)
                used.Add(i, false);
            used['j'] = true;


            for (int i = 0; i < 5; i++)
            {
                for (int y = 0; y < 5; y++)
                {
                    if (myStack.Count != 0)
                    {
                        bool addedOrNot = false;
                        while (myStack.Count != 0)
                        {
                            char temp = myStack.Pop();

                            if (used[temp] == false)
                            {
                                array[i, y] = temp;
                                used[temp] = true;
                                addedOrNot = true;
                                break;
                            }
                        }

                        if (!addedOrNot)
                        {
                            array[i, y] = usedOrNot(ref used);
                        }

                    }
                    else
                    {
                        array[i, y] = usedOrNot(ref used);
                    }

                }
            }



            IDictionary<char, KeyValuePair<int, int>> dimen = dims(array);


            string res = denc(array, mainPlain, dimen);
            if (res.Length % 2 == 0 && res[res.Length - 1] == 'x') res = res.Remove(res.Length - 1, 1);

            string res_new = "";

            for (int i = 0; i < res.Length; i++)
            {
                if (res[i] == 'x' && i > 0 && i < res.Length - 1 && res[i - 1] == res[i + 1] && i % 2 != 0)
                    continue;
                else
                    res_new += res[i];

            }




            return res_new;
        }

        public string Encrypt(string plainText, string key)
        {
            string mainPlain = plainText.ToLower();
            mainPlain = mainPlain.Replace('j', 'i');
            mainPlain = dub(mainPlain);
            string mainKey = key.ToLower();
            mainKey = mainKey.Replace('j', 'i');

            if (mainPlain.Length % 2 != 0) mainPlain += 'x';
            Stack<char> myStack = new Stack<char>();
            foreach (char x in mainKey.Reverse())
            {
                myStack.Push(x);
            }

            char[,] array = new char[5, 5];
            IDictionary<char, bool> used = new Dictionary<char, bool>();
            for (char i = 'a'; i <= 'z'; i++)
                used.Add(i, false);
            used['j'] = true;


            for (int i = 0; i < 5; i++)
            {
                for (int y = 0; y < 5; y++)
                {
                    if (myStack.Count != 0)
                    {
                        bool addedOrNot = false;
                        while (myStack.Count != 0)
                        {
                            char temp = myStack.Pop();

                            if (used[temp] == false)
                            {
                                array[i, y] = temp;
                                used[temp] = true;
                                addedOrNot = true;
                                break;
                            }
                        }

                        if (!addedOrNot)
                        {
                            array[i, y] = usedOrNot(ref used);
                        }

                    }
                    else
                    {
                        array[i, y] = usedOrNot(ref used);
                    }

                }
            }



            IDictionary<char, KeyValuePair<int, int>> dimen = dims(array);
            string res = enc(array, mainPlain, dimen);
            return res;
        }

        static string denc(char[,] array, string mainPlain, IDictionary<char, KeyValuePair<int, int>> dimen)
        {
            string res = "";
            int i = 0;
            while (res.Length < mainPlain.Length)
            {

                char c1 = mainPlain[i];
                char c2 = mainPlain[i + 1];

                int c1_x = dimen[c1].Key;
                int c1_y = dimen[c1].Value;
                int c2_x = dimen[c2].Key;
                int c2_y = dimen[c2].Value;
                i += 2;
                //---------------------------------------same row and same col
                if (c1_x == c2_x)
                {
                    if (c1_y != 0)
                    {
                        res += array[c1_x, c1_y - 1];
                    }
                    else
                    {
                        res += array[c1_x, 4];
                    }
                    if (c2_y != 0)
                    {
                        res += array[c2_x, c2_y - 1];
                    }
                    else
                    {
                        res += array[c2_x, 4];
                    }

                }

                else if (c1_y == c2_y)
                {
                    if (c1_x != 0)
                    {
                        res += array[c1_x - 1, c1_y];
                    }
                    else
                    {
                        res += array[4, c1_y];
                    }
                    if (c2_x != 0)
                    {
                        res += array[c2_x - 1, c2_y];
                    }
                    else
                    {
                        res += array[4, c2_y];
                    }
                }
                //----------------------------------------------------------------
                else
                {
                    res += array[c1_x, c2_y];
                    res += array[c2_x, c1_y];


                }
            }
            return res;
        }
        static string enc(char[,] array, string mainPlain, IDictionary<char, KeyValuePair<int, int>> dimen)
        {
            string res = "";
            int i = 0;
            while (res.Length < mainPlain.Length)
            {
                char c1 = mainPlain[i];
                char c2 = mainPlain[i + 1];
                int c1_x = dimen[c1].Key;
                int c1_y = dimen[c1].Value;
                int c2_x = dimen[c2].Key;
                int c2_y = dimen[c2].Value;
                i += 2;
                //---------------------------------------same row and same col

                if (c1_x == c2_x)
                {
                    if (c1_y != 4)
                    {
                        res += array[c1_x, c1_y + 1];
                    }
                    else
                    {
                        res += array[c1_x, 0];
                    }
                    if (c2_y != 4)
                    {
                        res += array[c2_x, c2_y + 1];
                    }
                    else
                    {
                        res += array[c2_x, 0];
                    }

                }

                else if (c1_y == c2_y)
                {
                    if (c1_x != 4)
                    {
                        res += array[c1_x + 1, c1_y];
                    }
                    else
                    {
                        res += array[0, c1_y];
                    }
                    if (c2_x != 4)
                    {
                        res += array[c2_x + 1, c2_y];
                    }
                    else
                    {
                        res += array[0, c2_y];
                    }
                }
                //----------------------------------------------------------------
                else
                {
                    res += array[c1_x, c2_y];
                    res += array[c2_x, c1_y];


                }
            }
            return res;
        }
        static IDictionary<char, KeyValuePair<int, int>> dims(char[,] array)
        {
            IDictionary<char, KeyValuePair<int, int>> dims = new Dictionary<char, KeyValuePair<int, int>>();
            for (int i = 0; i < 5; i++)
            {
                for (int y = 0; y < 5; y++)
                {
                    dims.Add(array[i, y], new KeyValuePair<int, int>(i, y));
                }
            }
            return dims;
        }
        static char usedOrNot(ref IDictionary<char, bool> used)
        {
            foreach (var x in used)
            {
                if (x.Value == false)
                {
                    used[x.Key] = true;
                    return x.Key;

                }

            }
            return 'x';
        }
        static string dub(string txt)
        {
            string ret = "";
            int i = 0;
            while (i != txt.Length)
            {
                if (txt[i] == txt[i + 1])
                {
                    ret = ret + txt[i] + 'x';
                    i++;
                }
                else
                {
                    ret = ret + txt[i] + txt[i + 1];
                    i += 2;
                }

                if (i == txt.Length - 1)
                {
                    ret += txt[i];
                    ret += 'x';
                    break;
                }
            }
            return ret;

        }
    }
}
