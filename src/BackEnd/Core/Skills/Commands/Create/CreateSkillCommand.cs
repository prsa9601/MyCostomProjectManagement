using BackEnd.Data.DB;
using BackEnd.Data.Entities.Skills;
using BackEnd.Data.Entities.Skills.Repository;
using BackEnd.Shared.CoreShared;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Core.Skills.Commands.Create
{
    public class CreateSkillCommand : IBaseCommand
    {
        public string Title { get; set; }
        public int SkillPercentage { get; set; }
        public string Icon { get; set; }
        public bool IsActive { get; set; }
        public SkillTypes SkillTypes { get; set; }
    }
    public class CreateSkillCommandHandler : IBaseCommandHandler<CreateSkillCommand>
    {
        private readonly ISkillRepository _skillRepository;
        public CreateSkillCommandHandler(ISkillRepository skillRepository)
        {
            this._skillRepository = skillRepository;
        }

        public async Task<OperationResult> Handle(CreateSkillCommand request, CancellationToken cancellationToken)
        {
            var skill = new TechnicalSkills(request.Title, request.SkillPercentage, 
                request.Icon, request.SkillTypes, request.IsActive);

            await _skillRepository.AddAsync(skill);
            await _skillRepository.SaveChangeAsync();
            return OperationResult.Success();
        }
    }
}
