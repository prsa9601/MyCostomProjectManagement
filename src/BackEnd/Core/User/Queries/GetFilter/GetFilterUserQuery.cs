using AutoMapper;
using BackEnd.Core.Role.Queries.DTOs;
using BackEnd.Core.User.Queries.DTOs;
using BackEnd.Core.User.Queries.DTOs.UserFilterDto;
using BackEnd.Data.DB;
using BackEnd.Shared.CoreShared.Queries;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Core.User.Queries.GetFilter
{
    public class GetFilterUserQuery : QueryFilter<UserFilterResult, UserFilterParam>
    {
        public GetFilterUserQuery(UserFilterParam filterParams) : base(filterParams)
        {
        }
    }
    public sealed class GetFilterUserQueryHandler : IQueryHandler<GetFilterUserQuery, UserFilterResult>
    {
        private readonly IDbContextFactory<Context> _dbContextFactory;
        private readonly IMapper _mapper;
        public GetFilterUserQueryHandler(IDbContextFactory<Context> dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public async Task<UserFilterResult> Handle(GetFilterUserQuery request, CancellationToken cancellationToken)
        {
            var context = await _dbContextFactory.CreateDbContextAsync();

            var @params = request.FilterParams;
            var users = context.Users.AsTracking().Include(i => i.UserBlackList)
                .Where(i => i.PhoneNumberIsVerify == true && i.FullName != null)
                .OrderBy(i => i.CreationDate).AsQueryable();

            if (!string.IsNullOrWhiteSpace(@params.Search))
            {
                users = users.Where(i => i.FullName.Contains
                (@params.Search, StringComparison.OrdinalIgnoreCase) || i.PhoneNumber.Contains
                (@params.Search, StringComparison.OrdinalIgnoreCase));
            }

            var skip = (@params.PageId - 1) * @params.Take;
            var model = new UserFilterResult()
            {
                Data = await users.Skip(skip).Take(@params.Take)
                    .Select(role => _mapper.Map<Data.Entities.User.User, UserDto>
                    (role)).ToListAsync(cancellationToken),
                FilterParams = @params
            };

            model.GeneratePaging(users, @params.Take, @params.PageId);
            return model;
        }
    }
}
