using CollectionManager.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CollectionManager.Services
{
    public class AdministrateUsersService
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AdministrateUsersService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<EntireUserViewModel> GetUserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return null;
            var userRole = await _userManager.GetRolesAsync(user);
            return new EntireUserViewModel(user, userRole.FirstOrDefault());
        }

        public async Task DeleteUserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
                await _userManager.DeleteAsync(user);
        }

        public async Task<IEnumerable<EntireUserViewModel>> GetUsersDataAsync()
        {
            List<EntireUserViewModel> users = new List<EntireUserViewModel>();
            foreach (var item in await _userManager.Users.ToListAsync())
            {
                var userRole = await _userManager.GetRolesAsync(item);
                users.Add(new EntireUserViewModel(item, userRole.FirstOrDefault()));
            }
            return users;
        }
        public async Task SwitchUserLockStateAsync(string userId)
        {
            IdentityUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                await _userManager.SetLockoutEnabledAsync(user, true);
                await LockoutUser(user);
            }
        }

        public async Task ChangeUserAdminRoleStateAsync(string userId)
        {
            IdentityUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                await ChangeRoles(user);
            }
        }

        private async Task ChangeRoles(IdentityUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            if (userRoles != null && userRoles.Count > 0)
                await _userManager.RemoveFromRoleAsync(user, "admin");
            else
                await _userManager.AddToRoleAsync(user, "admin");
        }
        private async Task LockoutUser(IdentityUser user)
        {
            if (user.LockoutEnd < DateTime.Now)
                await _userManager.SetLockoutEndDateAsync(user, DateTime.Now.AddYears(100));
            else
                await _userManager.SetLockoutEndDateAsync(user, DateTime.Now);
        }
    }
}
