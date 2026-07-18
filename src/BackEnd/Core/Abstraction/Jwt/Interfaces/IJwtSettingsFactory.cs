using BackEnd.Core.Abstraction.Jwt.Enum;

namespace BackEnd.Core.Abstraction.Jwt.Interfaces
{
    public interface IJwtSettingsFactory
    {
        IJwtServcie CreateSetting(TokenType tokenType);
    }
}
