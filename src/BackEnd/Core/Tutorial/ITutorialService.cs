using BackEnd.Data.DB;
using BackEnd.Data.Entities.Tutorial;
using BackEnd.Data.Infrastructure.Tutorial.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace BackEnd.Core.Tutorial
{
    public interface ITutorialService
    {
        Task<TutorialsFilterResult> AddRangeAsync(List<TutorialDto> tutorials, TutorialFilterParam filterParam);
    }

    public class TutorialService : ITutorialService
    {
        private readonly IDbContextFactory<Context> _dbContextFactory;

        public TutorialService(IDbContextFactory<Context> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<TutorialsFilterResult> AddRangeAsync(List<TutorialDto> tutorials,
            TutorialFilterParam filterParam)
        {
            try
            {
                var tutorialList = new List<Data.Entities.Tutorial.Tutorial>();

                using var db = await _dbContextFactory.CreateDbContextAsync();
                var tutorialApiIds = db.Tutorials.Select(i => i.TutorialApiId);
                tutorialList = tutorials.Where(i => !tutorialApiIds.Contains(i.Id)).Select(i =>
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
                    Slug = i.Title,
                }).ToList();

                await db.Tutorials.AddRangeAsync(tutorialList);
                await db.SaveChangesAsync();


                var result = db.Tutorials.OrderByDescending(i => i.CreationDate).AsQueryable();
                if (!filterParam.IsAdmin)
                {
                    result = result.Where(i => i.IsDelete == false && i.IsActive == true
                    && i.IsApproved == true);
                }

                if (filterParam.Status != null && filterParam.Status != TutorialStatus.None)
                {
                    result = filterParam.Status switch
                    {
                        TutorialStatus.None => result,
                        TutorialStatus.Active => result.Where(i => i.IsActive == true),
                        TutorialStatus.Approved => result.Where(i => i.IsApproved == true),
                        TutorialStatus.Deleted => result.Where(i => i.IsDelete == true),
                        TutorialStatus.Inactive => result.Where(i => i.IsActive == false),
                        TutorialStatus.Pending => result.Where(i => i.IsApproved == false)
                    };
                }

                var skip = (filterParam.PageId - 1) * filterParam.Take;
                var model = new TutorialsFilterResult()
                {
                    Data = await result.Skip(skip).Take(filterParam.Take)
                        .Select(i => new BackEnd.Data.Entities.Tutorial.Tutorial
                        {
                            Category = i.Category,
                            CodeBlockCount = i.CodeBlockCount,
                            Content = i.Content,
                            CreatedAt = i.CreatedAt,
                            Id = i.Id,
                            IsApproved = i.IsApproved,
                            Level = i.Level,
                            SectionCount = i.SectionCount,
                            Subject = i.Subject,
                            Title = i.Title,
                            CreationDate = i.CreationDate,
                            IsActive = i.IsActive,
                            IsDelete = i.IsDelete,
                            TutorialApiId = i.TutorialApiId,
                            Topic = i.Topic,
                            Slug = i.Slug ,
                        }).ToListAsync(),
                    FilterParams = new TutorialFilterParam
                    {
                        PageId = filterParam.PageId,
                        Take = filterParam.Take
                    },
                };

                model.GeneratePaging(result, filterParam.Take, filterParam.PageId);
                return model;
            }
            catch (Exception ex)
            {
                string m = ex.Message;
                return new TutorialsFilterResult();
            }
        }
    }
}
