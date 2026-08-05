using BackEnd.Data.DB;
using BackEnd.Data.Entities.User;
using BackEnd.Data.Entities.User.Repository;
using BackEnd.Shared.CoreShared.Repository;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Infrastructure.Repositories.User
{
    internal sealed class UserRepository : BaseRepository<BackEnd.Data.Entities.User.User>, IUserRepository
    {
        public UserRepository(Context context) : base(context)
        {
        }

        public async Task<Data.Entities.User.User?> GetTrackingWithPhoneNumber(string phoneNumber, params string[] includs)
        {
            IQueryable<Data.Entities.User.User> query = Context.Set<Data.Entities.User.User>().AsTracking();

            // اعمال Include‌ها (اگر آرایه خالی نباشد)
            if (includs != null && includs.Length > 0)
            {
                foreach (var include in includs)
                {
                    query = query.Include(include);
                }
            }

            return await query.FirstOrDefaultAsync(i => i.PhoneNumber == phoneNumber);
        }
    }
}
