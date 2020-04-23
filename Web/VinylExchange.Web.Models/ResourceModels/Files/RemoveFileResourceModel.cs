namespace VinylExchange.Web.Models.ResourceModels.File
{
    using System;
    using Services.Mapping;
    using Utility.Files;

    public class RemoveFileResourceModel : IMapFrom<UploadFileUtilityModel>
    {
        public Guid FileGuid { get; set; }
    }
}