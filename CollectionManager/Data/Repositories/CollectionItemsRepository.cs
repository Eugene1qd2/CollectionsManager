using CollectionManager.Data.Interfaces;
using CollectionManager.Models.Collection;
using CollectionManager.Models.CollectionItem;
using CollectionManager.Models.Fulltext;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.Text;

namespace CollectionManager.Data.Repositories
{
    public class CollectionItemsRepository : ICollectionItemsRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IFulltextRepository _fulltextRepository;
        public CollectionItemsRepository(ApplicationDbContext context, IFulltextRepository fulltextRepository)
        {
            _context = context;
            _fulltextRepository = fulltextRepository;
        }
        public async Task<bool> Create(EntireItemViewModel obj)
        {
            obj.CreationDate = DateTime.Now;
            var result = await _context.CollectionItems.AddAsync(obj);
            await _fulltextRepository.Create(new Models.Fulltext.FulltextItem(obj.EntireItemViewModelId,await GetFulltext(obj)));
            await _context.SaveChangesAsync();
            return result.State == EntityState.Added;
        }

        private async Task<string> GetFulltext(EntireItemViewModel obj)
        {
            StringBuilder sb = new StringBuilder(obj.Name.Trim());
            sb.Append(obj.CustomStringField1);
            sb.Append(obj.CustomStringField2);
            sb.Append(obj.CustomStringField3);
            sb.Append(obj.CustomTextField1);
            sb.Append(obj.CustomTextField2);
            sb.Append(obj.CustomTextField3);
            sb.Append(obj.CustomIntField1.ToString());
            sb.Append(obj.CustomIntField2.ToString());
            sb.Append(obj.CustomIntField3.ToString());
            sb.Append(obj.CustomDateField1.ToString());
            sb.Append(obj.CustomDateField2.ToString());
            sb.Append(obj.CustomDateField3.ToString());
            return sb.ToString().ToUpper();
        }

        public async Task<bool> Delete(EntireItemViewModel obj)
        {
            var result = _context.CollectionItems.Remove(obj);
            await _fulltextRepository.Delete(new FulltextItem()
            {
                ItemId = obj.EntireItemViewModelId,
                FoundInType = "Item"
            });
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
            await _fulltextRepository.Edit(new FulltextItem(obj.EntireItemViewModelId, await GetFulltext(obj)));
            await _context.SaveChangesAsync();
            return res.State == EntityState.Modified;
        }

        public async Task<IEnumerable<CollectionItemDataPair>> GetSomeLastPairs(int amount)
        {
            var res = (await (from it in _context.CollectionItems
                      join cl in _context.UserCollections
                      on it.CollectionId equals cl.EntireCollectionViewModelId
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
    }
}
