using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VinylExchange.Data.Models;
using VinylExchange.Models.Utility;

namespace VinylExchange.Services.HelperServices
{
    public interface  IReleaseFilesService
    {
        Task<IEnumerable<ReleaseFile>> AddFilesForRelease(Guid releaseId, string formSessionId);
    }
}
