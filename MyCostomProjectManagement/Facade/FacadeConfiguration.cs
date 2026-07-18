using MyCostomProjectManagement.Facade.Auth;

namespace MyCostomProjectManagement.Facade
{
    public static class FacadeConfiguration
    {
        public static IServiceCollection FacadeConfig(this IServiceCollection services)
        {
            services.AddScoped<IAuthFacade, AuthFacade>();

            return services;
        }
    }
}
