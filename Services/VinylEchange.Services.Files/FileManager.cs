namespace VinylExchange.Services.Files
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using VinylExchange.Common.Enumerations;
    using VinylExchange.Models.Utility;
    using VinylExchange.Services.MemoryCache;

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
            List<byte[]> filesContent = new List<byte[]>();
            foreach (UploadFileUtilityModel fileUtilityModel in uploadFileUtilityModels)
            {
                filesContent.Add(fileUtilityModel.FileByteContent);
            }

            return filesContent;
        }

        public IEnumerable<TModel> MapFilesToDbObjects<TModel>(
            IEnumerable<UploadFileUtilityModel> uploadFileUtilityModels,
            Guid entityId,
            string entityIdPropertyName,
            string subFolderName)
        {
            List<TModel> modelList = new List<TModel>();

            foreach (UploadFileUtilityModel fileUtilityModel in uploadFileUtilityModels)
            {
                string fileGuid = fileUtilityModel.FileGuid.ToString();
                string encodedFileName = this.ConvertFileNameToBase64(fileUtilityModel.FileName);
                string fileExtension = fileUtilityModel.FileExtension;
                FileType fileType = fileUtilityModel.FileType;
                DateTime createdOn = fileUtilityModel.CreatedOn;

                string contentFolderName = fileType == FileType.Audio ? AudioPath : ImagePath;
                string path = "\\" + subFolderName + contentFolderName + "\\";

                TModel modelInstance = (TModel)Activator.CreateInstance(typeof(TModel));

                Type modelType = typeof(TModel);

                modelType.GetProperty("Path").SetValue(modelInstance, path);
                modelType.GetProperty("FileName").SetValue(
                    modelInstance,
                    fileGuid + FileNameSplitter + encodedFileName + fileExtension);
                modelType.GetProperty("FileType").SetValue(modelInstance, fileType);
                modelType.GetProperty("CreatedOn").SetValue(modelInstance, createdOn);
                modelType.GetProperty(entityIdPropertyName).SetValue(modelInstance, entityId);

                modelList.Add(modelInstance);
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
            Type modelType = typeof(TModel);
            List<TModel> fileModelsList = fileModels.ToList();
            byte[][] filesContentArray = filesContent.ToArray();

            for (int i = 0; i < fileModelsList.Count; i++)
            {
                TModel fileModel = fileModelsList[i];
                byte[] fileContent = filesContentArray[i];

                object path = modelType.GetProperty("Path").GetValue(fileModel);
                object fileName = modelType.GetProperty("FileName").GetValue(fileModel);

                try
                {
                    using FileStream fileStream = File.OpenWrite(StorageFolderName + path + fileName);
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
            byte[] fileNameBytes = System.Text.Encoding.UTF8.GetBytes(fileName);
            return Convert.ToBase64String(fileNameBytes);
        }
    }
}