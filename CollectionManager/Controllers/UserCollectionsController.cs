using CollectionManager.Authorization;
using CollectionManager.Models.Collection;
using CollectionManager.Models.User;
using CollectionManager.Services;
using CollectionManager.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CollectionManager.Controllers
{
    public class UserCollectionsController : Controller
    {
        UserService _userService;
        ICollectionService _collectionService;
        IsOwnerAuthorizer _isOwnerAuthorizer;
        public UserCollectionsController(UserManager<IdentityUser> userManager, ICollectionService collectionService, IsOwnerAuthorizer isOwnerAuth)
        {
            _userService = new UserService(userManager);
            _collectionService = collectionService;
            _isOwnerAuthorizer = isOwnerAuth;
        }

        [Authorize(Policy = "UnlockedPolicy")]
        public async Task<IActionResult> Profile(string Id)
        {
            var profileUser = await _userService.GetUserById(Id);
            var collections = await _collectionService.GetByUserId(Id);
            if (profileUser != null)
            {
                UserProfileDataModel profileDataModel = new UserProfileDataModel(profileUser, collections.ToList());
                profileDataModel.SetAsOwner = await _isOwnerAuthorizer.AuthorizeAsync(HttpContext.User, Id);
                return View(profileDataModel);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
