using System;
using System.Text;
using System.Security.Cryptography;

namespace VisaCRUD.Security
{
    public static class CryptoService
    {
        public static String GetHashCode(String input)
        {
            if (input == null)
                return null;

            String result = "";
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] arr = Encoding.UTF8.GetBytes(input);

                byte[] hashed = sha256.ComputeHash(arr);

                foreach (var item in hashed)
                    result += $"{item:X2}";
            }

            return result;
        }
    }
}
