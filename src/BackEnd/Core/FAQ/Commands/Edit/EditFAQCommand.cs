using BackEnd.Data.Entities.FAQ.Repository;
using BackEnd.Shared.CoreShared;

namespace BackEnd.Core.FAQ.Commands.Edit
{
    public class EditFAQCommand : IBaseCommand
    {
        public Guid Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public int Sequense { get; set; }
        public bool IsActive { get; set; }
    }
    public class EditFAQCommandHandler : IBaseCommandHandler<EditFAQCommand>
    {
        private readonly IFAQRepository _repository;

        public EditFAQCommandHandler(IFAQRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(EditFAQCommand request, CancellationToken cancellationToken)
        {
            var faq = await _repository.GetTracking(request.Id);
            if (faq == null) return OperationResult.NotFound();

            bool affectedRows = await _repository.SortSequenseOfFAQTableForEditColumn(request.Sequense, faq.Sequense);
            faq.Edit(request.Question, request.Answer,
               request.Sequense, request.IsActive);

            await _repository.SaveChangeAsync();
            return OperationResult.Success();
        }
    }
}
