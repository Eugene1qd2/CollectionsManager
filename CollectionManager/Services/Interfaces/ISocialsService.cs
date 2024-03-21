using CollectionManager.Models.Collection;
using CollectionManager.Models.CollectionItem;
using CollectionManager.Models.Socials;

namespace CollectionManager.Services.Interfaces
{
    public interface ISocialsService
    {
        public Task CreateComment(CommentModel comment);
        public Task RemoveComment(string commentId);
        public Task CreateLike(LikeModel Like);
        public Task RemoveLike(string userId, string itemId);
        public Task<bool> CheckIfLiked(string userId,string itemId);
        public Task<int> GetLikesAmount(string itemId);
        public Task<IEnumerable<CommentModel>> GetCommentsByItemId(string itemId);
    }
}
