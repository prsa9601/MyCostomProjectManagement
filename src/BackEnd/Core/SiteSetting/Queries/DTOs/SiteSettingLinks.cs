using BackEnd.Shared.CoreShared.Queries;

namespace BackEnd.Core.SiteSetting.Queries.DTOs
{
    public class SiteLinksDto : BaseDto
    {
        public bool IsActive { get; set; }
        public int Sequense { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string Icon { get; set; }
    }
}
