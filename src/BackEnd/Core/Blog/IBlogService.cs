using BackEnd.Data.DB;
using BackEnd.Data.Entities.Blogs;
using BackEnd.Data.Infrastructure.Blog.DTOs;
using BackEnd.Data.Infrastructure.Tutorial.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace BackEnd.Core.Blog
{
    public interface IBlogService
    {
        Task<BlogEntityFilterResult> AddRangeAndGetAsync(List<BlogDto> tutorials, BlogFilterParam filterParam);
    }
    public class BlogService : IBlogService
    {
        private readonly IDbContextFactory<Context> _dbContextFactory;

        public BlogService(IDbContextFactory<Context> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<BlogEntityFilterResult> AddRangeAndGetAsync(List<BlogDto> blogs, BlogFilterParam filterParam)
        {
            try
            {
                var blogList = new List<Data.Entities.Blogs.Blog>();

                using var db = await _dbContextFactory.CreateDbContextAsync();
                var blogApiIds = db.Blogs.Select(i => i.BlogApiId);
                blogList = blogs.Where(i => !blogApiIds.Contains(i.Id)).Select(blog => new Data.Entities.Blogs.Blog
                {
                    Content = blog.Content,
                    CreatedAt = blog.CreatedAt,
                    IsUseful = blog.IsUseful,
                    Mode = blog.Mode,
                    Score = blog.Score,
                    ScoreReason = blog.ScoreReason,
                    SeoStatus = blog.SeoStatus,
                    Title = blog.Title,
                    Topic = blog.Topic,
                    BlogApiId = blog.Id,
                    IsApproved = false,
                    IsActive = true,
                    IsDelete = false,
                }).ToList();

                await db.Blogs.AddRangeAsync(blogList);
                await db.SaveChangesAsync();


                var result = db.Blogs.OrderByDescending(i => i.CreationDate).AsQueryable();
                if (!filterParam.IsAdmin)
                {
                    result = result.Where(i => i.IsDelete == false && i.IsActive == true
                    && i.IsApproved == true);
                }

                //if (filterParam.Status != null && filterParam.Status != TutorialStatus.None)
                //{
                //    result = filterParam.Status switch
                //    {
                //        TutorialStatus.None => result,
                //        TutorialStatus.Active => result.Where(i => i.IsActive == true),
                //        TutorialStatus.Approved => result.Where(i => i.IsApproved == true),
                //        TutorialStatus.Deleted => result.Where(i => i.IsDelete == true),
                //        TutorialStatus.Inactive => result.Where(i => i.IsActive == false),
                //        TutorialStatus.Pending => result.Where(i => i.IsApproved == false)
                //    };
                //}

                var skip = (filterParam.PageId - 1) * filterParam.Take;
                var model = new BlogEntityFilterResult()
                {
                    Data = await result.Skip(skip).Take(filterParam.Take)
                        .Select(blog => new Data.Entities.Blogs.Blog
                        {
                            Content = blog.Content,
                            CreatedAt = blog.CreatedAt,
                            Id = blog.Id,
                            IsUseful = blog.IsUseful,
                            Mode = blog.Mode,
                            Score = blog.Score,
                            ScoreReason = blog.ScoreReason,
                            SeoStatus = blog.SeoStatus,
                            Title = blog.Title,
                            Topic = blog.Topic,
                            BlogApiId = blog.BlogApiId,
                            CreationDate = blog.CreationDate,
                            IsActive = blog.IsActive,
                            IsApproved = blog.IsApproved,
                            IsDelete = blog.IsDelete
                        }).ToListAsync(),
                    FilterParams = new BlogFilterParam
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
                return new BlogEntityFilterResult();
            }
        }
    }
}
