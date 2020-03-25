namespace VinylExchange.Web.Models.ResourceModels.File
{
    #region

    using System;
    using VinylExchange.Services.Mapping;
    using VinylExchange.Web.Models.Utility;

    #endregion

    public class UploadFileResourceModel : IMapFrom<UploadFileUtilityModel>
    {
        public Guid FileGuid { get; set; }       
    }
}