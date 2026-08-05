using BackEnd.Core.Role.Commands.Create;
using BackEnd.Core.Role.Commands.Edit;
using BackEnd.Core.Role.Commands.Remove;
using BackEnd.Core.Role.Queries.DTOs;
using BackEnd.Core.Role.Queries.GetAll;
using BackEnd.Core.Role.Queries.GetFilter;
using BackEnd.Core.Role.Queries.GetRoles;
using BackEnd.Shared.CoreShared;
using MediatR;
using MyCostomProjectManagement.Shared.Attributes;

namespace MyCostomProjectManagement.Facade.Role
{
    public interface IRoleFacade
    {
        Task<OperationResult> Create(CreateRoleCommand command);
        Task<OperationResult> Edit(EditRoleCommand command);
        Task<OperationResult> Remove(RemoveRoleCommand command);

        Task<List<RoleDto>> GetRolesByRoleIds(List<Guid> roleIds);
        Task<RoleFilterResult> GetFilter(RoleFilterParam param);
        Task<List<RoleDto>> GetAll();
    }
    public class RoleFacade : IRoleFacade
    {
        private readonly IMediator _mediator;

        public RoleFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        [PermissionChecker(BackEnd.Data.Entities.Role.Permissions.CreateRole)]
        public async Task<OperationResult> Create(CreateRoleCommand command)
        {
            return await _mediator.Send(command);
        }

        [PermissionChecker(BackEnd.Data.Entities.Role.Permissions.EditRole)]
        public async Task<OperationResult> Edit(EditRoleCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<List<RoleDto>> GetAll()
        {
            return await _mediator.Send(new GetAllRolesQuery());
        }

        public async Task<RoleFilterResult> GetFilter(RoleFilterParam param)
        {
            return await _mediator.Send(new GetRoleByFilterQuery(param));
        }

        public async Task<List<RoleDto>> GetRolesByRoleIds(List<Guid> roleIds)
        {
            return await _mediator.Send(new GetRolesByRoleIdsQuery(roleIds));
        }

        [PermissionChecker(BackEnd.Data.Entities.Role.Permissions.DeleteRole)]
        public async Task<OperationResult> Remove(RemoveRoleCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
