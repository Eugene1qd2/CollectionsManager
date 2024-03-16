using CollectionManager.Data.Interfaces;
using CollectionManager.Models.Collection;
using CollectionManager.Models.CollectionItem;
using Microsoft.EntityFrameworkCore;

namespace CollectionManager.Data.Repositories
{
    public class CollectionItemsRepository : ICollectionItemsRepository
    {
        private readonly ApplicationDbContext _context;
        public CollectionItemsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(EntireItemViewModel obj)
        {
            var result = await _context.CollectionItems.AddAsync(obj);
            await _context.SaveChangesAsync();
            return result.State == EntityState.Added;
        }

        public async Task<bool> Delete(EntireItemViewModel obj)
        {
            var result = _context.CollectionItems.Remove(obj);
            await _context.SaveChangesAsync();
            return result.State == EntityState.Added;
        }

        public async Task<bool> DeleteById(string Id)
        {
            var item=await _context.CollectionItems.FirstOrDefaultAsync(x=>x.EntireItemViewModelId == Id);
            var result = _context.CollectionItems.Remove(item);
            await _context.SaveChangesAsync();
            return result.State == EntityState.Added;
        }

        public async Task<IEnumerable<EntireItemViewModel>> GetAll()
        {
            return _context.CollectionItems;
        }

        public IEnumerable<EntireItemViewModel> GetByCollectionId(string collectionId)
        {
            return _context.CollectionItems.Where(x=>x.CollectionId==collectionId);
        }

        public async Task<EntireItemViewModel> GetById(string Id)
        {
            return await _context.CollectionItems.FirstOrDefaultAsync(x => x.EntireItemViewModelId == Id);
        }

        public async Task<bool> Update(EntireItemViewModel obj)
        {
            var res= _context.CollectionItems.Update(obj);
            return res.State == EntityState.Added;
        }
    }
}
