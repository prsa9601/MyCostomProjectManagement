using BackEnd.Core.Tutorial;
using BackEnd.Data.Infrastructure.Tutorial.DTOs;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;

namespace BackEnd.Data.Infrastructure.Tutorial
{
    public interface ITutorialApiService
    {
        Task<TutorialFilterResult> GetFilter(TutorialFilterParam filterParam);
        Task<TutorialDto?> Get(int id);
    }
    public class TutorialApiService : ITutorialApiService
    {
        private const string ModuleName = "Tutorials";
        private readonly HttpClient _httpClient;
        private readonly ITutorialService _service;

        public TutorialApiService(HttpClient httpClient, ITutorialService service)
        {
            _httpClient = httpClient;
            _service = service;
        }

        public async Task<TutorialDto?> Get(int id)
        {
            string url = $"{UrlManagement.tutorial}{ModuleName}/{id}";


            var result = await _httpClient.GetFromJsonAsync<TutorialDto>(url);
            return result;
        }

        public async Task<TutorialFilterResult> GetFilter(TutorialFilterParam filterParam)
        {
            try
            {
                string url = $"{UrlManagement.tutorial}{ModuleName}?take={filterParam.Take}&pageId={filterParam.PageId}";


                var result = await _httpClient.GetFromJsonAsync<TutorialFilterResult>(url);

                await _service.AddRangeAsync(result.Data);
                return result;
            }
            catch (Exception e)
            {
                return default;
            }
        }
    }
}
