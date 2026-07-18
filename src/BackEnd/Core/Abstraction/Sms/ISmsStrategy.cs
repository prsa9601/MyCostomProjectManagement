using BackEnd.Core.Abstraction.Sms.Models;

namespace BackEnd.Core.Abstraction.Sms
{
    public interface ISmsStrategy
    {
        Task<SendSmsRespose> SendSms(string message);
    }
}
