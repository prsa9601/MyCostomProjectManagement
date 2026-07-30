using BackEnd.Data.DB;
using BackEnd.Data.Entities.PageManagement;
using BackEnd.Shared.CoreShared.Queries;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Core.PageManagement.Queries.GetList
{
    public class GetListPageManagementQuery : IQuery<List<Data.Entities.PageManagement.PageManagement>>
    {
    }
    public class GetListPageManagementQueryHandler : IQueryHandler<GetListPageManagementQuery,
        List<Data.Entities.PageManagement.PageManagement>>
    {
        private readonly IDbContextFactory<Context> _dbContextFactory;

        public GetListPageManagementQueryHandler(IDbContextFactory<Context> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<List<Data.Entities.PageManagement.PageManagement>> Handle(GetListPageManagementQuery request, CancellationToken cancellationToken)
        {
            var context = await _dbContextFactory.CreateDbContextAsync();
            var result = await context.PageManagements.ToListAsync();

            return result;
        }
    }
}
