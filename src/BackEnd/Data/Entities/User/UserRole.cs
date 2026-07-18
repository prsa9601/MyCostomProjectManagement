using BackEnd.Shared.DataShared;

namespace BackEnd.Data.Entities.User
{
    public class UserRole : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}
