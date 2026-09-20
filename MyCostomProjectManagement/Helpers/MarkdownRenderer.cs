using System.Text;
using System.Text.RegularExpressions;
using System.Net;

namespace MyCostomProjectManagement.Helpers
{
    /// <summary>
    /// Markdown Renderer سبک با Sanitization کامل برای جلوگیری از XSS
    /// </summary>
    public static class MarkdownRenderer
    {
        // ============================================================
        // الگوهای شناسایی لینک‌های خطرناک
        // ============================================================
        private static readonly Regex DangerousUrlRegex = new(
            @"^\s*(javascript|data|vbscript|file)\s*:",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

        // ============================================================
        // متد اصلی
        // ============================================================
        public static string Render(string? markdown)
        {
            if (string.IsNullOrWhiteSpace(markdown))
                return "<p style='color:#64748b;'>محتوایی ثبت نشده است.</p>";

            // ۱. محدودیت حجم ورودی (حداکثر ۵۰۰KB) برای جلوگیری از DoS
            const int maxLength = 500 * 1024;
            if (markdown.Length > maxLength)
                markdown = markdown.Substring(0, maxLength) + "\n\n> ⚠️ محتوا بیش از حد طولانی است.";

            var html = markdown;

            // ۲. نرمال‌سازی خطوط
            html = html.Replace("\r\n", "\n").Replace("\r", "\n");

            // ۳. استخراج Code Blocks قبل از هر چیز (تا Escape نشن)
            var codeBlocks = new List<string>();
            html = Regex.Replace(html, @"```(\w*)\n([\s\S]*?)```", match =>
            {
                var lang = match.Groups[1].Value;
                var code = match.Groups[2].Value.TrimEnd();

                // Escape کد برای جلوگیری از XSS
                code = WebUtility.HtmlEncode(code);

                var langClass = string.IsNullOrWhiteSpace(lang)
                    ? "language-plaintext"
                    : $"language-{SanitizeClassName(lang)}";

                var index = codeBlocks.Count;
                codeBlocks.Add($"<pre><code class=\"{langClass}\">{code}</code></pre>");
                return $"\u0001CODEBLOCK{index}\u0001";
            });

            // ۴. Escape بقیه HTML
            html = WebUtility.HtmlEncode(html);

            // ۵. Inline Code (با Escape)
            html = Regex.Replace(html, @"`([^`\n]+)`", match =>
            {
                var code = match.Groups[1].Value;
                return $"<code>{code}</code>";
            });

            // ۶. Headers (H1-H6)
            html = Regex.Replace(html, @"^######\s+(.+)$", "<h6>$1</h6>", RegexOptions.Multiline);
            html = Regex.Replace(html, @"^#####\s+(.+)$", "<h5>$1</h5>", RegexOptions.Multiline);
            html = Regex.Replace(html, @"^####\s+(.+)$", "<h4>$1</h4>", RegexOptions.Multiline);
            html = Regex.Replace(html, @"^###\s+(.+)$", "<h3>$1</h3>", RegexOptions.Multiline);
            html = Regex.Replace(html, @"^##\s+(.+)$", "<h2>$1</h2>", RegexOptions.Multiline);
            html = Regex.Replace(html, @"^#\s+(.+)$", "<h1>$1</h1>", RegexOptions.Multiline);

            // ۷. Bold / Italic
            html = Regex.Replace(html, @"\*\*\*(.+?)\*\*\*", "<strong><em>$1</em></strong>");
            html = Regex.Replace(html, @"\*\*(.+?)\*\*", "<strong>$1</strong>");
            html = Regex.Replace(html, @"\*(.+?)\*", "<em>$1</em>");
            html = Regex.Replace(html, @"__(.+?)__", "<strong>$1</strong>");
            html = Regex.Replace(html, @"_(.+?)_", "<em>$1</em>");
            html = Regex.Replace(html, @"~~(.+?)~~", "<del>$1</del>");

            // ۸. لینک‌ها [text](url) با Sanitization
            html = Regex.Replace(html, @"\[([^\]]+)\]\(([^)]+)\)", match =>
            {
                var text = match.Groups[1].Value;
                var url = match.Groups[2].Value.Trim();

                // ❌ جلوگیری از لینک‌های خطرناک
                if (!IsSafeUrl(url))
                    return text; // فقط متن رو نشون بده، لینک نکن

                // ✅ فقط لینک‌های http/https/mailto مجاز
                var safeUrl = WebUtility.HtmlEncode(url);
                return $"<a href=\"{safeUrl}\" target=\"_blank\" rel=\"noopener noreferrer nofollow\">{text}</a>";
            });

            // ۹. تصاویر ![alt](url) با Sanitization
            html = Regex.Replace(html, @"!\[([^\]]*)\]\(([^)]+)\)", match =>
            {
                var alt = match.Groups[1].Value;
                var url = match.Groups[2].Value.Trim();

                if (!IsSafeImageUrl(url))
                    return $"[تصویر: {alt}]";

                var safeUrl = WebUtility.HtmlEncode(url);
                var safeAlt = WebUtility.HtmlEncode(alt);
                return $"<img src=\"{safeUrl}\" alt=\"{safeAlt}\" loading=\"lazy\" style=\"max-width:100%;border-radius:12px;margin:16px 0;\" />";
            });

            // ۱۰. Blockquote
            html = Regex.Replace(html, @"^&gt;\s?(.+)$", "<blockquote>$1</blockquote>", RegexOptions.Multiline);

            // ۱۱. Horizontal Rule
            html = Regex.Replace(html, @"^(?:---+|\*\*\*+|___+)$", "<hr />", RegexOptions.Multiline);

            // ۱۲. لیست‌ها
            // لیست نامرتب
            html = Regex.Replace(html, @"^[\-\*\+]\s+(.+)$", "<li>$1</li>", RegexOptions.Multiline);
            // لیست مرتب
            html = Regex.Replace(html, @"^\d+\.\s+(.+)$", "<li>$1</li>", RegexOptions.Multiline);

            // Wrap لیست‌ها
            html = Regex.Replace(
                html,
                @"(<li>.*?</li>)(\s*<li>.*?</li>)*",
                m => "<ul>" + m.Value + "</ul>",
                RegexOptions.Singleline);

            // ۱۳. بازگردانی Code Blocks
            html = Regex.Replace(html, @"\u0001CODEBLOCK(\d+)\u0001", match =>
            {
                var index = int.Parse(match.Groups[1].Value);
                return index < codeBlocks.Count ? codeBlocks[index] : "";
            });

            // ۱۴. Wrap پاراگراف‌ها
            var lines = html.Split('\n');
            var sb = new StringBuilder();
            foreach (var line in lines)
            {
                var trimmed = line.TrimEnd();
                if (string.IsNullOrWhiteSpace(trimmed))
                    continue;

                if (IsBlockElement(trimmed))
                    sb.AppendLine(trimmed);
                else
                    sb.AppendLine($"<p>{trimmed}</p>");
            }

            return sb.ToString();
        }

        // ============================================================
        // اعتبارسنجی URL
        // ============================================================
        private static bool IsSafeUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return false;

            // حداکثر طول URL
            if (url.Length > 2000)
                return false;

            // جلوگیری از javascript:, data:, vbscript: و ...
            if (DangerousUrlRegex.IsMatch(url))
                return false;

            // فقط پروتکل‌های مجاز
            if (Uri.TryCreate(url, UriKind.Absolute, out var uri))
            {
                var scheme = uri.Scheme.ToLowerInvariant();
                return scheme is "http" or "https" or "mailto" or "tel";
            }

            // مسیرهای نسبی (نسبت به سایت خودمون) مجاز
            if (url.StartsWith("/") && !url.StartsWith("//"))
                return true;

            // hash-links مجاز
            if (url.StartsWith("#"))
                return true;

            return false;
        }

        private static bool IsSafeImageUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return false;

            if (DangerousUrlRegex.IsMatch(url))
                return false;

            if (Uri.TryCreate(url, UriKind.Absolute, out var uri))
            {
                return uri.Scheme is "http" or "https";
            }

            // تصاویر نسبی مجاز
            if (url.StartsWith("/") && !url.StartsWith("//"))
                return true;

            return false;
        }

        // ============================================================
        // کمکی‌ها
        // ============================================================
        private static bool IsBlockElement(string line)
        {
            if (string.IsNullOrWhiteSpace(line)) return false;

            var trimmed = line.TrimStart();

            return trimmed.StartsWith("<h") ||
                   trimmed.StartsWith("<ul") ||
                   trimmed.StartsWith("<ol") ||
                   trimmed.StartsWith("<li") ||
                   trimmed.StartsWith("<pre") ||
                   trimmed.StartsWith("<blockquote") ||
                   trimmed.StartsWith("<hr") ||
                   trimmed.StartsWith("<table") ||
                   trimmed.StartsWith("<thead") ||
                   trimmed.StartsWith("<tbody") ||
                   trimmed.StartsWith("<tr") ||
                   trimmed.StartsWith("<td") ||
                   trimmed.StartsWith("<th") ||
                   trimmed.StartsWith("<img") ||
                   trimmed.EndsWith("</ul>") ||
                   trimmed.EndsWith("</ol>") ||
                   trimmed.EndsWith("</pre>") ||
                   trimmed.EndsWith("</blockquote>") ||
                   trimmed.EndsWith("</table>");
        }

        private static string SanitizeClassName(string name)
        {
            // فقط حروف، اعداد، خط تیره و آندرلاین مجاز
            return Regex.Replace(name, @"[^a-zA-Z0-9_\-]", "");
        }
    }
}