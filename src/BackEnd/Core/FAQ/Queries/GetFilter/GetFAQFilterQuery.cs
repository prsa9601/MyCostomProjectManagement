using AutoMapper;
using BackEnd.Core.FAQ.Queries.DTOs;
using BackEnd.Core.User.Queries.DTOs;
using BackEnd.Core.User.Queries.DTOs.UserFilterDto;
using BackEnd.Data.DB;
using BackEnd.Data.Entities.User;
using BackEnd.Shared.CoreShared.Queries;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Core.FAQ.Queries.GetFilter
{
    public class GetFAQFilterQuery : QueryFilter<FAQFilterResult, FAQFilterParam>
    {
        public GetFAQFilterQuery(FAQFilterParam filterParams) : base(filterParams)
        {
        }
    }
    public class GetFAQFilterQueryHandler : IQueryHandler<GetFAQFilterQuery, FAQFilterResult>
    {
        private readonly IDbContextFactory<Context> _dbContextFactory;
        private readonly IMapper _mapper;

        public GetFAQFilterQueryHandler(IDbContextFactory<Context> dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public async Task<FAQFilterResult> Handle(GetFAQFilterQuery request, CancellationToken cancellationToken)
        {
            var context = await _dbContextFactory.CreateDbContextAsync();

            var @params = request.FilterParams;

            var result = context.FAQs.OrderBy(i => i.Sequense).AsQueryable();

            if (@params.IsActive != null)
            {
                result = result.Where(i => i.IsActive == @params.IsActive);
            }

            var skip = (@params.PageId - 1) * @params.Take;
            var model = new FAQFilterResult()
            {
                Data = await result.Skip(skip).Take(@params.Take)
                    .Select(faq => _mapper.Map<Data.Entities.FAQ.FAQ, FAQDto>
                    (faq)).ToListAsync(cancellationToken),
                FilterParams = @params
            };

            model.GeneratePaging(result, @params.Take, @params.PageId);
            return model;
        }
    }
}
