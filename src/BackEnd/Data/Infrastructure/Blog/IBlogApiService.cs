using BackEnd.Core.Blog;
using BackEnd.Core.Tutorial;
using BackEnd.Data.Infrastructure.Blog.DTOs;
using BackEnd.Data.Infrastructure.Tutorial.DTOs;
using System.Net.Http.Json;

namespace BackEnd.Data.Infrastructure.Blog
{
    public interface IBlogApiService
    {
        Task<BlogEntityFilterResult> GetFilter(BlogFilterParam filterParam);
        Task<BlogDto?> Get(int id);
    }
    public class BlogApiService : IBlogApiService
    {
        private const string ModuleName = "Article";
        private readonly HttpClient _httpClient;
        private readonly IBlogService _service;

        public BlogApiService(IBlogService service, HttpClient httpClient)
        {
            _service = service;
            _httpClient = httpClient;
        }

        public async Task<BlogDto?> Get(int id)
        {
            string url = $"{UrlManagement.tutorial}{ModuleName}/{id}";


            var result = await _httpClient.GetFromJsonAsync<BlogDto>(url);
            return result;
        }

        public async Task<BlogEntityFilterResult> GetFilter(BlogFilterParam filterParam)
        {
            try
            {
                string url = $"{UrlManagement.tutorial}{ModuleName}?take={filterParam.Take}&pageId={filterParam.PageId}";


                var result = await _httpClient.GetFromJsonAsync<BlogFilterResult>(url);

                var blogs = await _service.AddRangeAndGetAsync(result.Data, filterParam);
                return blogs;
            }
            catch (Exception e)
            {
                return default;
            }
        }
    }
}
