using BackEnd.Shared.DataShared;

namespace BackEnd.Data.Entities.SiteSettings
{
    public class SiteLinks : BaseEntity
    {
        public bool IsActive { get; set; }
        public int Sequense { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string Icon { get; set; }

        public SiteLinks(bool isActive, int sequense, string title, string address, string icon)
        {
            IsActive = isActive;
            Sequense = sequense;
            Title = title;
            Address = address;
            Icon = icon;
        }
    
        public void Edit(bool isActive, int sequense, string title, string address, string icon)
        {
            IsActive = isActive;
            Sequense = sequense;
            Title = title;
            Address = address;
            Icon = icon;
        }
    }
}
