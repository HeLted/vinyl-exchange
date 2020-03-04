using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VinylExchange.Data.Common.Enumerations;
using VinylExchange.Data.Models;
using VinylExchange.Services.Mapping;

namespace VinylExchange.Models.ResourceModels.Sales
{
    public class GetSaleResourceModel : IMapFrom<Sale>, IHaveCustomMappings
    {
        public Guid Id { get; set; }

        public Nullable<Guid> SellerId { get; set; }

        public string SellerUsername { get; set; }

        public Nullable<Guid> BuyerId { get; set; }

        public string BuyerUsername { get; set; }

        public Nullable<Guid> ReleaseId { get; set; }

        public Status Status { get; set; }

        public string StatusText { get; set; }

        public decimal Price { get; set; }

        public Condition VinylGrade { get; set; }
            
        public Condition SleeveGrade { get; set; }
              
        public string Description { get; set; }

        public decimal ShippingPrice { get; set; }

        public string ShipsFrom { get; set; }
        public string ShipsTo { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Sale, GetSaleResourceModel>()
               .ForMember(m => m.SellerUsername, ci => ci.MapFrom(x => x.Seller.UserName))
               .ForMember(m => m.BuyerUsername, ci => ci.MapFrom(x => x.Buyer.UserName))
               .ForMember(m => m.StatusText, ci => ci.MapFrom(x => x.Status.ToString()));               
        }
    }
}
