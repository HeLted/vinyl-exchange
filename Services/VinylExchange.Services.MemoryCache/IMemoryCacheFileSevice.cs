using System;
using System.Collections.Generic;
using System.Text;
using VinylExchange.Models.Utility;
using VinylExchange.Models.ViewModels.File;

namespace VinylExchange.Services.MemoryCache
{
    public interface IMemoryCacheFileSevice
    {
        UploadFileViewModel AddFile(UploadFileUtilityModel file, string formSessionId);
        DeleteFileViewModel RemoveFile(string formSessionId, Guid fileGuid);
        void RemoveAllFilesForFormSession(string formSessionId);
        IEnumerable<UploadFileUtilityModel> GetAllFilesForFormSession(string formSessionId);
    }
}
