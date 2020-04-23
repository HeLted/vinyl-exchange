namespace VinylExchange.Web.Models.ResourceModels.Genres
{
    using Data.Models;
    using Services.Mapping;

    public class CreateGenreResourceModel : IMapFrom<Genre>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}