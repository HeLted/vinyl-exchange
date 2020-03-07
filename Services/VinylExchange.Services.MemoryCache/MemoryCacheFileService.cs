namespace VinylExchange.Services.MemoryCache
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using VinylExchange.Models.ResourceModels.File;
    using VinylExchange.Models.Utility;

    public class MemoryCacheFileService : IMemoryCacheFileSevice
    {
        private readonly MemoryCacheManager cacheManager;

        public MemoryCacheFileService(MemoryCacheManager cacheManager)
        {
            this.cacheManager = cacheManager;
        }

        public UploadFileResourceModel AddFile(UploadFileUtilityModel file, Guid formSessionId)
        {
            string formSessionIdAsString = formSessionId.ToString();

            if (!this.cacheManager.IsSet(formSessionIdAsString))
            {
                this.cacheManager.Set(formSessionIdAsString, new List<UploadFileUtilityModel>(), 1800);
            }

            List<UploadFileUtilityModel> formSessionStorage =
                this.cacheManager.Get<List<UploadFileUtilityModel>>(formSessionIdAsString, null);

            formSessionStorage.Add(file);

            return new UploadFileResourceModel { FileId = file.FileGuid, FileName = file.FileName };
        }

        public IEnumerable<UploadFileUtilityModel> GetAllFilesForFormSession(Guid formSessionId)
        {
            return this.cacheManager.Get<List<UploadFileUtilityModel>>(formSessionId.ToString(), null);
        }

        public IEnumerable<UploadFileUtilityModel> RemoveAllFilesForFormSession(Guid formSessionId)
        {
            string formSessionIdAsString = formSessionId.ToString();

            string key = this.cacheManager.GetKeys().Where(x => x == formSessionIdAsString).SingleOrDefault();

            List<UploadFileUtilityModel> formSessionIdCopy = new List<UploadFileUtilityModel>();

            if (this.cacheManager.IsSet(formSessionIdAsString))
            {
                List<UploadFileUtilityModel> formSessionStorage =
                    this.cacheManager.Get<List<UploadFileUtilityModel>>(key, null);

                formSessionIdCopy.AddRange(formSessionStorage);

                formSessionStorage.Clear();

                this.cacheManager.Remove(formSessionIdAsString);

                return formSessionIdCopy;
            }
            else
            {
                throw new NullReferenceException();
            }
        }

        public DeleteFileResourceModel RemoveFile(Guid formSessionId, Guid fileGuid)
        {
            string formSessionIdAsString = formSessionId.ToString();

            string key = this.cacheManager.GetKeys().Where(x => x == formSessionId.ToString()).SingleOrDefault();

            if (this.cacheManager.IsSet(formSessionIdAsString))
            {
                List<UploadFileUtilityModel> formSessionStorage =
                    this.cacheManager.Get<List<UploadFileUtilityModel>>(key, null);

                UploadFileUtilityModel file = formSessionStorage.SingleOrDefault(x => x.FileGuid == fileGuid);

                formSessionStorage.Remove(file);

                return new DeleteFileResourceModel { FileId = file.FileGuid, FileName = file.FileName };
            }
            else
            {
                throw new NullReferenceException();
            }
        }
    }
}