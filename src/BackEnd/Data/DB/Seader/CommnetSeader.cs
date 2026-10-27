using BackEnd.Data.Entities.Comment;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BackEnd.Data.DB.Seeder;
    /// <summary>
    /// سیدر هوشمند (Upsert) برای نظرات کاربران.
    /// بر اساس ترکیب UserName + Text بررسی می‌کند که دیتای تکراری ایجاد نشود.
    /// </summary>
    public static class CommentSeeder
    {
        // ============================================================
        //  نقطه ورود — از DbInitializer صدا بزنید
        // ============================================================
        public static async Task SeedCommentsAsync(
            this IServiceProvider serviceProvider,
            bool forceReseed = false)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<Context>();
            var logger = scope.ServiceProvider.GetService<ILogger<Context>>();

            try
            {
                if (forceReseed)
                {
                    logger?.LogWarning("⚠️ forceReseed=true — همه نظرات حذف و دوباره اضافه می‌شوند");
                    context.Comments.RemoveRange(context.Comments);
                    await context.SaveChangesAsync();
                }

                await UpsertCommentsAsync(context, logger);

                logger?.LogInformation("✅ Seed نظرات کاربران با موفقیت تکمیل شد");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "❌ خطا در Seed کردن نظرات کاربران");
                throw;
            }
        }

        // ============================================================
        //  Upsert اصلی
        // ============================================================
        private static async Task UpsertCommentsAsync(Context context, ILogger? logger)
        {
            // گرفتن نظرات موجود (بر اساس نام کاربر و متن برای جلوگیری از تکرار)
            var existing = await context.Comments
                .AsNoTracking()
                .Select(x => new { x.UserName, x.Text })
                .ToListAsync();

            var existingSet = existing
                .Select(x => $"{x.UserName.Trim()}|{x.Text.Trim()}")
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            var seedData = GetCommentsSeed();
            int added = 0, skipped = 0;

            foreach (var item in seedData)
            {
                var key = $"{item.UserName.Trim()}|{item.Text.Trim()}";

                if (existingSet.Contains(key))
                {
                    // از قبل وجود دارد
                    skipped++;
                }
                else
                {
                    // رکورد جدید
                    item.Id = Guid.NewGuid();
                    item.CreationDate = DateTime.Now.AddDays(-Random.Shared.Next(1, 30)); // تاریخ تصادفی برای واقعی‌تر بودن
                    await context.Comments.AddAsync(item);
                    added++;
                }
            }

            if (added > 0)
                await context.SaveChangesAsync();

            logger?.LogInformation(
                "Comments → اضافه: {Added} | نادیده گرفته‌شده (تکراری): {Skipped}",
                added, skipped);
        }

        // ============================================================
        //  داده‌های Seed — نظرات
        // ============================================================
        private static List<Comment> GetCommentsSeed() => new()
        {
            new()
            {
                UserName = "علی رضایی",
                JobTitle = "مدیرعامل استارتاپ فناوری",
                Text = "همکاری با تیم محمد پارسا کریمی یکی از بهترین تجربه‌های کاری ما بود. پروژه ما با ASP.NET Core و معماری تمیز پیاده‌سازی شد و سرعت و امنیت سایت فوق‌العاده است. پشتیبانی و پاسخگویی در تمام مراحل عالی بود.",
                Score = 5,
                IsActive = true
            },
            new()
            {
                UserName = "سارا محمدی",
                JobTitle = "بنیان‌گذار فروشگاه اینترنتی",
                Text = "داشبورد مدیریتی که برای ما طراحی شد، دقیقاً همان چیزی بود که نیاز داشتیم. رابط کاربری زیبا، سرعت بالا و گزارش‌گیری دقیق. تیم توسعه در زمان‌بندی توافق‌شده پروژه را تحویل داد.",
                Score = 5,
                IsActive = true
            },
            new()
            {
                UserName = "امیرحسین کریمی",
                JobTitle = "مدیر فنی شرکت نرم‌افزاری",
                Text = "کیفیت کد و رعایت اصول SOLID و Clean Architecture در پروژه‌ای که تحویل گرفتیم واقعاً تحسین‌برانگیز بود. تیم ما به راحتی توانست روی کدهای نوشته‌شده توسعه بدهد.",
                Score = 5,
                IsActive = true
            },
            new()
            {
                UserName = "مریم نادری",
                JobTitle = "مدیر بازاریابی دیجیتال",
                Text = "APIهایی که برای اتصال به سرویس‌های خارجی توسعه داده شد، بسیار پایدار و امن هستند. مستندات Swagger کامل بود و کار تیم ما را بسیار راحت کرد.",
                Score = 4,
                IsActive = true
            },
            new()
            {
                UserName = "رضا احمدی",
                JobTitle = "کارآفرین",
                Text = "از مشاوره اولیه تا تحویل نهایی، همه چیز حرفه‌ای پیش رفت. پروژه ما شامل درگاه پرداخت، سیستم اشتراک و پنل ادمین بود که همه به بهترین شکل پیاده‌سازی شدند.",
                Score = 5,
                IsActive = true
            },
            new()
            {
                UserName = "نرگس حسینی",
                JobTitle = "مدیرعامل آژانس دیجیتال",
                Text = "طراحی سایت با Blazor و ASP.NET Core باعث شد سایت ما در برابر ترافیک بالا کاملاً پایدار باشد. تیم توسعه بسیار خوش‌قول و منظم بود.",
                Score = 5,
                IsActive = true
            },
            new()
            {
                UserName = "حسین موسوی",
                JobTitle = "مدیر IT",
                Text = "مهاجرت دیتابیس ما به SQL Server با کمترین Downtime انجام شد. بهینه‌سازی کوئری‌ها باعث شد سرعت سیستم تا ۳ برابر افزایش پیدا کند.",
                Score = 4,
                IsActive = true
            },
            new()
            {
                UserName = "فاطمه زهرا صادقی",
                JobTitle = "صاحب کسب‌وکار خانگی",
                Text = "برای اولین بار بود که می‌خواستم سایت فروشگاهی راه‌اندازی کنم. تیم پارسا کریمی با حوصله تمام مراحل را توضیح دادند و سایت زیبایی برای من ساختند. بسیار راضی هستم.",
                Score = 5,
                IsActive = true
            }
        };
    }