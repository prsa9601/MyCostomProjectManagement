using BackEnd.Core.Abstraction.Cookies.Interfaces;
using BackEnd.Core.User.Commands.Login;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyCostomProjectManagement.Facade.User;
using System.ClientModel.Primitives;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyCostomProjectManagement.Pages.Auth
{
    public class LoginModel : PageModel
    {
        private readonly ICookiesService _cookiesService;
        private readonly IUserFacade _userFacade;

        public LoginModel(ICookiesService cookiesService, IUserFacade userFacade)
        {
            _cookiesService = cookiesService;
            _userFacade = userFacade;
        }

        public async Task<IActionResult> OnGet(string phoneNumber)
        {
            var result = await Login(phoneNumber);
            if (result.Status == BackEnd.Shared.CoreShared.OperationResultStatus.Success)
            {
                SetCookies(result.Data.AuthToken, result.Data.RefreshToken);
            }
            return Redirect("/");
        }
        private async Task<BackEnd.Shared.CoreShared.OperationResult<LoginUserCommandResponse>> Login(string phoneNumber)
        {
            // ذخیره‌سازی فرضی (در پروژه واقعی، اینجا با API ارتباط برقرار کنید)
            return await _userFacade.Login(new BackEnd.Core.User.Commands.Login.LoginUserCommand
            {
                PhoneNumber = phoneNumber,
            });
           
        }

        private void SetCookies(string authToken, string refreshToken)
        {
            _cookiesService.SetCookie(Response, "auth-Token", authToken, new CookieOptions
            {
                HttpOnly = true,
                //Secure = true,
                Secure = false,
                SameSite = SameSiteMode.Strict,
                MaxAge = TimeSpan.FromMinutes(30),
                Path = "/",
            });
           
            _cookiesService.SetCookie(Response, "refresh-Token", refreshToken, new CookieOptions
            {
                HttpOnly = true,
                //Secure = true,
                Secure = false,
                SameSite = SameSiteMode.Strict,
                MaxAge = TimeSpan.FromDays(30),
                Path = "/",
            });
        }
    }
}
