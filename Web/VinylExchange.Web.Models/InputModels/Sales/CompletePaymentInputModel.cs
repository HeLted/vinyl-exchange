namespace VinylExchange.Web.Models.InputModels.Sales
{
    #region

    using System;
    using System.ComponentModel.DataAnnotations;

    #endregion

    public class CompletePaymentInputModel
    {
        [Required]
        public Guid? SaleId { get; set; }

        [Required]
        public string OrderId { get; set; }
    }
}