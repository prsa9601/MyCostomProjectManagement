using BackEnd.Core.User.Commands.Login;
using BackEnd.Core.User.Commands.Register;
using BackEnd.Shared.CoreShared;
using MediatR;

namespace MyCostomProjectManagement.Facade.User
{
    public interface IUserFacade
    {
        Task<OperationResult> Register(RegisterUserCommand command);
        Task<OperationResult<LoginUserCommandResponse>> Login(LoginUserCommand command);
    }
    public class UserFacade : IUserFacade
    {
        private readonly IMediator _mediator;

        public UserFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<OperationResult<LoginUserCommandResponse>> Login(LoginUserCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> Register(RegisterUserCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
