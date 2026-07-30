using BackEnd.Core.PageManagement.Commands;
using BackEnd.Core.PageManagement.Queries.GetList;
using BackEnd.Shared.CoreShared;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MyCostomProjectManagement.Facade.PageManagement
{
    public interface IPageManagementFacade
    {
        Task<OperationResult> Edit(EditPageManagerCommand command);
        Task<OperationResult> Set(SetPageManagementCommand command);

        Task<List<BackEnd.Data.Entities.PageManagement.PageManagement>> GetList();
    }
    public class PageManagementFacade : IPageManagementFacade
    {
        private readonly IMediator _mediator;
        private readonly IMemoryCache _cache;

        private const string pageCacheKey = "PageManagementItems";

        public PageManagementFacade(IMediator mediator, IMemoryCache cache)
        {
            _mediator = mediator;
            _cache = cache;
        }

        public async Task<List<BackEnd.Data.Entities.PageManagement.PageManagement>> GetList()
        {
            if (_cache.TryGetValue(pageCacheKey, out List<BackEnd.Data.Entities.PageManagement.PageManagement> cacheResult))
            {
                try
                {
                    //var result = JsonSerializer.DeserializeAsync<List<BackEnd.Data.Entities.PageManagement.PageManagement>>(cacheResult);
                    return cacheResult;
                }
                catch (Exception e)
                {
                    return await _mediator.Send(new GetListPageManagementQuery());
                }
            }
            else
            {
                return await _mediator.Send(new GetListPageManagementQuery());
            }
        }

        public async Task<OperationResult> Edit(EditPageManagerCommand command)
        {
            var restult = await _mediator.Send(command);
            var datas = await _mediator.Send(new GetListPageManagementQuery());
            _cache.Set<List<BackEnd.Data.Entities.PageManagement.PageManagement>>(pageCacheKey, datas);
            if (_cache.TryGetValue(pageCacheKey, out List<BackEnd.Data.Entities.PageManagement.PageManagement> cacheResult))
            {
                var r = cacheResult;
            }
            return restult;
        }

        public async Task<OperationResult> Set(SetPageManagementCommand command)
        {
            var restult = await _mediator.Send(command);
            var datas = await _mediator.Send(new GetListPageManagementQuery());
            _cache.Set<List<BackEnd.Data.Entities.PageManagement.PageManagement>>(pageCacheKey, datas);
            if (_cache.TryGetValue(pageCacheKey, out List<BackEnd.Data.Entities.PageManagement.PageManagement> cacheResult))
            {
                var r = cacheResult;
            }
            return restult;
        }
    }
}
