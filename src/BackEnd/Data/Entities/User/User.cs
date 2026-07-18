using BackEnd.Shared.DataShared;

namespace BackEnd.Data.Entities.User
{
    public class User : BaseEntity
    {
        public string? FullName { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberIsVerify { get; set; } = false;
        public string? Email { get; set; }
        public string? HashPassword { get; set; }

        public List<UserRole> UserRoles { get; set; } = new();
        public List<UserOtp> UserOtps { get; set; } = new();
        public List<UserSessionBlackList> UserSessionBlackList { get; set; } = new();
        public List<UserBlackList> UserBlackList { get; set; } = new();
        public List<UserOtpSession> UserOtpSessions { get; set; } = new();
        public List<UserSession> UserSessions { get; set; } = new();

        public void AddOtp(UserOtp userOtp)
        {
            userOtp.UserId = Id;
            UserOtps.Add(userOtp);
        }
        
        public void VerifyPhoneNumber()
        {
            PhoneNumberIsVerify = true;
        }
        
        
        public void Register(string fullname, string? email, string hashPassword)
        {
            FullName = fullname;
            HashPassword = hashPassword;
            if (!string.IsNullOrWhiteSpace(email))
            {
                Email = email;
            }
        }
        
        public void AddOtpSession(UserOtpSession userOtpSession)
        {
            userOtpSession.UserId = Id;
            UserOtpSessions.Add(userOtpSession);
        }
        
        public void AddSession(UserSession userSession)
        {
            userSession.UserId = Id;
            UserSessions.Add(userSession);
        }
    }
}
