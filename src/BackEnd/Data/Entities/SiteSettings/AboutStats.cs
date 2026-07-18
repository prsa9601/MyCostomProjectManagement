using BackEnd.Shared.DataShared;

namespace BackEnd.Data.Entities.SiteSettings
{
    public class AboutStats : BaseEntity
    {
        public int CustomerSatisfaction { get; set; }
        public int ExperienceYears { get; set; }
        public int ActiveCustomers { get; set; }
        public int SuccessProjects { get; set; }
        
        public AboutStats(int customerSatisfaction, int experienceYears, 
            int activeCustomers, int successProjects)
        {
            CustomerSatisfaction = customerSatisfaction;
            ExperienceYears = experienceYears;
            ActiveCustomers = activeCustomers;
            SuccessProjects = successProjects;
        }
        
        public void Edit(int customerSatisfaction, int experienceYears, 
            int activeCustomers, int successProjects)
        {
            CustomerSatisfaction = customerSatisfaction;
            ExperienceYears = experienceYears;
            ActiveCustomers = activeCustomers;
            SuccessProjects = successProjects;
        }

    }
}
