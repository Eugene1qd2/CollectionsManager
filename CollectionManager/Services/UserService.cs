using CollectionManager.Data.Interfaces;
using CollectionManager.Data.Repositories;
using CollectionManager.Models.Collection;
using CollectionManager.Models.User;
using CollectionManager.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CollectionManager.Services
{

    public class UserService
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IEnumerable<EntireUserViewModel>> GetUsersAsync()
        {
            List<EntireUserViewModel> users = new List<EntireUserViewModel>();
            foreach (var item in await _userManager.Users.ToListAsync())
            {
                var userRole = await _userManager.GetRolesAsync(item);
                users.Add(new EntireUserViewModel(item, userRole.FirstOrDefault()));
            }
            return users;
        }
        public async Task<EntireUserViewModel> GetUserById(string Id)
        {
            var user= await _userManager.FindByIdAsync(Id);
            if (user != null)
            {
                var userRole = await _userManager.GetRolesAsync(user);
                return new EntireUserViewModel(user, userRole.FirstOrDefault());
            }
            else
                return null;
        }
    }
}
