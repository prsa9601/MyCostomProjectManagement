using BackEnd.Data.Entities.Package;
using BackEnd.Shared.DataShared;
using System.Globalization;

namespace BackEnd.Data.Entities.Projects
{
    public class ProjectRequest : BaseEntity
    {
        public string UserFullName { get; set; }
        public string PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string ProjectDescription { get; set; }
        public int Approximate_Budget { get; set; }

        public ProjectType ProjectType { get; set; }
        public ProjectRequestStageStatus StageStatus { get; set; }

        public ProjectRequest(string userFullName, string phoneNumber,
            string projectDescription, int approximate_Budget)
        {
            UserFullName = userFullName;
            PhoneNumber = phoneNumber;
            ProjectDescription = projectDescription;
            Approximate_Budget = approximate_Budget;
        }

        public void SetEmail(string email)
        {
            Email = email;
        }

    }
    public class ProjectRequestStageStatus : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int StageSequense { get; set; }

    }
    public class ProjectType : BaseEntity
    {
        public string Title { get; set; }
        //public int Price { get; set; }

        public ProjectType(string title)
        {
            Title = title;
        }

    }
}
