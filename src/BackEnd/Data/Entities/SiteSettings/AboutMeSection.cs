using BackEnd.Shared.DataShared;

namespace BackEnd.Data.Entities.SiteSettings
{
    public class AboutMeSection : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Iamage { get; set; }
        public AboutStats AboutStats { get; set; }

        //public List<AboutMeSectionItems> Items { get; set; } = new();
    }
    //public class AboutMeSectionItems : BaseEntitie
    //{
    //    public string Title { get; set; }
    //    public string Icon { get; set; }
    //}
}
