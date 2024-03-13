 using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace latihan_lks6
{
    static class hash
    {

        public static string hasGenerate(String text)
        {
            using(SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(text));
                var sb = new StringBuilder(hash.Length * 2);

                foreach(byte b in hash)
                {

                    sb.Append(b.ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}
