using BackEnd.Data.DB;
using BackEnd.Data.Entities.SiteSettings;
using BackEnd.Data.Entities.SiteSettings.Repository;
using BackEnd.Shared.CoreShared.Repository;

namespace BackEnd.Infrastructure.Repositories.SiteSettings
{
    internal class SiteSettingRepository : BaseRepository<SiteSetting>, ISiteSettingRepository
    {
        public SiteSettingRepository(Context context) : base(context)
        {
        }
    }
}
