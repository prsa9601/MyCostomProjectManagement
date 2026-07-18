using BackEnd.Shared.DataShared;

namespace BackEnd.Data.Entities.User
{
    public class UserBlackList : BaseEntity
    {
        public Guid UserId { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
