using CollectionManager.Authorization;
using CollectionManager.Data.Interfaces;
using CollectionManager.Models.Collection;
using CollectionManager.Models.CollectionItem;
using CollectionManager.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CollectionManager.Controllers
{
    public class CollectionController : Controller
    {
        private DataCollectionViewModel currentCollection;
        private readonly ICollectionService _collectionService;
        private readonly IsOwnerAuthorizer _isOwnerAuthorizer;
        private readonly ICollectionItemService _collectionItemService;
        public CollectionController(ICollectionService collectionService, IsOwnerAuthorizer isOwnerAuth, ICollectionItemService collectionItemService)
        {
            _collectionService = collectionService;
            currentCollection = new DataCollectionViewModel();
            _isOwnerAuthorizer = isOwnerAuth;
            _collectionItemService = collectionItemService;
        }

        [HttpGet]
        [Authorize(Policy = "UnlockedPolicy")]
        public async Task<IActionResult> Create(string Id)
        {
            if (await _isOwnerAuthorizer.AuthorizeAsync(HttpContext.User, Id))
            {
                currentCollection.EntireCollectionViewModelId = Guid.NewGuid().ToString();
                currentCollection.OwnerId = Id;
                currentCollection.SetAsOwner = true;
                return View(currentCollection);
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        [Authorize(Policy = "UnlockedPolicy")]
        public async Task<IActionResult> Create(DataCollectionViewModel model)
        {
            if (await _isOwnerAuthorizer.AuthorizeAsync(HttpContext.User, model.OwnerId))
            {
                if (ModelState.IsValid)
                {
                    currentCollection = model;
                    await _collectionService.CreateData(currentCollection);
                    return RedirectToAction("Profile", "UserCollections", new { id = currentCollection.OwnerId });
                }
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize(Policy = "UnlockedPolicy")]
        public async Task<IActionResult> Edit(string Id)
        {
            if (await _isOwnerAuthorizer.AuthorizeAsync(HttpContext.User, Id))
            {
                var collectionViewModel = await _collectionService.GetById(Id);
                if (collectionViewModel != null)
                {
                    currentCollection = new DataCollectionViewModel(collectionViewModel);
                    currentCollection.SetAsOwner = true;
                    return View(currentCollection);
                }
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(DataCollectionViewModel model)
        {
            if (await _isOwnerAuthorizer.AuthorizeAsync(HttpContext.User, model.OwnerId))
            {
                if (ModelState.IsValid)
                {
                    currentCollection = model;
                    await _collectionService.EditData(currentCollection);
                    return RedirectToAction("Profile", "UserCollections", new { id = currentCollection.OwnerId });
                }
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> View(string Id)
        {
            //возможно что-то не то
            var collection = new DataCollectionViewModel(await _collectionService.GetById(Id));
            collection.SetAsOwner = await _isOwnerAuthorizer.AuthorizeAsync(HttpContext.User, collection.OwnerId);
            IEnumerable<EntireItemViewModel> items = await _collectionItemService.GetByCollectionId(Id);
            collection.Items = items.ToList();
            return View(collection);
        }

        [HttpGet]
        [Authorize(Policy = "UnlockedPolicy")]
        public async Task<IActionResult> Delete(string Id)
        {
            var model=await _collectionService.GetById(Id);
            if (model == null)
                return RedirectToAction("Index", "Home"); //ошибка
            return View(model);
        }
        [HttpPost]
        [Authorize(Policy = "UnlockedPolicy")]
        public async Task<IActionResult> Delete(EntireCollectionViewModel model)
        {
            await _collectionService.DeleteById(model.EntireCollectionViewModelId);
            return RedirectToAction("Profile", "UserCollections", model.OwnerId);
        }
    }
}
