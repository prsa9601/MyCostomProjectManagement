using BackEnd.Core.Abstraction.Jwt;
using BackEnd.Core.Abstraction.Jwt.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BackEnd.Infrastructure.Auth.Jwt
{
    public class JwtService : IJwtServcie
    {
        private readonly JwtSettings _jwtSettings;

        public JwtService(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }

        public string GenerateToken(Guid? userId, string phoneNumber, List<string>? roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.PhoneNumberVerified, phoneNumber),
           
                //new Claim(JwtRegisteredClaimNames.FamilyName),
            };
            if (userId != null)
                claims.Add(new Claim(ClaimTypes.NameIdentifier, userId.ToString()!));

            if (roles?.Count() > 0)
                claims.Add(new Claim(ClaimTypes.Role, string.Join("-", roles))!);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.Add(_jwtSettings.ExpiryTime),
                signingCredentials: credential
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public ClaimsPrincipal ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = _jwtSettings.Issuer,
                ValidateAudience = true,
                ValidAudience = _jwtSettings.Audience,
                ValidateLifetime = true,
                //ClockSkew = TimeSpan.Zero,
                ClockSkew = TimeSpan.FromMinutes(1),
            };
            //var user = tokenHandler.ValidateToken(token, validationParameters, out _);

            //return user;
            //return tokenHandler.ValidateToken(token, validationParameters, out _);
            try
            {
                var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

                // بررسی اضافی برای اطمینان
                if (validatedToken.ValidTo < DateTime.UtcNow)
                {
                    throw new SecurityTokenExpiredException("Token has expired");
                }

                return principal;
            }
            catch (SecurityTokenExpiredException)
            {
                // توکن منقضی شده
                //throw new Exception("Token Expired");
                return default;
            }
            catch (SecurityTokenInvalidSignatureException)
            {
                // امضا نامعتبر
                //throw new Exception("SignInKey Is Not Valid");
                return default;
            }
            catch (Exception ex)
            {
                // سایر خطاها
                //throw new SecurityTokenException("Invalid token", ex);\
                return default;
            }
        }

        public DateTime GetExpireDate(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            // خواندن توکن بدون اعتبارسنجی
            var jwtToken = tokenHandler.ReadJwtToken(token);

            // دریافت زمان انقضا
            var expiration = jwtToken.ValidTo;

            return expiration;
        }
    }
}
//configuration["JwtConfig:Audience"],