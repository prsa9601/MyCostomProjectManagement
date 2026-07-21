using AutoMapper;
using BackEnd.Core.User.Queries.DTOs;
using BackEnd.Data.DB;
using BackEnd.Data.Entities.User;
using BackEnd.Shared.CoreShared.Queries;
using Microsoft.EntityFrameworkCore;

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
            var user = await context.Users.AsTracking().Include(i => i.UserRoles).FirstOrDefaultAsync(i=>i.Id==request.userId);
            if (user == null) return default;

            var userDto = _mapper.Map<BackEnd.Data.Entities.User.User, UserDto>(user);
            return userDto;
        }
    }
}
