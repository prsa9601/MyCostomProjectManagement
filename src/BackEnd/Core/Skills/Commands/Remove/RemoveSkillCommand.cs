using BackEnd.Data.Entities.Skills.Repository;
using BackEnd.Shared.CoreShared;
using System.Runtime.CompilerServices;

namespace BackEnd.Core.Skills.Commands.Remove
{
    public class RemoveSkillCommand : IBaseCommand
    {
        public Guid SkillId { get; set; }
    }
    public class RemoveSkillCommandHandler : IBaseCommandHandler<RemoveSkillCommand>
    {
        private readonly ISkillRepository _repository;

        public RemoveSkillCommandHandler(ISkillRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(RemoveSkillCommand request, CancellationToken cancellationToken)
        {
            var removeResult = await _repository.DeleteOneEntity(i => i.Id.Equals(request.SkillId));
            if (removeResult == false) return OperationResult.Error();

            await _repository.SaveChangeAsync();
            return OperationResult.Success();
        }
    }
}
