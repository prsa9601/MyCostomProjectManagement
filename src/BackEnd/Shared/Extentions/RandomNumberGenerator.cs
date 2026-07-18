using NanoidDotNet;

namespace BackEnd.Shared.Extentions
{
    public static class RandomNumberGenerator
    {
        private const string allowCharakter = "0123456789";

        public static async Task<string> GenerateAsync(int lenth = 6)
        {
            return await Nanoid.GenerateAsync(allowCharakter, lenth);
        }
    }
}
