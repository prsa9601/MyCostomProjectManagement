using BackEnd.Shared.CoreShared.Queries;

namespace BackEnd.Core.User.Queries.DTOs.UserFilterDto
{
    public class UserFilterResult : BaseFilter<UserDto, UserFilterParam>
    {
    }
    public class UserFilterParam : BaseFilterParam
    {
        public string? Search { get; set; }
    }
}
