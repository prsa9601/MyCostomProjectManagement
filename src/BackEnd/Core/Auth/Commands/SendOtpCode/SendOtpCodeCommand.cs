using BackEnd.Shared.CoreShared;
using BackEnd.Data.Entities.User.Repository;
using MediatR;
using BackEnd.Shared.Extentions;
using BackEnd.Data.Entities.User;
using BackEnd.Core.Abstraction.Sms.Models;
using BackEnd.Core.Abstraction.Sms;

namespace BackEnd.Core.Auth.Commands.SendOtpCode
{
    public class SendOtpCodeCommand : IBaseCommand
    {
        public string PhoneNumber { get; set; }
    }

    public class SendOtpCodeCommandHandler : IBaseCommandHandler<SendOtpCodeCommand>
    {
        private readonly IUserRepository _repository;
        private readonly SmsServiceFactory _factory;

        public SendOtpCodeCommandHandler(IUserRepository repository, SmsServiceFactory factory)
        {
            _repository = repository;
            _factory = factory;
        }

        public async Task<OperationResult> Handle(SendOtpCodeCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByFilterAsync(i => i.PhoneNumber == request.PhoneNumber);
            var smsService = await _factory.CreateService(SmsServiceTypes.sms_ir);

            if (user == null)
            {
                var newUser = new Data.Entities.User.User()
                {
                    PhoneNumber = request.PhoneNumber,
                };
                string newOtpCode = await RandomNumberGenerator.GenerateAsync();
                var newUserOtp = new Data.Entities.User.UserOtp(newOtpCode);
                newUser.AddOtp(newUserOtp);

                await _repository.AddAsync(newUser);
                await _repository.SaveChangeAsync();

                //await smsService.SendSms(newOtpCode);
                return OperationResult.Success();
            }
            var existOtpCode = user.UserOtps.OrderByDescending(i => i.ExpireDate).FirstOrDefault(i => i.ExpireDate > DateTime.Now);
            if (existOtpCode != null)
            {
                return OperationResult.Error($"یکبار رمز یکبار مصرف برای شما ارسال شده \n لطفا" +
                    $" {(existOtpCode.ExpireDate - DateTime.Now).TotalMinutes} دقیقه دیگر تلاش کنید.");
            }

            string otpCode = await RandomNumberGenerator.GenerateAsync();

            var userOtp = new UserOtp(otpCode);
            user.AddOtp(userOtp);
            await _repository.SaveChangeAsync();
            //await smsService.SendSms(otpCode);

            return OperationResult.Success();
        }
    }
}
