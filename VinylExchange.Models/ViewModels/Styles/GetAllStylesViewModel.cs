using VinylExchange.Data.Models;
using VinylExchange.Services.Mapping;

namespace VinylExchange.Models.ViewModels.Styles
{
    public class GetAllStylesViewModel : IMapFrom<Style>
    {
        public int Id { get; set; }

        public string Name { get; set; }

    }
}
