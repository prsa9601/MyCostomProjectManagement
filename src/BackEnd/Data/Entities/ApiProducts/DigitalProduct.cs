using BackEnd.Shared.DataShared;

namespace BackEnd.Data.Entities.ApiProducts
{
    public class DigitalProduct : BaseEntity
    {
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
