using BackEnd.Core.Skills.Queries.DTOs;
using BackEnd.Core.Subscription.DTOs;
using BackEnd.Data.DB;
using BackEnd.Data.Entities.Subscription;
using BackEnd.Shared.CoreShared;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Core.Subscription
{
    public class SubscriptionService
    {
        private readonly IDbContextFactory<Context> _dbContextFactory;

        public SubscriptionService(IDbContextFactory<Context> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<OperationResult> Create(string subscriptionFor, string? description,
            SubscriptionType type, int token, bool isActive, int price)
        {
            await using var db = await _dbContextFactory.CreateDbContextAsync();
            var subscription = new Data.Entities.Subscription.Subscription(subscriptionFor, description, type, token, isActive);
            subscription.Price = price;

            await db.Subscriptions.AddAsync(subscription);
            await db.SaveChangesAsync();
            return OperationResult.Success();
        }

        public async Task<OperationResult> Edit(Guid id, string subscriptionFor, 
            string? description, SubscriptionType type, int token, bool isActive, int price)
        {
            await using var db = await _dbContextFactory.CreateDbContextAsync();
            var subscription = await db.Subscriptions.FirstOrDefaultAsync(i => i.Id.Equals(id));
            if (subscription == null) return OperationResult.NotFound();

            subscription.Edit(subscriptionFor, description, type, token, isActive);
            subscription.Price = price;
            await db.SaveChangesAsync();
            return OperationResult.Success();
        }

        public async Task<OperationResult> Remove(Guid id)
        {
            await using var db = await _dbContextFactory.CreateDbContextAsync();
            var subscription = await db.Subscriptions.FirstOrDefaultAsync(i => i.Id.Equals(id));
            if (subscription == null) return OperationResult.NotFound();

            db.Subscriptions.Remove(subscription);
            await db.SaveChangesAsync();
            return OperationResult.Success();
        }

        public async Task<Data.Entities.Subscription.Subscription> GetId(Guid id)
        {
            await using var db = await _dbContextFactory.CreateDbContextAsync();
            var subscription = await db.Subscriptions.FirstOrDefaultAsync(i => i.Id.Equals(id));
            if (subscription == null) return default;

            return subscription;
        }

        public async Task<SubscriptionFilterResult> GetFilter(SubscriptionFilterParam filterParam, CancellationToken cancellationToken)
        {
            await using var db = await _dbContextFactory.CreateDbContextAsync();
            int skip = (filterParam.PageId - 1) * filterParam.Take;
            //.Skip(skip).Take(filterParam.Take)
            var subscriptions = db.Subscriptions;
            if (subscriptions == null) return default;


            var model = new SubscriptionFilterResult()
            {
                Data = await subscriptions.Skip(skip).Take(filterParam.Take)
                  .Select(i => i.SubscriptionMap(_dbContextFactory)
                  ).ToListAsync(cancellationToken),
                FilterParams = filterParam
            };

            model.GeneratePaging(subscriptions, filterParam.Take, filterParam.PageId);
            return model;
        }
    }
}