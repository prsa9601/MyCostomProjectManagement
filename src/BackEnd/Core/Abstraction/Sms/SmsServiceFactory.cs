using BackEnd.Core.Abstraction.Sms.Models;
using BackEnd.Core.Abstraction.Sms.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection.Metadata.Ecma335;

namespace BackEnd.Core.Abstraction.Sms
{
    public class SmsServiceFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public SmsServiceFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<ISmsStrategy> CreateService(SmsServiceTypes type)
        {
            var service = type switch
            {
                SmsServiceTypes.sms_ir => _serviceProvider.GetRequiredService<ISms_irService>(),
                _ => _serviceProvider.GetRequiredService<ISms_irService>()
            };
            return service;
        }
    }
}
