using BackEnd.Core.Subscription.DTOs;
using BackEnd.Shared.CoreShared;

namespace MyCostomProjectManagement.Facade.Subscription
{
    public interface ISubscriptionFacade
    {
        Task<SubscriptionFilterResult> GetFilter(SubscriptionFilterParam filter);
        Task<OperationResult> Create(string subscriptionFor, 
            string? description, int type, int token, bool isActive, int price);
        Task<OperationResult> Edit(Guid id, string subscriptionFor,
            string? description, int type, int token, bool isActive, int price);
        Task<OperationResult> Remove(Guid id);
        Task<OperationResult> AssignToUser(Guid userId, Guid subscriptionId);
        Task<OperationResult> RemoveFromUser(Guid id, Guid userId);
        Task<SubscriptionUserDto?> GetActiveUserSubscription(Guid userId);
        Task<SubscriptionDto?> GetById(Guid id);
    }
}