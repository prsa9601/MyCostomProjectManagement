using BackEnd.Data.DB;
using BackEnd.Data.Entities.ProgrammingServices;
using BackEnd.Data.Entities.SpecializedTechnology;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BackEnd.Data.DB.Seeders
{
    /// <summary>
    /// سیدر هوشمند (Upsert) برای خدمات و تکنولوژی‌ها.
    /// بر اساس Title بررسی می‌کند:
    ///   - اگر موجود باشد → به‌روزرسانی می‌کند
    ///   - اگر نباشد → اضافه می‌کند
    /// بدون تکرار و بدون از دست دادن داده‌های کاربر.
    /// </summary>
    public static class ContentSeeder
    {
        // ============================================================
        //  نقطه ورود — از Program.cs صدا بزنید
        // ============================================================
        public static async Task SeedContentAsync(this IServiceProvider serviceProvider, bool forceReseed = false)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<Context>();
            var logger = scope.ServiceProvider.GetService<ILogger<Context>>();

            try
            {
                if (forceReseed)
                {
                    logger?.LogWarning("⚠️ forceReseed=true — همه داده‌ها حذف و دوباره اضافه می‌شوند");
                    await ForceReseedAsync(context, logger);
                }
                else
                {
                    await UpsertProgrammingServicesAsync(context, logger);
                    await UpsertSpecializedTechnologiesAsync(context, logger);
                }

                logger?.LogInformation("✅ Seed محتوا با موفقیت تکمیل شد");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "❌ خطا در Seed کردن داده‌های محتوا");
                throw;
            }
        }

        // ============================================================
        //  حالت Force — همه چیز را از نو می‌سازد (فقط برای Dev)
        // ============================================================
        private static async Task ForceReseedAsync(Context context, ILogger? logger)
        {
            logger?.LogWarning("در حال حذف تکنولوژی‌ها...");
            context.SpecializedTechnologies.RemoveRange(context.SpecializedTechnologies);
            await context.SaveChangesAsync();

            logger?.LogWarning("در حال حذف دسته‌بندی‌ها...");
            context.SpecializedTechnologyCategories.RemoveRange(context.SpecializedTechnologyCategories);
            await context.SaveChangesAsync();

            logger?.LogWarning("در حال حذف خدمات...");
            context.ProgrammingServices.RemoveRange(context.ProgrammingServices);
            await context.SaveChangesAsync();

            await UpsertProgrammingServicesAsync(context, logger);
            await UpsertSpecializedTechnologiesAsync(context, logger);
        }

        // ============================================================
        //  ۱. Upsert خدمات برنامه‌نویسی
        // ============================================================
        private static async Task UpsertProgrammingServicesAsync(Context context, ILogger? logger)
        {
            // داده‌های موجود را از دیتابیس بگیر
            var existing = await context.ProgrammingServices
                .ToDictionaryAsync(x => x.Title, StringComparer.OrdinalIgnoreCase);

            // لیست مرجع برای Seed
            var seedData = GetProgrammingServicesSeed();

            int added = 0, updated = 0, skipped = 0;

            foreach (var item in seedData)
            {
                if (existing.TryGetValue(item.Title, out var existingItem))
                {
                    // بررسی آیا نیاز به به‌روزرسانی دارد
                    bool needsUpdate = existingItem.Description != item.Description
                                    || existingItem.Icon != item.Icon
                                    || existingItem.IsActive != item.IsActive;

                    if (needsUpdate)
                    {
                        existingItem.Description = item.Description;
                        existingItem.Icon = item.Icon;
                        existingItem.IsActive = item.IsActive;
                        updated++;
                    }
                    else
                    {
                        skipped++;
                    }
                }
                else
                {
                    // رکورد جدید — با Id جدید اضافه کن
                    item.Id = Guid.NewGuid();
                    await context.ProgrammingServices.AddAsync(item);
                    added++;
                }
            }

            if (added > 0 || updated > 0)
            {
                await context.SaveChangesAsync();
            }

            logger?.LogInformation(
                "ProgrammingServices → اضافه: {Added} | به‌روزرسانی: {Updated} | بدون تغییر: {Skipped}",
                added, updated, skipped);
        }

        // ============================================================
        //  ۲. Upsert تکنولوژی‌های تخصصی
        // ============================================================
        private static async Task UpsertSpecializedTechnologiesAsync(Context context, ILogger? logger)
        {
            // ---------- دسته‌بندی‌ها ----------
            var existingCategories = await context.SpecializedTechnologyCategories
                .ToDictionaryAsync(x => x.Title, StringComparer.OrdinalIgnoreCase);

            var seedCategories = GetCategoriesSeed();
            int catAdded = 0, catUpdated = 0, catSkipped = 0;

            foreach (var cat in seedCategories)
            {
                if (existingCategories.TryGetValue(cat.Title, out var existingCat))
                {
                    if (existingCat.Icon != cat.Icon)
                    {
                        existingCat.Icon = cat.Icon;
                        catUpdated++;
                    }
                    else
                    {
                        catSkipped++;
                    }
                }
                else
                {
                    cat.Id = Guid.NewGuid();
                    await context.SpecializedTechnologyCategories.AddAsync(cat);
                    existingCategories[cat.Title] = cat; // به دیکشنری اضافه کن برای تکنولوژی‌ها
                    catAdded++;
                }
            }

            if (catAdded > 0 || catUpdated > 0)
                await context.SaveChangesAsync();

            logger?.LogInformation(
                "دسته‌بندی‌ها → اضافه: {Added} | به‌روزرسانی: {Updated} | بدون تغییر: {Skipped}",
                catAdded, catUpdated, catSkipped);

            // ---------- تکنولوژی‌ها ----------
            // نقشه Title → CategoryId (هم از موجود، هم از جدیدها)
            var categoryMap = existingCategories
                .ToDictionary(kv => kv.Key, kv => kv.Value.Id, StringComparer.OrdinalIgnoreCase);

            var existingTechs = await context.SpecializedTechnologies
                .ToDictionaryAsync(x => x.Title, StringComparer.OrdinalIgnoreCase);

            var seedTechs = GetTechnologiesSeed(categoryMap);
            int techAdded = 0, techUpdated = 0, techSkipped = 0;

            foreach (var tech in seedTechs)
            {
                if (existingTechs.TryGetValue(tech.Title, out var existingTech))
                {
                    bool needsUpdate = existingTech.Icon != tech.Icon
                                    || existingTech.CategoryId != tech.CategoryId;

                    if (needsUpdate)
                    {
                        existingTech.Icon = tech.Icon;
                        existingTech.CategoryId = tech.CategoryId;
                        techUpdated++;
                    }
                    else
                    {
                        techSkipped++;
                    }
                }
                else
                {
                    tech.Id = Guid.NewGuid();
                    await context.SpecializedTechnologies.AddAsync(tech);
                    techAdded++;
                }
            }

            if (techAdded > 0 || techUpdated > 0)
                await context.SaveChangesAsync();

            logger?.LogInformation(
                "تکنولوژی‌ها → اضافه: {Added} | به‌روزرسانی: {Updated} | بدون تغییر: {Skipped}",
                techAdded, techUpdated, techSkipped);
        }

        // ============================================================
        //  داده‌های Seed — خدمات برنامه‌نویسی
        // ============================================================
        private static List<ProgrammingServices> GetProgrammingServicesSeed() => new()
        {
            new() { Title = "طراحی سایت با ASP.NET Core", Description = "ساخت وب‌سایت‌های سریع، امن و مقیاس‌پذیر با ASP.NET Core و Blazor. طراحی واکنش‌گرا، SEO بهینه و Performance بالا.", Icon = "fas fa-globe", IsActive = true },
            new() { Title = "توسعه API و REST API", Description = "طراحی APIهای مقیاس‌پذیر با احراز هویت JWT، مستندات Swagger کامل، Rate Limiting و بهترین شیوه‌های REST.", Icon = "fas fa-code", IsActive = true },
            new() { Title = "داشبورد مدیریتی", Description = "ساخت داشبوردهای تحلیلی و مدیریتی حرفه‌ای با نمودارهای پیشرفته، گزارش‌گیری و خروجی Excel/PDF.", Icon = "fas fa-chart-line", IsActive = true },
            new() { Title = "پروژه‌های Blazor", Description = "ساخت اپلیکیشن‌های وب مدرن و تعاملی با Blazor Server و WebAssembly — تجربه‌ای شبیه SPA بدون جاوااسکریپت.", Icon = "fas fa-cubes", IsActive = true },
            new() { Title = "پایگاه داده و EF Core", Description = "طراحی و بهینه‌سازی پایگاه داده SQL Server و PostgreSQL با EF Core و Dapper — Migration‌های امن و Queryهای بهینه.", Icon = "fas fa-database", IsActive = true },
            new() { Title = "میکروسرویس و Docker", Description = "طراحی معماری میکروسرویس، Dockerize کردن اپلیکیشن و استقرار روی سرورهای ابری با CI/CD خودکار.", Icon = "fas fa-cloud", IsActive = true },
            new() { Title = "معماری Clean Architecture", Description = "پیاده‌سازی معماری تمیز، DDD، CQRS با MediatR — کد تست‌پذیر، مقیاس‌پذیر و قابل نگهداری در بلندمدت.", Icon = "fas fa-sitemap", IsActive = true },
            new() { Title = "Real-time با SignalR", Description = "ساخت سیستم‌های Real-time مثل چت، نوتیفیکیشن و داشبورد زنده با SignalR و WebSocket.", Icon = "fas fa-bolt", IsActive = true },
            new() { Title = "امنیت و احراز هویت", Description = "پیاده‌سازی JWT، OAuth2، IdentityServer و 2FA — با رعایت کامل استانداردهای OWASP Top 10.", Icon = "fas fa-shield-halved", IsActive = true },
            new() { Title = "بهینه‌سازی Performance", Description = "افزایش سرعت API با Redis Cache، Lazy Loading، بهینه‌سازی Query و Benchmark دقیق — تا ۴۰ برابر سریع‌تر.", Icon = "fas fa-rocket", IsActive = true },
            new() { Title = "درگاه پرداخت و اشتراک", Description = "اتصال به درگاه‌های ایرانی (زرین‌پال، آیدی‌پی) و خارجی + سیستم اشتراک دوره‌ای و مدیریت پرداخت.", Icon = "fas fa-credit-card", IsActive = true },
            new() { Title = "تست و CI/CD", Description = "Unit Test با xUnit، Integration Test با Testcontainers و راه‌اندازی CI/CD با GitHub Actions — استقرار خودکار و امن.", Icon = "fas fa-vial", IsActive = true }
        };

        // ============================================================
        //  داده‌های Seed — دسته‌بندی‌ها
        // ============================================================
        private static List<SpecializedTechnologyCategory> GetCategoriesSeed() => new()
        {
            new() { Title = "بک‌اند و API",           Icon = "fas fa-server" },
            new() { Title = "پایگاه داده و Cache",     Icon = "fas fa-database" },
            new() { Title = "DevOps و ابزارها",        Icon = "fas fa-cloud" },
            new() { Title = "فرانت‌اند",                Icon = "fas fa-palette" },
            new() { Title = "تست و QA",                Icon = "fas fa-vial" },
            new() { Title = "ابری و Cloud",            Icon = "fas fa-cloud-upload-alt" },
            new() { Title = "امنیت",                   Icon = "fas fa-shield-halved" },
            new() { Title = "پیام‌رسانی",               Icon = "fas fa-envelope-open-text" },
            new() { Title = "کتابخانه‌های .NET",       Icon = "fas fa-cube" },
            new() { Title = "معماری و الگوها",         Icon = "fas fa-sitemap" }
        };

        // ============================================================
        //  داده‌های Seed — تکنولوژی‌ها
        // ============================================================
        private static List<SpecializedTechnology> GetTechnologiesSeed(Dictionary<string, Guid> catMap)
        {
            // دسترسی راحت‌تر به Id دسته‌بندی
            Guid C(string name) => catMap[name];

            var list = new List<SpecializedTechnology>();

            void Add(string title, string icon, string catName)
                => list.Add(new SpecializedTechnology { Title = title, Icon = icon, CategoryId = C(catName) });

            // ===== ۱. بک‌اند و API =====
            Add("C#", "devicon-csharp-plain colored", "بک‌اند و API");
            Add(".NET Core", "devicon-dotnetcore-plain colored", "بک‌اند و API");
            Add(".NET 9 / 10", "devicon-dotnetcore-plain colored", "بک‌اند و API");
            Add("ASP.NET Core", "fas fa-fire", "بک‌اند و API");
            Add("ASP.NET MVC", "fas fa-window-maximize", "بک‌اند و API");
            Add("Web API", "fas fa-plug", "بک‌اند و API");
            Add("Minimal API", "fas fa-feather", "بک‌اند و API");
            Add("REST API", "fas fa-exchange-alt", "بک‌اند و API");
            Add("gRPC", "fas fa-bolt", "بک‌اند و API");
            Add("GraphQL", "fas fa-project-diagram", "بک‌اند و API");
            Add("SignalR", "fas fa-satellite-dish", "بک‌اند و API");
            Add("Blazor Server", "fas fa-cubes", "بک‌اند و API");
            Add("Blazor WebAssembly", "fas fa-cubes", "بک‌اند و API");
            Add("Razor Pages", "fas fa-file-code", "بک‌اند و API");
            Add("EF Core", "fas fa-database", "بک‌اند و API");
            Add("Dapper", "fas fa-rocket", "بک‌اند و API");
            Add("MediatR", "fas fa-tasks", "بک‌اند و API");
            Add("AutoMapper", "fas fa-exchange-alt", "بک‌اند و API");
            Add("FluentValidation", "fas fa-check-square", "بک‌اند و API");
            Add("Swagger / OpenAPI", "fas fa-book", "بک‌اند و API");

            // ===== ۲. پایگاه داده و Cache =====
            Add("SQL Server", "devicon-microsoftsqlserver-plain colored", "پایگاه داده و Cache");
            Add("PostgreSQL", "devicon-postgresql-plain colored", "پایگاه داده و Cache");
            Add("MySQL", "devicon-mysql-plain colored", "پایگاه داده و Cache");
            Add("SQLite", "devicon-sqlite-plain colored", "پایگاه داده و Cache");
            Add("MongoDB", "devicon-mongodb-plain colored", "پایگاه داده و Cache");
            Add("Redis", "devicon-redis-plain colored", "پایگاه داده و Cache");
            Add("Oracle Database", "devicon-oracle-original colored", "پایگاه داده و Cache");
            Add("Cassandra", "devicon-cassandra-plain colored", "پایگاه داده و Cache");
            Add("Elasticsearch", "devicon-elasticsearch-plain colored", "پایگاه داده و Cache");
            Add("LiteDB", "fas fa-database", "پایگاه داده و Cache");
            Add("SQL Tuning", "fas fa-tachometer-alt", "پایگاه داده و Cache");
            Add("Indexing & Partitioning", "fas fa-sitemap", "پایگاه داده و Cache");

            // ===== ۳. DevOps و ابزارها =====
            Add("Docker", "devicon-docker-plain colored", "DevOps و ابزارها");
            Add("Docker Compose", "devicon-docker-plain colored", "DevOps و ابزارها");
            Add("Kubernetes", "devicon-kubernetes-plain colored", "DevOps و ابزارها");
            Add("Git", "devicon-git-plain colored", "DevOps و ابزارها");
            Add("GitHub", "devicon-github-original", "DevOps و ابزارها");
            Add("GitLab", "devicon-gitlab-plain colored", "DevOps و ابزارها");
            Add("GitHub Actions", "fas fa-code-branch", "DevOps و ابزارها");
            Add("Jenkins", "devicon-jenkins-line colored", "DevOps و ابزارها");
            Add("Linux", "devicon-linux-plain", "DevOps و ابزارها");
            Add("Nginx", "fas fa-server", "DevOps و ابزارها");
            Add("Terraform", "devicon-terraform-plain colored", "DevOps و ابزارها");
            Add("Ansible", "devicon-ansible-plain colored", "DevOps و ابزارها");
            Add("Bash Scripting", "fas fa-terminal", "DevOps و ابزارها");
            Add("PowerShell", "fas fa-terminal", "DevOps و ابزارها");

            // ===== ۴. فرانت‌اند =====
            Add("HTML5", "devicon-html5-plain colored", "فرانت‌اند");
            Add("CSS3", "devicon-css3-plain colored", "فرانت‌اند");
            Add("JavaScript", "devicon-javascript-plain colored", "فرانت‌اند");
            Add("TypeScript", "devicon-typescript-plain colored", "فرانت‌اند");
            Add("Bootstrap", "devicon-bootstrap-plain colored", "فرانت‌اند");
            Add("Tailwind CSS", "devicon-tailwindcss-plain colored", "فرانت‌اند");
            Add("SASS / SCSS", "devicon-sass-original colored", "فرانت‌اند");
            Add("React", "devicon-react-original colored", "فرانت‌اند");
            Add("Vue.js", "devicon-vuejs-plain colored", "فرانت‌اند");
            Add("Angular", "devicon-angularjs-plain colored", "فرانت‌اند");
            Add("jQuery", "devicon-jquery-plain colored", "فرانت‌اند");
            Add("Alpine.js", "fas fa-mountain", "فرانت‌اند");
            Add("Chart.js", "fas fa-chart-bar", "فرانت‌اند");
            Add("Figma", "devicon-figma-plain colored", "فرانت‌اند");

            // ===== ۵. تست و QA =====
            Add("xUnit", "fas fa-check-double", "تست و QA");
            Add("NUnit", "fas fa-flask", "تست و QA");
            Add("MSTest", "fas fa-vial", "تست و QA");
            Add("Moq", "fas fa-cube", "تست و QA");
            Add("NSubstitute", "fas fa-exchange-alt", "تست و QA");
            Add("FluentAssertions", "fas fa-check", "تست و QA");
            Add("Testcontainers", "fas fa-cubes", "تست و QA");
            Add("Postman", "fas fa-bug", "تست و QA");
            Add("Insomnia", "fas fa-moon", "تست و QA");
            Add("Playwright", "fas fa-theater-masks", "تست و QA");
            Add("Selenium", "fas fa-robot", "تست و QA");
            Add("BenchmarkDotNet", "fas fa-tachometer-alt", "تست و QA");
            Add("SonarQube", "fas fa-shield-alt", "تست و QA");

            // ===== ۶. ابری و Cloud =====
            Add("Microsoft Azure", "devicon-azure-plain colored", "ابری و Cloud");
            Add("Azure App Service", "fas fa-cloud", "ابری و Cloud");
            Add("Azure Functions", "fas fa-bolt", "ابری و Cloud");
            Add("Azure SQL Database", "fas fa-database", "ابری و Cloud");
            Add("Azure Blob Storage", "fas fa-hdd", "ابری و Cloud");
            Add("Azure Key Vault", "fas fa-key", "ابری و Cloud");
            Add("Amazon AWS", "devicon-amazonwebservices-plain-wordmark colored", "ابری و Cloud");
            Add("AWS Lambda", "fas fa-server", "ابری و Cloud");
            Add("AWS S3", "fas fa-archive", "ابری و Cloud");
            Add("Google Cloud", "devicon-googlecloud-plain colored", "ابری و Cloud");
            Add("Cloudflare", "devicon-cloudflare-plain colored", "ابری و Cloud");
            Add("DigitalOcean", "devicon-digitalocean-plain colored", "ابری و Cloud");

            // ===== ۷. امنیت =====
            Add("JWT", "fas fa-key", "امنیت");
            Add("OAuth 2.0", "fas fa-user-shield", "امنیت");
            Add("OpenID Connect", "fas fa-id-card", "امنیت");
            Add("IdentityServer", "fas fa-shield-alt", "امنیت");
            Add("ASP.NET Identity", "fas fa-user-lock", "امنیت");
            Add("OWASP Top 10", "fas fa-shield-halved", "امنیت");
            Add("2FA / TOTP", "fas fa-mobile-alt", "امنیت");
            Add("HTTPS / TLS", "fas fa-lock", "امنیت");
            Add("CORS", "fas fa-network-wired", "امنیت");
            Add("Rate Limiting", "fas fa-tachometer-alt", "امنیت");
            Add("Data Protection", "fas fa-lock", "امنیت");
            Add("SQL Injection Prevention", "fas fa-shield-virus", "امنیت");
            Add("XSS / CSRF Protection", "fas fa-shield-virus", "امنیت");

            // ===== ۸. پیام‌رسانی =====
            Add("RabbitMQ", "fas fa-envelope-open-text", "پیام‌رسانی");
            Add("Apache Kafka", "devicon-apachekafka-original colored", "پیام‌رسانی");
            Add("Azure Service Bus", "fas fa-exchange-alt", "پیام‌رسانی");
            Add("AWS SQS / SNS", "fas fa-envelope", "پیام‌رسانی");
            Add("MassTransit", "fas fa-truck", "پیام‌رسانی");
            Add("NServiceBus", "fas fa-bus", "پیام‌رسانی");
            Add("MQTT", "fas fa-satellite-dish", "پیام‌رسانی");
            Add("WebSocket", "fas fa-plug", "پیام‌رسانی");
            Add("Redis Pub/Sub", "fas fa-broadcast-tower", "پیام‌رسانی");
            Add("Event-Driven Architecture", "fas fa-stream", "پیام‌رسانی");

            // ===== ۹. کتابخانه‌های .NET =====
            Add("Serilog", "fas fa-file-alt", "کتابخانه‌های .NET");
            Add("NLog", "fas fa-file-code", "کتابخانه‌های .NET");
            Add("Polly (Resilience)", "fas fa-redo", "کتابخانه‌های .NET");
            Add("Hangfire", "fas fa-clock", "کتابخانه‌های .NET");
            Add("Quartz.NET", "fas fa-hourglass-half", "کتابخانه‌های .NET");
            Add("FluentValidation", "fas fa-check-square", "کتابخانه‌های .NET");
            Add("MediatR", "fas fa-tasks", "کتابخانه‌های .NET");
            Add("AutoMapper", "fas fa-exchange-alt", "کتابخانه‌های .NET");
            Add("Newtonsoft.Json", "fas fa-code", "کتابخانه‌های .NET");
            Add("System.Text.Json", "fas fa-code", "کتابخانه‌های .NET");
            Add("Scrutor", "fas fa-search", "کتابخانه‌های .NET");
            Add("Refit", "fas fa-exchange-alt", "کتابخانه‌های .NET");
            Add("Carter", "fas fa-rocket", "کتابخانه‌های .NET");
            Add("MailKit", "fas fa-envelope", "کتابخانه‌های .NET");

            // ===== ۱۰. معماری و الگوها =====
            Add("Clean Architecture", "fas fa-layer-group", "معماری و الگوها");
            Add("Domain-Driven Design (DDD)", "fas fa-cube", "معماری و الگوها");
            Add("CQRS", "fas fa-exchange-alt", "معماری و الگوها");
            Add("Event Sourcing", "fas fa-history", "معماری و الگوها");
            Add("Microservices", "fas fa-network-wired", "معماری و الگوها");
            Add("Modular Monolith", "fas fa-th-large", "معماری و الگوها");
            Add("Hexagonal Architecture", "fas fa-cubes", "معماری و الگوها");
            Add("SOLID Principles", "fas fa-th", "معماری و الگوها");
            Add("Repository Pattern", "fas fa-database", "معماری و الگوها");
            Add("Unit of Work", "fas fa-tasks", "معماری و الگوها");
            Add("Dependency Injection", "fas fa-syringe", "معماری و الگوها");
            Add("Result Pattern", "fas fa-check-double", "معماری و الگوها");
            Add("Specification Pattern", "fas fa-filter", "معماری و الگوها");
            Add("Design Patterns (GoF)", "fas fa-puzzle-piece", "معماری و الگوها");

            return list;
        }
    }
}