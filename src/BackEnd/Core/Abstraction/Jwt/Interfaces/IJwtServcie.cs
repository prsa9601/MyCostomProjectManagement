using System.Security.Claims;

namespace BackEnd.Core.Abstraction.Jwt.Interfaces
{
    public interface IJwtServcie
    {
        string GenerateToken(Guid? userId, string phonNumber, List<string>? roles);
        ClaimsPrincipal ValidateToken(string token);
        DateTime GetExpireDate(string token);
    }
}
