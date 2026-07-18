using BackEnd.Data.Entities.User;
using BackEnd.Data.Entities.User.Repository;
using BackEnd.Shared.CoreShared;
using System.Globalization;

namespace BackEnd.Core.Auth.Commands.VerifyOtpCode
{
    public class VerifyOtpCodeCommand : IBaseCommand
    {
        public string PhoneNumber { get; set; }
        public string Token { get; set; }
    }

    public class VerifyOtpCodeCommandHandler : IBaseCommandHandler<VerifyOtpCodeCommand>
    {
        private readonly IUserRepository _repository;

        public VerifyOtpCodeCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(VerifyOtpCodeCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByFilterAsync(i => i.PhoneNumber.Equals(request.PhoneNumber));
            if (user == null) return OperationResult.NotFound("اول درخواست ارسال کد یکبار مصرف بدهید.");

            var userOtp = user.UserOtps.OrderByDescending(i => i.ExpireDate).FirstOrDefault(i => i.ExpireDate > DateTime.Now);
            if (userOtp == null) return OperationResult.NotFound("رمز یکبار مصرف منقضی شده لطفا مجددا درخواست رمز یکبار مصرف بدهید.");

            if (userOtp.Token != request.Token)
                return OperationResult.Error("رمز یکبار مصرف وارد شده اشتباه است.");

            var userOtpSession = new UserOtpSession(request.Token);
            user.AddOtpSession(userOtpSession);
            userOtp.InASctive();
            if (user.UserOtps.Count == 1)
            {
                user.VerifyPhoneNumber();
            }

            await _repository.SaveChangeAsync();
            return OperationResult.Success();
        }
    }
}
