using VinylExchange.Data.Models;
using VinylExchange.Services.Mapping;

namespace VinylExchange.Web.Models.ResourceModels.Genres
{
    public class RemoveGenreResourceModel : IMapFrom<Genre>
    {
        public int Id { get; set; }

    }
}
