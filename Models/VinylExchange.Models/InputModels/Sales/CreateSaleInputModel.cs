using System;
using System.ComponentModel.DataAnnotations;
using VinylExchange.Data.Common.Enumerations;
using VinylExchange.Data.Models;
using VinylExchange.Services.Mapping;

namespace VinylExchange.Models.InputModels.Sales
{
    public class CreateSaleInputModel : IMapTo<Sale>
    {
        [Required]
        public Guid ReleaseId { get; set; }

        [Required]
        public Guid SellerId { get; set; }
                 
        public Nullable<Guid> ShopId { get; set; }
               
        [Required]
       
        public decimal Price { get; set; }

        [Required]
        public Condition VinylCondition { get; set; }

        [Required]
        public Condition SleeveCondition { get; set; }

        [Required]
        public string Description { get; set; }
        
    }
}
