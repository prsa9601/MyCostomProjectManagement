using Microsoft.AspNetCore.Http;
using System;

namespace BackEnd.Core.Abstraction.Cookies.Interfaces
{
    public interface ICookiesService
    {
        void SetCookie(HttpResponse response, string key, string value, CookieOptions options = null);
        string GetCookie(HttpRequest request, string key);
        void DeleteCookie(HttpResponse response, string key);
        bool ContainsCookie(HttpRequest request, string key);
    }
}
