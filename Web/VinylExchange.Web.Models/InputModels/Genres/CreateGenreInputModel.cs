namespace VinylExchange.Web.Models.InputModels.Genres
{
    using System.ComponentModel.DataAnnotations;
    using Data.Models;
    using Services.Mapping;
    using static Common.Constants.ValidationConstants;


    public class CreateGenreInputModel : IMapTo<Genre>
    {
        [Required]
        [MinLength(3, ErrorMessage = InvalidMinLength)]
        [MaxLength(50, ErrorMessage = InvalidMaxLength)]
        public string Name { get; set; }
    }
}