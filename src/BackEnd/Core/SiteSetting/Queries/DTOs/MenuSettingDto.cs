using BackEnd.Data.Entities.SiteSettings;
using BackEnd.Shared.CoreShared.Queries;

namespace BackEnd.Core.SiteSetting.Queries.DTOs
{
    public class MenuSettingDto : BaseDto
    {
        public string Name { get; set; }
        public int Sequence { get; set; }
        public ImplementationStyle ImplementationStyle { get; set; } = ImplementationStyle.RightToLeft;
    }
 
}
