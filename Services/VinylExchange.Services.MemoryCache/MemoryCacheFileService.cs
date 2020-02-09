using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VinylExchange.Models.Utility;
using VinylExchange.Models.ViewModels.File;

namespace VinylExchange.Services.MemoryCache
{
    public class MemoryCacheFileService : IMemoryCacheFileSevice
    {

        private readonly MemoryCacheManager cacheManager;

        public MemoryCacheFileService(MemoryCacheManager cacheManager)
        {
            this.cacheManager = cacheManager;
        }


        public UploadFileViewModel AddFile(UploadFileUtilityModel file, string formSessionId)
        {

            if (!cacheManager.IsSet(formSessionId))
            {
                cacheManager.Set(formSessionId, new List<UploadFileUtilityModel>(), 1800);
            }

            var formSessionStorage = cacheManager.Get<List<UploadFileUtilityModel>>(formSessionId, null);

            formSessionStorage.Add(file);

            return new UploadFileViewModel
            {
                FileId = file.FileGuid,
                FileName = file.FileName

            };

        }

        public DeleteFileViewModel RemoveFile(string formSessionId, string fileGuid)
        {

            var key = cacheManager.GetKeys().Where(x => x == formSessionId).SingleOrDefault();

            var formSessionStorage = cacheManager.Get<List<UploadFileUtilityModel>>(key, null);

            var file = formSessionStorage.SingleOrDefault(x => x.FileGuid.ToString() == fileGuid);


            formSessionStorage.Remove(file);

            return new DeleteFileViewModel
            {
                FileId = file.FileGuid,
                FileName = file.FileName
            };


        }

        public void RemoveAllFilesForFormSession(string formSessionId)
        {

            var key = cacheManager.GetKeys().Where(x => x == formSessionId).SingleOrDefault();

            if (cacheManager.IsSet(formSessionId))
            {
                var formSessionStorage = cacheManager.Get<List<UploadFileUtilityModel>>(key, null);

                formSessionStorage.Clear();

                cacheManager.Remove(formSessionId);


                //returns sucessobject

            }
            else
            {
                //throws exception

            }


        }

        public IEnumerable<UploadFileUtilityModel> GetAllFilesForFormSession(string formSessionId)
        {
            return cacheManager.Get<List<UploadFileUtilityModel>>(formSessionId, null);
        }

    }
}
