using BackEnd.Shared.CoreShared.Queries;

namespace BackEnd.Core.SiteSetting.Queries.DTOs
{
    public class ProjectSectionSettingDto : BaseDto
    {
        public List<ProjectSectionSettingOptionDto> Options { get; set; }

    }
 
}
