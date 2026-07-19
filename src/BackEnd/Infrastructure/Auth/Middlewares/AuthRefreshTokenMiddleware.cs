using Azure.Core;
using BackEnd.Core.Abstraction.Cookies.Interfaces;
using BackEnd.Core.Abstraction.Jwt.Enum;
using BackEnd.Core.Abstraction.Jwt.Interfaces;
using BackEnd.Data.DB;
using BackEnd.Data.Entities.User;
using BackEnd.Infrastructure.Security.Hash.service;
using BackEnd.Infrastructure.Security.Hash.strategies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace BackEnd.Infrastructure.Auth.Middlewares
{
    public class AuthRefreshTokenMiddleware
    {
        private readonly RequestDelegate _next;
        //private readonly ILogger<AuthRefreshTokenMiddleware> _logger;
        //private readonly ICookiesService _cokkieService;
        //private readonly IJwtSettingsFactory _jwtSettingsFactory;
        //private readonly HashManager _hashManager;

        private const string authTokenCookieKey = "auth-Token";
        private const string refreshTokenCookieKey = "refresh-Token";

        public AuthRefreshTokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context, Context _context,
            ICookiesService _cokkieService, IJwtSettingsFactory _jwtSettingsFactory,
            HashManager _hashManager)
        {
            //_logger.LogInformation($"Request URL: {Microsoft.AspNetCore.Http.Extensions.UriHelper.GetDisplayUrl(context.Request)}");
            _hashManager = new HashManager(new Sha256Hasher());
            string authTokenFromCookie = _cokkieService.GetCookie(context.Request, authTokenCookieKey);
            string refreshTokenFromCookie = _cokkieService.GetCookie(context.Request, refreshTokenCookieKey);
            var authTokenService = _jwtSettingsFactory.CreateSetting(TokenType.AuthToken);
            var refreshTokenService = _jwtSettingsFactory.CreateSetting(TokenType.AuthRefreshToken);

            if (!string.IsNullOrWhiteSpace(authTokenFromCookie))
            {
                var authResult = CheckToken(TokenType.AuthToken, authTokenFromCookie, _context, _hashManager,
                    _jwtSettingsFactory);
                if (authResult.isSuccess == false)
                {
                    _cokkieService.DeleteCookie(context.Response, refreshTokenCookieKey);
                    if (!string.IsNullOrWhiteSpace(refreshTokenCookieKey))
                    {
                        var result = CheckToken(TokenType.AuthRefreshToken, refreshTokenFromCookie, _context,
                            _hashManager, _jwtSettingsFactory);
                        if (result.isSuccess == false)
                        {
                            _cokkieService.DeleteCookie(context.Response, refreshTokenCookieKey);
                        }
                        if (result.claim == null) 
                        {
                            _cokkieService.DeleteCookie(context.Response, authTokenCookieKey);
                            _cokkieService.DeleteCookie(context.Response, refreshTokenCookieKey);
                            await _next(context);
                            return;
                        }
                        Guid userId = Guid.TryParse(result.claim.FindFirst(ClaimTypes.NameIdentifier).Value, out var id) ? id : Guid.Empty;
                        var user = await _context.Users.Include(i => i.UserRoles).FirstOrDefaultAsync(i => i.Id == userId);
                        if (user == null)
                        {
                            _cokkieService.DeleteCookie(context.Response, authTokenCookieKey);
                            _cokkieService.DeleteCookie(context.Response, refreshTokenCookieKey);
                            await _next(context);
                            return;
                        }

                        string newAuthToken = authTokenService.GenerateToken(user.Id, user.PhoneNumber, null);
                        _cokkieService.DeleteCookie(context.Response, authTokenCookieKey);
                        _cokkieService.SetCookie(context.Response, authTokenCookieKey, newAuthToken, new CookieOptions
                        {
                            HttpOnly = true,
                            Secure = true,
                            SameSite = SameSiteMode.Strict,
                            MaxAge = TimeSpan.FromMinutes(30),
                            Path = "/",
                        });
                        await _next(context);
                        return;
                    }
                    
                }
            }

            if (!string.IsNullOrWhiteSpace(refreshTokenFromCookie))
            {
                var result = CheckToken(TokenType.AuthRefreshToken, refreshTokenFromCookie, _context, _hashManager,
                    _jwtSettingsFactory);
                if (result.isSuccess == false)
                {
                    _cokkieService.DeleteCookie(context.Response, authTokenCookieKey);
                    _cokkieService.DeleteCookie(context.Response, refreshTokenCookieKey);
                    await _next(context);
                    return;
                }
                Guid userId = Guid.TryParse(result.claim.FindFirst(ClaimTypes.NameIdentifier).Value, out var id) ? id : Guid.Empty;
                var user = await _context.Users.Include(i => i.UserRoles).FirstOrDefaultAsync(i => i.Id == userId);
                if (user == null)
                {
                    _cokkieService.DeleteCookie(context.Response, authTokenCookieKey);
                    _cokkieService.DeleteCookie(context.Response, refreshTokenCookieKey);
                    await _next(context);
                    return;
                }

                string newAuthToken = authTokenService.GenerateToken(user.Id, user.PhoneNumber, null);
                _cokkieService.DeleteCookie(context.Response, authTokenCookieKey);
                _cokkieService.SetCookie(context.Response, authTokenCookieKey, newAuthToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    MaxAge = TimeSpan.FromMinutes(30),
                    Path = "/",
                });

            }

            await _next(context);
        }
        private (string message, bool isSuccess, ClaimsPrincipal claim) CheckToken(
            Core.Abstraction.Jwt.Enum.TokenType tokenType, string token, Context _context, HashManager _hashManager,
            IJwtSettingsFactory _jwtSettingsFactory)
        {
            var service = _jwtSettingsFactory.CreateSetting(tokenType);
            var validateResult = service.ValidateToken(token);
            if (validateResult == null)
            {
                return ($"", false, null);
            }
            //Guid userId = Guid.TryParse(validateResult.FindFirst(ClaimTypes.NameIdentifier));
            var userPrincipal = validateResult.FindFirst(ClaimTypes.NameIdentifier).Value;
            Guid userId = Guid.TryParse(userPrincipal, out var guid) ? guid : Guid.Empty;
            if (userId == Guid.Empty)
            {
                //var userIdString = user.FindFirstValue(ClaimTypes.NameIdentifier);
                //return Guid.TryParse(userIdString, out var guid) ? guid : Guid.Empty;
                return ($"{tokenType} Token Invalid", false, null);
            }
            if (tokenType == Core.Abstraction.Jwt.Enum.TokenType.AuthRefreshToken)
            {
                var user = _context.Users.FirstOrDefault(i => i.Id == userId);
                bool userIsBlocked = user.UserBlackList.Any(i => i.ExpireDate > DateTime.Now);
                bool userSessionIsBlocked = user.UserSessionBlackList.Any(i => i.HashToken == _hashManager.Hash(token) &&
                i.UserId == userId);
                if (userIsBlocked)
                {
                    return ($"اکانت شما به دلیل فعالیت های غیر مجاز مسدود است.", false, null);
                }
                if (userSessionIsBlocked)
                {
                    return ($"سشن شما منقضی شده لطفا مجددا به اکانت خود وارد شوید.", false, null);
                }
            }

            return ($"{tokenType} Token Invalid", true, validateResult);
        }
    }
}
