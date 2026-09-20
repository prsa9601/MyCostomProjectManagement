using BackEnd.Data.DB;
using BackEnd.Data.Entities.FAQ;
using BackEnd.Data.Entities.FAQ.Repository;
using BackEnd.Shared.CoreShared.Repository;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BackEnd.Infrastructure.Repositories.FAQ
{
    public class FAQRepository : BaseRepository<Data.Entities.FAQ.FAQ>, IFAQRepository
    {
        public FAQRepository(Context context) : base(context)
        {
        }

        public async Task<bool> SortSequenseOfFAQTableForCreateColumn(int sequense)
        {
            int rowsAffected = await Context.Database.ExecuteSqlInterpolatedAsync(
                $"UPDATE FAQ.faq SET Sequense = Sequense + 1 WHERE Sequense >= {sequense}"
            );

            return rowsAffected > 0;
        }

        public async Task<bool> SortSequenseOfFAQTableForEditColumn(int sequense, int oldSequense)
        {
            int rowsAffected = 0;
            if (oldSequense > sequense)
            {

                rowsAffected = await Context.Database.ExecuteSqlInterpolatedAsync(
                   $"UPDATE FAQ.faq SET Sequense = Sequense + 1 WHERE Sequense < {oldSequense} And Sequense >= {sequense}"
               );
            }
            else if (oldSequense < sequense)
            {
                rowsAffected = await Context.Database.ExecuteSqlInterpolatedAsync(
                   $"UPDATE FAQ.faq SET Sequense = Sequense - 1 WHERE Sequense <= {sequense} And Sequense > {oldSequense}"
               );
            }
            else
            {
                return true;
            }

            return rowsAffected > 0;
        }

        public async Task<bool> SortSequenseOfFAQTableForRemoveColumn(int sequense)
        {
            int rowsAffected = await Context.Database.ExecuteSqlInterpolatedAsync(
               $"UPDATE FAQ.faq SET Sequense = Sequense - 1 WHERE Sequense > {sequense}"
            );

            return rowsAffected > 0;
        }
    }
}
