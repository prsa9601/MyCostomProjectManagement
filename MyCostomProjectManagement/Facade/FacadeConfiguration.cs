using MyCostomProjectManagement.Facade.Auth;
using MyCostomProjectManagement.Facade.User;

namespace MyCostomProjectManagement.Facade
{
    public static class FacadeConfiguration
    {
        public static IServiceCollection FacadeConfig(this IServiceCollection services)
        {
            services.AddScoped<IAuthFacade, AuthFacade>();
            services.AddScoped<IUserFacade, UserFacade>();

            return services;
        }
    }
}
