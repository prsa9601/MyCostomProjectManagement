using BackEnd.Shared.DataShared;

namespace BackEnd.Data.Entities.SpecializedTechnology
{
    public class SpecializedTechnology : BaseEntity
    {
        public string Title { get; set; }
        public string Icon { get; set; }
        public Guid CategoryId { get; set; }
        public bool IsActive { get; set; }
    }
    public class SpecializedTechnologyCategory : BaseEntity
    {
        public string Title { get; set; }
        public string Icon { get; set; }
        public bool IsActive { get; set; }
    }
}
