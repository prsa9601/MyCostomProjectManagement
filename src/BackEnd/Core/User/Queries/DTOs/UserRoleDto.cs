using BackEnd.Shared.CoreShared.Queries;

namespace BackEnd.Core.User.Queries.DTOs
{
    public class UserRoleDto : BaseDto
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}
