using CollectionManager.Models.Collection;

namespace CollectionManager.Data.Interfaces
{
    public interface ICollectionsRepository:IBaseRepository<UserCollectionModel>
    {
        Task<bool> Update(UserCollectionModel obj);
        Task<bool> DeleteById(string Id);
        Task<IEnumerable<UserCollectionModel>> GetByUserId(string userId);
    }
}
