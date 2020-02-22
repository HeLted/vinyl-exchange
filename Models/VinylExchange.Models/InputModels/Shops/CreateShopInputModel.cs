using System.ComponentModel.DataAnnotations;
using VinylExchange.Data.Common.Enumerations;
using VinylExchange.Data.Models;
using VinylExchange.Services.Mapping;

namespace VinylExchange.Models.InputModels.Shops
{
    public class CreateShopInputModel : IMapTo<Shop>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public ShopType ShopType { get; set; }
        public string WebAddress { get; set; }
        public string Country { get; set; }
        public string Town { get; set; }
        public string Address { get; set; }
        
    }
}
