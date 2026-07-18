using Konscious.Security.Cryptography;
using System;
using System.Security.Cryptography;
using System.Text;

namespace BackEnd.Infrastructure.Security.Hash.strategies
{
    public class Argon2Hasher : IHashStrategy
    {
        private const int SaltSize = 16; // 128 bits
        private const int HashSize = 32; // 256 bits
        private const int DegreeOfParallelism = 8; // Number of threads to use
        private const int Iterations = 4; // Number of iterations
        private const int MemorySize = 1024 * 1024; // 1 GB

        private byte[] Hash(string password, byte[] salt)
        {
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = salt,
                DegreeOfParallelism = DegreeOfParallelism,
                Iterations = Iterations,
                MemorySize = MemorySize
            };

            return argon2.GetBytes(HashSize);
        }

        public bool Verify(string value, string hashValue)
        {
            // Decode the stored hash
            byte[] combinedBytes = Convert.FromBase64String(hashValue);

            // Extract salt and hash
            byte[] salt = new byte[SaltSize];
            byte[] hash = new byte[HashSize];
            Array.Copy(combinedBytes, 0, salt, 0, SaltSize);
            Array.Copy(combinedBytes, SaltSize, hash, 0, HashSize);

            // Compute hash for the input password
            byte[] newHash = Hash(value, salt);

            // Compare the hashes
            return FixedTimeEquals(hash, newHash);
        }
        public static bool FixedTimeEquals(byte[] a, byte[] b)
        {
            if (a.Length != b.Length) return false;

            int result = 0;
            for (int i = 0; i < a.Length; i++)
            {
                result |= a[i] ^ b[i];
            }
            return result == 0;
        }
        public string Hash(string value)
        {
            // Generate a random salt
            byte[] salt = new byte[SaltSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Create hash
            byte[] hash = Hash(value, salt);

            // Combine salt and hash
            var combinedBytes = new byte[salt.Length + hash.Length];
            Array.Copy(salt, 0, combinedBytes, 0, salt.Length);
            Array.Copy(hash, 0, combinedBytes, salt.Length, hash.Length);

            // Convert to base64 for storage
            return Convert.ToBase64String(combinedBytes);
        }
    }
}
