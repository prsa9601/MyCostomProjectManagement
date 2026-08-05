using BackEnd.Core.Skills.Commands.Create;
using BackEnd.Core.Skills.Commands.Edit;
using BackEnd.Core.Skills.Commands.Remove;
using BackEnd.Core.Skills.Queries.DTOs;
using BackEnd.Core.Skills.Queries.GetAll;
using BackEnd.Core.Skills.Queries.GetFilter;
using BackEnd.Shared.CoreShared;
using MediatR;
using MyCostomProjectManagement.Shared.Attributes;

namespace MyCostomProjectManagement.Facade.Skills
{
    public interface ISkillFacade
    {
        Task<OperationResult> Create(CreateSkillCommand command);
        Task<OperationResult> Edit(EditSkillCommand command);
        Task<OperationResult> Remove(RemoveSkillCommand command);

        Task<SkillFilterResult> GetFilter(SkillFilterParam filterParam);
        Task<List<SkillDto>> GetAll();
    }
    public class SkillFacade : ISkillFacade
    {
        private readonly IMediator _mediator;

        public SkillFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        [PermissionChecker(BackEnd.Data.Entities.Role.Permissions.CreateSkills)]
        public async Task<OperationResult> Create(CreateSkillCommand command)
        {
            return await _mediator.Send(command);
        }

        [PermissionChecker(BackEnd.Data.Entities.Role.Permissions.EditSkills)]
        public async Task<OperationResult> Edit(EditSkillCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<List<SkillDto>> GetAll()
        {
            return await _mediator.Send(new GetAllSkillQuery());
        }

        public async Task<SkillFilterResult> GetFilter(SkillFilterParam filterParam)
        {
            return await _mediator.Send(new GetSkillFilterQuery(filterParam));
        }

        [PermissionChecker(BackEnd.Data.Entities.Role.Permissions.DeleteSkills)]
        public async Task<OperationResult> Remove(RemoveSkillCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
