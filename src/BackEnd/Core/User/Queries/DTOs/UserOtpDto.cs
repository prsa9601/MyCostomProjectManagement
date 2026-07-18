using BackEnd.Shared.CoreShared.Queries;

namespace BackEnd.Core.User.Queries.DTOs
{
    public class UserOtpDto : BaseDto
    {
        public string Token { get; set; }
        public Guid UserId { get; set; }
        public DateTime ExpireDate { get; set; }

    }
}
