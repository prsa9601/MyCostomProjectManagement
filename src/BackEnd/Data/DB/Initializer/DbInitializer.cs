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
                var role = await context.Roles.FirstOrDefaultAsync(i=>i.Name.Equals("Programmer"));
                var user = new User() 
                {
                    PhoneNumber = "09368823398",
                    FullName="محمد پارسا کریمی",
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
                    new TechnicalSkills ( "React", 90, "fab fa-react", SkillTypes.FrontEnd, false),
                    new TechnicalSkills ( "Vue.js", 85, "fab fa-react", SkillTypes.FrontEnd, false),
                    new TechnicalSkills ( "Node.js", 80, "fas fa-server", SkillTypes.BackEnd,false),
                    new TechnicalSkills ( "Python", 75, "fas fa-server", SkillTypes.BackEnd, true),
                    new TechnicalSkills ( "c#", 100, "fas fa-server", SkillTypes.BackEnd, true),
                    new TechnicalSkills ( "Asp.Net Core", 95, "fas fa-server", SkillTypes.BackEnd, true),
                    new TechnicalSkills ( "MongoDB", 85, "fas fa-database", SkillTypes.DataBase, true),
                    new TechnicalSkills ( "PostgreSQL", 70, "fas fa-database", SkillTypes.DataBase,true),
                    new TechnicalSkills ( "AWS", 65, "fas fa-cloud", SkillTypes.DevOps,false),
                    new TechnicalSkills ( "Docker", 60, "fas fa-cloud", SkillTypes.DevOps,true),
                    new TechnicalSkills ( "TypeScript", 88, "fab fa-react",SkillTypes.FrontEnd,false),
                    new TechnicalSkills ( "GraphQL", 55, "fas fa-server", SkillTypes.BackEnd,true),
                    new TechnicalSkills ( "Redis", 45, "fas fa-database", SkillTypes.DataBase,true),
                    new TechnicalSkills ( "Kubernetes", 40, "fas fa-cloud", SkillTypes.DevOps,false),
                    new TechnicalSkills ( "Figma", 70, "fas fa-code", SkillTypes.Others,false),
                    new TechnicalSkills ( "Git", 95, "fas fa-code", SkillTypes.Others,true)
                };



                await context.TechnicalSkills.AddRangeAsync(allSkills);

                await context.SaveChangesAsync();
            }
            await SetSiteSetting.SiteSettingInitializeAsync(serviceProvider);
        }
    }
}