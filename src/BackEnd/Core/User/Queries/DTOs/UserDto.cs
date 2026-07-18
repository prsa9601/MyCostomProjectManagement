using BackEnd.Data.Entities.User;
using BackEnd.Shared.CoreShared.Queries;
using BackEnd.Shared.DataShared;

namespace BackEnd.Core.User.Queries.DTOs
{
    public class UserDto : BaseDto
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string? Email { get; set; }

        public List<UserRoleDto> UserRoles { get; set; } = new();
        public List<UserOtpDto> UserOtps { get; set; } = new();
        public List<UserBlackListDto> UserBlackLists { get; set; } = new();

    }
}
