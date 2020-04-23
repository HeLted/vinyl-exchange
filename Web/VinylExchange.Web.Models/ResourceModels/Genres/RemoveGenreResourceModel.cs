namespace VinylExchange.Web.Models.ResourceModels.Genres
{
    using Data.Models;
    using Services.Mapping;

    public class RemoveGenreResourceModel : IMapFrom<Genre>
    {
        public int Id { get; set; }
    }
}