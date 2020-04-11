namespace VinylExchange.Web.Models.InputModels.Genres
{
    #region

    using System.ComponentModel.DataAnnotations;

    using VinylExchange.Data.Models;
    using VinylExchange.Services.Mapping;

    using static VinylExchange.Common.Constants.ValidationConstants;

    #endregion

    public class CreateGenreInputModel : IMapTo<Genre>
    {
        [Required]
        [MinLength(3, ErrorMessage = InvalidMinLength)]
        [MaxLength(50, ErrorMessage = InvalidMaxLength)]
        public string Name { get; set; }
    }
}