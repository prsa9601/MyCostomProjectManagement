using BackEnd.Data.Entities.User.Repository;
using BackEnd.Shared.CoreShared;
using System.Net.WebSockets;

namespace BackEnd.Core.User.Commands.SetUserRole
{
    public class SetUserRoleCommand : IBaseCommand
    {
        public Guid UserId { get; set; }
        public List<Guid> RoleIds { get; set; }
    }
    public class SetUserRoleCommandHandler : IBaseCommandHandler<SetUserRoleCommand>
    {
        private readonly IUserRepository _repository;

        public SetUserRoleCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(SetUserRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetTracking(request.UserId);
            if (user == null) return OperationResult.NotFound();

            user.SetUserRoles(request.RoleIds);

            await _repository.SaveChangeAsync();
            return OperationResult.Success();
        }
    }
}
