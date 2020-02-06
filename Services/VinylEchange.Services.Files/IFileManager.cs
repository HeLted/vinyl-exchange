using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VinylExchange.Data.Models;
using VinylExchange.Models.Utility;


namespace VinylExchange.Services.Files
{
    public interface IFileManager
    {
        public IEnumerable<UploadFileUtilityModel> RetrieveFilesFromCache(string formSessionId);
        IEnumerable<TModel> MapFilesToDbObjects
             <TModel>(IEnumerable<UploadFileUtilityModel> uploadFileUtilityModels,
             Guid entityId, string entityIdPropertyName, string subFolderName);

        IEnumerable<byte[]> GetFilesByteContent(IEnumerable<UploadFileUtilityModel> uploadFileUtilityModels);

        IEnumerable<TModel> SaveFilesToServer<TModel>
            (IEnumerable<TModel> fileModels, IEnumerable<byte[]> filesContent);
    }
}
