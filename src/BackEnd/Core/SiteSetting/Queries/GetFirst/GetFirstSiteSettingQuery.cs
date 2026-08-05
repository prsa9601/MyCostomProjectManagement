using AutoMapper;
using BackEnd.Core.SiteSetting.Queries.DTOs;
using BackEnd.Data.DB;
using BackEnd.Data.Entities.SiteSettings;
using BackEnd.Shared.CoreShared.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Core.SiteSetting.Queries.GetFirst
{
    public class GetFirstSiteSettingQuery : IQuery<SiteSettingsDto>
    {
    }

    public class GetFirstSiteSettingQueryHandler : IQueryHandler<GetFirstSiteSettingQuery, SiteSettingsDto>
    {
        private readonly IDbContextFactory<Context> _dbContextFactory;
        private readonly IMapper _mapper;

        public GetFirstSiteSettingQueryHandler(IDbContextFactory<Context> dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public async Task<SiteSettingsDto> Handle(GetFirstSiteSettingQuery request, CancellationToken cancellationToken)
        {
            var context = await _dbContextFactory.CreateDbContextAsync();
            var result = await context.SiteSettings.Include(i => i.SiteLinks)
                .Include(i => i.AboutMeSection).ThenInclude(i => i.AboutStats).FirstOrDefaultAsync();
            if (result == null) return default;

            return _mapper.Map<Data.Entities.SiteSettings.SiteSetting, SiteSettingsDto>(result);
        }
    }
}
