using BackEnd.Core.Abstraction.Jwt;
using BackEnd.Core.Abstraction.Jwt.Enum;
using BackEnd.Core.Abstraction.Jwt.Interfaces;

namespace BackEnd.Infrastructure.Auth.Jwt
{
    public class JwtSettingsFactory : IJwtSettingsFactory
    {

        public JwtSettingsFactory()
        {

        }

        public IJwtServcie CreateSetting(TokenType tokenType)
        {
            var setting = tokenType switch
            {
                TokenType.PhoneNumberConfirmedToken => new JwtSettings("DangMedicalSystem", "DangMedicalSystem.Api",
                "fjwgrwe8f94u8iojjkjkljklreoifw534hfow463{563}t2wte", TimeSpan.FromMinutes(30)),

                TokenType.AuthRefreshToken => new JwtSettings("DangMedicalSystem", "DangMedicalSystem.Api",
                "fjwgrwe8f94u8iojjkjkljklreowei{32rd563}t2wte", TimeSpan.FromDays(14)),

                TokenType.AuthToken => new JwtSettings("DangMedicalSystem", "DangMedicalSystem.Api",
                "wef23234jdhfjkhhwefuhuhkjefjwiojio{8ugferh76kl}few", TimeSpan.FromMinutes(30)),
                
                _ => new JwtSettings("DangMedicalSystem", "DangMedicalSystem.Api", "fjweoiffjnuweh87nw534hfow463{563}t2wte", TimeSpan.FromDays(3))
            };
            return new JwtService(setting);
        }
    }
}