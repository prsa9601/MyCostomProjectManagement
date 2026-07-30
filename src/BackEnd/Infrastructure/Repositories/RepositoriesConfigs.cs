using BackEnd.Data.Entities.ContactUs.Repository;
using BackEnd.Data.Entities.FAQ.Repository;
using BackEnd.Data.Entities.Portfolio.Repository;
using BackEnd.Data.Entities.Role.Repository;
using BackEnd.Data.Entities.SiteSettings.Repository;
using BackEnd.Data.Entities.Skills.Repository;
using BackEnd.Data.Entities.User.Repository;
using BackEnd.Infrastructure.Repositories.ContactUs;
using BackEnd.Infrastructure.Repositories.FAQ;
using BackEnd.Infrastructure.Repositories.Portfolio;
using BackEnd.Infrastructure.Repositories.Role;
using BackEnd.Infrastructure.Repositories.SiteSettings;
using BackEnd.Infrastructure.Repositories.Skills;
using BackEnd.Infrastructure.Repositories.User;
using Microsoft.Extensions.DependencyInjection;

namespace BackEnd.Infrastructure.Repositories
{
    public static class RepositoriesConfigs
    { 
        public static IServiceCollection RepositoryConfig(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IPortfolioRepository, PortfolioRepository>();
            services.AddScoped<ISkillRepository, SkillRepository>();
            services.AddScoped<IContactUsRepository, ContactUsRepository>();
            services.AddScoped<IFAQRepository, FAQRepository>();
            services.AddScoped<ISiteSettingRepository, SiteSettingRepository>();

            return services;
        }
    }
}
