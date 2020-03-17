namespace VinylExchange.Web.Models.ResourceModels.Sales
{
    #region

    using System;

    using AutoMapper;

    using VinylExchange.Data.Common.Enumerations;
    using VinylExchange.Data.Models;
    using VinylExchange.Services.Mapping;

    #endregion

    public class GetSaleResourceModel : IMapFrom<Sale>, IHaveCustomMappings
    {
        public Guid? BuyerId { get; set; }

        public string BuyerUsername { get; set; }

        public string Description { get; set; }

        public Guid Id { get; set; }

        public decimal Price { get; set; }

        public Guid? ReleaseId { get; set; }

        public Guid? SellerId { get; set; }

        public string SellerUsername { get; set; }

        public decimal ShippingPrice { get; set; }

        public string ShipsFrom { get; set; }

        public string ShipsTo { get; set; }

        public Condition SleeveGrade { get; set; }

        public Status Status { get; set; }

        public Condition VinylGrade { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Sale, GetSaleResourceModel>()
                .ForMember(m => m.SellerUsername, ci => ci.MapFrom(x => x.Seller.UserName)).ForMember(
                    m => m.BuyerUsername,
                    ci => ci.MapFrom(x => x.Buyer.UserName));
        }
    }
}