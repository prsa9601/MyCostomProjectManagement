using BackEnd.Shared.CoreShared.Queries;

namespace BackEnd.Core.SiteSetting.Queries.DTOs
{
    public class AboutStatsDto : BaseDto
    {
        public int CustomerSatisfaction { get; set; }
        public int ExperienceYears { get; set; }
        public int ActiveCustomers { get; set; }
        public int SuccessProjects { get; set; }
    }
 
}
