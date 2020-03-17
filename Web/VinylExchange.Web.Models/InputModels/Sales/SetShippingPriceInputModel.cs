namespace VinylExchange.Web.Models.InputModels.Sales
{
    #region

    using System;
    using System.ComponentModel.DataAnnotations;

    #endregion

    public class SetShippingPriceInputModel
    {
        [Required]
        public Guid? SaleId { get; set; }

        [Required]
        public decimal ShippingPrice { get; set; }
    }
}