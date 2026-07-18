using BackEnd.Shared.CoreShared.Queries;

namespace BackEnd.Core.SiteSetting.Queries.DTOs
{
    public class ProjectSectionSettingOptionDto : BaseDto
    {
        public string Title { get; set; }
        public List<Guid> ProjectIds { get; set; }
    }
 
}
