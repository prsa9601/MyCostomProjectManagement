using BackEnd.Infrastructure.Security.Hash.strategies;

namespace BackEnd.Infrastructure.Security.Hash.service
{
    public class HashManager
    {
        private readonly IHashStrategy _hashStrategy;

        public HashManager(IHashStrategy hashStrategy)
        {
            _hashStrategy = hashStrategy;
        }
        public string Hash(string value)
        {
            return _hashStrategy.Hash(value);
        }
    }
}
