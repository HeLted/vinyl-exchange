namespace VinylExchange.Web.Models.InputModels.Styles
{
    #region

    using System.ComponentModel.DataAnnotations;

    using VinylExchange.Data.Models;
    using VinylExchange.Services.Mapping;

    using static VinylExchange.Common.Constants.ValidationConstants;

    #endregion

    public class CreateStyleInputModel : IMapTo<Style>
    {
        [Required]
        [MinLength(3, ErrorMessage = InvalidMinLength)]
        [MaxLength(50, ErrorMessage = InvalidMaxLength)]
        [Display(Name = "Style Name")]
        public string Name { get; set; }

        [Required]
        public int GenreId { get; set; }
    }
}