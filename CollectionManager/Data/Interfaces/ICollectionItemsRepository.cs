using CollectionManager.Models.Collection;
using CollectionManager.Models.CollectionItem;

namespace CollectionManager.Data.Interfaces
{
    public interface ICollectionItemsRepository:IBaseRepository<EntireItemViewModel>
    {
        Task<bool> Update(EntireItemViewModel obj);
        Task<bool> DeleteById(string Id);
        IEnumerable<EntireItemViewModel> GetByCollectionId(string collectionId);
        IEnumerable<EntireItemViewModel> GetSomeLastItems(int amount);
        Task<IEnumerable<CollectionItemDataPair>> GetSomeLastPairs(int amount);
        Task<IEnumerable<CollectionItemDataPair>> GetAllPairs();
        IEnumerable<EntireItemViewModel> GetRange(int startm,int amount);
        Task<IEnumerable<CollectionItemDataPair>> FulltextSearch(string query);
        Task<IEnumerable<CollectionItemDataPair>> GetByTagIdPair(string tagId);
    }
}
