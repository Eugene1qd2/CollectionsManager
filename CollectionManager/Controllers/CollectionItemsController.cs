using CollectionManager.Authorization;
using CollectionManager.Services.Interfaces;
using CollectionManager.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CollectionManager.Models.CollectionItem;
using CollectionManager.Models.Collection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CollectionManager.Controllers
{
    public class CollectionItemsController : Controller
    {
        ICollectionService _collectionService;
        ICollectionItemService _collectionItemService;
        IsOwnerAuthorizer _isOwnerAuthorizer;
        UserManager<IdentityUser> _userManager;

        public CollectionItemsController(ICollectionItemService collectionItemService, ICollectionService collectionService, IsOwnerAuthorizer isOwnerAuth, UserManager<IdentityUser> userManager)
        {
            _collectionItemService = collectionItemService;
            _collectionService = collectionService;
            _isOwnerAuthorizer = isOwnerAuth;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize(Policy = "UnlockedPolicy")]
        public async Task<IActionResult> AppendItem(string Id)
        {
            var collectionOwner = await _collectionService.GetById(Id);
            if (await _isOwnerAuthorizer.AuthorizeAsync(User, collectionOwner.OwnerId))
            {
                return View(new CollectionItemDataModel(Guid.NewGuid().ToString(), Id, collectionOwner));
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        [Authorize(Policy = "UnlockedPolicy")]
        public async Task<IActionResult> AppendItem(CollectionItemDataModel model)
        {
            if (ModelState.IsValid)
            {
                await _collectionItemService.Create(model);
                return RedirectToAction("View", "Collection", new { Id = model.CollectionId });
            }
            model.collection = await _collectionService.GetById(model.CollectionId);
            return View(model);
        }
        [HttpGet]
        [Authorize(Policy = "UnlockedPolicy")]
        public async Task<IActionResult> View(string Id)
        {
            var model = await _collectionItemService.GetByIdTagged(Id);
            var collection = await _collectionService.GetById(model.CollectionId);
            var name = (await _userManager.FindByIdAsync(collection.OwnerId)).UserName;
            return View(new CollectionItemDataPair(model, collection, name));
        }

        [HttpGet]
        [Authorize(Policy = "UnlockedPolicy")]
        public async Task<IActionResult> DeleteItem(string Id)
        {
            var model = await _collectionItemService.GetById(Id);
            var collection = await _collectionService.GetById(model.CollectionId);
            return View(new CollectionItemDataPair(model, collection));
        }

        [HttpPost]
        [Authorize(Policy = "UnlockedPolicy")]
        public async Task<IActionResult> DeleteItem(CollectionItemDataPair modelId)
        {
            await _collectionItemService.DeleteById(modelId.item.EntireItemViewModelId);
            return RedirectToAction("View", "Collection", new { Id = modelId.item.CollectionId });
        }


        [HttpGet]
        [Authorize(Policy = "UnlockedPolicy")]
        public async Task<IActionResult> EditItem(string Id)
        {
            var item = new CollectionItemDataModel(await _collectionItemService.GetByIdTagged(Id));
            item.collection = await _collectionService.GetById(item.CollectionId);
            if (await _isOwnerAuthorizer.AuthorizeAsync(User, item.collection.OwnerId))
            {
                return View(item);
            }
            return RedirectToAction("NoAccess", "Home");
        }
        [HttpPost]
        [Authorize(Policy = "UnlockedPolicy")]
        public async Task<IActionResult> EditItem(CollectionItemDataModel model)
        {
            if (ModelState.IsValid)
            {
                await _collectionItemService.Edit(model);
                return RedirectToAction("View", "Collection", new { Id = model.CollectionId });
            }
            model.collection = await _collectionService.GetById(model.CollectionId);
            
            return View(model);
        }

    }
}
