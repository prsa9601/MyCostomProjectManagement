using BackEnd.Shared.CoreShared.Repository;

namespace BackEnd.Data.Entities.User.Repository
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<Data.Entities.User.User?> GetTrackingWithPhoneNumber(string phoneNumber, params string[] includs);

    }
}
