using BackEnd.Shared.DataShared;
using System.Globalization;

namespace BackEnd.Data.Entities.User
{
    public class UserSessionBlackList : BaseEntity
    {
        public string HashToken { get; set; }
        public Guid UserId { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
