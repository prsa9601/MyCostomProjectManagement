using BackEnd.Data.Entities.Role;
using BackEnd.Data.Entities.Role.Repository;
using BackEnd.Shared.CoreShared;

namespace BackEnd.Core.Role.Commands._RolePermission.SetPermission
{
    public class SetRolePermissionCommand : IBaseCommand
    {
        public Guid RoleId { get; set; }
        public Permissions Prmissions { get; set; }
    }

    public class SetRolePermissionCommandHandler : IBaseCommandHandler<SetRolePermissionCommand>
    {
        private readonly IRoleRepository _repository;

        public SetRolePermissionCommandHandler(IRoleRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(SetRolePermissionCommand request, CancellationToken cancellationToken)
        {

            return OperationResult.Success();
        }
    }
}
