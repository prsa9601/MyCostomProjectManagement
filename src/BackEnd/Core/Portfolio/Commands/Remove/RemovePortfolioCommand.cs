using BackEnd.Data.Entities.Portfolio.Repository;
using BackEnd.Shared.CoreShared;

namespace BackEnd.Core.Portfolio.Commands.Remove
{
    public class RemovePortfolioCommand : IBaseCommand
    {
        public Guid Id { get; set; }
    }
    public class RemovePortfolioCommandHandler : IBaseCommandHandler<RemovePortfolioCommand>
    {
        private readonly IPortfolioRepository _repository;

        public RemovePortfolioCommandHandler(IPortfolioRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(RemovePortfolioCommand request, CancellationToken cancellationToken)
        {
            var portfolio = await _repository.DeleteOneEntity(i => i.Id == request.Id);
            if (portfolio == false) return OperationResult.Error();

            return OperationResult.Success();
        }
    }
}
