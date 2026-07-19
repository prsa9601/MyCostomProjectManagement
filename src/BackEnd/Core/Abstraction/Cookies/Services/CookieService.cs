using BackEnd.Core.Abstraction.Cookies.Interfaces;
using Microsoft.AspNetCore.Http;
namespace BackEnd.Core.Abstraction.Cookies.Services
{
    public class CookieService : ICookiesService
    {
        private CookieOptions _defaultOptions;

        public CookieService()
        {
            _defaultOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                MaxAge = TimeSpan.FromDays(1),
                Path = "/",

                //HttpOnly = true,      // جلوگیری از دسترسی JavaScript
                //Secure = true,        // فقط از طریق HTTPS
                //SameSite = SameSiteMode.Strict, // جلوگیری از CSRF
                //Domain = "example.com", // محدود کردن دامنه
                //Path = "/",           // محدود کردن مسیر
                //Expires = DateTime.UtcNow.AddMinutes(15), // عمر کوتاه
                //// Max-Age = 900      // alternative
            };
        }

        /// <summary>
        /// افزودن کوکی جدید
        /// </summary>
        public void SetCookie(HttpResponse response, string key, string value, CookieOptions? options = null)
        {
            if (response == null)
                throw new ArgumentNullException(nameof(response));

            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("Key cannot be null or empty", nameof(key));

            var cookieOptions = options ?? _defaultOptions;
            response.Cookies.Append(key, value, cookieOptions);
        }

        /// <summary>
        /// دریافت مقدار کوکی
        /// </summary>
        public string GetCookie(HttpRequest request, string key)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("Key cannot be null or empty", nameof(key));

            return request.Cookies[key];
        }

        /// <summary>
        /// حذف کوکی
        /// </summary>
        public void DeleteCookie(HttpResponse response, string key)
        {
            if (response == null)
                throw new ArgumentNullException(nameof(response));

            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("Key cannot be null or empty", nameof(key));

            response.Cookies.Delete(key);
        }

        /// <summary>
        /// بررسی وجود کوکی
        /// </summary>
        public bool ContainsCookie(HttpRequest request, string key)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("Key cannot be null or empty", nameof(key));

            return request.Cookies.ContainsKey(key);
        }

        /// <summary>
        /// ایجاد تنظیمات سفارشی برای کوکی
        /// </summary>
        public CookieOptions CreateOptions(TimeSpan? maxAge = null, bool httpOnly = true,
                                          bool secure = true, SameSiteMode sameSite = SameSiteMode.Strict,
                                          string path = "/")
        {
            _defaultOptions = new CookieOptions
            {
                HttpOnly = httpOnly,
                Secure = secure,
                SameSite = sameSite,
                MaxAge = maxAge ?? TimeSpan.FromDays(1),
                Path = path
            };
            return new CookieOptions
            {
                HttpOnly = httpOnly,
                Secure = secure,
                SameSite = sameSite,
                MaxAge = maxAge ?? TimeSpan.FromDays(1),
                Path = path
            };
        }
    }
}