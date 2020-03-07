namespace VinylExchange.Web.Models.InputModels.Sales
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using VinylExchange.Data.Common.Enumerations;
    using VinylExchange.Data.Models;
    using VinylExchange.Services.Mapping;

    public class CreateSaleInputModel : IMapTo<Sale>
    {
        [Required]
        public string Description { get; set; }

        [Required]

        public decimal Price { get; set; }

        [Required]
        public Guid ReleaseId { get; set; }

        public Guid? ShopId { get; set; }

        [Required]
        public Condition SleeveGrade { get; set; }

        [Required]
        public Condition VinylGrade { get; set; }
    }
}