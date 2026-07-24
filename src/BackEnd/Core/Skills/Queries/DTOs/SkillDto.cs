using BackEnd.Data.Entities.Skills;
using BackEnd.Shared.CoreShared.Queries;
using System.Collections.Specialized;

namespace BackEnd.Core.Skills.Queries.DTOs
{
    public class SkillDto : BaseDto
    {
        public string Title { get; set; }
        public int SkillPercentage { get; set; }
        public string Icon { get; set; } = "fas fa-code";
        public bool IsActive { get; set; }
        public SkillTypes SkillTypes { get; set; }
    }
    public class SkillFilterParam : BaseFilterParam
    {
        public string Search { get; set; }
        public bool? IsActive { get; set; }
    }
    public class SkillFilterResult : BaseFilter<SkillDto, SkillFilterParam>
    {
    }
}
