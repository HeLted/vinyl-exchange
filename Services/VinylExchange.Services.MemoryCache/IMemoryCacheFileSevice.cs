namespace VinylExchange.Services.MemoryCache
{
    using Microsoft.AspNetCore.Http;
    #region

    using System;
    using System.Collections.Generic;

    using VinylExchange.Web.Models.InputModels.Files;
    using VinylExchange.Web.Models.Utility.Files;

    #endregion

    public interface IMemoryCacheFileSevice
    {
        TModel UploadFile<TModel>(IFormFile file, Guid? formSessionId);

        List<UploadFileUtilityModel> GetAllFilesForFormSession(Guid formSessionId);

        List<TModel> RemoveAllFilesForSession<TModel>(RemoveAllFilesForSessionInputModel inputModel);

        TModel RemoveFile<TModel>(RemoveFileInputModel inputModel);
    }
}