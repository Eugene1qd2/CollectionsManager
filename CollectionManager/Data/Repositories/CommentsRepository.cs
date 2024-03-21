using CollectionManager.Data.Interfaces;
using CollectionManager.Models.Collection;
using CollectionManager.Models.Socials;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace CollectionManager.Data.Repositories
{
    public class CommentsRepository : ICommentsRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(CommentModel obj)
        {
            var result = await _context.ItemComments.AddAsync(obj);
            await _context.SaveChangesAsync();
            return result.State == EntityState.Added;
        }

        public async Task<bool> Delete(CommentModel obj)
        {
            if (obj == null)
                return false;
            var result = _context.Remove(obj);
            await _context.SaveChangesAsync();
            return result.State == EntityState.Deleted;
        }

        public async Task<IEnumerable<CommentModel>> GetAll()
        {
            return await _context.ItemComments.ToListAsync();
        }

        public async Task<CommentModel> GetById(string Id)
        {
            return await _context.ItemComments.FirstOrDefaultAsync(x => x.CommentModelId == Id);
        }

        public async Task<IEnumerable<CommentModel>> GetByItemId(string itemId)
        {
            var result=await _context.ItemComments.Where(x=>x.ItemId== itemId).ToListAsync();
            return result;
        }
    }
}
