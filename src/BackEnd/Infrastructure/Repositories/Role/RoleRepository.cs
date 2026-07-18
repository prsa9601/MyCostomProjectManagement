using BackEnd.Data.DB;
using BackEnd.Data.Entities.Role.Repository;
using BackEnd.Shared.CoreShared.Repository;

namespace BackEnd.Infrastructure.Repositories.Role
{
    internal sealed class RoleRepository : BaseRepository<BackEnd.Data.Entities.Role.Role>, IRoleRepository
    {
        public RoleRepository(Context context) : base(context)
        {
        }
    }
}
