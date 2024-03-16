using CollectionManager.Data.Interfaces;
using CollectionManager.Models.Collection;
using Microsoft.EntityFrameworkCore;

namespace CollectionManager.Data.Repositories
{
    public class CollectionsRepository : ICollectionsRepository
    {
        private readonly ApplicationDbContext _context;
        public CollectionsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(EntireCollectionViewModel obj)
        {
            var result=await _context.UserCollections.AddAsync(obj);
            await _context.SaveChangesAsync();
            return result.State==EntityState.Added;
        }

        public async Task<bool> Delete(EntireCollectionViewModel obj)
        {
            if (obj == null)
                return false;
            var result = _context.Remove(obj);
            await _context.SaveChangesAsync();
            return result.State == EntityState.Deleted;
        }

        public async Task<bool> DeleteById(string Id)
        {
            var model= await _context.UserCollections.FirstAsync(x => x.EntireCollectionViewModelId == Id);
            if (model==null)
                return false;
            var result=_context.Remove(model);
            await _context.SaveChangesAsync();
            return result.State== EntityState.Deleted;
        }

        public Task<IEnumerable<EntireCollectionViewModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<EntireCollectionViewModel> GetById(string Id)
        {
            var result=await _context.UserCollections.FirstAsync(x=>x.EntireCollectionViewModelId==Id);
            return result;
        }

        public async Task<IEnumerable<EntireCollectionViewModel>> GetByUserId(string userId)
        {
            var result =await _context.UserCollections.Where(x=>x.OwnerId== userId).ToListAsync();
            return result;
        }

        public async Task<bool> Update(EntireCollectionViewModel obj)
        {
            var result=_context.Update(obj);
            await _context.SaveChangesAsync();
            return result.State == EntityState.Modified;
        }
    }
}
