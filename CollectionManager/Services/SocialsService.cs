using CollectionManager.Data.Interfaces;
using CollectionManager.Data.Repositories;
using CollectionManager.Models.Socials;
using CollectionManager.Services.Interfaces;
using System.ComponentModel.Design;

namespace CollectionManager.Services
{
    public class SocialsService : ISocialsService
    {
        private readonly ICommentsRepository _commentsRepository;
        private readonly ILikesRepository _likesRepository;
        public SocialsService(ICommentsRepository commentsRepository,ILikesRepository likesRepository)
        {
            _commentsRepository = commentsRepository;
            _likesRepository = likesRepository;
        }
        public async Task<bool> CheckIfLiked(string userId, string itemId)
        {
            return await _likesRepository.CheckIfLiked(userId, itemId);
        }

        public async Task CreateComment(CommentModel comment)
        {
            await _commentsRepository.Create(comment);
        }

        public async Task CreateLike(LikeModel Like)
        {
            await _likesRepository.Create(Like);
        }

        public async Task<IEnumerable<CommentModel>> GetCommentsByItemId(string itemId)
        {
            return await _commentsRepository.GetByItemId(itemId);
        }

        public async Task<int> GetLikesAmount(string itemId)
        {
            return await _likesRepository.GetLikesCountByItemId(itemId);
        }

        public async Task RemoveComment(string commentId)
        {
            var commentToDelete=await _commentsRepository.GetById(commentId);
            await _commentsRepository.Delete(commentToDelete);
        }

        public async Task RemoveLike(string userId, string itemId)
        {
            var likeToDelete = await _likesRepository.GetByUserItemPair(userId, itemId);
            await _likesRepository.Delete(likeToDelete);
        }
    }
}
