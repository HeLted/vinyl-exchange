using AutoMapper;
using System;
using VinylExchange.Data.Common.Enumerations;
using VinylExchange.Data.Models;
using VinylExchange.Services.Mapping;

namespace VinylExchange.Models.ResourceModels.Sales
{
    public class GetAllSalesForReleaseResouceModel : IMapFrom<Sale>, IHaveCustomMappings
    {
        public Guid Id { get; set; }

        public string SellerUsername { get; set; }
        public decimal Price { get; set; }

        public Condition VinylGrade { get; set; }

        public Condition SleeveGrade{ get; set; }

        public Guid SellerId { get; set; }
           
        public string Description { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Sale, GetAllSalesForReleaseResouceModel>()
                .ForMember(m => m.SellerUsername, ci => ci.MapFrom(x => x.Seller.UserName));
        }
    }
}
