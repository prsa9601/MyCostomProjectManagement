using BackEnd.Shared.DataShared;

namespace BackEnd.Data.Entities.Comment
{
    public class Comment : BaseEntity
    {
        public string UserName { get; set; }
        public string Text { get; set; }
        public int Score { get; set; }
        public string JobTitle { get; set; }
        public bool IsActive { get; set; }
    }
}
