namespace VinylExchange.Services.MemoryCache
{
    #region

    using System;
    using System.Collections.Generic;
    using VinylExchange.Web.Models.InputModels.Files;
    using VinylExchange.Web.Models.ResourceModels.File;
    using VinylExchange.Web.Models.Utility;

    #endregion

    public interface IMemoryCacheFileSevice
    {
        UploadFileResourceModel AddFile(UploadFileUtilityModel file, Guid formSessionId);

        List<UploadFileUtilityModel> GetAllFilesForFormSession(Guid formSessionId);

        List<TModel> RemoveAllFilesForSession<TModel>(RemoveAllFilesForSessionInputModel inputModel);

        DeleteFileResourceModel RemoveFile(Guid formSessionId, Guid fileGuid);
    }
}