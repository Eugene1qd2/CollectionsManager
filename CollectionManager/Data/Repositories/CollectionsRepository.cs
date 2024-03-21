using CollectionManager.Data.Interfaces;
using CollectionManager.Models.Collection;
using CollectionManager.Models.CollectionItem;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using System.Text;

namespace CollectionManager.Data.Repositories
{
    public class CollectionsRepository : ICollectionsRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IFulltextRepository _fulltextRepository;
        public CollectionsRepository(ApplicationDbContext context, IFulltextRepository fulltextRepository)
        {
            _context = context;
            _fulltextRepository = fulltextRepository;
        }

        private async Task<string> GetFulltext(EntireCollectionViewModel obj)
        {
            StringBuilder sb = new StringBuilder(obj.Title.Trim());
            sb.Append(obj.Theme);
            sb.Append(obj.Description);
            sb.Append(obj.CustomStringName1);
            sb.Append(obj.CustomStringName2);
            sb.Append(obj.CustomStringName3);
            sb.Append(obj.CustomTextName1);
            sb.Append(obj.CustomTextName2);
            sb.Append(obj.CustomTextName3);
            sb.Append(obj.CustomIntName1?.ToString());
            sb.Append(obj.CustomIntName2?.ToString());
            sb.Append(obj.CustomIntName3?.ToString());
            sb.Append(obj.CustomDateName1?.ToString());
            sb.Append(obj.CustomDateName2?.ToString());
            sb.Append(obj.CustomDateName3?.ToString());
            sb.Append(obj.CustomBoolName1?.ToString());
            sb.Append(obj.CustomBoolName2?.ToString());
            sb.Append(obj.CustomBoolName3?.ToString());
            return sb.ToString().ToUpper();
        }

        public async Task<bool> Create(EntireCollectionViewModel obj)
        {
            var result = await _context.UserCollections.AddAsync(obj);
            await _fulltextRepository.Create(new Models.Fulltext.FulltextItem(null, await GetFulltext(obj),"Collection", obj.EntireCollectionViewModelId));
            await _context.SaveChangesAsync();
            return result.State == EntityState.Added;
        }

        public async Task<bool> Delete(EntireCollectionViewModel obj)
        {
            if (obj == null)
                return false;
            var result = _context.Remove(obj);
            await _fulltextRepository.Delete(new Models.Fulltext.FulltextItem(null, await GetFulltext(obj), "Collection", obj.EntireCollectionViewModelId));
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
            await _fulltextRepository.Edit(new Models.Fulltext.FulltextItem(null, await GetFulltext(obj), "Collection", obj.EntireCollectionViewModelId));
            await _context.SaveChangesAsync();
            return result.State == EntityState.Modified;
        }
    }
}
