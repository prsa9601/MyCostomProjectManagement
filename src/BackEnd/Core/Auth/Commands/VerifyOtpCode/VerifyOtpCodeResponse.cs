namespace BackEnd.Core.Auth.Commands.VerifyOtpCode
{
    public enum UserAuthStatus
    {
        UserNeedToLogin,
        UserNeedToRegiter
    }
    public class VerifyOtpCodeResponse
    {
        public UserAuthStatus UserAuthStatus { get; set; }
    }
}
