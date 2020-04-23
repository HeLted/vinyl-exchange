namespace VinylExchange.Services.MemoryCache
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common.Constants;
    using Contracts;
    using Mapping;
    using Microsoft.AspNetCore.Http;
    using Web.Models.InputModels.Files;
    using Web.Models.Utility.Files;

    public class MemoryCacheFilesService : IMemoryCacheFilesSevice
    {
        private readonly IMemoryCacheManager cacheManager;

        public MemoryCacheFilesService(IMemoryCacheManager cacheManager)
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