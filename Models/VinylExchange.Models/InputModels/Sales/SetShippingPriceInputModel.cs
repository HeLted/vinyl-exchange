using System;
using System.ComponentModel.DataAnnotations;

namespace VinylExchange.Models.InputModels.Sales
{
    public  class SetShippingPriceInputModel
    {
        [Required]
        public Nullable<Guid> SaleId { get; set; }

        [Required]
        public decimal ShippingPrice { get; set; }

    }
}
