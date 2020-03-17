namespace VinylExchange.Web.Models.ResourceModels.Releases
{
    #region

    using System;

    using VinylExchange.Data.Models;
    using VinylExchange.Services.Mapping;

    #endregion

    public class CreateReleaseResourceModel : IMapFrom<Release>
    {
        public Guid Id { get; set; }
    }
}