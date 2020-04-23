namespace VinylExchange.Services.MemoryCache.Contracts
{
    #region

    #region

    using System;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Http;

    using VinylExchange.Web.Models.InputModels.Files;
    using VinylExchange.Web.Models.Utility.Files;

    #endregion

    #endregion

    public interface IMemoryCacheFilesSevice
    {
        TModel UploadFile<TModel>(IFormFile file, Guid? formSessionId);

        List<UploadFileUtilityModel> GetAllFilesForFormSession(Guid formSessionId);

        List<TModel> RemoveAllFilesForSession<TModel>(RemoveAllFilesForSessionInputModel inputModel);

        TModel RemoveFile<TModel>(RemoveFileInputModel inputModel);
    }
}