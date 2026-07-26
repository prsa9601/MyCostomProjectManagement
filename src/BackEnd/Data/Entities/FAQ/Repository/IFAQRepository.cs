using BackEnd.Shared.CoreShared.Repository;

namespace BackEnd.Data.Entities.FAQ.Repository
{
    public interface IFAQRepository : IBaseRepository<FAQ>
    {
        Task<bool> SortSequenseOfFAQTableForCreateColumn(int sequense);
        Task<bool> SortSequenseOfFAQTableForRemoveColumn(int sequense);
        Task<bool> SortSequenseOfFAQTableForEditColumn(int sequense, int oldSequense);
    }
}
