using BackEnd.Data.DB;
using BackEnd.Data.Entities.Subscription;
using BackEnd.Data.Entities.User;
using BackEnd.Shared.CoreShared;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Core.Subscription
{
    public class SubscriptionUserService
    {
        private readonly IDbContextFactory<Context> _dbContextFactory;

        public SubscriptionUserService(IDbContextFactory<Context> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<OperationResult> Create(Guid userId, Guid subscriptionId)
        {
            await using var db = await _dbContextFactory.CreateDbContextAsync();

            var subscription = await db.Subscriptions
                .FirstOrDefaultAsync(i => i.Id.Equals(subscriptionId));

            if (subscription == null) return OperationResult.NotFound();

            var user = await db.Users
                .FirstOrDefaultAsync(i => i.Id.Equals(userId));

            if (user == null) return OperationResult.NotFound();



            var subscriptionUser = new Data.Entities.Subscription.SubscriptionUser(subscription.Token,
                userId, subscriptionId, subscription.SubscriptionFor, subscription.Description, subscription.Type);

            await db.SubscriptionUsers.AddAsync(subscriptionUser);
            await db.SaveChangesAsync();
            return OperationResult.Success();
        }

        public async Task<OperationResult> Edit(Guid subscriptionId, Guid userId, Guid id)
        {
            await using var db = await _dbContextFactory.CreateDbContextAsync();
            var subscriptionUser = await db.SubscriptionUsers
                .FirstOrDefaultAsync(i => i.Id.Equals(id));

            if (subscriptionUser == null) return OperationResult.NotFound();

            var subscription = await db.Subscriptions
                .FirstOrDefaultAsync(i => i.Id.Equals(subscriptionId));

            if (subscription == null) return OperationResult.NotFound();

            var user = await db.Users
                .FirstOrDefaultAsync(i => i.Id.Equals(userId));

            if (user == null) return OperationResult.NotFound();

            subscriptionUser.Edit(subscription.Token,
                userId, subscriptionId, subscription.SubscriptionFor, subscription.Description, subscription.Type);

            await db.SaveChangesAsync();
            return OperationResult.Success();
        }

        public async Task<OperationResult> Remove(Guid id, Guid userId)
        {
            await using var db = await _dbContextFactory.CreateDbContextAsync();
            var subscriptionUser = await db.Subscriptions.FirstOrDefaultAsync(i => i.Id.Equals(id));
            if (subscriptionUser == null) return OperationResult.NotFound();

            var user = await db.Users
                .FirstOrDefaultAsync(i => i.Id.Equals(userId));

            if (user == null) return OperationResult.NotFound();

            //var userRoles = user.UserRoles;
            //var r = userRoles.Select(i => i.RoleId);
            //var roles = db.Roles.Include(i=>i.RolePermissions).Where(i => r.Contains(i.Id));

            //var p = roles.Select(i => i.RolePermissions);
            //var t =p.Any(i=>i.Any(b=>b.Permissions==Data.Entities.Role.Permissions.));


            db.Subscriptions.Remove(subscriptionUser);
            await db.SaveChangesAsync();
            return OperationResult.Success();
        }
    }
}
