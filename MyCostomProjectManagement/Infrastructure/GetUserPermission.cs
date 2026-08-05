using BackEnd.Data.Entities.Role;
using MyCostomProjectManagement.Facade.Role;
using MyCostomProjectManagement.Facade.User;

namespace MyCostomProjectManagement.Infrastructure
{
    public class GetUserPermission
    {
        private readonly IUserFacade _userFacade;
        private readonly IRoleFacade _roleFacade;
        private readonly UserAuthentication _userAuthentication;

        public GetUserPermission(IUserFacade userFacade, IRoleFacade roleFacade, UserAuthentication userAuthentication)
        {
            _userFacade = userFacade;
            _roleFacade = roleFacade;
            _userAuthentication = userAuthentication;
        }

        public async Task<bool> CheckAccess(Permissions permissions)
        {
            var currentUserId = await _userAuthentication.GetCurrentUserId();
            if (currentUserId == Guid.Empty) return false;

            var user = await _userFacade.GetId(currentUserId);
            if (user == null || user == default) return false;

            var roles = await _roleFacade.GetAll();
            if (roles == null || roles == default || roles.Count == 0) return false;

            var userRoleIds = user.UserRoles.Select(i => i.RoleId);
            var userRoles = roles.Where(i => userRoleIds.Contains(i.Id));

            return userRoles.Any(i => i.RolePermissions.Any(p => p.Permissions.Equals(permissions)));
        }
    }
}
