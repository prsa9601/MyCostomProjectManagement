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
            var facade = context.RequestServices.GetRequiredService<IPageManagementFacade>();
            var method = context.Request.Method;
            if (HttpMethods.IsConnect(method) ||
                HttpMethods.IsOptions(method) ||
                HttpMethods.IsTrace(method) ||
                context.WebSockets.IsWebSocketRequest)
            {
                await _next(context);
                return;
            }

            // تشخیص مسیرهای مورد نظر (مثلاً صفحات خصوصی)
            var path = context.Request.Path.Value?.ToLower() ?? "";
            var r = await facade.GetList();
            var noIndexPaths = new List<string>();
            noIndexPaths = r.Where(i => i.SeoIndexing == true).Select(i => i.Url).ToList();
            // ---------- تزریق هوشمند متا تگ با استفاده از OnStarting ----------
            // استریم اصلی را نگه می‌داریم تا بعداً بدنه را تغییر دهیم
            var originalBody = context.Response.Body;
            using var memoryStream = new MemoryStream();
            context.Response.Body = memoryStream;

            // اگر مسیر در لیست نبود یا صفحه خطا بود، بدون تغییر ادامه بده
            //if (!noIndexPaths.Any(p => path.StartsWith(p)) || context.Response.HasStarted)
            if (!noIndexPaths.Any(p => path.Equals(p)) || context.Response.HasStarted)
            {
                // ادامه زنجیره (اجرای بقیه Middlewareها و رندر صفحه)
                await _next(context);

                // اگر پاسخ موفق بود و محتوای HTML داشت، آن را تغییر بده
                if (context.Response.StatusCode == 200)
                {
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    var responseBody = await new StreamReader(memoryStream).ReadToEndAsync();

                    // فقط اگر محتوای HTML باشد (با بررسی <head> یا <html>)
                    if (responseBody.Contains("<head>") || responseBody.Contains("<HEAD>"))
                    {
                        var metaTag = "<meta name=\"robots\" content=\"noindex, nofollow\" />";
                        var modifiedBody = responseBody.Replace("</head>", metaTag + "</head>", StringComparison.OrdinalIgnoreCase);

                        // بازنویسی پاسخ با بدنه اصلاح‌شده
                        context.Response.Body = originalBody;
                        await context.Response.WriteAsync(modifiedBody);
                        return;
                    }
                }
            }
            else
            {


                await _next(context);

                if (context.Response.StatusCode == 200)
                {
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    var responseBody = await new StreamReader(memoryStream).ReadToEndAsync();

                    // فقط اگر محتوای HTML باشد (با بررسی <head> یا <html>)
                    if (responseBody.Contains("<head>") || responseBody.Contains("<HEAD>"))
                    {
                        var metaTag = "<meta name=\"robots\" content=\"index, follow\" />";
                        var modifiedBody = responseBody.Replace("</head>", metaTag + "</head>", StringComparison.OrdinalIgnoreCase);

                        // بازنویسی پاسخ با بدنه اصلاح‌شده
                        context.Response.Body = originalBody;
                        await context.Response.WriteAsync(modifiedBody);
                        return;
                    }
                }
              
            }

            // اگر شرط‌ها برقرار نبود، بدنه اصلی را برگردان (بدون تغییر)
            context.Response.Body = originalBody;
            memoryStream.Seek(0, SeekOrigin.Begin);
            await memoryStream.CopyToAsync(originalBody);
        }
    }
}
