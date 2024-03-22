using CollectionManager.Models.Collection;
using CollectionManager.Models.CollectionItem;
using CollectionManager.Models.Socials;
using CollectionManager.Models.Tag;

namespace CollectionManager.Data.Interfaces
{
    public interface ICommentsRepository:IBaseRepository<CommentModel>
    {
        Task<IEnumerable<CommentModel>> GetByItemId(string itemId);
        Task<IEnumerable<CommentModel>> FulltextSearch(string query);
    }
}
