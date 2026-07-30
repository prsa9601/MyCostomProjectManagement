using BackEnd.Shared.DataShared;

namespace BackEnd.Data.Entities.SiteSettings
{
    public class SiteSetting : BaseEntity
    {

        public bool SiteIsActive { get; set; }
        public GeneralSiteInformation? GeneralSiteInformation { get; set; }
        public List<MenuSetting>? MenuSettings { get; set; }
        public ProjectSectionSetting? ProjectSectionSetting { get; set; }
        public SpecializedServicesSection? SpecializedServicesSection { get; set; }
        public AboutMeSection AboutMeSection { get; set; }
        public List<SiteLinks> SiteLinks { get; set; }
    }
}
