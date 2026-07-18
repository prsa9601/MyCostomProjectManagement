using BackEnd.Shared.DataShared;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace BackEnd.Data.Entities.User
{
    public class UserOtp : BaseEntity
    {
        public string Token { get; set; }
        public Guid UserId { get; set; }
        public DateTime ExpireDate { get; set; }
        public bool IsActive { get; set; } 

        public UserOtp(string token)
        {
            ExpireDate = DateTime.Now.AddMinutes(2);
            IsActive = true;
            Token = token;
        }

        public void InASctive()
        {
            IsActive = false;
        }
    }
}
