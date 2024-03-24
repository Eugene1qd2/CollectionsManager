using CollectionManager.Authorization;
using CollectionManager.Data.Interfaces;
using CollectionManager.Models.Collection;
using CollectionManager.Models.CollectionItem;
using CollectionManager.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CollectionManager.Controllers
{
    public class CollectionController : Controller
    {
        private readonly ICollectionService _collectionService;
        private readonly IsOwnerAuthorizer _isOwnerAuthorizer;
        private readonly ICollectionItemService _collectionItemService;
        public CollectionController(ICollectionService collectionService, IsOwnerAuthorizer isOwnerAuth, ICollectionItemService collectionItemService)
        {
            _collectionService = collectionService;
            _isOwnerAuthorizer = isOwnerAuth;
            _collectionItemService = collectionItemService;
        }

        [HttpGet]
        [Authorize(Policy = "UnlockedPolicy")]
        public async Task<IActionResult> Create(string Id)
        {
            if (!(await _isOwnerAuthorizer.AuthorizeAsync(HttpContext.User, Id)))
                return RedirectToAction("NoAccess", "Home");
            var collection = new DataCollectionViewModel()
            {
                EntireCollectionViewModelId = Guid.NewGuid().ToString(),
                OwnerId = Id,
                SetAsOwner = true
            };
            return View(collection);
        }
        [HttpPost]
        [Authorize(Policy = "UnlockedPolicy")]
        public async Task<IActionResult> Create(DataCollectionViewModel model)
        {
            if (!(await _isOwnerAuthorizer.AuthorizeAsync(HttpContext.User, model.OwnerId)))
                return RedirectToAction("NoAccess", "Home");
            if (!ModelState.IsValid)
                return View(model);

            await _collectionService.CreateData(model);
            return RedirectToAction("Profile", "UserCollections", new { id = model.OwnerId });
        }

        [HttpGet]
        [Authorize(Policy = "UnlockedPolicy")]
        public async Task<IActionResult> Edit(string Id)
        {
            var collectionViewModel = await _collectionService.GetById(Id);
            if (collectionViewModel == null)
                return RedirectToAction("NotFound", "Home");
            if (!(await _isOwnerAuthorizer.AuthorizeAsync(HttpContext.User, collectionViewModel.OwnerId)))
                return RedirectToAction("NoAccess", "Home");
            var collection = new DataCollectionViewModel(collectionViewModel)
            {
                SetAsOwner = true,
            };
            return View(collection);
        }
        [HttpPost]
        [Authorize(Policy = "UnlockedPolicy")]
        public async Task<IActionResult> Edit(DataCollectionViewModel model)
        {
            if (!(await _isOwnerAuthorizer.AuthorizeAsync(HttpContext.User, model.OwnerId)))
                return RedirectToAction("NoAccess", "Home");
            if (!ModelState.IsValid)
                return View(model);
            await _collectionService.EditData(model);
            return RedirectToAction("Profile", "UserCollections", new { id = model.OwnerId });
        }

        [HttpGet]
        [Authorize(Policy = "UnlockedPolicy")]
        public async Task<IActionResult> View(string Id)
        {
            var collection = await _collectionService.GetById(Id);
            if (collection == null)
                return RedirectToAction("NotFound", "Home");
            var collectionData = new DataCollectionViewModel(collection);
            collectionData.SetAsOwner = await _isOwnerAuthorizer.AuthorizeAsync(HttpContext.User, collection.OwnerId);
            collectionData.Items = (await _collectionItemService.GetByCollectionId(Id)).ToList();
            return View(collectionData);
        }

        [HttpGet]
        [Authorize(Policy = "UnlockedPolicy")]
        public async Task<IActionResult> Delete(string Id)
        {
            var model = await _collectionService.GetById(Id);
            if (model == null)
                return RedirectToAction("NotFound", "Home");
            if (!(await _isOwnerAuthorizer.AuthorizeAsync(HttpContext.User, model.OwnerId)))
                return RedirectToAction("NoAccess", "Home");
            return View(model);
        }
        [HttpPost]
        [Authorize(Policy = "UnlockedPolicy")]
        public async Task<IActionResult> Delete(EntireCollectionViewModel model)
        {
            if (!(await _isOwnerAuthorizer.AuthorizeAsync(HttpContext.User, model.OwnerId)))
                return RedirectToAction("NoAccess", "Home");
            await _collectionService.DeleteById(model.EntireCollectionViewModelId);
            return RedirectToAction("Profile", "UserCollections", model.OwnerId);
        }
    }
}
