using VinylExchange.Data.Models;
using VinylExchange.Services.Mapping;

namespace VinylExchange.Models.ResourceModels.Styles
{
    public class GetAllStylesResourceModel : IMapFrom<Style>
    {
        public int Id { get; set; }

        public string Name { get; set; }

    }
}
