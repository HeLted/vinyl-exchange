namespace VinylExchange.Web.Models.ResourceModels.File
{
    using System;
    using Services.Mapping;
    using Utility.Files;

    public class UploadFileResourceModel : IMapFrom<UploadFileUtilityModel>
    {
        public Guid FileGuid { get; set; }
    }
}