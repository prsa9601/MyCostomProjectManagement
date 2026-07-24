using BackEnd.Core.Portfolio.Commands.Create;
using BackEnd.Core.Portfolio.Commands.Edit;
using BackEnd.Core.Portfolio.Commands.Remove;
using BackEnd.Core.Portfolio.Queries.DTOs;
using BackEnd.Core.Portfolio.Queries.GetFilter;
using BackEnd.Shared.CoreShared;
using MediatR;

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

        public async Task<OperationResult> Create(CreatePortfoliCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> Edit(EditPortfoliCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<PortfolioFilterResult> GetFilter(PortfolioFilterParam filterParam)
        {
            return await _mediator.Send(new GetPortfolioFilterQuery(filterParam));
        }

        public async Task<OperationResult> Remove(RemovePortfolioCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
