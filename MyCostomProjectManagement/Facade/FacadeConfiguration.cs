using MyCostomProjectManagement.Facade.Auth;
using MyCostomProjectManagement.Facade.ContactUs;
using MyCostomProjectManagement.Facade.FAQ;
using MyCostomProjectManagement.Facade.Portfolio;
using MyCostomProjectManagement.Facade.Role;
using MyCostomProjectManagement.Facade.Skills;
using MyCostomProjectManagement.Facade.User;

namespace MyCostomProjectManagement.Facade
{
    public static class FacadeConfiguration
    {
        public static IServiceCollection FacadeConfig(this IServiceCollection services)
        {
            services.AddScoped<IAuthFacade, AuthFacade>();
            services.AddScoped<IUserFacade, UserFacade>();
            services.AddScoped<IRoleFacade, RoleFacade>();
            services.AddScoped<ISkillFacade, SkillFacade>();
            services.AddScoped<IPortfolioFacade, PortfolioFacade>();
            services.AddScoped<IContactUsFacade, ContactUsFacade>();
            services.AddScoped<IFAQFacade, FAQFacade>();

            return services;
        }
    }
}
