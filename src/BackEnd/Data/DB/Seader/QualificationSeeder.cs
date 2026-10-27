using BackEnd.Data.Entities.Qualification;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;



namespace BackEnd.Data.DB.Seader;

/// <summary>
/// سیدر هوشمند (Upsert) برای مدارک تخصصی.
/// بر اساس Title بررسی می‌کند:
///   - اگر موجود باشد → در صورت نیاز به‌روزرسانی می‌کند
///   - اگر نباشد → اضافه می‌کند
/// بدون تکرار و بدون از دست دادن داده‌های کاربر.
/// </summary>
public static class QualificationSeeder
{
    // ============================================================
    //  نقطه ورود — از DbInitializer صدا بزنید
    // ============================================================
    public static async Task SeedQualificationsAsync(
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
                logger?.LogWarning("⚠️ forceReseed=true — همه مدارک تخصصی حذف و دوباره اضافه می‌شوند");
                context.SpecializedQualifications.RemoveRange(context.SpecializedQualifications);
                await context.SaveChangesAsync();
            }

            await UpsertQualificationsAsync(context, logger);

            logger?.LogInformation("✅ Seed مدارک تخصصی با موفقیت تکمیل شد");
        }
        catch (Exception ex)
        {
            logger?.LogError(ex, "❌ خطا در Seed کردن مدارک تخصصی");
            throw;
        }
    }

    // ============================================================
    //  Upsert اصلی
    // ============================================================
    private static async Task UpsertQualificationsAsync(Context context, ILogger? logger)
    {
        var existing = await context.SpecializedQualifications
            .ToDictionaryAsync(x => x.Title, StringComparer.OrdinalIgnoreCase);

        var seedData = GetQualificationsSeed();
        int added = 0, updated = 0, skipped = 0;

        foreach (var item in seedData)
        {
            if (existing.TryGetValue(item.Title, out var existingItem))
            {
                bool needsUpdate =
                    existingItem.Icon != item.Icon ||
                    existingItem.Provider != item.Provider ||
                    existingItem.IsActive != item.IsActive ||
                    !TagsEqual(existingItem.Tags, item.Tags);

                if (needsUpdate)
                {
                    existingItem.Icon = item.Icon;
                    existingItem.Provider = item.Provider;
                    existingItem.Tags = item.Tags;
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
                item.Id = Guid.NewGuid();
                item.CreationDate = DateTime.Now;
                await context.SpecializedQualifications.AddAsync(item);
                added++;
            }
        }

        if (added > 0 || updated > 0)
            await context.SaveChangesAsync();

        logger?.LogInformation(
            "SpecializedQualifications → اضافه: {Added} | به‌روزرسانی: {Updated} | بدون تغییر: {Skipped}",
            added, updated, skipped);
    }

    // ============================================================
    //  مقایسه تگ‌ها (case-insensitive، بدون توجه به ترتیب)
    // ============================================================
    private static bool TagsEqual(List<string>? a, List<string>? b)
    {
        if (a is null || a.Count == 0) return b is null || b.Count == 0;
        if (b is null || b.Count == 0) return false;

        if (a.Count != b.Count) return false;

        return a.OrderBy(x => x, StringComparer.OrdinalIgnoreCase)
                .SequenceEqual(b.OrderBy(x => x, StringComparer.OrdinalIgnoreCase),
                               StringComparer.OrdinalIgnoreCase);
    }

    // ============================================================
    //  داده‌های Seed — مدارک تخصصی
    // ============================================================
    private static List<SpecializedQualification> GetQualificationsSeed() => new()
        {
            // ============ Microsoft ============
            new()
            {
                Title = "Microsoft Certified: Azure Fundamentals (AZ-900)",
                Provider = "Microsoft",
                Icon = "fab fa-microsoft",
                Tags = new() { "Azure", "Cloud", "Microsoft", "Fundamentals" },
                IsActive = true
            },
            new()
            {
                Title = "Microsoft Certified: Azure Developer Associate (AZ-204)",
                Provider = "Microsoft",
                Icon = "fab fa-microsoft",
                Tags = new() { "Azure", "Cloud", "API", "C#", ".NET" },
                IsActive = true
            },
            new()
            {
                Title = "Microsoft Certified: Azure Solutions Architect Expert (AZ-305)",
                Provider = "Microsoft",
                Icon = "fab fa-microsoft",
                Tags = new() { "Azure", "Architecture", "Cloud", "Solutions" },
                IsActive = true
            },
            new()
            {
                Title = "MCSD: App Builder",
                Provider = "Microsoft",
                Icon = "fab fa-microsoft",
                Tags = new() { "C#", ".NET", "Architecture", "Desktop", "Web" },
                IsActive = true
            },
            new()
            {
                Title = "Microsoft Certified: .NET Developer",
                Provider = "Microsoft",
                Icon = "devicon-dotnetcore-plain colored",
                Tags = new() { ".NET", "C#", "Backend", "ASP.NET Core" },
                IsActive = true
            },
            new()
            {
                Title = "SQL Server Database Development",
                Provider = "Microsoft",
                Icon = "devicon-microsoftsqlserver-plain colored",
                Tags = new() { "SQL Server", "Database", "T-SQL", "Query Tuning" },
                IsActive = true
            },

            // ============ .NET / C# ============
            new()
            {
                Title = "C# Advanced Programming",
                Provider = "Pluralsight",
                Icon = "devicon-csharp-plain colored",
                Tags = new() { "C#", "OOP", "LINQ", "Async/Await", ".NET" },
                IsActive = true
            },
            new()
            {
                Title = "ASP.NET Core Web API Development",
                Provider = "Pluralsight",
                Icon = "fas fa-plug",
                Tags = new() { "ASP.NET Core", "Web API", "REST", "JWT" },
                IsActive = true
            },
            new()
            {
                Title = "Entity Framework Core Specialist",
                Provider = "Pluralsight",
                Icon = "fas fa-database",
                Tags = new() { "EF Core", "ORM", "LINQ", "Database", "Migration" },
                IsActive = true
            },
            new()
            {
                Title = "Design Patterns in C#",
                Provider = "Udemy",
                Icon = "fas fa-puzzle-piece",
                Tags = new() { "Design Patterns", "C#", "SOLID", "OOP" },
                IsActive = true
            },
            new()
            {
                Title = "Clean Architecture & Domain-Driven Design",
                Provider = "Pluralsight",
                Icon = "fas fa-layer-group",
                Tags = new() { "Clean Architecture", "DDD", "CQRS", "MediatR" },
                IsActive = true
            },
            new()
            {
                Title = "Microservices Architecture with .NET",
                Provider = "Udemy",
                Icon = "fas fa-network-wired",
                Tags = new() { "Microservices", "Docker", "RabbitMQ", "Architecture" },
                IsActive = true
            },

            // ============ DevOps / Cloud ============
            new()
            {
                Title = "Docker Certified Associate (DCA)",
                Provider = "Docker / Mirantis",
                Icon = "devicon-docker-plain colored",
                Tags = new() { "Docker", "DevOps", "Container", "Compose" },
                IsActive = true
            },
            new()
            {
                Title = "Certified Kubernetes Administrator (CKA)",
                Provider = "CNCF",
                Icon = "devicon-kubernetes-plain colored",
                Tags = new() { "Kubernetes", "DevOps", "Container", "Orchestration" },
                IsActive = true
            },
            new()
            {
                Title = "AWS Certified Developer – Associate",
                Provider = "Amazon Web Services",
                Icon = "devicon-amazonwebservices-plain-wordmark colored",
                Tags = new() { "AWS", "Cloud", "Lambda", "S3", "DevOps" },
                IsActive = true
            },

            // ============ Testing / Quality ============
            new()
            {
                Title = "Unit Testing & xUnit Professional",
                Provider = "Pluralsight",
                Icon = "fas fa-vial",
                Tags = new() { "xUnit", "Testing", "Moq", "Quality" },
                IsActive = true
            },

            // ============ Security ============
            new()
            {
                Title = "JWT Authentication & Authorization",
                Provider = "Udemy",
                Icon = "fas fa-key",
                Tags = new() { "JWT", "Authentication", "Security", "ASP.NET Core" },
                IsActive = true
            },
            new()
            {
                Title = "OWASP Top 10 — Security Best Practices",
                Provider = "OWASP Foundation",
                Icon = "fas fa-shield-halved",
                Tags = new() { "Security", "OWASP", "XSS", "SQL Injection", "CSRF" },
                IsActive = true
            },

            // ============ Project Management ============
            new()
            {
                Title = "Professional Scrum Master I (PSM I)",
                Provider = "Scrum.org",
                Icon = "fas fa-users",
                Tags = new() { "Scrum", "Agile", "Project Management" },
                IsActive = true
            },

            // ============ Git / Version Control ============
            new()
            {
                Title = "Git & GitHub Professional",
                Provider = "Udemy",
                Icon = "devicon-git-plain colored",
                Tags = new() { "Git", "GitHub", "Version Control", "CI/CD" },
                IsActive = true
            }
        };
}