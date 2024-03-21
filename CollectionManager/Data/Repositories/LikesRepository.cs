using CollectionManager.Data.Interfaces;
using CollectionManager.Models.Collection;
using CollectionManager.Models.Socials;
using Microsoft.EntityFrameworkCore;

namespace CollectionManager.Data.Repositories
{
    public class LikesRepository : ILikesRepository
    {
        private readonly ApplicationDbContext _context;

        public LikesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CheckIfLiked(string userId, string itemId)
        {
            var result= await _context.ItemLikes.CountAsync(x=>x.ItemId == itemId && x.UserId==userId);
            return result > 0;
        }

        public async Task<bool> Create(LikeModel obj)
        {
            var result = await _context.ItemLikes.AddAsync(obj);
            await _context.SaveChangesAsync();
            return result.State == EntityState.Added;
        }

        public async Task<bool> Delete(LikeModel obj)
        {
            if (obj == null)
                return false;
            var result = _context.Remove(obj);
            await _context.SaveChangesAsync();
            return result.State == EntityState.Deleted;
        }

        public async Task<IEnumerable<LikeModel>> GetAll()
        {
            return await _context.ItemLikes.ToListAsync();
        }

        public async Task<LikeModel> GetById(string Id)
        {
            return await _context.ItemLikes.FirstOrDefaultAsync(x => x.LikeModelId == Id);
        }

        public async Task<LikeModel> GetByUserItemPair(string userId, string itemId)
        {
            var result=await _context.ItemLikes.FirstOrDefaultAsync(x=>x.UserId == userId && x.ItemId == itemId);
            return result;
        }

        public Task<int> GetLikesCountByItemId(string itemId)
        {
            var result=_context.ItemLikes.CountAsync( x=>x.ItemId == itemId);
            return result;
        }
    }
}
