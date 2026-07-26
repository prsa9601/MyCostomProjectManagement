using Microsoft.JSInterop;

namespace MyCostomProjectManagement.Infrastructure
{
    public class UserAlertService
    {
        private readonly IJSRuntime _jsRuntime;

        public UserAlertService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task ShowAlert(string message, string type = "info", string? title = null, int? duration = null)
        {
            await _jsRuntime.InvokeVoidAsync("showUserAlert", message, type, title ?? "", duration ?? 0);
        }

        public async Task ShowStatus(int statusCode, string? customMessage = null, string? customTitle = null, int? duration = null)
        {
            await _jsRuntime.InvokeVoidAsync("showUserStatus", statusCode, customMessage ?? "", customTitle ?? "", duration ?? 0);
        }

        public async Task Success(string message, string? title = null, int? duration = null)
            => await ShowAlert(message, "success", title ?? "✅ موفقیت", duration);

        public async Task Error(string message, string? title = null, int? duration = null)
            => await ShowAlert(message, "error", title ?? "❌ خطا", duration);

        public async Task Warning(string message, string? title = null, int? duration = null)
            => await ShowAlert(message, "warning", title ?? "⚠️ هشدار", duration);

        public async Task Info(string message, string? title = null, int? duration = null)
            => await ShowAlert(message, "info", title ?? "ℹ️ اطلاعیه", duration);

        public async Task Remove(string id)
            => await _jsRuntime.InvokeVoidAsync("UserAlert.remove", id);

        public async Task ClearAll()
            => await _jsRuntime.InvokeVoidAsync("UserAlert.clearAll");
    }
}