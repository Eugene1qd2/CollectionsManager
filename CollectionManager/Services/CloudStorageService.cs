using CollectionManager.Data.Interfaces;
using CollectionManager.Services.Interfaces;

namespace CollectionManager.Services
{
    public class CloudStorageService : ICloudStorageService
    {
        ICloudStorage _cloudStorage;
        public CloudStorageService(ICloudStorage cloudStorage)
        {
            _cloudStorage = cloudStorage;
        }
        public async Task<string> UploadFile(IFormFile formFile,string fileNameForStorage)
        {
            var result = await _cloudStorage.UploadFileAsync(formFile, fileNameForStorage);
            return result;
        }
    }
}
