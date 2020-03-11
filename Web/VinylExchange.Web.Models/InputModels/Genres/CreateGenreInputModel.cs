namespace VinylExchange.Web.Models.InputModels.Genres
{
    using System.ComponentModel.DataAnnotations;
    using VinylExchange.Data.Models;
    using VinylExchange.Services.Mapping;

    public class CreateGenreInputModel : IMapTo<Genre>
    {
        [Required]
        public string Name { get; set; }
    }
}
