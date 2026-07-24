using BackEnd.Data.Entities.Skills;
using MediatR;
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

            // بررسی کن که آیا جدول Coupon (یا هر جدول دیگر) خالی است
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
        }
    }
}