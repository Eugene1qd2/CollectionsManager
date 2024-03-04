using CollectionManager.Data.Interfaces;
using CollectionManager.Models.Collection;
using Microsoft.EntityFrameworkCore;

namespace CollectionManager.Data.Repositories
{
    public class CollectionsRepository : ICollectionsRepository
    {
        private readonly ApplicationDbContext _context;
        public CollectionsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(UserCollectionModel obj)
        {
            await _context.UserCollections.AddAsync(obj);
            await _context.SaveChangesAsync();
            return true;
        }

        public Task<bool> Delete(UserCollectionModel obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteById(string Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserCollectionModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<UserCollectionModel> GetById(string Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserCollectionModel>> GetByUserId(string userId)
        {
            var result =await _context.UserCollections.Where(x=>x.OwnerId== userId).ToListAsync();
            return result;
        }

        public Task<bool> Update(UserCollectionModel obj)
        {
            throw new NotImplementedException();
        }
    }
}
