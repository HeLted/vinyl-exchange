namespace VinylExchange.Models.ResourceModels.Genres
{
    using VinylExchange.Data.Models;
    using VinylExchange.Services.Mapping;

    public class GetAllGenresResourceModel : IMapFrom<Genre>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}