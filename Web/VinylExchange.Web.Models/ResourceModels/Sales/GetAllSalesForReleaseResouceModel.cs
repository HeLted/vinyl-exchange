namespace VinylExchange.Web.Models.ResourceModels.Sales
{
    using System;

    using AutoMapper;

    using VinylExchange.Data.Common.Enumerations;
    using VinylExchange.Data.Models;
    using VinylExchange.Services.Mapping;

    public class GetAllSalesForReleaseResouceModel : IMapFrom<Sale>, IHaveCustomMappings
    {
        public string Description { get; set; }

        public Guid Id { get; set; }

        public decimal Price { get; set; }

        public Guid SellerId { get; set; }

        public string SellerUsername { get; set; }

        public Condition SleeveGrade { get; set; }

        public Condition VinylGrade { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Sale, GetAllSalesForReleaseResouceModel>().ForMember(
                m => m.SellerUsername,
                ci => ci.MapFrom(x => x.Seller.UserName));
        }
    }
}