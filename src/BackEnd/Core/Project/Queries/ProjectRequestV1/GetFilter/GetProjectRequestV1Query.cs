using AutoMapper;
using BackEnd.Core.Project.Queries.ProjectRequestV1.DTOs;
using BackEnd.Core.Role.Queries.DTOs;
using BackEnd.Data.DB;
using BackEnd.Shared.CoreShared.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BackEnd.Core.Project.Queries.ProjectRequestV1.GetFilter
{
    public class GetProjectRequestV1Query : QueryFilter<ProjectRequestV1FilterResult, ProjectRequestV1FilterParam>
    {
        public GetProjectRequestV1Query(ProjectRequestV1FilterParam filterParams) : base(filterParams)
        {
        }
    }
    public class GetProjectRequestV1QueryHadler : IQueryHandler<GetProjectRequestV1Query, ProjectRequestV1FilterResult>
    {
        private readonly IDbContextFactory<Context> _dbContextFactory;
        private readonly IMapper _mapper;

        public GetProjectRequestV1QueryHadler(IDbContextFactory<Context> dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public async Task<ProjectRequestV1FilterResult> Handle(GetProjectRequestV1Query request, CancellationToken cancellationToken)
        {
            var _context = await _dbContextFactory.CreateDbContextAsync();

            var @params = request.FilterParams;
            var result = _context.ProjectRequestV1.
                OrderByDescending(d => d.CreationDate).AsQueryable();


            //if (!string.IsNullOrWhiteSpace(@params.UserIds))
            //    result = result.Where(r => r.Email.Contains(@params.Email));

            if (!string.IsNullOrWhiteSpace(@params.Search))
                result = result.Where(r => r.PhoneNumber.Contains(@params.Search) || r.FullName.Contains(@params.Search));


            var skip = (@params.PageId - 1) * @params.Take;
            var model = new ProjectRequestV1FilterResult()
            {
                Data = await result.Skip(skip).Take(@params.Take)
                    .Select(role => _mapper.Map<Data.Entities.ProjectRequestV1.ProjectRequestV1, ProjectRequestV1Dto>
                    (role)).ToListAsync(cancellationToken),
                FilterParams = @params
            };

            model.GeneratePaging(result, @params.Take, @params.PageId);
            return model;
        }
    }
}
