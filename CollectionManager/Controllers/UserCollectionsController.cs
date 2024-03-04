using CollectionManager.Models.Collection;
using CollectionManager.Models.User;
using CollectionManager.Services;
using CollectionManager.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CollectionManager.Controllers
{
    public class UserCollectionsController : Controller
    {
        UserService _userService;
        ICollectionService _collectionService;
        public UserCollectionsController(UserManager<IdentityUser> userManager,ICollectionService collectionService)
        {
            _userService = new UserService(userManager);
            _collectionService = collectionService;
        }

        [Authorize(Policy = "UnlockedPolicy")]
        public async Task<IActionResult> Profile(string Id)
        {
            var profileUser = await _userService.GetUserById(Id);
            
            if (profileUser == null)
                return RedirectToAction("Index", "Home");
            var collections=await _collectionService.GetByUserId(Id);
            UserCollectionsViewModel userCollections = new UserCollectionsViewModel()
            {
                User = profileUser,
                Collections = collections.ToList(),
            };
            //получаю id, вывожу данные на основе id
            //есть кнопки, которые не всем видны 
            return View(userCollections);
        }
    }
}
