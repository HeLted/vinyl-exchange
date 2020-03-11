using VinylExchange.Data.Models;
using VinylExchange.Services.Mapping;

namespace VinylExchange.Web.Models.ResourceModels.Genres
{
    public class CreateGenreResourceModel : IMapFrom<Genre>
    {
        public int Id { get; set; }
    }
}
