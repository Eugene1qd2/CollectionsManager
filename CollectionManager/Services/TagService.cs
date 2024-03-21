using CollectionManager.Data.Interfaces;
using CollectionManager.Data.Repositories;
using CollectionManager.Models.Collection;
using CollectionManager.Models.CollectionItem;
using CollectionManager.Models.Tag;
using CollectionManager.Services.Interfaces;

namespace CollectionManager.Services
{
    public class TagService : ITagService
    {
        ITagRepository _tagRepository;
        public TagService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task Create(TagModel model)
        {
            if(model == null) throw new ArgumentNullException("model");
            await _tagRepository.Create(model);
        }

        public async Task CreateTags(List<TagModel> tags)
        {
            await _tagRepository.CraeteTags(tags);
        }

        public async Task DeleteById(string objId)
        {
            var tag=await _tagRepository.GetById(objId);
            await _tagRepository.Delete(tag);
        }

        public async Task<IEnumerable<TagModel>> GetAll()
        {
            return await _tagRepository.GetAll();
        }

        public async Task<TagModel> GetById(string objId)
        {
            return await _tagRepository.GetById(objId);
        }

        public async Task<IEnumerable<TagModel>> GetByItemId(string itemId)
        {
            return await _tagRepository.GetByItemId(itemId);
        }
    }
}
