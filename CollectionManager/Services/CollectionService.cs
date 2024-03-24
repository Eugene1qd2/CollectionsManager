using CollectionManager.Data.Interfaces;
using CollectionManager.Data.Repositories;
using CollectionManager.Models.Collection;
using CollectionManager.Services.Interfaces;

namespace CollectionManager.Services
{
    public class CollectionService : ICollectionService
    {
        ICollectionsRepository _collectionsRepository;
        ICloudStorageService _cloudStorageService;
        public CollectionService(ICollectionsRepository repository, ICloudStorageService cloudStorageService)
        {
            _collectionsRepository = repository;
            _cloudStorageService = cloudStorageService;
        }

        public async Task Create(EntireCollectionViewModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }

            await _collectionsRepository.Create(model);
        }

        public async Task CreateData(DataCollectionViewModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }
            if (model.ImageFile != null)
            {
                model.CloudPictureName = ICloudStorageService.FormFileName(model.Title, model.ImageFile.FileName);
                var pictureLink = await _cloudStorageService.UploadFile(model.ImageFile, model.CloudPictureName);
                model.PictureLink = pictureLink;
            }
            await _collectionsRepository.Create(model);
        }

        public async Task DeleteById(string objId)
        {
            await _collectionsRepository.DeleteById(objId);
        }

        public async Task Edit(EntireCollectionViewModel model)
        {
            await _collectionsRepository.Update(model);
        }

        public async Task EditData(DataCollectionViewModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }
            if (model.shouldClearImage)
            {
                model.PictureLink = null;
            }
            else
            if (model.ImageFile != null)
            {
                model.CloudPictureName = ICloudStorageService.FormFileName(model.Title, model.ImageFile.FileName);
                var pictureLink = await _cloudStorageService.UploadFile(model.ImageFile, model.CloudPictureName);
                model.PictureLink = pictureLink;
            }
            await _collectionsRepository.Update(model);
        }

        public async Task<EntireCollectionViewModel> GetById(string objId)
        {
            return await _collectionsRepository.GetById(objId);
        }

        public async Task<IEnumerable<EntireCollectionViewModel>> GetByUserId(string userId)
        {
            return await _collectionsRepository.GetByUserId(userId);
        }

        public async Task<IEnumerable<DataCollectionViewModel>> GetBiggestData(int amount)
        {
            var colls = (await _collectionsRepository.GetByItemsCount(amount)).ToList();
            return colls;
        }
    }
}
