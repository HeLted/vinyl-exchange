namespace VinylExchange.Web.Models.ResourceModels.Files
{
    #region

    using System;

    using VinylExchange.Services.Mapping;
    using VinylExchange.Web.Models.Utility.Files;

    #endregion

    public class RemoveAllFilesForSessionResourceModel : IMapFrom<UploadFileUtilityModel>
    {
        public Guid FileGuid { get; set; }
    }
}