using BackEnd.Data.Entities.Role;
using BackEnd.Data.Entities.Role.Repository;
using BackEnd.Shared.CoreShared;

namespace BackEnd.Core.Role.Commands.Edit
{
    public record class EditRloeCommand(Guid roleId, string title, List<RolePermission> rolePermissions) : IBaseCommand;
    
    public sealed class EditRloeCommandHandler : IBaseCommandHandler<EditRloeCommand>
    {
        public readonly IRoleRepository _repository;

        public EditRloeCommandHandler(IRoleRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(EditRloeCommand request, CancellationToken cancellationToken)
        {
            var role = await _repository.GetTracking(request.roleId);
            if (role == null) return OperationResult.NotFound();

            role.Edit(request.title, request.rolePermissions);
            await _repository.SaveChangeAsync();
            return OperationResult.Success();
        }
    }
}
