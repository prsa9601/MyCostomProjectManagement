using BackEnd.Data.Entities.Role;
using BackEnd.Data.Entities.Skills;
using BackEnd.Data.Entities.User;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Data.DB.Initializer
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<Context>();
            var mediatR = scope.ServiceProvider.GetRequiredService<IMediator>();

            // اگر دیتابیس وجود نداشته باشد، ایجادش کن
            await context.Database.EnsureCreatedAsync();

            if (!context.Roles.Any())
            {
                List<Permissions> permissions = new List<Permissions>
                {
                    Permissions.AccessToAdminPanel,
                    Permissions.CreateApi,
                    Permissions.CreateContactUs,
                    Permissions.CreateCustomProject,
                    Permissions.CreateFAQ,
                    Permissions.CreatePageManagement,
                    Permissions.CreatePortfolio,
                    Permissions.CreateRole,
                    Permissions.CreateSiteSetting,
                    Permissions.CreateSkills,
                    Permissions.CreateUser,
                    Permissions.DeleteApi,
                    Permissions.DeleteContactUs,
                    Permissions.DeleteCustomProject,
                    Permissions.DeleteFAQ,
                    Permissions.DeletePortfolio,
                    Permissions.DeleteRole,
                    Permissions.DeleteSiteSetting,
                    Permissions.DeleteSkills,
                    Permissions.DeleteUser,
                    Permissions.EditApi,
                    Permissions.EditContactUs,
                    Permissions.EditCustomProject,
                    Permissions.EditFAQ,
                    Permissions.EditPageManagement,
                    Permissions.EditPortfolio,
                    Permissions.EditRole,
                    Permissions.EditSiteSetting,
                    Permissions.EditSkills,
                    Permissions.EditUser,
                    Permissions.GetApi,
                    Permissions.GetContactUs,
                    Permissions.GetCustomProject,
                    Permissions.GetFAQ,
                    Permissions.GetPageManagement,
                    Permissions.GetPortfolio,
                    Permissions.GetRole,
                    Permissions.GetSiteSetting,
                    Permissions.GetSkills,
                    Permissions.GetUser,
                };

                List<RolePermission> rolePermissions = new();
                foreach (var item in permissions)
                {
                    rolePermissions.Add(new RolePermission()
                    {
                        Permissions = item,
                    });
                }
                var programmerRole = new Role("Programmer", rolePermissions, "fa-users-cog", "#dc3545", "برنامه نویس سایت");
                await context.Roles.AddAsync(programmerRole);
                await context.SaveChangesAsync();
            }
            if (!context.Users.Any())
            {
                var role = await context.Roles.FirstOrDefaultAsync(i => i.Name.Equals("Programmer"));
                var user = new User()
                {
                    PhoneNumber = "09368823398",
                    FullName = "محمد پارسا کریمی",
                    Email = "parsa9601m@gmail.com",
                    PhoneNumberIsVerify = true,
                };

                user.AddUserRoles(role.Id);
                await context.AddAsync(user);
                await context.SaveChangesAsync();
            }

            if (!context.TechnicalSkills.Any())
            {
                var allSkills = new List<TechnicalSkills>
                {
                    //new TechnicalSkills ( "React", 90, "fab fa-react", SkillTypes.FrontEnd, false),
                    //new TechnicalSkills ( "Vue.js", 85, "fab fa-react", SkillTypes.FrontEnd, false),
                    //new TechnicalSkills ( "Node.js", 80, "fas fa-server", SkillTypes.BackEnd,false),
                    //new TechnicalSkills ( "Python", 75, "fas fa-server", SkillTypes.BackEnd, true),
                    //new TechnicalSkills ( "c#", 100, "fas fa-server", SkillTypes.BackEnd, true),
                    //new TechnicalSkills ( "Asp.Net Core", 95, "fas fa-server", SkillTypes.BackEnd, true),
                    //new TechnicalSkills ( "MongoDB", 85, "fas fa-database", SkillTypes.DataBase, true),
                    //new TechnicalSkills ( "PostgreSQL", 70, "fas fa-database", SkillTypes.DataBase,true),
                    //new TechnicalSkills ( "AWS", 65, "fas fa-cloud", SkillTypes.DevOps,false),
                    //new TechnicalSkills ( "Docker", 60, "fas fa-cloud", SkillTypes.DevOps,true),
                    //new TechnicalSkills ( "TypeScript", 88, "fab fa-react",SkillTypes.FrontEnd,false),
                    //new TechnicalSkills ( "GraphQL", 55, "fas fa-server", SkillTypes.BackEnd,true),
                    //new TechnicalSkills ( "Redis", 45, "fas fa-database", SkillTypes.DataBase,true),
                    //new TechnicalSkills ( "Kubernetes", 40, "fas fa-cloud", SkillTypes.DevOps,false),
                    //new TechnicalSkills ( "Figma", 70, "fas fa-code", SkillTypes.Others,false),
                    //new TechnicalSkills ( "Git", 95, "fas fa-code", SkillTypes.Others,true)
                    // ========== BackEnd ==========
                     new TechnicalSkills ( "C#", 100, "fas fa-code", SkillTypes.BackEnd, true),
                     new TechnicalSkills ( "ASP.NET Core", 95, "fas fa-server", SkillTypes.BackEnd, true),
                     new TechnicalSkills ( "ASP.NET Core Web API", 90, "fas fa-server", SkillTypes.BackEnd, false),
                     new TechnicalSkills ( "EF Core", 90, "fas fa-database", SkillTypes.BackEnd, false),
                     new TechnicalSkills ( "LINQ", 90, "fas fa-code", SkillTypes.BackEnd, false),
                     new TechnicalSkills ( "JWT", 85, "fas fa-key", SkillTypes.BackEnd, false),
                     new TechnicalSkills ( "CQRS", 80, "fas fa-code-branch", SkillTypes.BackEnd, false),
                     new TechnicalSkills ( "Clean Architecture", 85, "fas fa-layer-group", SkillTypes.BackEnd, true),
                     new TechnicalSkills ( "SOLID Principles", 80, "fas fa-cubes", SkillTypes.BackEnd, false),
                     new TechnicalSkills ( "Identity Framework", 80, "fas fa-user-lock", SkillTypes.BackEnd, false),
                     new TechnicalSkills ( "Authorization & Authentication", 85, "fas fa-shield-alt", SkillTypes.BackEnd, false),
                     new TechnicalSkills ( "OOP", 90, "fas fa-object-group", SkillTypes.BackEnd, false),
                     new TechnicalSkills ( "Repository Pattern", 85, "fas fa-folder-open", SkillTypes.BackEnd, false),
                     new TechnicalSkills ( "UnitOfWork", 75, "fas fa-database", SkillTypes.BackEnd, false),
                     new TechnicalSkills ( "DDD", 75, "fas fa-cubes", SkillTypes.BackEnd, false),
                     new TechnicalSkills ( "Rich Domain Model", 70, "fas fa-cube", SkillTypes.BackEnd, false),
                     new TechnicalSkills ( "Microservices", 75, "fas fa-network-wired", SkillTypes.BackEnd, false),
                     new TechnicalSkills ( "Multi-Tenant", 70, "fas fa-users", SkillTypes.BackEnd, false),
                     new TechnicalSkills ( "Middleware", 75, "fas fa-plug", SkillTypes.BackEnd, false),
                     new TechnicalSkills ( "RESTful API", 90, "fas fa-code", SkillTypes.BackEnd, true),
                     new TechnicalSkills ( "gRPC", 55, "fas fa-exchange-alt", SkillTypes.BackEnd, false),
                     new TechnicalSkills ( "RabbitMQ", 70, "fas fa-exchange-alt", SkillTypes.BackEnd, false),
                     new TechnicalSkills ( "Hangfire", 65, "fas fa-clock", SkillTypes.BackEnd, false),
                     new TechnicalSkills ( "XUnit Test", 70, "fas fa-flask", SkillTypes.BackEnd, false),
                     new TechnicalSkills ( "Swagger/OpenAPI", 85, "fas fa-file-alt", SkillTypes.BackEnd, false),
                     new TechnicalSkills ( "MemoryCache", 65, "fas fa-memory", SkillTypes.BackEnd, false),
                     new TechnicalSkills ( "Rate Limiting", 60, "fas fa-tachometer-alt", SkillTypes.BackEnd, false),
                     new TechnicalSkills ( "TokenBucket Algorithm", 60, "fas fa-fill-drip", SkillTypes.BackEnd, false),
                     new TechnicalSkills ( "Factory Method Pattern", 70, "fas fa-cog", SkillTypes.BackEnd, false),
                     new TechnicalSkills ( "ConcurrentQueue", 65, "fas fa-tasks", SkillTypes.BackEnd, false),
                     new TechnicalSkills ( "SemaphoreSlim", 60, "fas fa-lock", SkillTypes.BackEnd, false),
                     new TechnicalSkills ( "Lock-Free Collection", 55, "fas fa-unlock", SkillTypes.BackEnd, false),
                     new TechnicalSkills ( "Flash/Auto-Flash System", 55, "fas fa-bolt", SkillTypes.BackEnd, false),
                     new TechnicalSkills ( "NBoMber (Load Test)", 50, "fas fa-chart-line", SkillTypes.BackEnd, false),
                     
                     // ========== DataBase ==========
                     new TechnicalSkills ( "SQL Server", 85, "fas fa-database", SkillTypes.DataBase, true),
                     new TechnicalSkills ( "MongoDB", 70, "fas fa-database", SkillTypes.DataBase, false),
                     new TechnicalSkills ( "Redis", 60, "fas fa-database", SkillTypes.DataBase, false),
                     new TechnicalSkills ( "Dapper", 70, "fas fa-database", SkillTypes.DataBase, false),
                     
                     // ========== FrontEnd ==========
                     new TechnicalSkills ( "Razor Pages", 80, "fas fa-file-code", SkillTypes.FrontEnd, false),
                     new TechnicalSkills ( "HTML/CSS", 70, "fab fa-html5", SkillTypes.FrontEnd, false),
                     new TechnicalSkills ( "JavaScript", 75, "fab fa-js", SkillTypes.FrontEnd, false),
                     
                     // ========== DevOps & Tools ==========
                     new TechnicalSkills ( "Docker", 60, "fab fa-docker", SkillTypes.DevOps, false),
                     new TechnicalSkills ( "Git", 90, "fab fa-git-alt", SkillTypes.DevOps, true),
                     new TechnicalSkills ( "GitHub", 85, "fab fa-github", SkillTypes.DevOps, false),
                     
                     // ========== Others ==========
                     new TechnicalSkills ( "Postman", 75, "fas fa-paper-plane", SkillTypes.Others, false)
                };



                await context.TechnicalSkills.AddRangeAsync(allSkills);

                await context.SaveChangesAsync();
            }
            await SetSiteSetting.SiteSettingInitializeAsync(serviceProvider);
        }
    }
}