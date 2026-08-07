using AutoMapper;
using BackEnd.Core.Project.Queries.ProjectRequestV1.DTOs;
using BackEnd.Data.Entities.ProjectRequestV1;

namespace BackEnd.Core.Project.Queries.ProjectRequestV1.Mappers
{
    public class ProjectRequestV1AutoMapperProfile : Profile
    {
        public ProjectRequestV1AutoMapperProfile()
        {
            CreateMap<Data.Entities.ProjectRequestV1.ProjectRequestV1, ProjectRequestV1Dto>();
        }
    }
}
