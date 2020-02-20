using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VinylExchange.Common.Enumerations;
using VinylExchange.Models.Utility;
using VinylExchange.Services.MemoryCache;

namespace VinylExchange.Services.Files
{
    public class FileManager : IFileManager
    {
        private const string storageFolderName = @"MediaStorage\";
                
        private readonly IMemoryCacheFileSevice memoryCacheFileSevice;
        public FileManager(IMemoryCacheFileSevice memoryCacheFileSevice)
        {         
            this.memoryCacheFileSevice = memoryCacheFileSevice;
        }


        public IEnumerable<UploadFileUtilityModel> RetrieveFilesFromCache(Guid formSessionId)
        {
            return this.memoryCacheFileSevice.GetAllFilesForFormSession(formSessionId);

        }

        public IEnumerable<byte[]> GetFilesByteContent(IEnumerable<UploadFileUtilityModel> uploadFileUtilityModels)
        {
            List<byte[]> filesContent = new List<byte[]>();
            foreach (var fileUtilityModel in uploadFileUtilityModels)
            {
                filesContent.Add(fileUtilityModel.FileByteContent);
            }

            return filesContent;
        }


        public IEnumerable<TModel> MapFilesToDbObjects
            <TModel>(IEnumerable<UploadFileUtilityModel> uploadFileUtilityModels,
            Guid entityId,string entityIdPropertyName, string subFolderName)
        {
            List<TModel> modelList = new List<TModel>();

            foreach (var fileUtilityModel in uploadFileUtilityModels)
            {

                var fileGuid = fileUtilityModel.FileGuid.ToString();
                var encodedFileName = this.ConvertFileNameToBase64(fileUtilityModel.FileName);
                var fileExtension = fileUtilityModel.FileExtension;
                var fileType = fileUtilityModel.FileType;
                var createdOn = fileUtilityModel.CreatedOn;

                var contentFolderName = fileType == FileType.Audio ? @"\Audio" : @"\Image";
                var path = "\\" + subFolderName + contentFolderName + "\\";

                var modelInstance = (TModel)Activator.CreateInstance(typeof(TModel));

                var modelType = typeof(TModel);

                modelType.GetProperty("Path").SetValue(modelInstance, path);
                modelType.GetProperty("FileName")
                    .SetValue(modelInstance, fileGuid + "@---@" + encodedFileName + fileExtension);
                modelType.GetProperty("FileType").SetValue(modelInstance, fileType);
                modelType.GetProperty("CreatedOn").SetValue(modelInstance, createdOn);
                modelType.GetProperty(entityIdPropertyName).SetValue(modelInstance, entityId);

                modelList.Add(modelInstance);

            }

            return modelList;

        }

        public IEnumerable<TModel> SaveFilesToServer<TModel>
            (IEnumerable<TModel> fileModels, IEnumerable<byte[]> filesContent)
        {
            var modelType = typeof(TModel);
            var fileModelsList = fileModels.ToList();
            var filesContentArray = filesContent.ToArray();

            for (int i = 0; i < fileModelsList.Count; i++)
            {
                var fileModel = fileModelsList[i];
                var fileContent = filesContentArray[i];

                var path = modelType.GetProperty("Path").GetValue(fileModel);
                var fileName = modelType.GetProperty("FileName").GetValue(fileModel);

                try
                {
                    using (var fileStream = File.OpenWrite(storageFolderName + path + fileName))
                    {
                        fileStream.Write(fileContent);
                    }
                }
                catch(Exception ex)
                {
                    fileModelsList.Remove(fileModel);
                }
               
            }

            return fileModelsList;

        }            

        private string ConvertFileNameToBase64(string fileName)
        {
            var fileNameBytes = System.Text.Encoding.UTF8.GetBytes(fileName);
            return System.Convert.ToBase64String(fileNameBytes);
        }
        
    }
}
