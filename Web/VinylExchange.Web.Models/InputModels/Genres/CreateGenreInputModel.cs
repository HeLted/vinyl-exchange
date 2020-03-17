namespace VinylExchange.Web.Models.InputModels.Genres
{
    #region

    using System.ComponentModel.DataAnnotations;

    using VinylExchange.Data.Models;
    using VinylExchange.Services.Mapping;

    #endregion

    public class CreateGenreInputModel : IMapTo<Genre>
    {
        [Required]
        public string Name { get; set; }
    }
}