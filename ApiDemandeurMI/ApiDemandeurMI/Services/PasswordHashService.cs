using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace ApiDemandeurMI.Services
{
    public static class PasswordHashService
    {
        private const int SaltSize = 128 / 8;
        private const int KeySize = 256 / 8;
        private const int Iteration = 10000;
        private static readonly HashAlgorithmName _hashAlgorithmName = HashAlgorithmName.SHA256;
        private static char Delimiter = ';';

        public static string GenerateHash(String password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iteration, _hashAlgorithmName, KeySize);

            return string.Join(Delimiter, Convert.ToBase64String(salt), Convert.ToBase64String(hash));
        }

        public static bool CompareHash(string password , string inputPasword)
        {
            string[] elements = password.Split(Delimiter);
            byte[] salt = Convert.FromBase64String(elements[0]);
            byte[] hash = Convert.FromBase64String(elements[1]);

            byte[] hashInput = Rfc2898DeriveBytes.Pbkdf2(inputPasword, salt, Iteration, _hashAlgorithmName, KeySize);
            return CryptographicOperations.FixedTimeEquals(hash, hashInput); 
        }
    }
}
