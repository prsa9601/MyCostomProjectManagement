using AutoMapper;
using BackEnd.Core.Skills.Queries.DTOs;
using BackEnd.Core.User.Queries.DTOs;
using BackEnd.Core.User.Queries.DTOs.UserFilterDto;
using BackEnd.Data.DB;
using BackEnd.Shared.CoreShared.Queries;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Core.Skills.Queries.GetFilter
{
    public class GetSkillFilterQuery : QueryFilter<SkillFilterResult, SkillFilterParam>
    {
        public GetSkillFilterQuery(SkillFilterParam filterParams) : base(filterParams)
        {
        }
    }
    public class GetSkillFilterQueryHandler : IQueryHandler<GetSkillFilterQuery, SkillFilterResult>
    {
        private readonly IDbContextFactory<Context> _dbContextFactory;
        private readonly IMapper _mapper;

        public GetSkillFilterQueryHandler(IDbContextFactory<Context> dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public async Task<SkillFilterResult> Handle(GetSkillFilterQuery request, CancellationToken cancellationToken)
        {
            var context = await _dbContextFactory.CreateDbContextAsync();

            var @params = request.FilterParams;
            var skills = context.TechnicalSkills.AsTracking()
                .OrderBy(i => i.CreationDate).AsQueryable();


            var skip = (@params.PageId - 1) * @params.Take;
            var model = new SkillFilterResult()
            {
                Data = await skills.Skip(skip).Take(@params.Take)
                    .Select(skill => _mapper.Map<Data.Entities.Skills.TechnicalSkills, SkillDto>
                    (skill)).ToListAsync(cancellationToken),
                FilterParams = @params
            };

            model.GeneratePaging(skills, @params.Take, @params.PageId);
            return model;
        }
    }
}
