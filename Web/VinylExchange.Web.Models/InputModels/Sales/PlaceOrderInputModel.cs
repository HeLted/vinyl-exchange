namespace VinylExchange.Web.Models.InputModels.Sales
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class PlaceOrderInputModel
    {
        [Required]
        public Guid? SaleId { get; set; }

        [Required]
        public Guid? AddressId { get; set; }
    }
}