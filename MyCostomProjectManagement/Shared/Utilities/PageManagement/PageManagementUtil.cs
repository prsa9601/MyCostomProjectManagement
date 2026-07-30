using Microsoft.Extensions.Caching.Memory;
using MyCostomProjectManagement.Facade.PageManagement;
using MyCostomProjectManagement.Shared.Extensions;
using static MyCostomProjectManagement.Components.Pages.Admin.PageManagement.Index;

namespace MyCostomProjectManagement.Shared.Utilities.PageManagement
{
    public class PageManagementUtil
    {
        private readonly IPageManagementFacade _pageManagementFacade;
        private readonly IMemoryCache _memoryCache;

        public PageManagementUtil(IPageManagementFacade pageManagementFacade, IMemoryCache memoryCache)
        {
            _pageManagementFacade = pageManagementFacade;
            _memoryCache = memoryCache;
        }

        public async Task<List<BackEnd.Data.Entities.PageManagement.PageManagement>> GetPagesItem()
        {
            List<BackEnd.Data.Entities.PageManagement.PageManagement> PageItems = new();
            var pageResult = await _pageManagementFacade.GetList();

            PageItems = GetRoutesExtentions.GetAllRoutes().Select(i => new BackEnd.Data.Entities.PageManagement.PageManagement
            {
                Name = i.Replace("/", ""),
                CustomMessage = "        تیم ما در حال بهبود و به‌روزرسانی این بخش است تا تجربه‌ای بهتر را\r\n            برای شما فراهم کند. لطفاً شکیبا باشید و پس از اتمام، دوباره بازدید کنید.",
                IsUnderConstruction = false,
                Url = i,
            }).ToList();
            foreach (var item in PageItems.ToList())
            {
                var s = pageResult.FirstOrDefault(i => i.Url == item.Url);
                if (s != null)
                {
                    //s.SeoIndexing = item.SeoIndexing;
                    //item = s;
                    var index = PageItems.IndexOf(item); // پیدا کردن ایندکس
                    if (index != -1)
                        PageItems[index] = s;
                }
            }
            if (pageResult.Count == PageItems.Count && pageResult.Equals(PageItems))
            {
                foreach (var item in pageResult)
                {
                    if (!PageItems.Contains(item))
                    {
                        var setResult1 = await _pageManagementFacade.Set(new BackEnd.Core.PageManagement.Commands.SetPageManagementCommand
                        {
                            pageManagements = PageItems
                        });
                        return PageItems;
                    }
                }
                return pageResult;
            }
            var setResult = await _pageManagementFacade.Set(new BackEnd.Core.PageManagement.Commands.SetPageManagementCommand
            {
                pageManagements = PageItems
            });
            return PageItems;
        }

        public async Task<bool> SetPagesItem(List<BackEnd.Data.Entities.PageManagement.PageManagement> pages)
        {
            var result = await _pageManagementFacade.Set(new BackEnd.Core.PageManagement.Commands.SetPageManagementCommand
            {
                pageManagements = pages
            });

            return result.Status == BackEnd.Shared.CoreShared.OperationResultStatus.Success ? true : false;
        }

        public async Task<bool> ToggleStatus(BackEnd.Data.Entities.PageManagement.PageManagement page)
        {
            var result = await _pageManagementFacade.Edit(new BackEnd.Core.PageManagement.Commands.EditPageManagerCommand
            {
                pageManagement = page
            });

            return result.Status == BackEnd.Shared.CoreShared.OperationResultStatus.Success ? true : false;
        }

        public async Task<bool> CheckStatus(string path)
        {
            var pageResult = await _pageManagementFacade.GetList();
            var pageExist = pageResult.Select(i => i.Url).FirstOrDefault(path);
            if (pageExist != null) return true;
            return false;
        }

    }
}
