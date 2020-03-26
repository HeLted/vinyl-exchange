namespace VinylExchange.Web.Models.ResourceModels.File
{
    #region

    using System;

    using VinylExchange.Services.Mapping;
    using VinylExchange.Web.Models.Utility;
    using VinylExchange.Web.Models.Utility.Files;

    #endregion

    public class RemoveFileResourceModel : IMapFrom<UploadFileUtilityModel>
    {
        public Guid FileGuid { get; set; }
    }
}