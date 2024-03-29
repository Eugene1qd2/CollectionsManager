﻿using CollectionManager.Models.Collection;

namespace CollectionManager.Data.Interfaces
{
    public interface ICollectionsRepository:IBaseRepository<EntireCollectionViewModel>
    {
        Task<bool> Update(EntireCollectionViewModel obj);
        Task<bool> DeleteById(string Id);
        Task<IEnumerable<EntireCollectionViewModel>> GetByUserId(string userId);
        Task<IEnumerable<DataCollectionViewModel>> GetByItemsCount(int topAmount);
        Task<IEnumerable<DataCollectionViewModel>> FulltextSearch(string query);
    }
}
