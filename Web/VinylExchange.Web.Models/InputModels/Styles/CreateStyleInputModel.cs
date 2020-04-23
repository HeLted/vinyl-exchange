namespace VinylExchange.Web.Models.InputModels.Styles
{
    using System.ComponentModel.DataAnnotations;
    using Data.Models;
    using Services.Mapping;
    using static Common.Constants.ValidationConstants;


    public class CreateStyleInputModel : IMapTo<Style>
    {
        [Required]
        [MinLength(3, ErrorMessage = InvalidMinLength)]
        [MaxLength(50, ErrorMessage = InvalidMaxLength)]
        [Display(Name = "Style Name")]
        public string Name { get; set; }

        [Required] public int GenreId { get; set; }
    }
}