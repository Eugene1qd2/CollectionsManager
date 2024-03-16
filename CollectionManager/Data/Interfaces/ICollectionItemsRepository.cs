using CollectionManager.Models.Collection;
using CollectionManager.Models.CollectionItem;

namespace CollectionManager.Data.Interfaces
{
    public interface ICollectionItemsRepository:IBaseRepository<EntireItemViewModel>
    {
        Task<bool> Update(EntireItemViewModel obj);
        Task<bool> DeleteById(string Id);
        IEnumerable<EntireItemViewModel> GetByCollectionId(string collectionId);
    }
}
