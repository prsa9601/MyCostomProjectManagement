using BackEnd.Core.User.Commands.Login;
using BackEnd.Core.User.Commands.Register;
using BackEnd.Core.User.Commands.SetUserRole;
using BackEnd.Core.User.Queries.DTOs;
using BackEnd.Core.User.Queries.DTOs.UserFilterDto;
using BackEnd.Core.User.Queries.GetFilter;
using BackEnd.Core.User.Queries.GetId;
using BackEnd.Shared.CoreShared;
using MediatR;

namespace MyCostomProjectManagement.Facade.User
{
    public interface IUserFacade
    {
        Task<OperationResult> Register(RegisterUserCommand command);
        Task<OperationResult<LoginUserCommandResponse>> Login(LoginUserCommand command);
        Task<OperationResult> SetUserRole(SetUserRoleCommand command);
        
        Task<UserDto> GetId(Guid userId);
        Task<UserFilterResult> GetFilter(UserFilterParam param);
    }
    public class UserFacade : IUserFacade
    {
        private readonly IMediator _mediator;

        public UserFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<UserFilterResult> GetFilter(UserFilterParam param)
        {
            return await _mediator.Send(new GetFilterUserQuery(param));
        }

        public async Task<UserDto> GetId(Guid userId)
        {
            return await _mediator.Send(new GetUserByIdQuery(userId));
        }

        public async Task<OperationResult<LoginUserCommandResponse>> Login(LoginUserCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> Register(RegisterUserCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> SetUserRole(SetUserRoleCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
