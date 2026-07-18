using BackEnd.Core.Abstraction.Sms;
using BackEnd.Core.Abstraction.Sms.Services;
using BackEnd.Infrastructure.ExternalAPIs.Sms;
using Microsoft.Extensions.DependencyInjection;

namespace BackEnd.Infrastructure.ExternalAPIs
{
    public static class ExternalAPIConfigs
    {
        public static IServiceCollection ExternalApiConfig(this IServiceCollection services)
        {
            services.AddScoped<SmsServiceFactory>();
            services.AddScoped<ISms_irService, Sms_irService>();

            return services;
        }
    }
}
