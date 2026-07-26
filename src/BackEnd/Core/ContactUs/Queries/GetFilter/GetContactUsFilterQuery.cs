using AutoMapper;
using BackEnd.Core.ContactUs.Queries.DTOs;
using BackEnd.Core.User.Queries.DTOs;
using BackEnd.Core.User.Queries.DTOs.UserFilterDto;
using BackEnd.Data.DB;
using BackEnd.Data.Entities.User;
using BackEnd.Shared.CoreShared.Queries;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Core.ContactUs.Queries.GetFilter
{
    public class GetContactUsFilterQuery : QueryFilter<ContactUsFilterResult, ContactUsFilterParam>
    {
        public GetContactUsFilterQuery(ContactUsFilterParam filterParams) : base(filterParams)
        {
        }
    }

    public class GetContactUsFilterQueryHandler : IQueryHandler<GetContactUsFilterQuery, ContactUsFilterResult>
    {
        private readonly IDbContextFactory<Context> _dbContextFactory;
        private readonly IMapper _mapper;

        public GetContactUsFilterQueryHandler(IDbContextFactory<Context> dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public async Task<ContactUsFilterResult> Handle(GetContactUsFilterQuery request, CancellationToken cancellationToken)
        {
            var @params = request.FilterParams;
            var context = await _dbContextFactory.CreateDbContextAsync();
            var result = context.ContactUs.OrderByDescending(i => i.CreationDate);


            var skip = (@params.PageId - 1) * @params.Take;
            var model = new ContactUsFilterResult()
            {
                Data = await result.Skip(skip).Take(@params.Take)
                    .Select(contact => _mapper.Map<Data.Entities.ContactUs.ContactUs, ContactUsDto>
                    (contact)).ToListAsync(cancellationToken),
                FilterParams = @params
            };

            model.GeneratePaging(result, @params.Take, @params.PageId);
            return model;
        }
    }
}
