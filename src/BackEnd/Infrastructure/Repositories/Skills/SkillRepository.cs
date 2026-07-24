using BackEnd.Data.DB;
using BackEnd.Data.Entities.Skills.Repository;
using BackEnd.Shared.CoreShared.Repository;

namespace BackEnd.Infrastructure.Repositories.Skills
{
    internal class SkillRepository : BaseRepository<Data.Entities.Skills.TechnicalSkills>, ISkillRepository
    {
        public SkillRepository(Context context) : base(context)
        {
        }
    }
}
