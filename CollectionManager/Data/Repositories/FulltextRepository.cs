using CollectionManager.Data.Interfaces;
using CollectionManager.Models.Collection;
using CollectionManager.Models.Fulltext;
using CollectionManager.Models.Socials;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;

namespace CollectionManager.Data.Repositories
{
    public class FulltextRepository : IFulltextRepository
    {
        private readonly ApplicationDbContext _context;

        public FulltextRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(FulltextItem obj)
        {
            var result = await _context.FulltextStrings.AddAsync(obj);
            await _context.SaveChangesAsync();
            return result.State == EntityState.Added;
        }

        public async Task<bool> Delete(FulltextItem obj)
        {
            if (obj == null)
                return false;

            var item = await GetFulltextItem(obj);
            var result = _context.Remove(item);
            await _context.SaveChangesAsync();
            return result.State == EntityState.Deleted;
        }

        public async Task Edit(FulltextItem obj)
        {
            var item = await GetFulltextItem(obj);
            if (item != null)
                item.Fulltext = obj.Fulltext;
            _context.SaveChanges();
        }
        private async Task<FulltextItem> GetFulltextItem(FulltextItem obj)
        {
            FulltextItem item=null;
            switch (obj.FoundInType)
            {
                case "Item":
                    item = await _context.FulltextStrings.FirstOrDefaultAsync(x => x.ItemId == obj.ItemId);
                    break;
                case "Collection":
                    item = await _context.FulltextStrings.FirstOrDefaultAsync(x => x.CollectionId == obj.CollectionId);
                    break;
                case "Comment":
                    item = await _context.FulltextStrings.FirstOrDefaultAsync(x => x.CommentId == obj.CommentId);
                    break;
            }
            return item;
        }
        public async Task<IEnumerable<FulltextItemResult>> FindItems(string searchText)
        {
            var items = await
                        (from ft in _context.FulltextStrings
                         join it in _context.CollectionItems
                         on ft.ItemId equals it.EntireItemViewModelId
                         join cl in _context.UserCollections
                         on it.CollectionId equals cl.EntireCollectionViewModelId
                         where ft.Fulltext.Contains(searchText)
                         select new FulltextItemResult()
                         {
                             item = new CollectionItemDataPair(it, cl),
                             ItemId = ft.ItemId,
                             Fulltext = ft.Fulltext,
                             FoundInType = ft.FoundInType,
                             FulltextItemId = ft.FulltextItemId,
                         }).ToListAsync();
            return items;
        }

        public async Task<IEnumerable<FulltextItem>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<FulltextItem> GetById(string Id)
        {
            return await _context.FulltextStrings.FirstOrDefaultAsync(x => x.FulltextItemId == Id);
        }

        public async Task<IEnumerable<FulltextCollectionResult>> FindCollections(string searchText)
        {
            var collections = await
                        (from ft in _context.FulltextStrings
                         join cl in _context.UserCollections
                         on ft.CollectionId equals cl.EntireCollectionViewModelId
                         where ft.Fulltext.Contains(searchText)
                         select new FulltextCollectionResult()
                         {
                             collection = new DataCollectionViewModel(cl),
                             CollectionId = ft.CollectionId,
                             Fulltext = ft.Fulltext,
                             FoundInType = ft.FoundInType,
                             FulltextItemId = ft.FulltextItemId,
                         }).ToListAsync();
            return collections;
        }

        public async Task<IEnumerable<FulltextCommentResult>> FindComments(string searchText)
        {
            var comments = await
                        (from ft in _context.FulltextStrings
                         join cm in _context.ItemComments
                         on ft.CommentId equals cm.CommentModelId
                         where ft.Fulltext.Contains(searchText)
                         select new FulltextCommentResult()
                         {
                             comment = cm,
                             CommentId = ft.CommentId,
                             Fulltext = ft.Fulltext,
                             FoundInType = ft.FoundInType,
                             FulltextItemId = ft.FulltextItemId,
                         }).ToListAsync();
            return comments;
        }
    }
}
