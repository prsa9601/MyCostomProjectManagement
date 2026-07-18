using BackEnd.Shared.DataShared;

namespace BackEnd.Data.Entities.Portfolio
{
    public class Portfolio : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}
