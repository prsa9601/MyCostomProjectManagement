using BackEnd.Data.DB;
using BackEnd.Data.Entities.User.Repository;
using BackEnd.Shared.CoreShared.Repository;

namespace BackEnd.Infrastructure.Repositories.User
{
    internal sealed class UserRepository : BaseRepository<BackEnd.Data.Entities.User.User>, IUserRepository
    {
        public UserRepository(Context context) : base(context)
        {
        }
    }
}
