using BackEnd.Core.Portfolio.Commands.Create;
using BackEnd.Core.Portfolio.Commands.Edit;
using BackEnd.Core.Portfolio.Commands.Remove;
using BackEnd.Core.Portfolio.Queries.DTOs;
using BackEnd.Core.Portfolio.Queries.GetFilter;
using BackEnd.Shared.CoreShared;
using MediatR;
using MyCostomProjectManagement.Shared.Attributes;

namespace MyCostomProjectManagement.Facade.Portfolio
{
    public interface IPortfolioFacade
    {
        Task<OperationResult> Create(CreatePortfoliCommand command);
        Task<OperationResult> Edit(EditPortfoliCommand command);
        Task<OperationResult> Remove(RemovePortfolioCommand command);

        Task<PortfolioFilterResult> GetFilter(PortfolioFilterParam filterParam);
    }
    public class PortfolioFacade : IPortfolioFacade
    {
        private readonly IMediator _mediator;

        public PortfolioFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        [PermissionChecker(BackEnd.Data.Entities.Role.Permissions.CreatePortfolio)]
        public async Task<OperationResult> Create(CreatePortfoliCommand command)
        {
            return await _mediator.Send(command);
        }

        [PermissionChecker(BackEnd.Data.Entities.Role.Permissions.EditPortfolio)]
        public async Task<OperationResult> Edit(EditPortfoliCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<PortfolioFilterResult> GetFilter(PortfolioFilterParam filterParam)
        {
            return await _mediator.Send(new GetPortfolioFilterQuery(filterParam));
        }

        [PermissionChecker(BackEnd.Data.Entities.Role.Permissions.DeletePortfolio)]
        public async Task<OperationResult> Remove(RemovePortfolioCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
