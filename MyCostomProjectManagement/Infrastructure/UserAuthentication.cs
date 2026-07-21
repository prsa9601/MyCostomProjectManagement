using BackEnd.Shared.UserClaimExtentions;
using Microsoft.AspNetCore.Components.Authorization;

namespace MyCostomProjectManagement.Infrastructure
{
    public class UserAuthentication
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public UserAuthentication(AuthenticationStateProvider authenticationStateProvider)
        {
            _authenticationStateProvider = authenticationStateProvider;
        }
        public async Task<Guid> GetCurrentUserId()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var userAuth = authState.User;
            if (userAuth.Identity.IsAuthenticated == false)
            {

            }
            return userAuth.GetUserId();
        }
    }
}
