namespace VinylExchange.Services.Files
{
    using System;
    using System.Collections.Generic;

    using VinylExchange.Models.Utility;

    public interface IFileManager
    {
        IEnumerable<byte[]> GetFilesByteContent(IEnumerable<UploadFileUtilityModel> uploadFileUtilityModels);

        IEnumerable<TModel> MapFilesToDbObjects<TModel>(
            IEnumerable<UploadFileUtilityModel> uploadFileUtilityModels,
            Guid entityId,
            string entityIdPropertyName,
            string subFolderName);

        public IEnumerable<UploadFileUtilityModel> RetrieveFilesFromCache(Guid formSessionId);

        IEnumerable<TModel> SaveFilesToServer<TModel>(IEnumerable<TModel> fileModels, IEnumerable<byte[]> filesContent);
    }
}