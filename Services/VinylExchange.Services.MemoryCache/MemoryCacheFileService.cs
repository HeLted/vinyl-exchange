using System;
using System.Collections.Generic;
using System.Linq;
using VinylExchange.Models.ResourceModels.File;
using VinylExchange.Models.Utility;


namespace VinylExchange.Services.MemoryCache
{
    public class MemoryCacheFileService : IMemoryCacheFileSevice
    {

        private readonly MemoryCacheManager cacheManager;

        public MemoryCacheFileService(MemoryCacheManager cacheManager)
        {
            this.cacheManager = cacheManager;
        }


        public  UploadFileResourceModel AddFile(UploadFileUtilityModel file, Guid formSessionId)
        {
            var formSessionIdAsString = formSessionId.ToString();

            if (!cacheManager.IsSet(formSessionIdAsString))
            {
                cacheManager.Set(formSessionIdAsString, new List<UploadFileUtilityModel>(), 1800);
            }

            var formSessionStorage = cacheManager.Get<List<UploadFileUtilityModel>>(formSessionIdAsString, null);

            formSessionStorage.Add(file);

            return new UploadFileResourceModel
            {
                FileId = file.FileGuid,
                FileName = file.FileName

            };

        }

        public DeleteFileResourceModel RemoveFile(Guid formSessionId, Guid fileGuid)
        {
            var formSessionIdAsString = formSessionId.ToString();

            var key = cacheManager.GetKeys().Where(x => x == formSessionId.ToString()).SingleOrDefault();

            
            if (this.cacheManager.IsSet(formSessionIdAsString))
            {
                var formSessionStorage = cacheManager.Get<List<UploadFileUtilityModel>>(key, null);

                var file = formSessionStorage.SingleOrDefault(x => x.FileGuid == fileGuid);

                formSessionStorage.Remove(file);

                return new DeleteFileResourceModel
                {
                    FileId = file.FileGuid,
                    FileName = file.FileName
                };
            }
            else
            {
                throw new NullReferenceException();
            }

        }

        public IEnumerable<UploadFileUtilityModel> RemoveAllFilesForFormSession(Guid formSessionId)
        {
            var formSessionIdAsString = formSessionId.ToString();

            var key = cacheManager.GetKeys().Where(x => x == formSessionIdAsString).SingleOrDefault();

            List<UploadFileUtilityModel> formSessionIdCopy = new List<UploadFileUtilityModel>();

            if (cacheManager.IsSet(formSessionIdAsString))
            {
                var formSessionStorage = cacheManager.Get<List<UploadFileUtilityModel>>(key, null);

                formSessionIdCopy.AddRange(formSessionStorage);

                formSessionStorage.Clear();

                cacheManager.Remove(formSessionIdAsString);

                return formSessionIdCopy;

            }
            else
            {
                throw new NullReferenceException();

            }


        }

        public IEnumerable<UploadFileUtilityModel> GetAllFilesForFormSession(Guid formSessionId)
        {
            return cacheManager.Get<List<UploadFileUtilityModel>>(formSessionId.ToString(), null);
        }

    }
}
