using BackEnd.Data.Entities.FAQ;
using BackEnd.Data.Entities.FAQ.Repository;
using BackEnd.Shared.CoreShared;

namespace BackEnd.Core.FAQ.Commands.Create
{
    public class CraeteFAQCommand : IBaseCommand
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public int Sequense { get; set; }
        public bool IsActive { get; set; }
    }
    public class CraeteFAQCommandHandler : IBaseCommandHandler<CraeteFAQCommand>
    {
        private readonly IFAQRepository _repository;

        public CraeteFAQCommandHandler(IFAQRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(CraeteFAQCommand request, CancellationToken cancellationToken)
        {
            var faq = new Data.Entities.FAQ.FAQ(request.Question, request.Answer, 
                request.Sequense, request.IsActive);

            bool affectedRows = await _repository.SortSequenseOfFAQTableForCreateColumn(request.Sequense);
            await _repository.AddAsync(faq);
            await _repository.SaveChangeAsync();
            return OperationResult.Success();
        }
    }
}
