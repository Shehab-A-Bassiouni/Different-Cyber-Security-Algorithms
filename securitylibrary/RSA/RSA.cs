using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.RSA
{
    public class RSA
    {
        public long Fast_Power(long B, long exp, long mod)
        {
            if (exp == 0)
                return 1;
            long r = Fast_Power(B, exp / 2, mod);
            r = (r * r) % mod;
            if (exp % 2 != 0)
                r = (r * (B % mod)) % mod;
            return r;
        }
        public long Multiplicative_Inverse(long num, long B)
        {
            List<long> l = new List<long>() {
                -1, 0, 0, 0,
            };
            List<long> n = new List<long>() {
                -1, 0, 1, num,
            };
            List<long> b = new List<long>() {
                -1, 1, 0, B,
            };
            while (true)
            {
                if (n[3] == 0)
                    return -1;
                else if (n[3] == 1)
                    return ((n[2] % B) + B) % B;
                long q = b[3] / n[3];
                l[1] = b[1] - (q * n[1]);
                l[2] = b[2] - (q * n[2]);
                l[3] = b[3] - (q * n[3]);
                b[1] = n[1]; b[2] = n[2]; b[3] = n[3];
                n[1] = l[1]; n[2] = l[2]; n[3] = l[3];
            }
        }
        public int Encrypt(int p, int q, int M, int e)
        {
            long n = p * q;
            int Enc_Num = (int)Fast_Power(M, e, n);
            return Enc_Num;
        }

        public int Decrypt(int p, int q, int C, int e)
        {
            long d = Multiplicative_Inverse(e, (p - 1) * (q - 1));
            int Dec_Num = (int)Fast_Power(C, d, p * q);
            return Dec_Num;
        }
    }
}

