namespace VinylExchange.Services.MemoryCache
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Linq;

    using VinylExchange.Web.Models.ResourceModels.File;
    using VinylExchange.Web.Models.Utility;

    #endregion

    public class MemoryCacheFileService : IMemoryCacheFileSevice
    {
        private readonly MemoryCacheManager cacheManager;

        public MemoryCacheFileService(MemoryCacheManager cacheManager)
        {
            this.cacheManager = cacheManager;
        }

        public UploadFileResourceModel AddFile(UploadFileUtilityModel file, Guid formSessionId)
        {
            var formSessionIdAsString = formSessionId.ToString();

            if (!this.cacheManager.IsSet(formSessionIdAsString))
                this.cacheManager.Set(formSessionIdAsString, new List<UploadFileUtilityModel>(), 1800);

            var formSessionStorage = this.cacheManager.Get<List<UploadFileUtilityModel>>(formSessionIdAsString, null);

            formSessionStorage.Add(file);

            return new UploadFileResourceModel { FileId = file.FileGuid, FileName = file.FileName };
        }

        public List<UploadFileUtilityModel> GetAllFilesForFormSession(Guid formSessionId)
        {
            try
            {
                return this.cacheManager.Get<List<UploadFileUtilityModel>>(formSessionId.ToString(), null);
            }
            catch
            {
                return new List<UploadFileUtilityModel>();
            }
        }

        public List<UploadFileUtilityModel> RemoveAllFilesForFormSession(Guid formSessionId)
        {
            var formSessionIdAsString = formSessionId.ToString();

            var key = this.cacheManager.GetKeys().Where(x => x == formSessionIdAsString).SingleOrDefault();

            var formSessionIdCopy = new List<UploadFileUtilityModel>();

            if (this.cacheManager.IsSet(formSessionIdAsString))
            {
                var formSessionStorage = this.cacheManager.Get<List<UploadFileUtilityModel>>(key, null);

                formSessionIdCopy.AddRange(formSessionStorage);

                formSessionStorage.Clear();

                this.cacheManager.Remove(formSessionIdAsString);

                return formSessionIdCopy;
            }

            throw new NullReferenceException("Session with this key is not set!");
        }

        public DeleteFileResourceModel RemoveFile(Guid formSessionId, Guid fileGuid)
        {
            var formSessionIdAsString = formSessionId.ToString();

            var key = this.cacheManager.GetKeys().Where(x => x == formSessionId.ToString()).SingleOrDefault();

            if (this.cacheManager.IsSet(formSessionIdAsString))
            {
                var formSessionStorage = this.cacheManager.Get<List<UploadFileUtilityModel>>(key, null);

                var file = formSessionStorage.SingleOrDefault(x => x.FileGuid == fileGuid);

                formSessionStorage.Remove(file);

                return new DeleteFileResourceModel { FileId = file.FileGuid, FileName = file.FileName };
            }

            throw new NullReferenceException("Session with this key is not set!");
        }
    }
}