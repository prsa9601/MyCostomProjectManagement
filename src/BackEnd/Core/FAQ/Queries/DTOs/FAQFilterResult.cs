using BackEnd.Shared.CoreShared.Queries;

namespace BackEnd.Core.FAQ.Queries.DTOs
{
    public class FAQFilterResult : BaseFilter<FAQDto, FAQFilterParam>
    {
    }
    public class FAQFilterParam : BaseFilterParam
    {
        public bool? IsActive { get; set; } = null;
    }
    public class FAQDto : BaseDto
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public int Sequense { get; set; }
        public bool IsActive { get; set; }
    }
}
