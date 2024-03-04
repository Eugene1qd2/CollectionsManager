using CollectionManager.Data.Interfaces;
using CollectionManager.Data.Repositories;
using CollectionManager.Models.Collection;
using CollectionManager.Services.Interfaces;

namespace CollectionManager.Services
{
    public class CollectionService : ICollectionService
    {
        ICollectionsRepository _repository;
        public CollectionService(ICollectionsRepository repository)
        {
            _repository = repository;
        }

        public async Task Create(UserCollectionModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }
            await _repository.Create(model);
        }

        public async Task DeleteById(string objId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserCollectionModel>> GetByUserId(string userId)
        {
            return await _repository.GetByUserId(userId);
        }
    }
}
