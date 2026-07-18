using BackEnd.Shared.DataShared;

namespace BackEnd.Data.Entities.SiteSettings
{
    public class ProjectSectionSetting : BaseEntity
    {
        public List<ProjectSectionSettingOption> Options { get; set; }

    }

    public class ProjectSectionSettingOption : BaseEntity
    {
        public string Title { get; set; }
        public List<Guid> ProjectIds { get; set; }
    }
}
