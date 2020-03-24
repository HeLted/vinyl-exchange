namespace VinylExchange.Web.Models.InputModels.Styles
{
    using System.ComponentModel.DataAnnotations;
    #region

    using VinylExchange.Data.Models;
    using VinylExchange.Services.Mapping;

    #endregion

    public class CreateStyleInputModel : IMapTo<Style>
    {
        [Required]
        public int? GenreId { get; set; }
        [Required]
        [Display(Name="Style Name")]
        public string Name { get; set; }
    }
}