using BackEnd.Shared.DataShared;

namespace BackEnd.Data.Entities.Role
{
    public class RolePermission : BaseEntity
    {
        public Guid RoleId { get; set; }
        public Permissions Permissions { get; set; }
    }
}
