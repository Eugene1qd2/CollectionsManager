﻿using CollectionManager.Models.Collection;
using CollectionManager.Models.CollectionItem;

namespace CollectionManager.Services.Interfaces
{
    public interface ICollectionItemService
    {
        public Task<IEnumerable<EntireItemViewModel>> GetByCollectionId(string collectionId);
        public Task<IEnumerable<CollectionItemDataPair>> GetByCollectionIdPair(string collectionId);
        public Task<IEnumerable<EntireItemViewModel>> GetByCollectionIdTagged(string uscollectionIderId);
        public Task<EntireItemViewModel> GetById(string objId);
        public Task<IEnumerable<CollectionItemDataPair>> GetSomeLast(int count);
        public Task<IEnumerable<CollectionItemDataPair>> GetAll();
        public Task<IEnumerable<CollectionItemDataPair>> GetByTagId(string tagId);
        public Task<EntireItemViewModel> GetByIdTagged(string objId);
        public Task Create(EntireItemViewModel model);
        public Task Edit(EntireItemViewModel model);
        public Task DeleteById(string objId);
    }
}
