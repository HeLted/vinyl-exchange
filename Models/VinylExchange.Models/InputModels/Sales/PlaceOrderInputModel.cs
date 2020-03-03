using System;
using System.ComponentModel.DataAnnotations;

namespace VinylExchange.Models.InputModels.Sales
{
    public class PlaceOrderInputModel
    {
      
        [Required]
        public Nullable<Guid> SaleId { get; set; }

        [Required]
        public Nullable<Guid> AddressId { get; set; }

    }
}
