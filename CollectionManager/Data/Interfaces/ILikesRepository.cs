using CollectionManager.Models.Collection;
using CollectionManager.Models.CollectionItem;
using CollectionManager.Models.Socials;
using CollectionManager.Models.Tag;

namespace CollectionManager.Data.Interfaces
{
    public interface ILikesRepository:IBaseRepository<LikeModel>
    {
        Task<int> GetLikesCountByItemId(string itemId);
        Task<LikeModel> GetByUserItemPair(string userId,string itemId);
        Task<bool> CheckIfLiked(string userId,string itemId);
    }
}
