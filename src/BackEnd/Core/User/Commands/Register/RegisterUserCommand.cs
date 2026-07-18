using BackEnd.Data.Entities.User.Repository;
using BackEnd.Infrastructure.Security.Hash.service;
using BackEnd.Infrastructure.Security.Hash.strategies;
using BackEnd.Shared.CoreShared;

namespace BackEnd.Core.User.Commands.Register
{
    public class RegisterUserCommand : IBaseCommand
    {
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; }
    }

    public class RegisterUserCommandHandler : IBaseCommandHandler<RegisterUserCommand>
    {
        private readonly IUserRepository _repository;
        private readonly HashManager _hashManager;

        public RegisterUserCommandHandler(IUserRepository repository)
        {
            _repository = repository;
            _hashManager = new HashManager(new Sha256Hasher());
        }

        public async Task<OperationResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByFilterAsync(i => i.PhoneNumber == request.PhoneNumber);
            if (user == null) return OperationResult.NotFound();
            if (user.PhoneNumberIsVerify == false) return OperationResult.Error("لطفا درخواست رمز یکبار مصرف بدهید.");

            var userOtpSession = user.UserOtpSessions.OrderByDescending(i => i.ExpireDate)
                .FirstOrDefault(i => i.ExpireDate > DateTime.Now && i.IsActive == true);

            if (userOtpSession == null) return OperationResult.NotFound("لطفا ابتدا درخواست رمز عبور یکبار مصرف بدهید.");

            if (user.FullName != null) return OperationResult.BadRequest();

            string hashPassword = _hashManager.Hash(request.Password);
            user.Register(request.FullName, request.Email, hashPassword);
            userOtpSession.InActive();
            await _repository.SaveChangeAsync();

            return OperationResult.Success();
        }
    }
}
