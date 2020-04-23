namespace VinylExchange.Web.Models.InputModels.Sales
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CancelOrderInputModel
    {
        [Required]
        public Guid? SaleId { get; set; }
    }
}