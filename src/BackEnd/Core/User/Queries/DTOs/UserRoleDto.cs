using BackEnd.Core.Role.Queries.DTOs;
using BackEnd.Data.Entities.Role;
using BackEnd.Shared.CoreShared.Queries;

namespace BackEnd.Core.User.Queries.DTOs
{
    public class UserRoleDto : BaseDto
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        public string Title { get; set; }
        public List<RolePermissionDto> RolePermissions { get; set; }
    }
}
