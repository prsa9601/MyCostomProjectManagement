using BackEnd.Shared.CoreShared.Queries;

namespace BackEnd.Core.Skills.Queries.DTOs
{
    public class SkillsDto : BaseDto
    {
        public string Title { get; set; }
        public int SkillPercentage { get; set; }
        public string Icon { get; set; }
    }
}
