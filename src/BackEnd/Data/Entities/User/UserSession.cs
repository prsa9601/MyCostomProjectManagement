
using BackEnd.Shared.DataShared;
using System.Reflection.Metadata.Ecma335;

namespace BackEnd.Data.Entities.User
{
    public class UserSession : BaseEntity
    {
        public string HashRefreshToken { get; set; }
        public DateTime ExpireDate { get; set; }
        public bool IsActive { get; set; }
        public Guid UserId { get; set; }

        public UserSession(string hashRefreshToken, DateTime expireDate)
        {
            HashRefreshToken = hashRefreshToken;
            ExpireDate = expireDate;
        }

    }
}
