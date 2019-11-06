using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace TripERP.Common
{
    class HashGenerate
    {
        public static string GenerateMySQLHash(string key)
        {
            byte[] keyArray = Encoding.UTF8.GetBytes(key);

            // SHA1Managed enc = new SHA1Managed();
            SHA256 enc = SHA256.Create();

            byte[] encodedKey = enc.ComputeHash(keyArray);            

            StringBuilder myBuilder = new StringBuilder(encodedKey.Length);

            foreach (byte b in encodedKey)

                myBuilder.Append(b.ToString("X"));

            return "*" + myBuilder.ToString();
        }

    }
}
