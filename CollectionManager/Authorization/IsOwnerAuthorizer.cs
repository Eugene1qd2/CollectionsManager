using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;
using System.Security.Claims;

namespace CollectionManager.Authorization
{
    public class IsOwnerAuthorizer
    {
        UserManager<IdentityUser> _userManager;

        public IsOwnerAuthorizer(UserManager<IdentityUser>
            userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> AuthorizeAsync(ClaimsPrincipal context, string ownerId)
        {
            if (context == null || context.Claims == null || ownerId == null)
                return false;
            var user = await _userManager.GetUserAsync(context);
            if (user == null) 
                return false;
            var roles = await _userManager.GetRolesAsync(user);
            if (ownerId == user.Id || roles.Contains("admin"))
            {
                return true;
            }
            return false;
        }
    }
}
