namespace CollectionManager.Services.Interfaces
{
    public interface ICloudStorageService
    {
        public Task<string> UploadFile(IFormFile file,string fileNameForStorage);
        public static string FormFileName(string title, string fileName)
        {
            var fileExtension = Path.GetExtension(fileName);
            var fileNameForStorage = $"{title}-{DateTime.Now.ToString("yyyyMMddHHmmss")}{fileExtension}";
            return fileNameForStorage;
        }
    }
}
