using AutoMapper;
using BackEnd.Core.Abstraction.Cookies.Interfaces;
using BackEnd.Core.Abstraction.Cookies.Services;
using BackEnd.Core.Abstraction.Jwt.Interfaces;
using BackEnd.Core.ContactUs.Queries.Mappers;
using BackEnd.Core.FAQ.Queries.Mapper;
using BackEnd.Core.Portfolio.Queries.Mappers;
using BackEnd.Core.Portfolio.Resolver;
using BackEnd.Core.Project.Queries.ProjectRequestV1.Mappers;
using BackEnd.Core.Role.Queries.Mappers;
using BackEnd.Core.SiteSetting.Queries.Mappers;
using BackEnd.Core.Skills.Queries.Mappers;
using BackEnd.Core.Subscription;
using BackEnd.Core.User.Commands.Register;
using BackEnd.Core.User.Queries.GetId;
using BackEnd.Core.User.Queries.Mappers;
using BackEnd.Data.DB;
using BackEnd.Infrastructure.Auth.Jwt;
using BackEnd.Infrastructure.ExternalAPIs;
using BackEnd.Infrastructure.Repositories;
using BackEnd.Infrastructure.Security.Hash.service;
using BackEnd.Infrastructure.Security.Hash.strategies;
using BackEnd.Shared.Utilities.FileUtil;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BackEnd.Core
{
    public static class CoreConfiguration
    {
        public static IServiceCollection CoreConfig(this IServiceCollection services, IConfiguration configuration)
        {

            //services.AddAutoMapper(typeof(CoreConfiguration));
            //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
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
            services.AddScoped<IFileService, FileService>();

            services.AddScoped<IJwtSettingsFactory, JwtSettingsFactory>();
            services.AddScoped<IHashStrategy, Sha256Hasher>();
            services.AddScoped<HashManager>();
            services.AddScoped<ICookiesService, CookieService>();

            services.AddScoped<UserRolesResolver>();
            services.AddScoped<SubscriptionService>();
            services.AddScoped<SubscriptionUserService>();
            services.AddScoped<PortfolioFileAutoMapperResolver>();

            services.DBConfig(configuration);
            services.RepositoryConfig();
            services.ExternalApiConfig();

            var loggerFactory = services.BuildServiceProvider().GetRequiredService<ILoggerFactory>();
            var serviceProvider = services.BuildServiceProvider();
            // ثبت AutoMapper به روش دستی
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UserAutoMapperProfile>();
                cfg.AddProfile<RoleAutoMapperProfile>();
                cfg.AddProfile<SkillAutoMapperProfile>();
                cfg.AddProfile<SiteSettingAutoMapperProfile>();
                cfg.AddProfile<ContactUsAutoMapperProfile>();
                cfg.AddProfile<PortfolioAutoMapperProfile>();
                cfg.AddProfile<FAQAutoMapperProfile>();

                cfg.AddProfile<ProjectRequestV1AutoMapperProfile>();
                cfg.ConstructServicesUsing(serviceProvider.GetService);
            }, loggerFactory);

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);


            return services;
        }
    }
}
