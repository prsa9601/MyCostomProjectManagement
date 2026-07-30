using BackEnd.Shared.DataShared;

namespace BackEnd.Data.Entities.PageManagement
{
    public class PageManagement : BaseEntity
    {
        public string Name { get; set; }
        public string Url { get; set; } = "";
        public bool IsUnderConstruction { get; set; }
        public bool SeoIndexing { get; set; } = false;
        public string CustomMessage { get; set; } = "";
        public string MetaRobots { get; set; } = "index, follow";

    }
}
