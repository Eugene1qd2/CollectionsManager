using CollectionManager.Models.Collection;
using CollectionManager.Models.CollectionItem;
using CollectionManager.Models.Tag;

namespace CollectionManager.Data.Interfaces
{
    public interface ITagRepository:IBaseRepository<TagModel>
    {
        Task<IEnumerable<TagModel>> GetByItemId(string collectionId);
        Task<bool> SetItemTags(EntireItemViewModel item);
        Task<bool> ClearItemTags(string itemId);
        Task<bool> CraeteTags(List<TagModel> tags);
    }
}
