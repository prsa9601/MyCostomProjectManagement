using System.Text;
using System.Text.RegularExpressions;
using MyCostomProjectManagement.Facade.PageManagement;

namespace MyCostomProjectManagement.Shared.Middleware
{
    public class RobotsMiddleware
    {
        private readonly RequestDelegate _next;

        public RobotsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // متدهای CONNECT و WebSocket نباید بدنه داشته باشند
            var method = context.Request.Method;
            if (HttpMethods.IsConnect(method) ||
                HttpMethods.IsOptions(method) ||
                HttpMethods.IsTrace(method) ||
                context.WebSockets.IsWebSocketRequest)
            {
                await _next(context);
                return;
            }

            // ─── نرمال‌سازی path ───
            var rawPath = context.Request.Path.Value?.ToLowerInvariant() ?? "/";
            var path = rawPath.Length > 1 ? rawPath.TrimEnd('/') : rawPath;

            var facade = context.RequestServices.GetRequiredService<IPageManagementFacade>();
            var pages = await facade.GetList();

            // ─── صفحاتی که SeoIndexing = true هستند ───
            var indexablePatterns = pages
                .Where(i => i.SeoIndexing)
                .Select(i => NormalizePattern(i.Url))
                .Where(p => !string.IsNullOrEmpty(p))
                .ToList();

            bool shouldIndex = indexablePatterns.Any(p => MatchesPattern(path, p));

            // ─── بافر کردن بدنه ───
            var originalBody = context.Response.Body;
            await using var memoryStream = new MemoryStream();
            context.Response.Body = memoryStream;

            try
            {
                await _next(context);

                // اگر پاسخ HTML نبود یا خطا بود، بدون تغییر برگردان
                if (context.Response.StatusCode != 200)
                {
                    context.Response.Body = originalBody;
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    await memoryStream.CopyToAsync(originalBody);
                    return;
                }

                memoryStream.Seek(0, SeekOrigin.Begin);
                using var reader = new StreamReader(memoryStream, leaveOpen: true);
                var body = await reader.ReadToEndAsync();

                bool isHtml =
                    (context.Response.ContentType ?? "").Contains("text/html", StringComparison.OrdinalIgnoreCase)
                    || body.Contains("<head>", StringComparison.OrdinalIgnoreCase);

                if (!isHtml)
                {
                    context.Response.Body = originalBody;
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    await memoryStream.CopyToAsync(originalBody);
                    return;
                }

                // ─── حذف meta robotsهای قبلی برای جلوگیری از تضاد ───
                var cleaned = Regex.Replace(
                    body,
                    @"<meta\s+name=[""']robots[""']\s+content=[""'][^""']*[""']\s*/?>",
                    "",
                    RegexOptions.IgnoreCase);

                // ─── تزریق نسخه‌ی درست ───
                var metaTag = shouldIndex
                    ? "<meta name=\"robots\" content=\"index, follow, max-image-preview:large\" />"
                    : "<meta name=\"robots\" content=\"noindex, nofollow\" />";

                var modified = Regex.Replace(
                    cleaned,
                    @"</head>",
                    metaTag + Environment.NewLine + "</head>",
                    RegexOptions.IgnoreCase);

                // ─── نوشتن پاسخ ───
                context.Response.Body = originalBody;
                var bytes = Encoding.UTF8.GetBytes(modified);
                context.Response.ContentLength = bytes.Length;
                await context.Response.Body.WriteAsync(bytes);
            }
            catch
            {
                // در صورت خطا، بدنه‌ی اصلی را دست‌نخورده برگردان
                context.Response.Body = originalBody;
                memoryStream.Seek(0, SeekOrigin.Begin);
                await memoryStream.CopyToAsync(originalBody);
                throw;
            }
        }

        /// <summary>
        /// نرمال‌سازی الگو: trim، lowercase، حذف اسلش انتهایی
        /// </summary>
        private static string NormalizePattern(string? url)
        {
            if (string.IsNullOrWhiteSpace(url)) return "";
            var p = url.Trim().ToLowerInvariant();
            if (p.Length > 1) p = p.TrimEnd('/');
            return p;
        }

        /// <summary>
        /// بررسی می‌کند که آیا path با الگو مطابقت دارد یا نه.
        /// الگوهایی مثل {Slug}، {Id}، {*any} با هر مقداری مچ می‌شوند.
        /// </summary>
        /// <summary>
        /// بررسی می‌کند که آیا path با الگو مطابقت دارد یا نه.
        /// الگوهایی مثل {Slug}، {Id}، {*any} با هر مقداری مچ می‌شوند.
        /// </summary>
        private static bool MatchesPattern(string path, string pattern)
        {
            if (string.IsNullOrEmpty(pattern)) return false;

            // ریشه
            if (pattern == "/") return path == "/";

            // الگوی ثابت (بدون placeholder)
            if (!pattern.Contains('{'))
                return path.Equals(pattern, StringComparison.OrdinalIgnoreCase);

            // ─── تقسیم بر اساس placeholder ───
            // مثال: "/tutorials/{slug}/edit" → ["/tutorials/", "/edit"]
            // مثال: "/tutorials/{slug}"      → ["/tutorials/", ""]
            var parts = System.Text.RegularExpressions.Regex.Split(
                pattern,
                @"\{[^}]*\}");     // هر {چیزی}

            // ─── ساخت regex: escape هر بخش ثابت، join با [^/]+ ───
            var escapedParts = parts.Select(System.Text.RegularExpressions.Regex.Escape);
            var regexPattern = "^" + string.Join("[^/]+", escapedParts) + "$";

            return System.Text.RegularExpressions.Regex.IsMatch(
                path,
                regexPattern,
                System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        }
    }
}