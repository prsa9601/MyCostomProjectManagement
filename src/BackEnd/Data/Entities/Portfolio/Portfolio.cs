using BackEnd.Shared.DataShared;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.Data.Entities.Portfolio
{
    public class Portfolio : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public PortfolioCategory Category { get; set; }

        public string? Link { get; set; }
        public PortfolioFile File { get; set; }

        public Portfolio(string title, string description, PortfolioCategory category, string? link)
        {
            Title = title;
            Description = description;
            Category = category;
            Link = link;
        }

        public void Edit(string title, string description, PortfolioCategory category, string? link)
        {
            Title = title;
            Description = description;
            Category = category;
            Link = link;
        }

        public void SetFile(string fileAddress)
        {
            File = new PortfolioFile(fileAddress);
        }
    }
    public enum PortfolioCategory
    {
        [Display(Name = "Api")]
        API,
        [Display(Name = "وب سایت")]
        WebSite,
        [Display(Name = "فروشگاه")]
        Shop,
        [Display(Name = "وب سایت کاستوم")]
        CustomSite,
        [Display(Name = "اپلیکیشن")]
        Application, 
        [Display(Name = "داشبورد")]
        Dashboard, 
        [Display(Name = "سایر")]
        Others
    }
}
