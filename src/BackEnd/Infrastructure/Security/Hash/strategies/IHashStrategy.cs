namespace BackEnd.Infrastructure.Security.Hash.strategies
{
    public interface IHashStrategy
    {
        string Hash(string value); 
        bool Verify(string value, string hashValue); 
    }
}
