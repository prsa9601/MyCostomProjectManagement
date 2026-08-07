using BackEnd.Data.Entities.ProjectRequestV1;
using BackEnd.Shared.CoreShared.Queries;
using BackEnd.Shared.DataShared;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Core.Project.Queries.ProjectRequestV1.DTOs
{
    public class ProjectRequestV1Dto : BaseDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public ProjectRequestType Type { get; set; }
        public long Price { get; set; }
        public string Description { get; set; }
        public bool IsWasSeen { get; set; }
        public bool IsWorked { get; set; }
        public bool IsDone { get; set; }
    }
    
    public class ProjectRequestV1FilterParam : BaseFilterParam
    {
        public string Search { get; set; }
    }
    
    public class ProjectRequestV1FilterResult : BaseFilter<ProjectRequestV1Dto, ProjectRequestV1FilterParam>
    {

    }
}
