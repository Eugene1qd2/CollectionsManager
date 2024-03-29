using CollectionManager.Data.Interfaces;
using CollectionManager.Data.Repositories;
using CollectionManager.Models.Collection;
using CollectionManager.Models.CollectionItem;
using CollectionManager.Services.Interfaces;
using Ganss.XSS;

namespace CollectionManager.Services
{
    public class CollectionItemService : ICollectionItemService
    {
        ICollectionItemsRepository _collectionItemsRepository;
        ICollectionsRepository _collectionRepository;
        ITagRepository _tagRepository;
        IHtmlSanitizer _htmlSanitizer;

        public CollectionItemService(ICollectionItemsRepository repository, ITagRepository tagRepository,ICollectionsRepository collectionsRepository)
        {
            _collectionRepository=collectionsRepository;
            _collectionItemsRepository = repository;
            _tagRepository = tagRepository;
            _htmlSanitizer = new HtmlSanitizer();
        }

        public async Task Create(EntireItemViewModel model)
        {
            model.CustomTextField1= _htmlSanitizer.Sanitize(model.CustomTextField1);
            model.CustomTextField2= _htmlSanitizer.Sanitize(model.CustomTextField2);
            model.CustomTextField3= _htmlSanitizer.Sanitize(model.CustomTextField3);
            await _collectionItemsRepository.Create(model);
            await _tagRepository.SetItemTags(model);
        }

        public async Task DeleteById(string objId)
        {
            await _collectionItemsRepository.DeleteById(objId);
        }

        public async Task Edit(EntireItemViewModel model)
        {
            await _collectionItemsRepository.Update(model);
            await _tagRepository.ClearItemTags(model.EntireItemViewModelId);
            await _tagRepository.SetItemTags(model);
        } 

        public async Task<EntireItemViewModel> GetById(string objId)
        {
            return await _collectionItemsRepository.GetById(objId);
        }

        public async Task<IEnumerable<EntireItemViewModel>> GetByCollectionId(string userId)
        {
            return _collectionItemsRepository.GetByCollectionId(userId);
        }

        public async Task<IEnumerable<EntireItemViewModel>> GetByCollectionIdTagged(string userId)
        {
            throw new Exception("Not emplimented");
        }

        public async Task<EntireItemViewModel> GetByIdTagged(string objId)
        {
            var model= await _collectionItemsRepository.GetById(objId);
            model.Tags=(await _tagRepository.GetByItemId(objId)).ToList();
            return model;  
        }

        public async Task<IEnumerable<CollectionItemDataPair>> GetSomeLast(int count)
        {
            var items = await _collectionItemsRepository.GetSomeLastPairs(count);
            return items;
        }

        public async Task<IEnumerable<CollectionItemDataPair>> GetAll()
        {
            var items=await _collectionItemsRepository.GetAllPairs();
            return items;
        }

        public async Task<IEnumerable<CollectionItemDataPair>> GetByTagId(string tagId)
        {
            var items= await _collectionItemsRepository.GetByTagIdPair(tagId);
            return items;
        }

        public async Task<IEnumerable<CollectionItemDataPair>> GetByCollectionIdPair(string collectionId)
        {
            var items = await _collectionItemsRepository.GetByCollectionIdPair(collectionId);
            return items;
        }
    }
}
