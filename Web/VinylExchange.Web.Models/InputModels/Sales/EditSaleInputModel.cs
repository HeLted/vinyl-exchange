namespace VinylExchange.Web.Models.InputModels.Sales
{
    #region

    using System;
    using System.ComponentModel.DataAnnotations;

    using VinylExchange.Data.Common.Enumerations;
    using VinylExchange.Data.Models;
    using VinylExchange.Services.Mapping;

    #endregion

    public class EditSaleInputModel : IMapTo<Sale>
    {
          [Required]
        [MinLength(10,ErrorMessage ="Invalid min length of field!")]
        [MaxLength(400,ErrorMessage ="Invalid max length of field!")]
        public string Description { get; set; }

        [Required]
        [Range(0,100000)]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Sale Id")]
        public Guid? SaleId { get; set; }

        [Required]
        [Display(Name = "Ships From")]
        public Guid? ShipsFromAddressId { get; set; }

        [Required]
        [Range((int)Condition.Poor,(int)Condition.Mint,ErrorMessage ="Please select correct option for field")]
        public Condition SleeveGrade { get; set; }

        [Required]
        [Range((int)Condition.Poor,(int)Condition.Mint,ErrorMessage ="Please select correct option for field")]
        public Condition VinylGrade { get; set; }
    }
}