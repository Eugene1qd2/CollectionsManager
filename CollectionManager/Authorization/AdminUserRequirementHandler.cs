using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Security.Principal;

namespace CollectionManager.Authorization
{
    public class AdminUserRequirementHandler : AuthorizationHandler<AdminUserRequirement>
    {
        UserManager<IdentityUser> _userManager { get; set; }
        SignInManager<IdentityUser> _signInManager { get; set; }

        public AdminUserRequirementHandler(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminUserRequirement requirement)
        {
            var user = await _userManager.GetUserAsync(context.User);
            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                if (user != null && !roles.Contains("admin"))
                {
                    await _signInManager.SignOutAsync();
                    context.Fail();
                    return;
                }
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

        }
    }
}
