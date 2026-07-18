using BackEnd.Core.Abstraction.Sms.Models;
using BackEnd.Core.Abstraction.Sms.Services;

namespace BackEnd.Infrastructure.ExternalAPIs.Sms
{
    public sealed class Sms_irService : ISms_irService
    {
        public Task<SendSmsRespose> SendSms(string message)
        {
            return default;
        }
    }
}
