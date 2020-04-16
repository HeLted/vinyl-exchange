namespace VinylExchange.Services.MemoryCache
{
    #region

    #region

    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.AspNetCore.Http;

    using VinylExchange.Common.Constants;
    using VinylExchange.Services.Mapping;
    using VinylExchange.Web.Models.InputModels.Files;
    using VinylExchange.Web.Models.Utility.Files;

    #endregion

    #endregion

    public class MemoryCacheFileService : IMemoryCacheFileSevice
    {
        private readonly MemoryCacheManager cacheManager;

        public MemoryCacheFileService(MemoryCacheManager cacheManager)
        {
            this.cacheManager = cacheManager;
        }

        public TModel UploadFile<TModel>(IFormFile file, Guid? formSessionId)
        {
            var formSessionIdAsString = formSessionId.ToString();

            if (!this.cacheManager.IsSet(formSessionIdAsString))
            {
                this.cacheManager.Set(formSessionIdAsString, new List<UploadFileUtilityModel>(), 1800);
            }

            var formSessionStorage = this.cacheManager.Get<List<UploadFileUtilityModel>>(formSessionIdAsString, null);

            var fileUtilityModel = new UploadFileUtilityModel(file);

            formSessionStorage.Add(fileUtilityModel);

            return fileUtilityModel.To<TModel>();
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

        public List<TModel> RemoveAllFilesForSession<TModel>(RemoveAllFilesForSessionInputModel inputModel)
        {
            var formSessionIdAsString = inputModel.FormSessionId.ToString();

            var key = this.cacheManager.GetKeys().Where(x => x == formSessionIdAsString).SingleOrDefault();

            var deletedFormSessionCacheCopy = new List<TModel>();

            if (this.cacheManager.IsSet(formSessionIdAsString))
            {
                var formSessionStorage = this.cacheManager.Get<List<UploadFileUtilityModel>>(key, null);

                deletedFormSessionCacheCopy.AddRange(formSessionStorage.Select(uf => uf.To<TModel>()));

                formSessionStorage.Clear();

                this.cacheManager.Remove(formSessionIdAsString);

                return deletedFormSessionCacheCopy;
            }

            throw new NullReferenceException(NullReferenceExceptionsConstants.FormSessionKeyNotFound);
        }

        public TModel RemoveFile<TModel>(RemoveFileInputModel inputModel)
        {
            var formSessionIdAsString = inputModel.FormSessionId.ToString();

            var key = this.cacheManager.GetKeys().Where(x => x == formSessionIdAsString).SingleOrDefault();

            if (this.cacheManager.IsSet(formSessionIdAsString))
            {
                var formSessionStorage = this.cacheManager.Get<List<UploadFileUtilityModel>>(key, null);

                var file = formSessionStorage.SingleOrDefault(x => x.FileGuid == inputModel.Id);

                formSessionStorage.Remove(file);

                return file.To<TModel>();
            }

            throw new NullReferenceException(NullReferenceExceptionsConstants.FormSessionKeyNotFound);
        }
    }
}