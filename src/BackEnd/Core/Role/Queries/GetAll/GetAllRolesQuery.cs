using AutoMapper;
using BackEnd.Core.Role.Queries.DTOs;
using BackEnd.Data.DB;
using BackEnd.Shared.CoreShared.Queries;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Core.Role.Queries.GetAll
{
    public class GetAllRolesQuery : IQuery<List<RoleDto>>
    {
    }
    public class GetAllRolesQueryHandler : IQueryHandler<GetAllRolesQuery, List<RoleDto>>
    {
        private readonly IDbContextFactory<Context> _dbContextFactory;
        private readonly IMapper _mapper;

        public GetAllRolesQueryHandler(IDbContextFactory<Context> dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public async Task<List<RoleDto>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            var context = await _dbContextFactory.CreateDbContextAsync();

            var roles = await context.Roles.Include(i => i.RolePermissions).OrderByDescending(i => i.CreationDate).ToListAsync();
            if (roles == null || roles.Count == 0) return new();

            return _mapper.Map<List<Data.Entities.Role.Role>, List<RoleDto>>(roles);
        }
    }
}
