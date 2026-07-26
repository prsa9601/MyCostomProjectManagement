using BackEnd.Core.FAQ.Commands.Create;
using BackEnd.Core.FAQ.Commands.Edit;
using BackEnd.Core.FAQ.Commands.Remove;
using BackEnd.Core.FAQ.Queries.DTOs;
using BackEnd.Core.FAQ.Queries.GetFilter;
using BackEnd.Shared.CoreShared;
using MediatR;
using System.Runtime.CompilerServices;

namespace MyCostomProjectManagement.Facade.FAQ
{
    public interface IFAQFacade
    {
        Task<OperationResult> Create(CraeteFAQCommand command);
        Task<OperationResult> Edit(EditFAQCommand command);
        Task<OperationResult> Remove(RemoveFAQCommand command);

        Task<FAQFilterResult> GetFilter(FAQFilterParam filterParam);
    }
    public class FAQFacade : IFAQFacade
    {
        private readonly IMediator _mediator;

        public FAQFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<OperationResult> Create(CraeteFAQCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> Edit(EditFAQCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<FAQFilterResult> GetFilter(FAQFilterParam filterParam)
        {
            return await _mediator.Send(new GetFAQFilterQuery(filterParam));
        }

        public async Task<OperationResult> Remove(RemoveFAQCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
