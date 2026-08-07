using BackEnd.Core.Project.Commands.ProjectRequestV1.Create;
using BackEnd.Core.Project.Commands.ProjectRequestV1.Edit;
using BackEnd.Core.Project.Queries.ProjectRequestV1.DTOs;
using BackEnd.Core.Project.Queries.ProjectRequestV1.GetFilter;
using BackEnd.Shared.CoreShared;
using MediatR;

namespace MyCostomProjectManagement.Facade.ProjectRequestV1
{
    public interface IProjectRequestV1Facade
    {
        Task<OperationResult> Create(CreateProjectRequestV1Command command);
        Task<OperationResult> Edit(EditProjectRequestV1Command command);
        //Task<OperationResult> Remove(RemoveProjectRequestV1Command command);

        Task<ProjectRequestV1FilterResult> GetFilter(ProjectRequestV1FilterParam filterParam);
    }
    public class ProjectRequestV1Facade : IProjectRequestV1Facade
    {
        private readonly IMediator _mediator;

        public ProjectRequestV1Facade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<OperationResult> Create(CreateProjectRequestV1Command command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> Edit(EditProjectRequestV1Command command)
        {
            return await _mediator.Send(command);
        }

        public async Task<ProjectRequestV1FilterResult> GetFilter(ProjectRequestV1FilterParam filterParam)
        {
            return await _mediator.Send(new GetProjectRequestV1Query(filterParam));
        }
    }
}
