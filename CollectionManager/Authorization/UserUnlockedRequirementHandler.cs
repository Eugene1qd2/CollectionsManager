using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace CollectionManager.Authorization
{
    public class UserUnlockedRequirementHandler : AuthorizationHandler<UserUnlockedRequirement>
    {
        UserManager<IdentityUser> _userManager { get; set; }
        SignInManager<IdentityUser> _signInManager { get; set; }

        public UserUnlockedRequirementHandler(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, UserUnlockedRequirement requirement)
        {
            var user = await _userManager.GetUserAsync(context.User);
            if (user != null && user.LockoutEnd > DateTime.Now)
            {
                await _signInManager.SignOutAsync();
                context.Fail();
                return;
            }
            context.Succeed(requirement);
        }
    }
}
