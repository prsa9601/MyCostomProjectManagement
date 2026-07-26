using BackEnd.Data.Entities.ContactUs.Repository;
using BackEnd.Shared.CoreShared;

namespace BackEnd.Core.ContactUs.Commands.Remove
{
    public class RemoveContactUsCommand : IBaseCommand
    {
        public Guid Id { get; set; }
    }
    public class RemoveContactUsCommandHandler : IBaseCommandHandler<RemoveContactUsCommand>
    {
        private readonly IContactUsRepository _repository;

        public RemoveContactUsCommandHandler(IContactUsRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(RemoveContactUsCommand request, CancellationToken cancellationToken)
        {
            var removeResult = await _repository.DeleteOneEntity(i => i.Id == request.Id);
            if (!removeResult) return OperationResult.Error();

            await _repository.SaveChangeAsync();
            return OperationResult.Success();
        }
    }
}
