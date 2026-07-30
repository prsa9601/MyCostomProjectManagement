using BackEnd.Core.SiteSetting.Queries.DTOs;
using BackEnd.Core.SiteSetting.Queries.GetFirst;
using MediatR;

namespace MyCostomProjectManagement.Facade.SiteSettings
{
    public interface ISiteSettingFacade
    {
        Task<SiteSettingsDto> Get();
    }
    public class SiteSettingFacade : ISiteSettingFacade
    {
        private readonly IMediator _mediator;

        public SiteSettingFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<SiteSettingsDto> Get()
        {
            return await _mediator.Send(new GetFirstSiteSettingQuery());
        }
    }
}
