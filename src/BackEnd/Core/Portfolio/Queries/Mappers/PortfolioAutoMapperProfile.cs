using AutoMapper;
using BackEnd.Core.Portfolio.Queries.DTOs;
using BackEnd.Core.Portfolio.Resolver;
using BackEnd.Core.User.Queries.DTOs;

namespace BackEnd.Core.Portfolio.Queries.Mappers
{
    public class PortfolioAutoMapperProfile : Profile
    {
        public PortfolioAutoMapperProfile()
        {
            CreateMap<BackEnd.Data.Entities.Portfolio.Portfolio, PortfolioDto>()
                .ForMember(dest => dest.PortfolioFile, opt => opt.Ignore())
                     .ForMember(dest => dest.PortfolioFile, opt => opt.MapFrom<PortfolioFileAutoMapperResolver>());
        }
    }
}
