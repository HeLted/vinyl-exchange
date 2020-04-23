namespace VinylExchange.Web.Models.ResourceModels.Releases
{
    using System;
    using Data.Models;
    using Services.Mapping;

    public class CreateReleaseResourceModel : IMapFrom<Release>
    {
        public Guid Id { get; set; }
    }
}