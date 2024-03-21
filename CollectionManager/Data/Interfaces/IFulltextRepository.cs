using CollectionManager.Models.Collection;
using CollectionManager.Models.CollectionItem;
using CollectionManager.Models.Fulltext;
using CollectionManager.Models.Socials;
using CollectionManager.Models.Tag;

namespace CollectionManager.Data.Interfaces
{
    public interface IFulltextRepository:IBaseRepository<FulltextItem>
    {
        Task<IEnumerable<FulltextItemResult>> FindItems(string searchText);
        Task<IEnumerable<FulltextCollectionResult>> FindCollections(string searchText);
        Task<IEnumerable<FulltextCommentResult>> FindComments(string searchText);
        Task Edit(FulltextItem item);
    }
}
