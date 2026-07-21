using AutoMapper;
using BackEnd.Core.Role.Queries.DTOs;
using BackEnd.Data.DB;
using BackEnd.Data.Entities.Role;
using BackEnd.Shared.CoreShared.Queries;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Core.Role.Queries.GetRoles
{
    public record class GetRolesByRoleIdsQuery(List<Guid> roleIds) : IQuery<List<RoleDto>>;

    public class GetRolesByRoleIdsQueryHandler : IQueryHandler<GetRolesByRoleIdsQuery, List<RoleDto>>
    {
        private readonly IDbContextFactory<Context> _dbContextFactory;
        private readonly IMapper _mapper;

        public GetRolesByRoleIdsQueryHandler(IDbContextFactory<Context> dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public async Task<List<RoleDto>> Handle(GetRolesByRoleIdsQuery request, CancellationToken cancellationToken)
        {
            var context = await _dbContextFactory.CreateDbContextAsync();
            var roles = await context.Roles.AsNoTracking().Where(i => request.roleIds.Contains(i.Id)).ToListAsync();

            if (roles == null || roles.Count == 0) return default;

            var roleMapper = roles.Select(i => _mapper.Map<BackEnd.Data.Entities.Role.Role, RoleDto>(i));

            return roleMapper.ToList();
        }
    }
}
