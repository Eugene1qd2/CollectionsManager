using CollectionManager.Data.Interfaces;
using CollectionManager.Data.Repositories;
using CollectionManager.Models.Collection;
using CollectionManager.Models.CollectionItem;
using CollectionManager.Services.Interfaces;

namespace CollectionManager.Services
{
    public class CollectionItemService : ICollectionItemService
    {
        ICollectionItemsRepository _collectionItemsRepository;
        ICollectionsRepository _collectionRepository;
        ITagRepository _tagRepository;
        public CollectionItemService(ICollectionItemsRepository repository, ITagRepository tagRepository,ICollectionsRepository collectionsRepository)
        {
            _collectionRepository=collectionsRepository;
            _collectionItemsRepository = repository;
            _tagRepository = tagRepository;
        }

        public async Task Create(EntireItemViewModel model)
        {
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
    }
}
