using BackEnd.Core.Subscription;
using BackEnd.Core.Subscription.DTOs;
using BackEnd.Data.Entities.Subscription;
using BackEnd.Shared.CoreShared;

namespace MyCostomProjectManagement.Facade.Subscription
{
    public class SubscriptionFacade : ISubscriptionFacade
    {
        private readonly SubscriptionService _service;
        private readonly SubscriptionUserService _userService;

        public SubscriptionFacade(SubscriptionService service, SubscriptionUserService userService)
        {
            _service = service;
            _userService = userService;
        }

        public Task<SubscriptionFilterResult> GetFilter(SubscriptionFilterParam filter)
            => _service.GetFilter(filter, CancellationToken.None);

        public Task<OperationResult> Create(string subscriptionFor, string? description,
            int type, int token, bool isActive, int price)
            => _service.Create(subscriptionFor, description, (SubscriptionType)type, token, isActive, price);

        public Task<OperationResult> Edit(Guid id, string subscriptionFor, string? description, int type,
            int token, bool isActive, int price)
            => _service.Edit(id, subscriptionFor, description, (SubscriptionType)type, token, isActive, price);

        public Task<OperationResult> Remove(Guid id)
            => _service.Remove(id);

        public Task<OperationResult> AssignToUser(Guid userId, Guid subscriptionId)
            => _userService.Create(userId, subscriptionId);

        public Task<OperationResult> RemoveFromUser(Guid id, Guid userId)
            => _userService.Remove(id, userId);

        public async Task<SubscriptionUserDto?> GetActiveUserSubscription(Guid userId)
        {
            // بارگذاری همه‌ی Subscriptionها و جستجو در SubscriptionUserDtos
            var filter = new SubscriptionFilterParam { PageId = 1, Take = 200, UserId = userId };
            var result = await _service.GetFilter(filter, CancellationToken.None);

            return result?.Data
                .SelectMany(s => s.SubscriptionUserDtos ?? new List<SubscriptionUserDto>())
                .FirstOrDefault(su => su.UserId == userId);
        }
        public async Task<SubscriptionDto?> GetById(Guid id)
        {
            var filter = new SubscriptionFilterParam { PageId = 1, Take = 1, SubscriptionId = id };
            var result = await _service.GetFilter(filter, CancellationToken.None);
            return result?.Data?.FirstOrDefault();
        }
    }
}