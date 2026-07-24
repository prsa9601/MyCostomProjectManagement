using AutoMapper;
using BackEnd.Core.Portfolio.Queries.DTOs;
using BackEnd.Core.Role.Queries.DTOs;
using BackEnd.Core.User.Queries.DTOs;
using BackEnd.Data.DB;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Core.Portfolio.Resolver
{
    public class PortfolioFileAutoMapperResolver : IValueResolver<Data.Entities.Portfolio.Portfolio, PortfolioDto, PortfolioFileDto>
    {
        private readonly IDbContextFactory<Context> _dbContextFactory;

        public PortfolioFileAutoMapperResolver(IDbContextFactory<Context> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public PortfolioFileDto Resolve(Data.Entities.Portfolio.Portfolio source, PortfolioDto destination, PortfolioFileDto destMember, ResolutionContext context)
        {
            var _context = _dbContextFactory.CreateDbContext();
            var file = source.File.GetFile();
            var portfolioFileDto = new PortfolioFileDto() 
            {
                FileAddress = file.address,
                IsImage = file.IsImage,
                CreationDate = source.File.CreationDate,
                Id = source.File.Id,
            };
            return portfolioFileDto;
        }
    }
}
