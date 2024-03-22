using CollectionManager.Data.Interfaces;
using CollectionManager.Models.Collection;
using CollectionManager.Models.CollectionItem;
using Korzh.EasyQuery.Linq;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using System.Text;

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
            var result = await _context.UserCollections.AddAsync(obj);
            await _context.SaveChangesAsync();
            return result.State == EntityState.Added;
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
            var model = await _context.UserCollections.FirstAsync(x => x.EntireCollectionViewModelId == Id);
            if (model == null)
                return false;
            var result = _context.Remove(model);
            await _context.SaveChangesAsync();
            return result.State == EntityState.Deleted;
        }

        public Task<IEnumerable<EntireCollectionViewModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<EntireCollectionViewModel> GetById(string Id)
        {
            var result = await _context.UserCollections.FirstAsync(x => x.EntireCollectionViewModelId == Id);
            return result;
        }

        public async Task<IEnumerable<DataCollectionViewModel>> GetByItemsCount(int topAmount)
        {
            var collections=await _context.UserCollections.ToListAsync();
            var resList=new List<DataCollectionViewModel>();    
            foreach (var collection in collections)
            {
                var resItem=new DataCollectionViewModel(collection);
                resItem.ItemsCount = _context.CollectionItems.Count(x => x.CollectionId==resItem.EntireCollectionViewModelId);
                resList.Add(resItem);
            }
            return resList.OrderBy(x=>x.ItemsCount).Reverse().Take(topAmount);
        }

        public async Task<IEnumerable<EntireCollectionViewModel>> GetByUserId(string userId)
        {
            var result = await _context.UserCollections.Where(x => x.OwnerId == userId).ToListAsync();
            return result;
        }

        public async Task<bool> Update(EntireCollectionViewModel obj)
        {
            var result = _context.Update(obj);
            await _context.SaveChangesAsync();
            return result.State == EntityState.Modified;
        }
        public async Task<IEnumerable<DataCollectionViewModel>> FulltextSearch(string query)
        {
            return (await _context.UserCollections.FullTextSearchQuery(query).ToListAsync()).Select(x => new DataCollectionViewModel(x)
            {
                ItemsCount = _context.CollectionItems.Count(y => y.CollectionId == x.EntireCollectionViewModelId)
            });
        }
    }
}
