using Microsoft.JSInterop;

namespace MyCostomProjectManagement.Infrastructure
{
    /// <summary>
    /// سرویس نمایش پیام‌های هشدار با استفاده از کدهای جاوااسکریپت
    /// این سرویس جایگزین AlertService مبتنی بر کامپوننت Blazor می‌شود
    /// و مستقیماً با IJSRuntime کار می‌کند
    /// </summary>
    public class JsAlertService
    {
        private readonly IJSRuntime _jsRuntime;

        public JsAlertService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        // ============================================================
        // متد اصلی نمایش هشدار با پارامترهای متغیر
        // ============================================================

        /// <summary>
        /// نمایش هشدار با پارامترهای مختلف
        /// </summary>
        /// <param name="message">متن پیام</param>
        /// <param name="type">نوع: success, error, warning, info (پیش‌فرض: info)</param>
        /// <param name="title">عنوان (اختیاری)</param>
        /// <param name="duration">مدت زمان به میلی‌ثانیه (اختیاری)</param>
        /// اگه مدت زمان نال باشه حذف نمیشه
        public async Task ShowAlert(string message, string type = "info", string? title = null, int? duration = 5000 )
        {
            // ارسال پارامترها به تابع showAlert در جاوااسکریپت
            await _jsRuntime.InvokeVoidAsync("showAlert", message, type, title ?? "", duration ?? 0);
        }

        /// <summary>
        /// نمایش هشدار بر اساس کد وضعیت HTTP
        /// </summary>
        /// <param name="statusCode">کد وضعیت</param>
        /// <param name="customMessage">پیام سفارشی (اختیاری)</param>
        /// <param name="customTitle">عنوان سفارشی (اختیاری)</param>
        /// <param name="duration">مدت زمان به میلی‌ثانیه (اختیاری)</param>
        public async Task ShowStatus(int statusCode, string? customMessage = null, string? customTitle = null, int? duration = 5000)
        {
            await _jsRuntime.InvokeVoidAsync("showStatus", statusCode, customMessage ?? "", customTitle ?? "", duration ?? 0);
        }

        // ============================================================
        // متدهای کمکی با نام‌های آشنا
        // ============================================================

        /// <summary>
        /// نمایش پیام موفقیت
        /// </summary>
        public async Task Success(string message, string? title = null, int? duration = 5000)
            => await ShowAlert(message, "success", title ?? "موفقیت آمیز", duration);

        /// <summary>
        /// نمایش پیام خطا
        /// </summary>
        public async Task Error(string message, string? title = null, int? duration = 5000)
            => await ShowAlert(message, "error", title ?? "خطا", duration);

        /// <summary>
        /// نمایش پیام هشدار
        /// </summary>
        public async Task Warning(string message, string? title = null, int? duration = 5000)
            => await ShowAlert(message, "warning", title ?? "هشدار", duration);

        /// <summary>
        /// نمایش پیام اطلاعیه
        /// </summary>
        public async Task Info(string message, string? title = null, int? duration = 5000)
            => await ShowAlert(message, "info", title ?? "اطلاعیه", duration);

        // ============================================================
        // متدهای مدیریت هشدارها
        // ============================================================

        /// <summary>
        /// حذف یک هشدار با شناسه
        /// </summary>
        public async Task RemoveAlert(string id)
        {
            await _jsRuntime.InvokeVoidAsync("window.Alert.remove", id);
        }

        /// <summary>
        /// پاک کردن تمام هشدارها
        /// </summary>
        public async Task ClearAll()
        {
            await _jsRuntime.InvokeVoidAsync("window.Alert.clearAll");
        }

        /// <summary>
        /// به‌روزرسانی تنظیمات (اختیاری)
        /// </summary>
        public async Task UpdateConfig(object config)
        {
            await _jsRuntime.InvokeVoidAsync("window.Alert.config", config);
        }
    }
}