using BackEnd.Data.Entities.Role;
using BackEnd.Shared.CoreShared.Queries;
using BackEnd.Shared.DataShared;

namespace BackEnd.Core.Role.Queries.DTOs
{
    public class RoleDto : BaseDto
    {
        public string Name { get; set; }
        public string Icon { get; set; } = "fa-crown";
        public string IconColorCode { get; set; } = "#CCFFCC";
        public string? Description { get; set; }
        public bool IsDefault { get; set; } = false;

        public List<RolePermissionDto>? RolePermissions { get; set; } = new();
    }
}
