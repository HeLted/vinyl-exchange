namespace VinylExchange.Web.Models.InputModels.Sales
{
    #region

    using System;
    using System.ComponentModel.DataAnnotations;

    #endregion

    public class ConfirmItemRecievedInputModel
    {
        [Required]
        public Guid? SaleId { get; set; }
    }
}