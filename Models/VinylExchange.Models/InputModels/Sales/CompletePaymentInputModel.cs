namespace VinylExchange.Models.InputModels.Sales
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CompletePaymentInputModel
    {
        [Required]
        public string OrderId { get; set; }

        [Required]
        public Guid? SaleId { get; set; }
    }
}