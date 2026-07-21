using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BackEnd.Data.DB
{
    public static class DataBaseConfiguration
    {
        public static IServiceCollection DBConfig(this IServiceCollection services
            , IConfiguration configuration)
        {
           // services.AddDbContext<Context>(options =>
           //options.UseSqlServer(configuration.GetSection("ConnectionStrings")["DefaultConnection"]));

            services.AddDbContextFactory<Context>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                // سایر تنظیمات
            });
            ////MediatR
            //services.AddMediatR(cfg =>
            //{
            //    cfg.RegisterServicesFromAssemblies(
            //        typeof(Directories).Assembly,
            //        typeof(RegisterUserCommandHandler).Assembly
            //    );
            //});

            //configuration.GetConnectionString("DefaultConnection");
            return services;
        }
    }
}
