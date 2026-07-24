using BackEnd.Data.DB;
using BackEnd.Data.Entities.Portfolio;
using BackEnd.Data.Entities.Portfolio.Repository;
using BackEnd.Shared.CoreShared.Repository;

namespace BackEnd.Infrastructure.Repositories.Portfolio
{
    internal class PortfolioRepository :  BaseRepository<Data.Entities.Portfolio.Portfolio>, IPortfolioRepository
    {
        public PortfolioRepository(Context context) : base(context)
        {
        }
    }
}
