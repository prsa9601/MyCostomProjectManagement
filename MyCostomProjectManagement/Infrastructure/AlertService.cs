using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCostomProjectManagement.Infrastructure
{
    public enum AlertType
    {
        Success,
        Error,
        Warning,
        Info
    }

    public class AlertItem
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; } = "";
        public string Message { get; set; } = "";
        public AlertType Type { get; set; } = AlertType.Info;
        public int Duration { get; set; } = 5;
        public bool IsRemoving { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

    public class AlertService
    {
        private readonly List<AlertItem> _alerts = new();
        private readonly System.Timers.Timer _cleanupTimer;

        public IReadOnlyList<AlertItem> Alerts => _alerts.AsReadOnly();

        // رویداد – کامپوننت‌ها به آن مشترک می‌شوند
        public event Action? OnChange;

        public AlertService()
        {
            // پاک‌سازی خودکار هشدارهای منقضی شده هر ۱۰ ثانیه
            _cleanupTimer = new System.Timers.Timer(10000);
            _cleanupTimer.Elapsed += (s, e) => CleanupExpired();
            _cleanupTimer.Start();
        }

        // ----- متدهای نمایش هشدار -----
        public void ShowAlert(string message, AlertType type = AlertType.Info, string? title = null, int duration = 5)
        {
            var alert = new AlertItem
            {
                Message = message,
                Type = type,
                Title = title ?? GetDefaultTitle(type),
                Duration = duration
            };

            _alerts.Add(alert);
            NotifyStateChanged();

            _ = Task.Delay(duration * 1000).ContinueWith(_ => RemoveAlert(alert.Id));
        }

        public void Success(string message, string? title = null, int duration = 4)
            => ShowAlert(message, AlertType.Success, title ?? "موفقیت آمیز", duration);

        public void Error(string message, string? title = null, int duration = 6)
            => ShowAlert(message, AlertType.Error, title ?? "خطا", duration);

        public void Warning(string message, string? title = null, int duration = 5)
            => ShowAlert(message, AlertType.Warning, title ?? "هشدار", duration);

        public void Info(string message, string? title = null, int duration = 4)
            => ShowAlert(message, AlertType.Info, title ?? "اطلاعیه", duration);

        // ----- نمایش بر اساس کد وضعیت HTTP -----
        public void ShowStatus(int statusCode, string? customMessage = null)
        {
            var (message, type) = GetStatusInfo(statusCode);
            ShowAlert(customMessage ?? message, type);
        }

        private (string message, AlertType type) GetStatusInfo(int statusCode) =>
            statusCode switch
            {
                200 => ("درخواست با موفقیت انجام شد.", AlertType.Success),
                201 => ("مورد جدید با موفقیت ایجاد شد.", AlertType.Success),
                202 => ("درخواست پذیرفته شد و در حال پردازش است.", AlertType.Success),
                204 => ("درخواست با موفقیت انجام شد (بدون محتوا).", AlertType.Success),
                301 => ("منبع به صورت دائم انتقال یافته است.", AlertType.Info),
                302 => ("منبع به صورت موقت انتقال یافته است.", AlertType.Info),
                304 => ("منبع تغییری نداشته است.", AlertType.Info),
                400 => ("درخواست نامعتبر است. لطفاً اطلاعات را بررسی کنید.", AlertType.Error),
                401 => ("شما مجوز دسترسی ندارید. لطفاً وارد شوید.", AlertType.Error),
                403 => ("شما دسترسی به این بخش را ندارید.", AlertType.Error),
                404 => ("مورد درخواستی یافت نشد.", AlertType.Error),
                405 => ("روش درخواست مجاز نیست.", AlertType.Error),
                408 => ("زمان درخواست به پایان رسید. لطفاً مجدداً تلاش کنید.", AlertType.Warning),
                409 => ("تضاد در داده‌ها رخ داده است. لطفاً بررسی کنید.", AlertType.Error),
                410 => ("منبع درخواستی دیگر موجود نیست.", AlertType.Error),
                411 => ("طول محتوا مشخص نشده است.", AlertType.Error),
                413 => ("حجم داده ارسالی بیش از حد مجاز است.", AlertType.Error),
                415 => ("نوع داده پشتیبانی نمی‌شود.", AlertType.Error),
                422 => ("داده‌های ارسالی نامعتبر است.", AlertType.Error),
                429 => ("تعداد درخواست‌ها بیش از حد مجاز است. لطفاً کمی صبر کنید.", AlertType.Warning),
                500 => ("خطای داخلی سرور رخ داده است. لطفاً مجدداً تلاش کنید.", AlertType.Error),
                501 => ("سرویس مورد نظر پیاده‌سازی نشده است.", AlertType.Error),
                502 => ("درگاه سرور پاسخ نامعتبر ارسال کرده است.", AlertType.Error),
                503 => ("سرویس در دسترس نیست. لطفاً بعداً تلاش کنید.", AlertType.Warning),
                504 => ("زمان پاسخ‌گویی سرور به پایان رسید.", AlertType.Error),
                _ => ($"خطای ناشناخته (کد {statusCode})", AlertType.Error)
            };

        // ----- مدیریت لیست -----
        public void RemoveAlert(string id)
        {
            var alert = _alerts.FirstOrDefault(a => a.Id == id);
            if (alert == null) return;

            alert.IsRemoving = true;
            NotifyStateChanged();

            _ = Task.Delay(400).ContinueWith(_ =>
            {
                _alerts.RemoveAll(a => a.Id == id);
                NotifyStateChanged();
            });
        }

        public void ClearAll()
        {
            _alerts.Clear();
            NotifyStateChanged();
        }

        // ----- پاک‌سازی خودکار (توسط تایمر) -----
        private void CleanupExpired()
        {
            var expired = _alerts.Where(a => DateTime.Now - a.CreatedAt > TimeSpan.FromSeconds(a.Duration + 2)).ToList();
            if (expired.Any())
            {
                foreach (var alert in expired)
                    _alerts.Remove(alert);

                NotifyStateChanged();
            }
        }

        // ----- اطلاع‌رسانی به کامپوننت‌ها (بدون Dispatcher) -----
        private void NotifyStateChanged() => OnChange?.Invoke();

        // ----- عنوان پیش‌فرض -----
        private static string GetDefaultTitle(AlertType type) =>
            type switch
            {
                AlertType.Success => "موفقیت آمیز",
                AlertType.Error => "خطا",
                AlertType.Warning => "هشدار",
                _ => "اطلاعیه"
            };

        // ----- آزادسازی منابع (اختیاری) -----
        // public void Dispose()
        // {
        //     _cleanupTimer?.Stop();
        //     _cleanupTimer?.Dispose();
        // }
    }
}