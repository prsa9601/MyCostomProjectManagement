using AutoMapper;
using BackEnd.Core.FAQ.Queries.DTOs;
using BackEnd.Data.Entities.FAQ;

namespace BackEnd.Core.FAQ.Queries.Mapper
{
    public class FAQAutoMapperProfile : Profile
    {
        public FAQAutoMapperProfile()
        {
            CreateMap<Data.Entities.FAQ.FAQ, FAQDto>();
        }
    }
}
