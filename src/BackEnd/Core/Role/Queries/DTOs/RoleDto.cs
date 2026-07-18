using BackEnd.Data.Entities.Role;
using BackEnd.Shared.CoreShared.Queries;
using BackEnd.Shared.DataShared;

namespace BackEnd.Core.Role.Queries.DTOs
{
    public class RoleDto : BaseDto
    {
        public string Name { get; set; }
        public List<RolePermissionDto> RolePermissions { get; set; }
    }
}
