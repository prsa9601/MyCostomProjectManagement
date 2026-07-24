using AutoMapper;
using BackEnd.Core.Skills.Queries.DTOs;

namespace BackEnd.Core.Skills.Queries.Mappers
{
    public class SkillAutoMapperProfile : Profile
    {
        public SkillAutoMapperProfile()
        {
            CreateMap<BackEnd.Data.Entities.Skills.TechnicalSkills, SkillDto>();

        }
    }
}
