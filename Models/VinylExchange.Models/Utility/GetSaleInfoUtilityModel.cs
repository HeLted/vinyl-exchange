using System;
using VinylExchange.Data.Common.Enumerations;
using VinylExchange.Data.Models;
using VinylExchange.Services.Mapping;

namespace VinylExchange.Models.Utility
{
    public class GetSaleInfoUtilityModel : IMapFrom<Sale>
    {
        public Guid Id { get; set; }

        public Nullable<Guid> SellerId { get; set; }
     
        public Nullable<Guid> BuyerId { get; set; }
         
        public Nullable<Guid> ReleaseId { get; set; }

        public Status Status { get; set; }

     
    }
}
