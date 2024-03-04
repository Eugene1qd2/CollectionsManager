using CollectionManager.Models.Collection;

namespace CollectionManager.Services.Interfaces
{
    public interface ICollectionService
    {
        public Task<IEnumerable<UserCollectionModel>> GetByUserId(string userId);
        public Task Create(UserCollectionModel model);
        public Task DeleteById(string objId);

    }
}
