﻿using CollectionManager.Models.User;
using CollectionManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CollectionManager.Controllers
{
    public class AdminPanelController : Controller
    {
        AdministrateUsersService _administrateUsersService;
        public AdminPanelController(UserManager<IdentityUser> userManager)
        {
            _administrateUsersService = new AdministrateUsersService(userManager);
        }

        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> Users()
        {
            IEnumerable<EntireUserViewModel> entireUsers = await _administrateUsersService.GetUsersDataAsync();
            return View(entireUsers);
        }
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> ChangeAdminRoleState(string Id)
        {
            await _administrateUsersService.ChangeUserAdminRoleStateAsync(Id);
            return RedirectToAction("Users", "AdminPanel");
        }
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> ChangeLockState(string Id)
        {
            await _administrateUsersService.SwitchUserLockStateAsync(Id);
            return RedirectToAction("Users", "AdminPanel");
        }
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> ViewUserProfile(string Id)
        {
            return RedirectToAction("Profile", "UserCollections", Id);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpGet]
        public async Task<IActionResult> DeleteUser(string Id)
        {
            var userToDelete = await _administrateUsersService.GetUserByIdAsync(Id);
            if (userToDelete != null)
                return View(userToDelete);
            return RedirectToAction("Users");
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost]
        public async Task<IActionResult> DeleteUser(EntireUserViewModel model)
        {
            await _administrateUsersService.DeleteUserByIdAsync(model.Id);
            return RedirectToAction("Users");
        }
    }
}
