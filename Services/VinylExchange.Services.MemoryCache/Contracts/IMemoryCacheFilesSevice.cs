namespace VinylExchange.Services.MemoryCache.Contracts
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Http;
    using Web.Models.InputModels.Files;
    using Web.Models.Utility.Files;

    public interface IMemoryCacheFilesSevice
    {
        TModel UploadFile<TModel>(IFormFile file, Guid? formSessionId);

        List<UploadFileUtilityModel> GetAllFilesForFormSession(Guid formSessionId);

        List<TModel> RemoveAllFilesForSession<TModel>(RemoveAllFilesForSessionInputModel inputModel);

        TModel RemoveFile<TModel>(RemoveFileInputModel inputModel);
    }
}