namespace VinylExchange.Services.Files
{
    #region

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    using VinylExchange.Common.Enumerations;
    using VinylExchange.Services.MemoryCache;
    using VinylExchange.Web.Models.Utility.Files;

    #endregion

    public class FileManager : IFileManager
    {
        private const string AudioPath = @"\Audio";

        private const string FileNameSplitter = "@---@";

        private const string ImagePath = @"\Image";

        private const string StorageFolderName = @"MediaStorage\";

        private readonly IMemoryCacheFileSevice memoryCacheFileSevice;

        public FileManager(IMemoryCacheFileSevice memoryCacheFileSevice)
        {
            this.memoryCacheFileSevice = memoryCacheFileSevice;
        }

        public IEnumerable<byte[]> GetFilesByteContent(IEnumerable<UploadFileUtilityModel> uploadFileUtilityModels)
        {
            var filesContent = new List<byte[]>();
            foreach (var fileUtilityModel in uploadFileUtilityModels)
                filesContent.Add(fileUtilityModel.FileByteContent);

            return filesContent;
        }

        public IEnumerable<TModel> MapFilesToDbObjects<TModel>(
            IEnumerable<UploadFileUtilityModel> uploadFileUtilityModels,
            Guid entityId,
            string entityIdPropertyName,
            string subFolderName)
        {
            var modelList = new List<TModel>();

            var isFist = true;

            foreach (var fileUtilityModel in uploadFileUtilityModels)
            {
                var fileGuid = fileUtilityModel.FileGuid.ToString();
                var encodedFileName = this.ConvertFileNameToBase64(fileUtilityModel.FileName);
                var fileExtension = fileUtilityModel.FileExtension;
                var fileType = fileUtilityModel.FileType;
                var createdOn = fileUtilityModel.CreatedOn;

                var contentFolderName = fileType == FileType.Audio ? AudioPath : ImagePath;
                var path = "\\" + subFolderName + contentFolderName + "\\";

                var modelInstance = (TModel)Activator.CreateInstance(typeof(TModel));

                var modelType = typeof(TModel);

                modelType.GetProperty("Path").SetValue(modelInstance, path);
                modelType.GetProperty("FileName").SetValue(
                    modelInstance,
                    fileGuid + FileNameSplitter + encodedFileName + fileExtension);
                modelType.GetProperty("FileType").SetValue(modelInstance, fileType);
                modelType.GetProperty("CreatedOn").SetValue(modelInstance, createdOn);

                if (isFist && fileType == FileType.Image)
                {
                    modelType.GetProperty("IsPreview").SetValue(modelInstance, true);
                }
                else
                {
                    modelType.GetProperty("IsPreview").SetValue(modelInstance, false);
                }

                modelType.GetProperty(entityIdPropertyName).SetValue(modelInstance, entityId);

                modelList.Add(modelInstance);

                isFist = false;
            }

            return modelList;
        }

        public IEnumerable<UploadFileUtilityModel> RetrieveFilesFromCache(Guid formSessionId)
        {
            return this.memoryCacheFileSevice.GetAllFilesForFormSession(formSessionId);
        }

        public IEnumerable<TModel> SaveFilesToServer<TModel>(
            IEnumerable<TModel> fileModels,
            IEnumerable<byte[]> filesContent)
        {
            var modelType = typeof(TModel);
            var fileModelsList = fileModels.ToList();
            var filesContentArray = filesContent.ToArray();

            for (var i = 0; i < fileModelsList.Count; i++)
            {
                var fileModel = fileModelsList[i];
                var fileContent = filesContentArray[i];

                var path = modelType.GetProperty("Path").GetValue(fileModel);
                var fileName = modelType.GetProperty("FileName").GetValue(fileModel);

                try
                {
                    using var fileStream = File.OpenWrite(StorageFolderName + path + fileName);
                    fileStream.Write(fileContent);
                }
                catch
                {
                    fileModelsList.Remove(fileModel);
                }
            }

            return fileModelsList;
        }

        private string ConvertFileNameToBase64(string fileName)
        {
            var fileNameBytes = Encoding.UTF8.GetBytes(fileName);
            return Convert.ToBase64String(fileNameBytes);
        }
    }
}