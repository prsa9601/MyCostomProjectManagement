using BackEnd.Shared.DataShared;

namespace BackEnd.Data.Entities.ProgrammingServices
{
    public class ProgrammingServices : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public bool IsActive { get; set; }
    }
}
