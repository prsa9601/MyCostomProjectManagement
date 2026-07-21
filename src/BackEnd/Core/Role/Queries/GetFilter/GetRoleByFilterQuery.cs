using AutoMapper;
using BackEnd.Core.Role.Queries.DTOs;
using BackEnd.Data.DB;
using BackEnd.Data.Entities.Role;
using BackEnd.Shared.CoreShared.Queries;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Core.Role.Queries.GetFilter
{
    public class GetRoleByFilterQuery : QueryFilter<RoleFilterResult, RoleFilterParam>
    {
        public GetRoleByFilterQuery(RoleFilterParam filterParams) : base(filterParams)
        {
        }
    }
    public class GetRoleByFilterQueryHandler : IQueryHandler<GetRoleByFilterQuery, RoleFilterResult>
    {
        private readonly IDbContextFactory<Context> _dbContextFactory;
        private readonly IMapper _mapper;

        public GetRoleByFilterQueryHandler(IDbContextFactory<Context> dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public async Task<RoleFilterResult> Handle(GetRoleByFilterQuery request, CancellationToken cancellationToken)
        {
            var _context = await _dbContextFactory.CreateDbContextAsync();

            var @params = request.FilterParams;
            var result = _context.Roles.Include(i=>i.RolePermissions).OrderByDescending(d => d.Id).AsQueryable();


            //if (!string.IsNullOrWhiteSpace(@params.UserIds))
            //    result = result.Where(r => r.Email.Contains(@params.Email));

            if (!string.IsNullOrWhiteSpace(@params.Search))
                result = result.Where(r => r.Name.Contains(@params.Search));


            var skip = (@params.PageId - 1) * @params.Take;
            var model = new RoleFilterResult()
            {
                Data = await result.Skip(skip).Take(@params.Take)
                    .Select(role => _mapper.Map<Data.Entities.Role.Role, RoleDto>
                    (role)).ToListAsync(cancellationToken),
                FilterParams = @params
            };

            model.GeneratePaging(result, @params.Take, @params.PageId);
            return model;
        }
    }
}
