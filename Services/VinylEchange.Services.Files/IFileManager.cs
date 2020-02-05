using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VinylExchange.Models.Utility;
using VinylExchange.Services.Files.SettingEnums;

namespace VinylExchange.Services.Files
{
    public interface IFileManager
    {
        Task<IEnumerable<KeyValuePair<string, string>>> SaveFiles
            (IEnumerable<UploadFileUtilityModel> files, string directoryName);
    }
}
