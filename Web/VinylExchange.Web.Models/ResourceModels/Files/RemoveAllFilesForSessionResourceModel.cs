namespace VinylExchange.Web.Models.ResourceModels.Files
{
    using System;

    using VinylExchange.Services.Mapping;
    using VinylExchange.Web.Models.Utility.Files;

    public class RemoveAllFilesForSessionResourceModel : IMapFrom<UploadFileUtilityModel>
    {
        public Guid FileGuid { get; set; }
    }
}