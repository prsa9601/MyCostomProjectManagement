using BackEnd.Shared.CoreShared.Queries;

namespace BackEnd.Core.Role.Queries.DTOs
{
    public class RoleFilterParam : BaseFilterParam
    {
        public string Search { get; set; }
        public Guid UserId { get; set; }
    }
    public class RoleFilterResult : BaseFilter<RoleDto, RoleFilterParam>
    {
    }
}
