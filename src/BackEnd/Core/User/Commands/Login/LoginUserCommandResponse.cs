namespace BackEnd.Core.User.Commands.Login
{
    public class LoginUserCommandResponse
    {
        public string AuthToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
