using CollectionManager.Models;
using CollectionManager.Models.Collection;
using CollectionManager.Models.Fulltext;
using CollectionManager.Models.ViewModels;
using CollectionManager.Services;
using CollectionManager.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CollectionManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICollectionService _collectionService;
        private readonly ICollectionItemService _collectionItemService;
        private readonly ISearchService _searchService;
        private readonly ITagService _tagService;
        public HomeController(ILogger<HomeController> logger, ICollectionService collectionService, ICollectionItemService collectionItemService, ITagService tagService, ISearchService searchService)
        {
            _logger = logger;
            _collectionService = collectionService;
            _collectionItemService = collectionItemService;
            _tagService = tagService;
            _searchService = searchService;
        }

        [Authorize(Policy = "UnlockedPolicy")]
        public async Task<IActionResult> Index()
        {
            var tags = await _tagService.GetAll();
            var collection = await _collectionService.GetBiggestData(3);
            var items = await _collectionItemService.GetSomeLast(5);
            var model = new IndexViewModel(tags, collection, items);
            return View(model);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize(Policy = "UnlockedPolicy")]
        public IActionResult Privacy()
        {
            return View();
        }
        [Authorize(Policy = "UnlockedPolicy")]
        public IActionResult NoAccess()
        {
            return View();
        }
        [Authorize(Policy = "UnlockedPolicy")]
        public IActionResult NotFound()
        {
            return View();
        }
        [Authorize(Policy = "UnlockedPolicy")]
        [HttpPost]
        public async Task<IActionResult> Search(string searchString,string tagId)
        {
            SearchViewModel searchViewModel = new SearchViewModel();
            if (!string.IsNullOrEmpty(searchString))
            { 
                searchViewModel.Items = await _searchService.SearchItems(searchString);
                searchViewModel.Collections = await _searchService.SearchCollections(searchString);
                searchViewModel.Comments = await _searchService.SearchComments(searchString);
            }else
            if(tagId != null)
            {
                searchViewModel.General = await _collectionItemService.GetByTagId(tagId);
                searchViewModel.searchTag=(await _tagService.GetById(tagId)).Tag;
            }
            else
                searchViewModel.General = await _collectionItemService.GetAll();
            return View(searchViewModel);
        }
        [Authorize(Policy = "UnlockedPolicy")]
        [HttpGet]
        public async Task<IActionResult> Search()
        {
            SearchViewModel searchViewModel = new SearchViewModel();
            searchViewModel.General= await _collectionItemService.GetAll();
            return View(searchViewModel);
        }
    }
}
