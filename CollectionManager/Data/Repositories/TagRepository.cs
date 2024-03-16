using CollectionManager.Data.Interfaces;
using CollectionManager.Models.Collection;
using CollectionManager.Models.CollectionItem;
using CollectionManager.Models.Tag;
using Microsoft.EntityFrameworkCore;

namespace CollectionManager.Data.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly ApplicationDbContext _context;
        public TagRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ClearItemTags(string itemId)
        {
            var itemTags = _context.ItemTags.Where(x => x.ItemId == itemId);
            foreach (var tag in itemTags)
            {
                _context.ItemTags.Remove(tag);
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CraeteTags(List<TagModel> tags)
        {
            foreach (var tag in tags)
            {
                if (tag != null)
                    await _context.Tags.AddAsync(tag);
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Create(TagModel obj)
        {
            var result = await _context.Tags.AddAsync(obj);
            await _context.SaveChangesAsync();
            return result.State == EntityState.Added;
        }

        public async Task<bool> Delete(TagModel obj)
        {
            _context.Remove(obj);
            return true;
        }

        public async Task<IEnumerable<TagModel>> GetAll()
        {
            return _context.Tags != null ? _context.Tags : new List<TagModel>();
        }

        public async Task<TagModel> GetById(string Id)
        {
            var result = await _context.Tags.FirstOrDefaultAsync(x => x.TagModelId == Id);
            return result;
        }

        public async Task<IEnumerable<TagModel>> GetByItemId(string itemId)
        {
            var tags = _context.ItemTags.Where(x => x.ItemId == itemId);
            var result = new List<TagModel>();
            foreach (var tag in tags)
            {
                var item = await _context.Tags.FirstOrDefaultAsync(x => x.TagModelId == tag.TagModelId);
                result.Add(item);
            }
            return result;
        }

        public async Task<bool> SetItemTags(EntireItemViewModel item)
        {
            foreach (var tag in item.Tags)
            {
                if (tag.IsUserTag == 1)
                    await Create(tag);
                await _context.ItemTags.AddAsync(new ItemTagModel(tag.TagModelId, item.EntireItemViewModelId));
            }
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
