using CollectionManager.Data.Interfaces;
using CollectionManager.Data.Repositories;
using CollectionManager.Models.Collection;
using CollectionManager.Models.CollectionItem;
using CollectionManager.Services.Interfaces;
using System.Text;

namespace CollectionManager.Services
{
    public class CollectionService : ICollectionService
    {
        ICollectionsRepository _collectionsRepository;
        ICollectionItemsRepository _collectionItemsRepository;
        ICloudStorageService _cloudStorageService;
        public CollectionService(ICollectionsRepository repository, ICloudStorageService cloudStorageService, ICollectionItemsRepository collectionItemsRepository)
        {
            _collectionsRepository = repository;
            _cloudStorageService = cloudStorageService;
            _collectionItemsRepository = collectionItemsRepository;
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

        public async Task<string> ExportToCsv(EntireCollectionViewModel collection)
        {
            List<EntireItemViewModel> entities = _collectionItemsRepository.GetByCollectionId(collection.EntireCollectionViewModelId).ToList();
            List<object> items = (from item in entities
                                  select new[] {
                                          item.EntireItemViewModelId,
                                          item.CollectionId,
                                          item.Name,
                                          item.CreationDate.ToString(),
                                          item.GetTagsString(),
                                          item.CustomStringField1,
                                          item.CustomStringField2,
                                          item.CustomStringField3,
                                          item.CustomIntField1.ToString(),
                                          item.CustomIntField2.ToString(),
                                          item.CustomIntField3.ToString(),
                                          item.CustomTextField1,
                                          item.CustomTextField2,
                                          item.CustomTextField3,
                                          collection.CustomBoolState1==CollectionFieldState.TRUE?item.CustomBoolField1.ToString():"",
                                          collection.CustomBoolState2==CollectionFieldState.TRUE?item.CustomBoolField2.ToString():"",
                                          collection.CustomBoolState3==CollectionFieldState.TRUE?item.CustomBoolField3.ToString():"",
                                          item.CustomDateField1.ToString(),
                                          item.CustomDateField2.ToString(),
                                          item.CustomDateField3.ToString(),
                                }).ToList<object>();

            items.Insert(0, new string[20] {"Id" , "Collection Id", "Name", "Creation date", "Tags",
                collection.CustomStringName1, collection.CustomStringName2, collection.CustomStringName3,
                collection.CustomIntName1,collection.CustomIntName2,collection.CustomIntName3,
                collection.CustomTextName1,collection.CustomTextName2,collection.CustomTextName3,
                collection.CustomBoolName1,collection.CustomBoolName2,collection.CustomBoolName3,
                collection.CustomDateName1,collection.CustomDateName2,collection.CustomDateName3 });

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < items.Count; i++)
            {
                string[] item = (string[])items[i];
                for (int j = 0; j < item.Length; j++)
                {
                    sb.Append(item[j] + "; ");
                }
                sb.Append("\r\n");
            }
            return sb.ToString();
        }
    }
}
