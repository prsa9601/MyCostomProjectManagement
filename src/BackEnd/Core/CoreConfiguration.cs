using BackEnd.Core.Abstraction.Jwt.Interfaces;
using BackEnd.Core.User.Commands.Register;
using BackEnd.Data.DB;
using BackEnd.Infrastructure.Auth.Jwt;
using BackEnd.Infrastructure.ExternalAPIs;
using BackEnd.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BackEnd.Core
{
    public static class CoreConfiguration 
    {
        public static IServiceCollection CoreConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(CoreConfiguration));
            //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            //services.AddAutoMapper();
            //MediatR
            //services.AddMediatR(cfg =>
            //{
            //    cfg.RegisterServicesFromAssemblies(
            //        typeof(CoreConfiguration).Assembly,
            //        typeof(RegisterUserCommandHandler).Assembly
            //    );
            //});
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CoreConfiguration).Assembly));

            services.AddScoped<IJwtSettingsFactory, JwtSettingsFactory>();

            services.DBConfig(configuration);
            services.RepositoryConfig();
            services.ExternalApiConfig();

            return services;
        }
    }
}
