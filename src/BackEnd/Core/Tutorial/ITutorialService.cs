using BackEnd.Data.DB;
using BackEnd.Data.Infrastructure.Tutorial.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace BackEnd.Core.Tutorial
{
    public interface ITutorialService
    {
        Task AddRangeAsync(List<TutorialDto> tutorials);
    }

    public class TutorialService : ITutorialService
    {
        private readonly IDbContextFactory<Context> _dbContextFactory;

        public TutorialService(IDbContextFactory<Context> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task AddRangeAsync(List<TutorialDto> tutorials)
        {
            try
            {
                var tutorialList = new List<Data.Entities.Tutorial.Tutorial>();

                using var db = await _dbContextFactory.CreateDbContextAsync();
                var tutorialApiIds = db.Tutorials.Select(i => i.TutorialApiId);
                tutorialList = tutorials.Where(i => tutorialApiIds.Contains(i.Id)).Select(i =>
                new Data.Entities.Tutorial.Tutorial
                {
                    Category = i.Category,
                    CodeBlockCount = i.CodeBlockCount,
                    Content = i.Content,
                    CreatedAt = i.CreatedAt,
                    IsApproved = i.IsApproved,
                    Level = i.Level,
                    Subject = i.Subject,
                    SectionCount = i.SectionCount,
                    Title = i.Title,
                    Topic = i.Topic,
                    TutorialApiId = i.Id,
                    IsActive = true,
                    IsDelete = false,
                }).ToList();

                await db.Tutorials.AddRangeAsync(tutorialList);
                await db.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                string m = ex.Message;
            }
        }
    }
}
