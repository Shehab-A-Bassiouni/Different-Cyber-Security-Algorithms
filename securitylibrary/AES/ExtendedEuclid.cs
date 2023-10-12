using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.AES
{
    public class ExtendedEuclid
    {
        //Get Multiplicative Inverse
        public int GetMultiplicativeInverse(int number, int baseN)
        {
            int n = number;
            int b = baseN;
            int x1 = 1, x2 = 0, x3 = b;
            int y1 = 0, y2 = 1, y3 = n;

            while (y3 != 0 && y3 != 1)
            {
                int z = x3 / y3;
                int t1 = x1 - (z * y1);
                int t2 = x2 - (z * y2);
                int t3 = x3 - (z * y3);

                x1 = y1; x2 = y2; x3 = y3;
                y1 = t1; y2 = t2; y3 = t3;
            }

            if (y3 == 0)
            {
                return -1;
            }
            else if (y3 == 1)
            {
                if (y2 < -1)
                    y2 = y2 + baseN;
                return y2;
            }
            return -1;
        }
    }
}