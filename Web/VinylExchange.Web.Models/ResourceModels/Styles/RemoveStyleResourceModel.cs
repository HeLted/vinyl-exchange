using System;
using VinylExchange.Data.Models;
using VinylExchange.Services.Mapping;

namespace VinylExchange.Web.Models.ResourceModels.Styles
{
    public class RemoveStyleResourceModel : IMapFrom<Style>
    {
        public int Id { get; set; }
    }
}
