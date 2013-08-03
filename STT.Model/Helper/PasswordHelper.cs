using System;
using System.Security.Cryptography;
using System.Text;

namespace STT.Model.Helper
{
    internal static class PasswordHelper
    {
        public static string GenerateSalt()
        {
            var buffer = new byte[16];
            var provider = new RNGCryptoServiceProvider();
            provider.GetBytes(buffer);
            return Convert.ToBase64String(buffer);
        }

        public static string EncodePassword(string password, string salt)
        {
            var bytes = Encoding.Unicode.GetBytes(password);
            var src = Convert.FromBase64String(salt);
            var dst = new byte[src.Length + bytes.Length];

            Buffer.BlockCopy(src, 0, dst, 0, src.Length);
            Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);
            var algorithm = HashAlgorithm.Create("SHA1");
            if (algorithm == null)
                throw new InvalidOperationException("Cannot create SHA1 algorithm.");

            var inArray = algorithm.ComputeHash(dst);
            return Convert.ToBase64String(inArray);
        }
    }
}
