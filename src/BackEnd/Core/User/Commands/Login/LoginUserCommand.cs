using BackEnd.Core.Abstraction.Jwt.Enum;
using BackEnd.Core.Abstraction.Jwt.Interfaces;
using BackEnd.Data.Entities.User;
using BackEnd.Data.Entities.User.Repository;
using BackEnd.Infrastructure.Security.Hash.service;
using BackEnd.Infrastructure.Security.Hash.strategies;
using BackEnd.Shared.CoreShared;

namespace BackEnd.Core.User.Commands.Login
{
    public class LoginUserCommand : IBaseCommand<LoginUserCommandResponse>
    {
        public string PhoneNumber { get; set; }
    }

    public class LoginUserCommandHandler : IBaseCommandHandler<LoginUserCommand, LoginUserCommandResponse>
    {
        private readonly IUserRepository _repository;
        private readonly IJwtSettingsFactory _jwtSettingsFactory;
        private readonly HashManager _hashManager;

        public LoginUserCommandHandler(IUserRepository repository, IJwtSettingsFactory jwtSettingsFactory)
        {
            _repository = repository;
            _jwtSettingsFactory = jwtSettingsFactory;
            _hashManager = new HashManager(new Sha256Hasher());
        }

        //کارهای جی دبلیو تی توکن و سشن کاربر انجام شود
        public async Task<OperationResult<LoginUserCommandResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByFilterWithIncludsAsync(i => i.PhoneNumber == request.PhoneNumber,
                "UserOtpSessions");
            if (user == null) return OperationResult<LoginUserCommandResponse>.NotFound();
            if (user.PhoneNumberIsVerify == false) return OperationResult<LoginUserCommandResponse>.Error("لطفا درخواست رمز یکبار مصرف بدهید.");

            var userOtpSession = user.UserOtpSessions.OrderByDescending(i => i.ExpireDate)
                .FirstOrDefault(i => i.ExpireDate > DateTime.Now && i.IsActive == true && i.UserId == user.Id);

            if (userOtpSession == null) return OperationResult<LoginUserCommandResponse>.NotFound("لطفا ابتدا درخواست رمز عبور یکبار مصرف بدهید.");

            if (user.FullName == null) return OperationResult<LoginUserCommandResponse>.BadRequest();

            var authTokenService = _jwtSettingsFactory.CreateSetting(TokenType.AuthToken);
            var refreshTokenService = _jwtSettingsFactory.CreateSetting(TokenType.AuthRefreshToken);

            if (authTokenService == null || refreshTokenService == null) return OperationResult<LoginUserCommandResponse>.Error("ارور سمت سرور لطفا مجددا تلاش کنید.");

            string authToken = authTokenService.GenerateToken(user.Id, user.PhoneNumber, null);
            string refreshToken = refreshTokenService.GenerateToken(user.Id, user.PhoneNumber, null);

            var userSession = new UserSession(_hashManager.Hash(refreshToken), refreshTokenService.GetExpireDate(refreshToken));
            userSession.ActivatingSession();
            user.AddSession(userSession);

            userOtpSession.InActive();
            await _repository.SaveChangeAsync();
           
            return OperationResult<LoginUserCommandResponse>.Success(new LoginUserCommandResponse
            {
                AuthToken = authToken,
                RefreshToken = refreshToken,
            });
        }
    }
}
