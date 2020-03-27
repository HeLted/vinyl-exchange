namespace VinylExchange.Web.Models.InputModels.Sales
{
    #region

    using System;
    using System.ComponentModel.DataAnnotations;

    using VinylExchange.Data.Common.Enumerations;
    using VinylExchange.Data.Models;
    using VinylExchange.Services.Mapping;

    using static VinylExchange.Common.Constants.ValidationConstants;

    #endregion

    public class CreateSaleInputModel : IMapTo<Sale>
    {
        [Required]
        [Range((int)Condition.Poor, (int)Condition.Mint, ErrorMessage = SelectCorrectEnumOption)]
        public Condition VinylGrade { get; set; }

        
        [Required]
        [Range((int)Condition.Poor, (int)Condition.Mint, ErrorMessage = SelectCorrectEnumOption)]
        public Condition SleeveGrade { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = InvalidMinLength)]
        [MaxLength(400, ErrorMessage = InvalidMaxLength)]
        public string Description { get; set; }

        [Required]
        [Range(0, 100000)]
        public decimal Price { get; set; }
        
        [Required]
        [Display(Name = "Ships From")]

        public Guid? ShipsFromAddressId { get; set; }

        [Required]
        [Display(Name = "Release Id")]
        public Guid? ReleaseId { get; set; }

        
    }
}