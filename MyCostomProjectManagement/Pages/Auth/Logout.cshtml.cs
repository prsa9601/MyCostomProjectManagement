using BackEnd.Core.Abstraction.Cookies.Interfaces;
using BackEnd.Core.Abstraction.Jwt.Interfaces;
using BackEnd.Data.DB;
using BackEnd.Infrastructure.Security.Hash.service;
using BackEnd.Infrastructure.Security.Hash.strategies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyCostomProjectManagement.Infrastructure;

namespace MyCostomProjectManagement.Pages.Auth
{
    public class LogoutModel : PageModel
    {
        private readonly ICookiesService _cookiesService;
        private readonly IDbContextFactory<Context> _dbContextFactory;
        private readonly UserAuthentication _userAuthentication;
        private readonly IJwtSettingsFactory _jwtSettingsFactory;
        private readonly HashManager _hashManager;

        public LogoutModel(ICookiesService cookiesService, IDbContextFactory<Context> dbContextFactory, UserAuthentication userAuthentication, IJwtSettingsFactory jwtSettingsFactory)
        {
            _cookiesService = cookiesService;
            _dbContextFactory = dbContextFactory;
            _userAuthentication = userAuthentication;
            _jwtSettingsFactory = jwtSettingsFactory;
            _hashManager = new HashManager(new Sha256Hasher());
        }

        public async Task<IActionResult> OnGet()
        {
            var context = await _dbContextFactory.CreateDbContextAsync();
            Guid userId = await _userAuthentication.GetCurrentUserId();
            if (userId == Guid.Empty) return Redirect("/");

            var user = await context.Users.AsTracking().Include(i => i.UserSessionBlackList).FirstOrDefaultAsync(i => i.Id == userId);
            if (user == null) return Redirect("/");
            Request.Cookies.TryGetValue("refresh-Token", out string refreshToken);

            var service = _jwtSettingsFactory.CreateSetting(BackEnd.Core.Abstraction.Jwt.Enum.TokenType.AuthRefreshToken);
            var expireDate = service.GetExpireDate(refreshToken);
            var hashToken = _hashManager.Hash(refreshToken);
            user.UserSessionBlackList.Add(new BackEnd.Data.Entities.User.UserSessionBlackList
            {
                UserId = userId,
                HashToken = hashToken,
                ExpireDate = expireDate,
            });
            RemoveCookies();
            await context.SaveChangesAsync();
            return Redirect("/");
        }
        private void RemoveCookies()
        {
            _cookiesService.DeleteCookie(Response, "auth-Token");

            _cookiesService.DeleteCookie(Response, "refresh-Token");
        }
    }
}
