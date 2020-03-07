namespace VinylExchange.Web.Models.InputModels.Sales
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class SetShippingPriceInputModel
    {
        [Required]
        public Guid? SaleId { get; set; }

        [Required]
        public decimal ShippingPrice { get; set; }
    }
}