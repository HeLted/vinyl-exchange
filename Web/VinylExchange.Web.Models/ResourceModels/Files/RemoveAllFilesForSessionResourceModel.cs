using System;
using VinylExchange.Services.Mapping;
using VinylExchange.Web.Models.Utility;

namespace VinylExchange.Web.Models.ResourceModels.Files
{
    public class RemoveAllFilesForSessionResourceModel : IMapFrom<UploadFileUtilityModel>
    {
        public Guid FileGuid { get; set; }
    }
}
