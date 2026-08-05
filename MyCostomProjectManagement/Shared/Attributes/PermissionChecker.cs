using BackEnd.Data.DB;
using BackEnd.Data.Entities.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using MyCostomProjectManagement.Facade.Role;
using MyCostomProjectManagement.Facade.User;
using MyCostomProjectManagement.Shared.Utilities;
using System.Security;

namespace MyCostomProjectManagement.Shared.Attributes
{
    public class PermissionChecker(Permissions permission) : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        private Context _context;
        private IRoleFacade _roleFacade;
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (HasAllowAnonymous(context))
                return;

            _context = context.HttpContext.RequestServices.GetRequiredService<Context>();
            _roleFacade = context.HttpContext.RequestServices.GetRequiredService<IRoleFacade>();
          
            if (context.HttpContext.User.Identity != null &&
                context.HttpContext.User.Identity.IsAuthenticated)
            {
                if (await UserHasPermission(context) == false)
                {
                    context.Result = new ForbidResult();
                }
            }
            else
            {
                context.Result = new UnauthorizedObjectResult("Unauthorize");
            }
        }
        private bool HasAllowAnonymous(AuthorizationFilterContext context)
        {
            //comment
            if (_context == null && _roleFacade == null)
            {
                return false;
                throw new Exception("مشکل سمت سرور به وجود آمده");
            }
            var metaData = context.ActionDescriptor.EndpointMetadata.OfType<dynamic>().ToList();
            bool hasAllowAnonymous = false;
            foreach (var f in metaData)
            {
                try
                {
                    hasAllowAnonymous = f.TypeId.Name == "AllowAnonymousAttribute";
                    if (hasAllowAnonymous)
                        break;
                }
                catch
                {
                    // ignored
                }
            }

            return hasAllowAnonymous;
        }
        private async Task<bool> UserHasPermission(AuthorizationFilterContext context)
        {
            var user = await _context.Users.FirstOrDefaultAsync(i =>
            i.PhoneNumber == context.HttpContext.User.GetPhoneNumber());
            
            if (user == null)
                return false;

            var roleIds = user.UserRoles.Select(s => s.RoleId).ToList();
            var roles = await _roleFacade.GetAll();
            if (roles == null)
            {
                return false;
            }
            var userRoles = roles.Where(i => roleIds.Contains(i.Id)).ToList();

            return userRoles.Any(i => i.RolePermissions.Any(i=>i.Permissions.Equals(permission)));
        }
    }
}
