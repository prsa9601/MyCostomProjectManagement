using BackEnd.Shared.CoreShared.Queries;

namespace BackEnd.Core.User.Queries.DTOs
{
    public class UserBlackListDto : BaseDto
    {
        public string HashToken { get; set; }
        public Guid UserId { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
