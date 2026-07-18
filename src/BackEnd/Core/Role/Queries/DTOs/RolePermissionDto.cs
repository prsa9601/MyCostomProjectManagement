using BackEnd.Data.Entities.Role;
using BackEnd.Shared.CoreShared.Queries;

namespace BackEnd.Core.Role.Queries.DTOs
{
    public class RolePermissionDto : BaseDto
    {
        public Guid RoleId { get; set; }
        public Permissions Permissions { get; set; }
    }
}
