using AutoMapper;
using BackEnd.Core.Role.Queries.DTOs;
using BackEnd.Core.User.Queries.DTOs;
using BackEnd.Core.User.Queries.GetId;
using BackEnd.Data.Entities.User;

namespace BackEnd.Core.User.Queries.Mappers
{
    public class UserAutoMapperProfile : Profile
    {
        public UserAutoMapperProfile()
        {
            //CreateMap<BackEnd.Data.Entities.User.User, UserDto>()
            //    .ReverseMap();
            //.ForMember(dest => dest.FName, opt => opt.MapFrom(src => src.FirstName))
            //.ForMember(dest => dest.LName, opt => opt.MapFrom(src => src.LastName))
            CreateMap<BackEnd.Data.Entities.User.User, UserDto>()
                      // پراپرتی‌های ساده به‌صورت خودکار مپ می‌شوند (با تطابق نام)
                      .ForMember(dest => dest.UserRoles, opt => opt.Ignore()) // دستی پر می‌شود
                      
                     .ForMember(dest => dest.UserRoles, opt => opt.MapFrom<UserRolesResolver>());                                   
                      // اگر UserOtps و UserBlackLists هم خودکار مپ می‌شوند، نیازی به Ignore ندارند
                      //.AfterMap((src, dest, resolutionContext) =>
                      //{
                      //    if (resolutionContext.TryGetItems(out var items))
                      //    {
                      //        if (items.TryGetValue("RolesDict", out var dictObj))
                      //        {
                      //            var dict = dictObj as Dictionary<Guid, List<UserRoleDto>>;
                      //            if (dict != null)
                      //            {
                      //                dest.UserRoles = dict.TryGetValue(src.Id, out var roles)
                      //                                 ? roles
                      //                                 : new List<UserRoleDto>();
                      //            }
                      //        }
                      //    }
                      //});

        }
    }
}
