using BackEnd.Shared.DataShared;

namespace BackEnd.Data.Entities.SiteSettings
{
    public class SpecializedServicesSection : BaseEntity
    {
        public string Title { get; set; }
        public List<SpecializedServicesSectionOptions> Options { get; set; }
    }
    
    public class SpecializedServicesSectionOptions : BaseEntity
    {
        public string Icon { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
