using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.DiffieHellman
{
    public class DiffieHellman
    {
        public long Fast_Power(long B, long e, long mod)
        {
            if (e == 0)
                return 1;
            long r = Fast_Power(B, e / 2, mod);
            r = (r * r) % mod;
            if (e % 2 != 0)
                r = (r * (B % mod)) % mod;
            return r;
        }
        public List<int> Get_Keys(int q, int a, int xa, int xb)
        {
            long ya = Fast_Power(a, xa, q);
            long yb = Fast_Power(a, xb, q);
            List<int> K = new List<int>{

                (int)Fast_Power(ya, xb, q),
                (int)Fast_Power(yb, xa, q)
            };
            return K;
        }
    }
}
