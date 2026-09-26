using BackEnd.Shared.CoreShared.Queries;

namespace BackEnd.Data.Infrastructure.Tutorial.DTOs
{
    public class TutorialFilterParam : BaseFilterParam
    {
        public bool IsAdmin { get; set; }
        public string SearchTerm { get; set; }
        public string Level { get; set; }
        public string Category { get; set; }
    }
}
