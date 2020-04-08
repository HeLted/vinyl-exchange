namespace VinylExchange.Web.Models.InputModels.Sales
{
    #region

    using System;
    using System.ComponentModel.DataAnnotations;

    #endregion

    public class PlaceOrderInputModel
    {
        [Required]
        public Guid? SaleId { get; set; }

        [Required]
        public Guid? AddressId { get; set; }
    }
}