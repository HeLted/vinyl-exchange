namespace VinylExchange.Services.MemoryCache
{
    using System;
    using System.Collections.Generic;

    using VinylExchange.Models.ResourceModels.File;
    using VinylExchange.Models.Utility;

    public interface IMemoryCacheFileSevice
    {
        UploadFileResourceModel AddFile(UploadFileUtilityModel file, Guid formSessionId);

        IEnumerable<UploadFileUtilityModel> GetAllFilesForFormSession(Guid formSessionId);

        IEnumerable<UploadFileUtilityModel> RemoveAllFilesForFormSession(Guid formSessionId);

        DeleteFileResourceModel RemoveFile(Guid formSessionId, Guid fileGuid);
    }
}