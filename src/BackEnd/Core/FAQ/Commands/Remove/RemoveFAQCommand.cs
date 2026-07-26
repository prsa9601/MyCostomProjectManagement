using BackEnd.Data.Entities.FAQ.Repository;
using BackEnd.Shared.CoreShared;

namespace BackEnd.Core.FAQ.Commands.Remove
{
    public class RemoveFAQCommand : IBaseCommand
    {
        public Guid Id { get; set; }
    }
    public class RemoveFAQCommandHandler : IBaseCommandHandler<RemoveFAQCommand>
    {
        private readonly IFAQRepository _repository;

        public RemoveFAQCommandHandler(IFAQRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(RemoveFAQCommand request, CancellationToken cancellationToken)
        {
            var faq = await _repository.GetTracking(request.Id);
            if (faq == null) return OperationResult.NotFound();

            bool affectedRows = await _repository.SortSequenseOfFAQTableForRemoveColumn(faq.Sequense);

            var result = await _repository.DeleteAsync(faq);
            await _repository.SaveChangeAsync();
            return result ? OperationResult.Success() : OperationResult.Error();
        }
    }
}
