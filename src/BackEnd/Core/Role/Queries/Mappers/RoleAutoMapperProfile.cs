using AutoMapper;
using BackEnd.Core.Role.Queries.DTOs;

namespace BackEnd.Core.Role.Queries.Mappers
{
    public class RoleAutoMapperProfile : Profile
    {
        public RoleAutoMapperProfile()
        {
            CreateMap<BackEnd.Data.Entities.Role.Role, RoleDto>()
            .ReverseMap();
            // اضافه کردن مپ برای RolePermission
            CreateMap<BackEnd.Data.Entities.Role.RolePermission, RolePermissionDto>()
                .ReverseMap();
        }
    }
}
