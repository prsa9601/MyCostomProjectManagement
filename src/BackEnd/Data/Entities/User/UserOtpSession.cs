using BackEnd.Shared.DataShared;

namespace BackEnd.Data.Entities.User
{
    public class UserOtpSession : BaseEntity
    {
        public Guid UserId { get; set; }
        public DateTime ExpireDate { get; set; }
        public string Token { get; set; }
        public bool IsActive { get; set; }
        //public bool IsVerify { get; set; } = false;

        public UserOtpSession(string token)
        {
            Token = token;
            ExpireDate = DateTime.Now.AddMinutes(5);
            IsActive = true;
        }

        public void InActive()
        {
            IsActive = false;
        }

        //public void IsVerifyed()
        //{
        //    ExpireDate.AddMinutes(5);
        //    IsActive = true;
        //    IsVerify = true;
        //}
    }
}
