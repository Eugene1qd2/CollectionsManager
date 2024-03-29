﻿using CollectionManager.Models.Collection;

namespace CollectionManager.Services.Interfaces
{
    public interface ICollectionService
    {
        public Task<IEnumerable<EntireCollectionViewModel>> GetByUserId(string userId);
        public Task<EntireCollectionViewModel> GetById(string objId);
        public Task<IEnumerable<DataCollectionViewModel>> GetBiggestData(int amount);
        public Task Create(EntireCollectionViewModel model);
        public Task CreateData(DataCollectionViewModel model);
        public Task Edit(EntireCollectionViewModel model);
        public Task EditData(DataCollectionViewModel model);
        public Task<string> ExportToCsv(EntireCollectionViewModel collection);
        public Task DeleteById(string objId);
    }
}
