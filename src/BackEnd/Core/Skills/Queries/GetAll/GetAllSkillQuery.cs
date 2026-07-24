using AutoMapper;
using BackEnd.Core.Skills.Queries.DTOs;
using BackEnd.Data.DB;
using BackEnd.Shared.CoreShared.Queries;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Core.Skills.Queries.GetAll
{
    public class GetAllSkillQuery : IQuery<List<SkillDto>>
    {
    }
    public class GetAllSkillQueryHandler : IQueryHandler<GetAllSkillQuery, List<SkillDto>>
    {
        private readonly IDbContextFactory<Context> _dbContextFactory;
        private readonly IMapper _mapper;

        public GetAllSkillQueryHandler(IDbContextFactory<Context> dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public async Task<List<SkillDto>> Handle(GetAllSkillQuery request, CancellationToken cancellationToken)
        {
            var context = await _dbContextFactory.CreateDbContextAsync();
            var skill = context.TechnicalSkills.OrderByDescending(i => i.CreationDate);

            if (skill == null) return default;
            return _mapper.Map<List<Data.Entities.Skills.TechnicalSkills>, List<SkillDto>>(skill.ToList());
        }
    }
}
