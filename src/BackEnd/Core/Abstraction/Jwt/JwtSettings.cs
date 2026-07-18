namespace BackEnd.Core.Abstraction.Jwt
{
    public class JwtSettings
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public string SecretKey { get; set; }
        public TimeSpan ExpiryTime { get; set; }

        public JwtSettings()
        {
            
        }
        public JwtSettings(string audience, string issuer, string secretKey, TimeSpan expiryMinute)
        {
            Audience = audience;
            Issuer = issuer;
            SecretKey = secretKey;
            ExpiryTime = expiryMinute;
        }
    }
}
