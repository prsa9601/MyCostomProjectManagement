using BackEnd.Data.Entities.Subscription;
using BackEnd.Shared.CoreShared.Queries;
using BackEnd.Shared.DataShared;

namespace BackEnd.Core.Subscription.DTOs
{
    public class SubscriptionDto : BaseDto
    {
        /// <summary>
        /// Title
        /// </summary>
        public string SubscriptionFor { get; set; }
        public string? Description { get; set; }
        public SubscriptionType Type { get; set; }
        public int Token { get; set; } = 0;
        public int Price { get; set; }
        public bool IsActive { get; set; } = false;
        public List<SubscriptionUserDto> SubscriptionUserDtos { get; set; } = new();
    }    
    public class SubscriptionFilterParam : BaseFilterParam
    {
        public string Search { get; set; }
        public bool? IsActive { get; set; }
        public Guid? UserId { get; set; }
        public Guid? SubscriptionId { get; set; }
    }
    public class SubscriptionFilterResult : BaseFilter<SubscriptionDto, SubscriptionFilterParam>
    {
    }

    public class SubscriptionUserDto : BaseDto
    {
        public Guid UserId { get; set; }
        public Guid SubscriptionId { get; set; }
        public string UserName { get; set; }
        public int UsedToken { get; set; }
        public int RemainingToken { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        public string SubscriptionFor { get; set; }
        public string? Description { get; set; }
        public SubscriptionType Type { get; set; }

    }
    public class SubscriptionUserFilterParam : BaseFilterParam
    {
        public string Search { get; set; }
        public bool? IsActive { get; set; }
        public Guid? UserId { get; set; }
        public Guid? SubscriptionId { get; set; }
    }
    public class SubscriptionUserFilterResult : BaseFilter<SubscriptionUserDto, SubscriptionUserFilterParam>
    {
    }
}
