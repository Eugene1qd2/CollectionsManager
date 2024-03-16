using CollectionManager.Models.Collection;
using CollectionManager.Models.CollectionItem;
using CollectionManager.Models.Tag;

namespace CollectionManager.Services.Interfaces
{
    public interface ITagService
    {
        public Task<IEnumerable<TagModel>> GetAll();
        public Task<IEnumerable<TagModel>> GetByItemId(string itemId);
        public Task<TagModel> GetById(string objId);
        public Task Create(TagModel model);
        public Task CreateTags(List<TagModel> tags);
        public Task DeleteById(string objId);
    }
}
