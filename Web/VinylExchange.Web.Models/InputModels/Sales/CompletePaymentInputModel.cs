namespace VinylExchange.Web.Models.InputModels.Sales
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CompletePaymentInputModel
    {
        [Required] public Guid? SaleId { get; set; }

        [Required] public string OrderId { get; set; }
    }
}