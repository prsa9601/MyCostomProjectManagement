using BackEnd.Shared.DataShared;

namespace BackEnd.Data.Entities.Qualification
{
    public class SpecializedQualification : BaseEntity
    {
        public string Icon { get; set; }
        public string Title { get; set; }
        public string Provider { get; set; }
        public List<string> Tags { get; set; }
        public bool IsActive { get; set; }
    }
}
