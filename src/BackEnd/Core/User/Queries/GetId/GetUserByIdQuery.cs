using AutoMapper;
using BackEnd.Core.Role.Queries.DTOs;
using BackEnd.Core.User.Queries.DTOs;
using BackEnd.Data.DB;
using BackEnd.Data.Entities.User;
using BackEnd.Shared.CoreShared.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace BackEnd.Core.User.Queries.GetId
{
    public record class GetUserByIdQuery(Guid userId) : IQuery<UserDto>;

    public class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IDbContextFactory<Context> _dbContextFactory;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(IDbContextFactory<Context> dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var context = await _dbContextFactory.CreateDbContextAsync();
            var user = await context.Users.AsTracking().Include(i => i.UserRoles).FirstOrDefaultAsync(i => i.Id == request.userId);
            if (user == null) return default;



            var userDto = _mapper.Map<BackEnd.Data.Entities.User.User, UserDto>(user);
            return userDto;
        }
    }
    public class UserRolesResolver : IValueResolver<Data.Entities.User.User, UserDto, List<UserRoleDto>>
    {
        private readonly IDbContextFactory<Context> _dbContextFactory;

        public UserRolesResolver(IDbContextFactory<Context> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public List<UserRoleDto> Resolve(Data.Entities.User.User source, UserDto destination, List<UserRoleDto> destMember,
            ResolutionContext context)
        {
            var _context = _dbContextFactory.CreateDbContext();
            // اگر UserId نال باشد یا کاربر وجود نداشته باشد، لیست خالی برگردان
            if (source == null || source.Id == Guid.Empty || source.UserRoles.Count() == 0)
                return new List<UserRoleDto>();

            var roles = _context.Roles.AsNoTracking().Include(i => i.RolePermissions)
                .Where(i => source.UserRoles.Select(i => i.RoleId)
                .Contains(i.Id)).Include(i => i.RolePermissions).ToList();

            var userRoles = roles.Select(i => new UserRoleDto
            {
                Id = source.UserRoles.FirstOrDefault(x => x.RoleId == i.Id).Id,
                RoleId = i.Id,
                CreationDate = source.UserRoles.FirstOrDefault(x => x.RoleId == i.Id).CreationDate,
                RolePermissions = i.RolePermissions.Select(p => new RolePermissionDto
                {
                    CreationDate = p.CreationDate,
                    Id = i.Id,
                    Permissions = p.Permissions,
                    RoleId = p.Id,
                }).ToList(),
                Title = i.Name,
                UserId = source.Id,
            }).ToList();

            return userRoles;
        }
    }

}
