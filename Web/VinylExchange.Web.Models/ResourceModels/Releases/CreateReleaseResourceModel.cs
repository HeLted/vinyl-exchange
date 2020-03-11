using System;
using VinylExchange.Data.Models;
using VinylExchange.Services.Mapping;

namespace VinylExchange.Web.Models.ResourceModels.Releases
{
    public class CreateReleaseResourceModel : IMapFrom<Release>
    {
        public Guid Id { get; set; }
    }
}
