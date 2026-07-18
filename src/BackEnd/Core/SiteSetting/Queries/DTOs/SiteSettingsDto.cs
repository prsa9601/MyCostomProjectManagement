using BackEnd.Data.Entities.SiteSettings;
using BackEnd.Shared.CoreShared.Queries;
using BackEnd.Shared.DataShared;

namespace BackEnd.Core.SiteSetting.Queries.DTOs
{
    public class SiteSettingsDto : BaseDto
    {
        public bool SiteIsActive { get; set; }
        public GeneralSiteInformationDto GeneralSiteInformation { get; set; }
        public List<MenuSettingDto> MenuSettings { get; set; }
        public ProjectSectionSettingDto ProjectSectionSetting { get; set; }
        public SpecializedServicesSectionDto SpecializedServicesSection { get; set; }
        public AboutMeSectionDto AboutMeSection { get; set; }
    }
 
}
