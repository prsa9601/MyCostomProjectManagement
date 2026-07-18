using BackEnd.Shared.DataShared;

namespace BackEnd.Data.Entities.Skills
{
    public class TechnicalSkills : BaseEntity
    {
        public string Title { get; set; }
        public int SkillPercentage { get; set; }
        public string Icon { get; set; }
    }
}
