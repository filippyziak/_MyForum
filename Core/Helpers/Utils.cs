using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using MyForum.Core.Extensions;

namespace MyForum.Core.Helpers
{
    public static class Utils
    {
        #region guid
        public static string Id() => Guid.NewGuid().ToString("N").Substring(0, 16).ToUpper();
        public static string Token(int length = 8) => Guid.NewGuid().ToString("N").Substring(0, length).ToUpper();
        public static string NewGuid(int length = 16) => Guid.NewGuid().ToString().Replace("-", "").Substring(0, length);

        #endregion

        #region hashing

        public static string GenerateHash(string password, string passwordSalt)
        {
            var passwordBinary = Encoding.UTF8.GetBytes(password);
            var passwordSaltBinary = Encoding.UTF8.GetBytes(passwordSalt);

            using (SHA512 sha512 = SHA512.Create())
            {
                var passwordHashBinary = sha512.ComputeHash(Utils.CombineByteArrays(passwordBinary, passwordSaltBinary));
                return passwordHashBinary.ConvertHashToString();
            }
        }

        public static string CreateSalt(int saltSize = 128)
        {
            var rngCryptoService = new RNGCryptoServiceProvider();

            byte[] saltBinary = new byte[saltSize];
            rngCryptoService.GetBytes(saltBinary);

            var salt = BitConverter.ToString(saltBinary).Replace("-", "");

            return salt;
        }

        private static byte[] CombineByteArrays(params byte[][] arrays)
        {
            byte[] resultArray = new byte[arrays.Sum(x => x.Length)];
            int offset = 0;

            foreach (byte[] data in arrays)
            {
                Buffer.BlockCopy(data, 0, resultArray, offset, data.Length);
                offset += data.Length;
            }
            return resultArray;
        }

        public static bool VerifyPassword(string password, string saltedPasswordHash, string passwordSalt)
        {
            string passwordHashToCheck = GenerateHash(password, passwordSalt);

            for (int i = 0; i < passwordHashToCheck.Length; i++)
                if (passwordHashToCheck[i] != saltedPasswordHash[i]) return false;

            return true;
        }
        #endregion

        #region enum
        public static string EnumToString<T>(T value) where T : struct, IConvertible
            => Enum.GetName(typeof(T), value);

        #endregion

    }
}