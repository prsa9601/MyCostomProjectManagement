using BackEnd.Shared.DataShared;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Data.Entities.Subscription
{
    public class SubscriptionUser : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid SubscriptionId { get; set; }
        public int UsedToken { get; set; }
        public int RemainingToken { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        public string SubscriptionFor { get; set; }
        public string? Description { get; set; }
        public SubscriptionType Type { get; set; }

        public SubscriptionUser()
        {
            
        }
        public SubscriptionUser(int token, Guid userId, Guid subscriptionId, string subscriptionFor, string? description, SubscriptionType type)
        {
            RemainingToken = token;
            UserId = userId;
            SubscriptionId = subscriptionId;
            SubscriptionFor = subscriptionFor;
            Description = description;
            Type = type;
        }
        
        public void Edit(int token, Guid userId, Guid subscriptionId, string subscriptionFor, string? description, SubscriptionType type)
        {
            RemainingToken = token;
            UserId = userId;
            SubscriptionId = subscriptionId;
            SubscriptionFor = subscriptionFor;
            Description = description;
            Type = type;
        }
        
        public void UseToken(int token)
        {
            RemainingToken -= token;
            UsedToken += token;
        }

    }
    public class Subscription : BaseEntity
    {
        public Subscription(string subscriptionFor, string? description, SubscriptionType type, int token, bool isActive)
        {
            SubscriptionFor = subscriptionFor;
            Description = description;
            Type = type;
            Token = token;
            IsActive = isActive;
        }
        public void Edit(string subscriptionFor, string? description, SubscriptionType type, int token, bool isActive)
        {
            SubscriptionFor = subscriptionFor;
            Description = description;
            Type = type;
            Token = token;
            IsActive = isActive;
        }

        /// <summary>
        /// Title
        /// </summary>
        public string SubscriptionFor { get; set; }
        public string? Description { get; set; }
        public SubscriptionType Type { get; set; }
        public int Token { get; set; } = 0;
        public int Price { get; set; }
        public bool IsActive { get; set; } = false;
        public Subscription()
        {
            
        }
    }
    public enum SubscriptionType 
    {
        AutomaticArticleGenerator
    }
}
