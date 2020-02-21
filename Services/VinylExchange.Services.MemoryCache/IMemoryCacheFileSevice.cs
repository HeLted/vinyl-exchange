using System;
using System.Collections.Generic;
using VinylExchange.Models.ResourceModels.File;
using VinylExchange.Models.Utility;


namespace VinylExchange.Services.MemoryCache
{
    public interface IMemoryCacheFileSevice
    {
        UploadFileResourceModel AddFile(UploadFileUtilityModel file, Guid formSessionId);
        DeleteFileResourceModel RemoveFile(Guid formSessionId, Guid fileGuid);
        void RemoveAllFilesForFormSession(Guid formSessionId);
        IEnumerable<UploadFileUtilityModel> GetAllFilesForFormSession(Guid formSessionId);
    }
}
