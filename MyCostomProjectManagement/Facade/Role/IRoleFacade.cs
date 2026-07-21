using BackEnd.Core.Role.Commands.Create;
using BackEnd.Core.Role.Commands.Edit;
using BackEnd.Core.Role.Commands.Remove;
using BackEnd.Core.Role.Queries.DTOs;
using BackEnd.Core.Role.Queries.GetFilter;
using BackEnd.Core.Role.Queries.GetRoles;
using BackEnd.Shared.CoreShared;
using MediatR;

namespace MyCostomProjectManagement.Facade.Role
{
    public interface IRoleFacade
    {
        Task<OperationResult> Create(CreateRoleCommand command);
        Task<OperationResult> Edit(EditRoleCommand command);
        Task<OperationResult> Remove(RemoveRoleCommand command);

        Task<List<RoleDto>> GetRolesByRoleIds(List<Guid> roleIds);
        Task<RoleFilterResult> GetFilter(RoleFilterParam param);
    }
    public class RoleFacade : IRoleFacade
    {
        private readonly IMediator _mediator;

        public RoleFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<OperationResult> Create(CreateRoleCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> Edit(EditRoleCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<RoleFilterResult> GetFilter(RoleFilterParam param)
        {
            return await _mediator.Send(new GetRoleByFilterQuery(param));
        }

        public async Task<List<RoleDto>> GetRolesByRoleIds(List<Guid> roleIds)
        {
            return await _mediator.Send(new GetRolesByRoleIdsQuery(roleIds));
        }

        public async Task<OperationResult> Remove(RemoveRoleCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
