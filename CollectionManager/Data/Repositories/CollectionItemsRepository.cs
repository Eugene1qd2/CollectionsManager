using CollectionManager.Data.Interfaces;
using CollectionManager.Models.Collection;
using CollectionManager.Models.CollectionItem;
using CollectionManager.Models.Fulltext;
using Korzh.EasyQuery.Linq;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using System.Runtime.CompilerServices;
using System.Text;

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
            obj.CreationDate = DateTime.Now;
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

        public IEnumerable<EntireItemViewModel> GetSomeLastItems(int amount)
        {
            var res=from i in _context.CollectionItems
                    orderby i.CreationDate descending
                    select new {i};
            return res.Select(x=>x.i).ToList().Take(amount);
        }
        public IEnumerable<EntireItemViewModel> GetRange(int start,int amount)
        {
            var res = _context.CollectionItems.Take(new Range(start, start + amount));
            return res;
        }

        public async Task<bool> Update(EntireItemViewModel obj)
        {
            var res= _context.CollectionItems.Update(obj);
            await _context.SaveChangesAsync();
            return res.State == EntityState.Modified;
        }

        public async Task<IEnumerable<CollectionItemDataPair>> GetSomeLastPairs(int amount)
        {
            var res = (await (from it in _context.CollectionItems
                      join cl in _context.UserCollections
                      on it.CollectionId equals cl.EntireCollectionViewModelId
                      orderby it.CreationDate descending
                      select new CollectionItemDataPair(it, cl)).ToListAsync()).Take(amount);
            return res;
        }

        public async Task<IEnumerable<CollectionItemDataPair>> GetAllPairs()
        {
            var res = await (from it in _context.CollectionItems
                            join cl in _context.UserCollections
                            on it.CollectionId equals cl.EntireCollectionViewModelId
                            select new CollectionItemDataPair(it, cl)).ToListAsync();
            return res;
        }
        private async Task<IEnumerable<CollectionItemDataPair>> GetAllPairs(IEnumerable<string> ids)
        {
            var res = await (from it in _context.CollectionItems
                             where ids.Contains(it.EntireItemViewModelId)
                             join cl in _context.UserCollections
                             on it.CollectionId equals cl.EntireCollectionViewModelId
                             select new CollectionItemDataPair(it, cl)).ToListAsync();
            return res;
        }
        public async Task<IEnumerable<CollectionItemDataPair>> FulltextSearch(string query)
        {
            var items = (await _context.CollectionItems.FullTextSearchQuery(query).ToListAsync()).Select(x=>x.EntireItemViewModelId);
            return await GetAllPairs(items);
        }

        public async Task<IEnumerable<CollectionItemDataPair>> GetByTagIdPair(string tagId)
        {
            var res = await(from tg in _context.ItemTags
                            where tg.TagModelId==tagId
                            join it in _context.CollectionItems
                            on tg.ItemId equals it.EntireItemViewModelId
                            join cl in _context.UserCollections
                            on it.CollectionId equals cl.EntireCollectionViewModelId
                            select new CollectionItemDataPair(it, cl)).ToListAsync();
            return res;
        }
    }
}
