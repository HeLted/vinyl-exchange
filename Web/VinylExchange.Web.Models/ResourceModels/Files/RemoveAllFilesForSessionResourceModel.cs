namespace VinylExchange.Web.Models.ResourceModels.Files
{
    using System;
    using Services.Mapping;
    using Utility.Files;

    public class RemoveAllFilesForSessionResourceModel : IMapFrom<UploadFileUtilityModel>
    {
        public Guid FileGuid { get; set; }
    }
}