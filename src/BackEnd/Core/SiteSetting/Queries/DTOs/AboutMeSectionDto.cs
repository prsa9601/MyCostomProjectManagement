using BackEnd.Data.Entities.SiteSettings;
using BackEnd.Shared.CoreShared.Queries;

namespace BackEnd.Core.SiteSetting.Queries.DTOs
{
    public class AboutMeSectionDto : BaseDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Iamage { get; set; }
        public AboutStats AboutStats { get; set; }
    }
 
}
