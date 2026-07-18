using BackEnd.Data.Entities.Role.Repository;
using BackEnd.Data.Entities.User.Repository;
using BackEnd.Infrastructure.Repositories.Role;
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

            return services;
        }
    }
}
