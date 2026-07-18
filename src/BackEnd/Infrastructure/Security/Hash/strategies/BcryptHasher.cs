using BCrypt.Net;

namespace BackEnd.Infrastructure.Security.Hash.strategies
{
    public class BcryptHasher : IHashStrategy
    {
        public string Hash(string value)
        {
            return BCrypt.Net.BCrypt.HashPassword(value);
        }
        public bool Verify(string value, string hashValue)
        {
            return BCrypt.Net.BCrypt.Verify(value, hashValue);
        }
    }
}
