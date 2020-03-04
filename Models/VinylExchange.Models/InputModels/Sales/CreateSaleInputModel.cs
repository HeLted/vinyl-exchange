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

                       
        public Nullable<Guid> ShopId { get; set; }
               
        [Required]
       
        public decimal Price { get; set; }

        [Required]
        public Condition VinylGrade { get; set; }

        [Required]
        public Condition SleeveGrade { get; set; }

        [Required]
        public string Description { get; set; }
        
    }
}
