using BackEnd.Data.Entities.Role;
using BackEnd.Data.Entities.Role.Repository;
using BackEnd.Shared.CoreShared;

namespace BackEnd.Core.Role.Commands.Edit
{
    public record class EditRoleCommand(Guid roleId, string title, List<RolePermission> rolePermissions,
        string Icon, string IconColorCode, string Description, bool isDefault) : IBaseCommand;
    
    public sealed class EditRloeCommandHandler : IBaseCommandHandler<EditRoleCommand>
    {
        public readonly IRoleRepository _repository;

        public EditRloeCommandHandler(IRoleRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(EditRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _repository.GetTracking(request.roleId);
            if (role == null) return OperationResult.NotFound();

            role.Edit(request.title, request.rolePermissions, 
                request.Icon, request.IconColorCode, request.Description);
            await _repository.SaveChangeAsync();
            return OperationResult.Success();
        }
    }
}
