using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DesafioAPI.Utils
{
    public static class Cryptography
    {
        public static string GeneratePasswordMD5(string password)
        {
            MD5 md5 = MD5.Create();

            byte[] senhaCrip = md5.ComputeHash(Encoding.Default.GetBytes(password));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < senhaCrip.Length; i++)
            {
                builder.Append(senhaCrip[i].ToString("x2"));
            }

            return builder.ToString();
        }
    }
}
