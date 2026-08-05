using AutoMapper;
using BackEnd.Core.SiteSetting.Queries.DTOs;
using BackEnd.Data.Entities.SiteSettings;

namespace BackEnd.Core.SiteSetting.Queries.Mappers
{
    public class SiteSettingAutoMapperProfile : Profile
    {
        public SiteSettingAutoMapperProfile()
        {
            CreateMap<Data.Entities.SiteSettings.SiteSetting, SiteSettingsDto>();
            CreateMap<Data.Entities.SiteSettings.SiteLinks, SiteLinksDto>();
            CreateMap<Data.Entities.SiteSettings.GeneralSiteInformation, GeneralSiteInformationDto>();
            CreateMap<Data.Entities.SiteSettings.MenuSetting, MenuSettingDto>();
            CreateMap<Data.Entities.SiteSettings.SpecializedServicesSection, SpecializedServicesSectionDto>();
            CreateMap<Data.Entities.SiteSettings.ProjectSectionSetting, ProjectSectionSettingDto>();
            CreateMap<Data.Entities.SiteSettings.SpecializedServicesSection, SpecializedServicesSectionDto>();
            CreateMap<Data.Entities.SiteSettings.AboutMeSection, AboutMeSectionDto>();
            CreateMap<Data.Entities.SiteSettings.AboutStats, AboutStatsDto>();
           
            CreateMap<Data.Entities.SiteSettings.SpecializedServicesSectionOptions, SpecializedServicesSectionOptionsDto>();
            CreateMap<Data.Entities.SiteSettings.ProjectSectionSettingOption, ProjectSectionSettingOptionDto>();
        }
    }
}
