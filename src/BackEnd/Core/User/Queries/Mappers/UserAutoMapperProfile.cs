using AutoMapper;
using BackEnd.Core.User.Queries.DTOs;
using BackEnd.Data.Entities.User;

namespace BackEnd.Core.User.Queries.Mappers
{
    public class UserAutoMapperProfile : Profile
    {
        public UserAutoMapperProfile()
        {
            CreateMap<BackEnd.Data.Entities.User.User, UserDto>()
                .ReverseMap();
            //.ForMember(dest => dest.FName, opt => opt.MapFrom(src => src.FirstName))
            //.ForMember(dest => dest.LName, opt => opt.MapFrom(src => src.LastName))
        }
    }
}
