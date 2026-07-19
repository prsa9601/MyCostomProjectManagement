using System.Security.Claims;

namespace BackEnd.Shared.UserClaimExtentions
{

    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal user)
        {
            var userIdString = user.FindFirst(ClaimTypes.NameIdentifier).Value;
            return Guid.TryParse(userIdString, out var guid) ? guid : Guid.Empty;
        }
    }
}
