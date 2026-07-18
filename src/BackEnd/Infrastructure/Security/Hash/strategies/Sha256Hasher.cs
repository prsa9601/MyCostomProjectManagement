using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace BackEnd.Infrastructure.Security.Hash.strategies
{
    public class Sha256Hasher : IHashStrategy
    {
        public string Hash(string value)
        {
            using (SHA256 sha256Hash = SHA256.Create()) 
            {
                // ComputeHash - returns byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(value));

                // Convert byte array to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public bool Verify(string value, string hashValue)
        {
            string hashInputValue = Hash(value);
            return hashInputValue.Equals(hashValue);    
        }
    }
}
