using BackEnd.Shared.CoreShared.Queries;

namespace BackEnd.Core.SiteSetting.Queries.DTOs
{
    public class SpecializedServicesSectionDto : BaseDto
    {
        public string Title { get; set; }
        public List<SpecializedServicesSectionOptionsDto> Options { get; set; }
    }
 
}
