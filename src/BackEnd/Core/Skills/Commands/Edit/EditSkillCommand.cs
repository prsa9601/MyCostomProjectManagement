using BackEnd.Data.Entities.Skills;
using BackEnd.Data.Entities.Skills.Repository;
using BackEnd.Shared.CoreShared;

namespace BackEnd.Core.Skills.Commands.Edit
{
    public class EditSkillCommand : IBaseCommand
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int SkillPercentage { get; set; }
        public string Icon { get; set; }
        public bool IsActive { get; set; }
        public SkillTypes SkillTypes { get; set; }
    }
    public class EditSkillCommandHandler : IBaseCommandHandler<EditSkillCommand>
    {
        private readonly ISkillRepository _repository;

        public EditSkillCommandHandler(ISkillRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(EditSkillCommand request, CancellationToken cancellationToken)
        {
            var skill = await _repository.GetTracking(request.Id);
            if (skill == null) return OperationResult.NotFound();

            skill.Edit(request.Title, request.SkillPercentage, request.Icon, request.SkillTypes,
                request.IsActive);
            await _repository.SaveChangeAsync();
            return OperationResult.Success();
        }
    }
}
