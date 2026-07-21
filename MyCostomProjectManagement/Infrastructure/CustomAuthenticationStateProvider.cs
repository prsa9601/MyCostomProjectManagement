using BackEnd.Core.Abstraction.Jwt.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.Tokens;
using MyCostomProjectManagement.Facade.Role;
using MyCostomProjectManagement.Facade.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyCostomProjectManagement.Infrastructure
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IJwtSettingsFactory _jwtSettingsFactory;
        private readonly IUserFacade _useracade;
        private readonly IRoleFacade _roleFacade;

        public CustomAuthenticationStateProvider(IHttpContextAccessor httpContextAccessor,
            IJwtSettingsFactory jwtSettingsFactory, IUserFacade useracade, IRoleFacade roleFacade)
        {
            _httpContextAccessor = httpContextAccessor;
            _jwtSettingsFactory = jwtSettingsFactory;
            _useracade = useracade;
            _roleFacade = roleFacade;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var jwtTokenFacory = _jwtSettingsFactory.CreateSetting(BackEnd.Core.Abstraction.Jwt.Enum.TokenType.AuthToken);
            var context = _httpContextAccessor.HttpContext;
            if (context == null)
            {
                return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
            }
            ClaimsPrincipal user;
            var token = context.Request.Cookies["auth-Token"];
            if (string.IsNullOrEmpty(token))
            {
                var refreshtoken = context.Request.Cookies["refresh-Token"];
                var jwtRefreshTokenFacory = _jwtSettingsFactory.CreateSetting(BackEnd.Core.Abstraction.Jwt.Enum.TokenType.AuthRefreshToken);
                if (string.IsNullOrWhiteSpace(refreshtoken))
                {
                    return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
                }
                user = jwtRefreshTokenFacory.ValidateToken(refreshtoken);
                if (user == null)
                {

                    return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
                }
                else
                {
                    var refreShTokenClaim = ParseJwt(refreshtoken);
                    if (refreShTokenClaim == null || !refreShTokenClaim.Any())
                        return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));

                    Guid.TryParse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var id);
                    var userDto = await _useracade.GetId(id);
                    var roles = await _roleFacade.GetRolesByRoleIds(userDto.UserRoles.Select(i => i.RoleId).ToList());
                    if (userDto == null)
                    {

                    }
                    else
                    {
                        string newAuthToken = jwtTokenFacory.GenerateToken(userDto.Id, userDto.PhoneNumber,
                            roles.Select(i => i.Name.ToString()).ToList() ?? new());

                        var claimsR = ParseJwt(newAuthToken);
                        if (claimsR == null || !claimsR.Any())
                            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));

                        context.Response.Cookies.Append("auth-Token", newAuthToken);
                        var identityR = new ClaimsIdentity(claimsR, "jwt_cookie");
                        var userResultR = new ClaimsPrincipal(identityR);
                        return await Task.FromResult(new AuthenticationState(userResultR));
                    }
                }
                return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
            }
            try
            {
                jwtTokenFacory.ValidateToken(token);

            }
            catch (SecurityTokenExpiredException)
            {
                var refreshtoken = context.Request.Cookies["refresh-Token"];
                var jwtRefreshTokenFacory = _jwtSettingsFactory.CreateSetting(BackEnd.Core.Abstraction.Jwt.Enum.TokenType.AuthRefreshToken);

                if (string.IsNullOrWhiteSpace(refreshtoken))
                {
                    context.Response.Cookies.Delete("auth-Token");
                    return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
                }
                user = jwtRefreshTokenFacory.ValidateToken(refreshtoken);
                if (user == null)
                {
                    context.Response.Cookies.Delete("auth-Token");
                    context.Response.Cookies.Delete("refresh-Token");

                    return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
                }
                else
                {
                    var refreShTokenClaim = ParseJwt(refreshtoken);
                    if (refreShTokenClaim == null || !refreShTokenClaim.Any())
                        return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));

                    Guid.TryParse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var id);
                    var userDto = await _useracade.GetId(id);
                    var roles = await _roleFacade.GetRolesByRoleIds(userDto.UserRoles.Select(i=>i.RoleId).ToList());

                    if (userDto == null)
                    {

                    }
                    else
                    {
                        string newAuthToken = jwtTokenFacory.GenerateToken(userDto.Id, userDto.PhoneNumber,
                            roles.Select(i => i.Name.ToString()).ToList() ?? new());

                        var claimsR = ParseJwt(newAuthToken);
                        if (claimsR == null || !claimsR.Any())
                            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));

                        context.Response.Cookies.Append("auth-Token", newAuthToken);
                        var identityR = new ClaimsIdentity(claimsR, "jwt_cookie");
                        var userResultR = new ClaimsPrincipal(identityR);
                        return await Task.FromResult(new AuthenticationState(userResultR));
                    }
                }
            }

            var claims = ParseJwt(token);
            if (claims == null || !claims.Any())
                return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));

            var identity = new ClaimsIdentity(claims, "jwt_cookie");
            var userResult = new ClaimsPrincipal(identity);
            return await Task.FromResult(new AuthenticationState(userResult));
        }
        //public override Task<AuthenticationState> GetAuthenticationStateAsync()
        //{
        //    var context = _httpContextAccessor.HttpContext;
        //    if (context == null)
        //        return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));

        //    var token = context.Request.Cookies["auth-Token"];
        //    if (string.IsNullOrEmpty(token))
        //        return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));

        //    var claims = ParseJwt(token);
        //    if (claims == null || !claims.Any())
        //        return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));

        //    var identity = new ClaimsIdentity(claims, "jwt_cookie");
        //    var user = new ClaimsPrincipal(identity);
        //    return Task.FromResult(new AuthenticationState(user));
        //}

        private IEnumerable<Claim> ParseJwt(string jwt)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(jwt);
                return token.Claims;
            }
            catch
            {
                return null;
            }
        }

        public void NotifyUserLogin(string token)
        {
            var claims = ParseJwt(token);
            var identity = new ClaimsIdentity(claims, "jwt_cookie");
            var user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public void NotifyUserLogout()
        {
            var anonymous = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymous)));
        }
    }
}
