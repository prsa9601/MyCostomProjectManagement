using AutoMapper;
using BackEnd.Core.Portfolio.Queries.DTOs;
using BackEnd.Core.User.Queries.DTOs;
using BackEnd.Core.User.Queries.DTOs.UserFilterDto;
using BackEnd.Data.DB;
using BackEnd.Shared.CoreShared.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BackEnd.Core.Portfolio.Queries.GetFilter
{
    public class GetPortfolioFilterQuery : QueryFilter<PortfolioFilterResult, PortfolioFilterParam>
    {
        public GetPortfolioFilterQuery(PortfolioFilterParam filterParams) : base(filterParams)
        {
        }
    }
    public class GetPortfolioFilterQueryHandler : IQueryHandler<GetPortfolioFilterQuery, PortfolioFilterResult>
    {
        private readonly IDbContextFactory<Context> _dbContextFactory;
        private readonly IMapper _mapper;

        public GetPortfolioFilterQueryHandler(IDbContextFactory<Context> dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public async Task<PortfolioFilterResult> Handle(GetPortfolioFilterQuery request, CancellationToken cancellationToken)
        {
            var context = await _dbContextFactory.CreateDbContextAsync();

            var @params = request.FilterParams;
            var portfolios = context.Portfolios.AsTracking().Include(i => i.File)
                .OrderBy(i => i.CreationDate).AsQueryable();

            //if (@params.GetPortfolioFileType != null)
            //{
            
            portfolios = @params.GetPortfolioFileType switch
                {
                    GetPortfolioType.GetOnlyVideo => portfolios.Where(i => i.File.IsVideo == true &&
                    i.File.VideoAddress != null && i.File.IsImage == false &&
                    i.File.ImageAddress == null),

                    GetPortfolioType.GetOnlyImage => portfolios.Where(i => i.File.IsImage == true &&
                    i.File.ImageAddress != null && i.File.IsVideo == false &&
                    i.File.VideoAddress == null),
                    GetPortfolioType.GetAll => portfolios,
                    _ => portfolios
                };
            //}

            var skip = (@params.PageId - 1) * @params.Take;
            var model = new PortfolioFilterResult()
            {
                Data = await portfolios.Skip(skip).Take(@params.Take)
                    .Select(user => _mapper.Map<Data.Entities.Portfolio.Portfolio, PortfolioDto>
                    (user)).ToListAsync(cancellationToken),
                FilterParams = @params
            };

            model.GeneratePaging(portfolios, @params.Take, @params.PageId);
            return model;
        }
    }
}
