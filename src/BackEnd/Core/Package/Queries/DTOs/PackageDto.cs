using BackEnd.Data.Entities.Package;
using BackEnd.Shared.CoreShared.Queries;

namespace BackEnd.Core.Package.Queries.DTOs
{
    public class PackageDto : BaseDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public Badge Badge { get; set; }

        public DateTime ExpiresAt { get; set; }
        public List<string> Features { get; set; }
    }
}
