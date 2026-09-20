using BackEnd.Core.Subscription.DTOs;
using BackEnd.Data.DB;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Core.Subscription
{
    public static class SubscriptionMapper
    {
        public static SubscriptionDto SubscriptionMap(this Data.Entities.Subscription.Subscription subscription,
            IDbContextFactory<Context> dbContextFactory)
        {
            using var db = dbContextFactory.CreateDbContext();

            var subcriptionUsers = db.SubscriptionUsers.Where(i => i.SubscriptionId == subscription.Id);
            var result = new SubscriptionDto
            {
                Id = subscription.Id,
                SubscriptionFor = subscription.SubscriptionFor,
                CreationDate = subscription.CreationDate,
                Description = subscription.Description,
                IsActive = subscription.IsActive,
                Price = subscription.Price,
                Token = subscription.Token,
                Type = subscription.Type,
                SubscriptionUserDtos = subcriptionUsers.Select(i => i.SubscriptionUserMap(dbContextFactory)).ToList(),
            };

            return result;
        }
        public static SubscriptionUserDto SubscriptionUserMap(this Data.Entities.Subscription.SubscriptionUser subscriptionUser,
            IDbContextFactory<Context> dbContextFactory)
        {
            using var db = dbContextFactory.CreateDbContext();

            var user = db.Users.FirstOrDefault(i => i.Id.Equals(subscriptionUser.UserId));
            var subscription = db.Subscriptions.FirstOrDefault(i => i.Id.Equals(subscriptionUser.SubscriptionId));
            return new SubscriptionUserDto
            {
                Id = subscriptionUser.Id,
                SubscriptionFor = subscriptionUser.SubscriptionFor,
                CreationDate = subscriptionUser.CreationDate,
                Description = subscriptionUser.Description,
                UserId = subscriptionUser.UserId,
                SubscriptionId = subscriptionUser.SubscriptionId,
                UserName = user == null ? "" : user.FullName,
                RemainingToken = subscriptionUser.RemainingToken,
                UsedToken = subscriptionUser.UsedToken,
                Type = subscriptionUser.Type,
            };
        }
    }
}
