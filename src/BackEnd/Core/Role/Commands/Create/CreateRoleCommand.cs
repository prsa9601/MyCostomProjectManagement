using BackEnd.Data.Entities.Role;
using BackEnd.Data.Entities.Role.Repository;
using BackEnd.Shared.CoreShared;

namespace BackEnd.Core.Role.Commands.Create
{
    public record class CreateRoleCommand(string title, List<RolePermission> rolePermissions,
        string Icon, string IconColorCode, string Description, bool isDefault) : IBaseCommand;
    

    public class CreateRoleCommandHandler : IBaseCommandHandler<CreateRoleCommand>
    {
        private readonly IRoleRepository _repository;

        public CreateRoleCommandHandler(IRoleRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = new BackEnd.Data.Entities.Role.Role(request.title, request.rolePermissions,
                request.Icon, request.IconColorCode, request.Description);

            await _repository.AddAsync(role);
            await _repository.SaveChangeAsync();
            return OperationResult.Success();
        }
    }
}
